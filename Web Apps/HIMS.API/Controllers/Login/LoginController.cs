using Asp.Versioning;
using HIMS.Core;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.Api.Models.Login;
using HIMS.API.Extensions;
using HIMS.API.Security;
using HIMS.API.Utility;
using HIMS.Core.Domain.Common;
using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using HIMS.Services.Permissions;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Cryptography;
using System.Text;

namespace HIMS.API.Controllers.Login
{
    //[Route("api/payment/callback")]
    //[ApiController]
    //public class MpesaCallbackController : ControllerBase
    //{
    //    [HttpPost]
    //    public async Task<IActionResult> PostAsync([FromBody] Dictionary<string, object> callbackData)
    //    {
    //        // Save callbackData to DB
    //        string path = "C:\\PaymentDataLogs\\";
    //        if (!System.IO.Directory.Exists(path))
    //            System.IO.Directory.CreateDirectory(path);
    //        string filename = path + "\\" + AppTime.Now.ToString("dd_MM_yyyy") + ".txt";
    //        System.IO.File.AppendAllText(filename, JsonConvert.SerializeObject(callbackData));

    //        return Ok(new { ResultCode = 0, ResultDesc = "Success", Data = callbackData });
    //    }
    //}

    //[ApiController]
    //[Route("api/payment")]
    //public class MpesaCallbackController : ControllerBase
    //{
    //    [HttpPost("validation")]
    //    public IActionResult Validate([FromBody] JsonElement payload)
    //    {
    //        string path = "C:\\PaymentDataLogs\\";
    //        if (!System.IO.Directory.Exists(path))
    //            System.IO.Directory.CreateDirectory(path);
    //        string filename = path + "\\" + AppTime.Now.ToString("dd_MM_yyyy") + ".txt";
    //        System.IO.File.AppendAllText(filename, "\n Validation =>" + payload.ToString());
    //        return Ok(new { ResultCode = 0, ResultDesc = "Success" });
    //    }

    //    [HttpPost("confirmation")]
    //    public IActionResult Confirm([FromBody] JsonElement payload)
    //    {
    //        string path = "C:\\PaymentDataLogs\\";
    //        if (!System.IO.Directory.Exists(path))
    //            System.IO.Directory.CreateDirectory(path);
    //        string filename = path + "\\" + AppTime.Now.ToString("dd_MM_yyyy") + ".txt";
    //        System.IO.File.AppendAllText(filename, "\n Confirmation =>" + payload.ToString());
    //        var obj = JsonConvert.DeserializeObject<MpesaCallbackRoot>(payload.ToString());
    //        var result = MPesaResponse.MapMpesaToTestA(obj);
    //        return Ok(new { ResultCode = 0, ResultDesc = "Success", Data = result });
    //    }
    //}


    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class LoginController : BaseController
    {
        protected readonly IUserService _userService;
        private readonly IPermissionService _IPermissionService;
        private readonly IMenuService _IMenuService;
        private readonly ILoginV2KeyProvider _loginKeyProvider;
        private readonly ILoginV2Store _loginStore;
        private readonly IConfiguration _configuration;
        private const int WebTokenValidityMinutes = 720;
        private const int MobileTokenValidityMinutes = 90 * 24 * 60;

        public LoginController(IUserService userService, IPermissionService permission, IMenuService iMenuService,
            ILoginV2KeyProvider loginKeyProvider, ILoginV2Store loginStore, IConfiguration configuration)
        {
            _userService = userService;
            _IPermissionService = permission;
            _IMenuService = iMenuService;
            _loginKeyProvider = loginKeyProvider;
            _loginStore = loginStore;
            _configuration = configuration;
        }

        [NonAction]
        private static bool HasActiveSession(LoginManager user, LoginType loginType)
        {
            string? activeToken = loginType == LoginType.Mobile ? user.MobileToken : user.UserToken;
            return !string.IsNullOrWhiteSpace(activeToken) && user.UserId > 0;
        }

        [NonAction]
        private static int GetTokenValidityMinutes(LoginType loginType)
        {
            return loginType == LoginType.Mobile ? MobileTokenValidityMinutes : WebTokenValidityMinutes;
        }

        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        [SwaggerOperation(Description = "LEGACY V1 COMPATIBILITY (HTTPS required): new clients must use Challenge/AuthenticateV2.")]
        public async Task<ApiResponse> Authenticate([FromBody] AuthenticateModel model)
        {
            ApiResponse? transportError = RequireHttps();
            if (transportError != null) return transportError;
            //string id = Guid.NewGuid().ToString();
            //string secret = ApiKeyUtility.EncryptString(id + "|" + AppTime.Now.AddYears(1).ToString("yyyy-MM-dd"));
            //string secret = ConfigurationHelper.config.GetSection("Licence:ApiSecret").Value ?? "";
            //string apiKey = ConfigurationHelper.config.GetSection("Licence:ApiKey").Value ?? "";
            string[] keys = ApiKeyUtility.DecryptString(AppSettings.Settings.Licence.ApiSecret).Split('|');
            if (AppSettings.Settings.Licence.ApiKey == keys[0] && Convert.ToDateTime(keys[1]) >= AppTime.Now)
            {
                if (string.IsNullOrWhiteSpace(model.CaptchaCode))
                {
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Captcha code is required");
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(model.Username))
                    {
                        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Username is required");
                    }
                    else
                    {
                        if (VerifyCaptcha(model.CaptchaCode, model.CaptchaToken) && !string.IsNullOrWhiteSpace(model.Password))
                        {
                            LoginManager user = await _userService.CheckLogin(model.Username, model.Password);
                            if (user == null)
                            {
                                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Authentication Failed! Invalid username or password.");
                            }
                            else if (HasActiveSession(user, model.LoginType))
                            {
                                return await AlreadyActive(user, model.LoginType);
                            }
                            else
                            {
                                return await AfterLogin(user, model.LoginType);
                            }
                        }
                        else
                        {
                            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Captcha code is expired OR Invalid");
                        }
                    }
                }
            }
            else
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "API product key expired.");
            }
        }

        [HttpPost("mobile-authenticate")]
        [AllowAnonymous]
        [SwaggerOperation(Description = "Mobile-only authentication endpoint with long session validity.")]
        public async Task<ApiResponse> MobileAuthenticate([FromBody] AuthenticateModel model)
        {
            ApiResponse? transportError = RequireHttps();
            if (transportError != null) return transportError;
            if (string.IsNullOrWhiteSpace(model.Username))
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Username is required");
            }

            LoginManager user = await _userService.CheckLogin(model.Username, model.Password);
            if (user == null)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Authentication Failed! Invalid username or password.");
            }

            if (HasActiveSession(user, LoginType.Mobile))
            {
                return await AlreadyActive(user, LoginType.Mobile);
            }

            return await AfterLogin(user, LoginType.Mobile);
        }

        [HttpPost]
        [Route("[action]")]
        [AllowAnonymous]
        [SwaggerOperation(Description = "for get CaptchaCode & CaptchaToken call GetCaptcha (Next) API.")]
        public async Task<ApiResponse> ConfirmAuthenticate([FromBody] ADAuthenticateModel model)
        {
            ApiResponse? transportError = RequireHttps();
            if (transportError != null) return transportError;
            if (string.IsNullOrWhiteSpace(model.Token))
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Confirmation token is required.");
            var consumed = await _loginStore.ConsumeConfirmationAsync(model.Token, model.LoginType, DateTimeOffset.UtcNow);
            if (consumed == null)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Confirmation token is invalid, expired, or already used.");

            LoginManager user = await _userService.GetById(checked((int)consumed.Value.UserId));
            if (user == null)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status401Unauthorized, "Authentication Failed.");
            return await AfterLogin(user, consumed.Value.LoginType);
        }

        [HttpGet("Challenge")]
        [AllowAnonymous]
        [SwaggerOperation(Description = "Secure login V2 RSA public key and one-time challenge.")]
        public async Task<ApiResponse> Challenge([FromQuery] LoginType loginType)
        {
            ApiResponse? transportError = RequireHttps();
            if (transportError != null) return transportError;
            if (!_loginKeyProvider.IsConfigured)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status503ServiceUnavailable,
                    _loginKeyProvider.ConfigurationError ?? "Login V2 is not configured.");
            if (!Enum.IsDefined(typeof(LoginType), loginType))
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Invalid loginType.");

            string captchaCode = Captcha.GenerateCaptchaCode();
            CaptchaResult captcha = Captcha.GenerateCaptchaImage(200, 60, captchaCode);
            string challengeId = Convert.ToHexString(RandomNumberGenerator.GetBytes(16));
            string nonce = Convert.ToBase64String(RandomNumberGenerator.GetBytes(12));
            var expiresAt = DateTimeOffset.UtcNow.AddMinutes(_configuration.GetValue("LoginV2:ChallengeMinutes", 5));
            LoginV2Key key = _loginKeyProvider.Current;
            string context = GetV2Context(key);
            var challenge = new LoginV2Challenge
            {
                Id = challengeId, Kid = key.Kid, Context = context, Nonce = nonce,
                LoginType = loginType, ExpiresAt = expiresAt,
                CaptchaHash = SHA256.HashData(Encoding.UTF8.GetBytes(captchaCode))
            };
            try
            {
                await _loginStore.CreateChallengeAsync(challenge);
            }
            catch (Exception)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status503ServiceUnavailable,
                    "Login V2 transaction storage is unavailable. Verify the HimsLoginChallenge database migration.");
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Challenge created.", new
            {
                version = 1,
                kid = key.Kid,
                algorithm = "RSA-OAEP-256",
                publicKey = key.PublicKey,
                challengeId,
                nonce,
                expiresAt,
                captchaId = challengeId,
                img = captcha.CaptchBase64Data,
                context
            });
        }

        [HttpPost("AuthenticateV2")]
        [AllowAnonymous]
        [SwaggerOperation(Description = "Secure login V2. The challenge is atomically consumed before credential validation.")]
        public async Task<ApiResponse> AuthenticateV2([FromBody] LoginV2Envelope envelope)
        {
            ApiResponse? transportError = RequireHttps();
            if (transportError != null) return transportError;
            if (!_loginKeyProvider.IsConfigured)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status503ServiceUnavailable,
                    _loginKeyProvider.ConfigurationError ?? "Login V2 is not configured.");
            if (envelope.Version != 1 || string.IsNullOrWhiteSpace(envelope.ChallengeId))
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Invalid login V2 envelope.");

            LoginV2Challenge? challenge = await _loginStore.ConsumeChallengeAsync(envelope.ChallengeId, DateTimeOffset.UtcNow);
            if (challenge == null)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Challenge is invalid, expired, or already used.");
            if (string.IsNullOrWhiteSpace(envelope.Kid) ||
                !_loginKeyProvider.TryGet(envelope.Kid, out LoginV2Key key))
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status409Conflict,
                    "Login key is no longer supported. Request a new challenge.",
                    new { code = "LOGIN_KEY_UNSUPPORTED", requestedKid = envelope.Kid });
            if (envelope.Kid != challenge.Kid || envelope.LoginType != challenge.LoginType)
                return InvalidV2();

            LoginV2Plaintext plaintext;
            try
            {
                plaintext = LoginV2Crypto.Decrypt(envelope, challenge, key);
            }
            catch (Exception ex) when (ex is CryptographicException or FormatException or System.Text.Json.JsonException)
            {
                return InvalidV2();
            }

            byte[] suppliedCaptcha = SHA256.HashData(Encoding.UTF8.GetBytes(plaintext.CaptchaCode ?? string.Empty));
            if (!CryptographicOperations.FixedTimeEquals(suppliedCaptcha, challenge.CaptchaHash) ||
                plaintext.LoginType != challenge.LoginType ||
                string.IsNullOrWhiteSpace(plaintext.Username) ||
                string.IsNullOrWhiteSpace(plaintext.Password))
                return InvalidV2();

            LoginManager user = await _userService.CheckLogin(plaintext.Username, plaintext.Password);
            if (user == null)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status401Unauthorized,
                    "Authentication Failed! Invalid username, password, or captcha.");
            return HasActiveSession(user, plaintext.LoginType)
                ? await AlreadyActive(user, plaintext.LoginType)
                : await AfterLogin(user, plaintext.LoginType);
        }

        [HttpPost("ConfirmAuthenticateV2")]
        [AllowAnonymous]
        public async Task<ApiResponse> ConfirmAuthenticateV2([FromBody] ConfirmAuthenticateV2Model model)
        {
            ApiResponse? transportError = RequireHttps();
            if (transportError != null) return transportError;
            if (string.IsNullOrWhiteSpace(model.Token))
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Confirmation token is required.");
            var consumed = await _loginStore.ConsumeConfirmationAsync(model.Token, model.LoginType, DateTimeOffset.UtcNow);
            if (consumed == null)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status401Unauthorized,
                    "Confirmation token is invalid, expired, or already used.");
            LoginManager user = await _userService.GetById(checked((int)consumed.Value.UserId));
            if (user == null)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status401Unauthorized, "Authentication Failed.");
            return await AfterLogin(user, consumed.Value.LoginType);
        }

        [NonAction]
        private async Task<ApiResponse> AfterLogin(LoginManager user, LoginType loginType)
        {
            if (loginType == LoginType.Mobile)
                user.MobileToken = Guid.NewGuid().ToString();
            else
                user.UserToken = Guid.NewGuid().ToString();
            await _userService.UpdateAsync(user, 0, "");
            (var permissionString, var permissions) = await GetPermissions(user.WebRoleId.Value);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Login Successfully.", new
            {
                status = "Ok",
                user.UserToken,
                user.WebRoleId,
                Permissions = JsonConvert.SerializeObject(permissions),
                Token = CommonExtensions.GenerateToken(user, _loginKeyProvider.Current,
                    GetTokenValidityMinutes(loginType), loginType,
                    AppSettings.Settings.AuthenticationSettings.Issuer,
                    AppSettings.Settings.AuthenticationSettings.Audience),
                UserName = user.FirstName + " " + user.LastName,
                user.UserId,
                RefreshToken = loginType == LoginType.Mobile ? user.MobileToken : null,
                DeviceId = loginType == LoginType.Mobile ? EncryptionUtility.EncryptText(user.UserId.ToString(), SecurityKeys.EnDeKey) : null,
                //User = user, // This includes all fields of the user object
                User = new
                {
                    user.LastName,
                    user.FirstName,
                    user.UserName,
                    user.MobileNo,
                    user.IsBillReview,
                    user.IsAdminMultiview,
                    user.IsGrnverify,
                    user.IsIndentVerify,
                    user.WebRoleId,
                    user.StoreId,
                    user.UnitId,
                    user.AddedBy,
                    user.CreatedBy,
                    user.DoctorId,
                    user.IsPoverify,
                    user.IsPhlebotomist,
                    TLoginStoreDetails = user.TLoginStoreDetails.Select(x => new { x.StoreId, x.LoginId, x.StoreName }),
                    TLoginUnitDetails = user.TLoginUnitDetails.Select(x => new { x.UnitId, x.LoginId, x.UnitName }),
                    TLoginAccessDetails = user.TLoginAccessDetails.Select(x => new { x.AccessValueId, x.AccessValue, x.AccessInputValue }),
                    TLoginCashCounterDetails = user.TLoginCashCounterDetails.Select(x => new { x.CashCounterId, x.LoginId, x.LoginCashCounterDetId })
                    //TLoginUnitDetails = user.TLoginUnitDetails.Select(x => new { x.UnitId, x.LoginId, x.UnitName })
                }
            });
        }

        [NonAction]
        private async Task<ApiResponse> AlreadyActive(LoginManager user, LoginType loginType)
        {
            string token = await _loginStore.CreateConfirmationAsync(user.UserId, loginType, DateTimeOffset.UtcNow.AddMinutes(10));
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, string.Empty, new
            {
                token,
                status = "Already Active",
                msg = "There is another session is already active. Do you want to continue?"
            });
        }

        [NonAction]
        private ApiResponse? RequireHttps()
        {
            bool allowDevelopmentHttp = _configuration.GetValue("LoginSecurity:AllowHttpDevelopment", false);
            bool allowLocalNetworkHttp = _configuration.GetValue("LoginSecurity:AllowHttpLocalNetwork", false);
            bool isLocalConnection = 
                LocalNetworkPolicy.IsLoopbackOrPrivate(Request.HttpContext.Connection.RemoteIpAddress) &&
                LocalNetworkPolicy.IsLoopbackOrPrivateHost(Request.Host.Host);
            bool isDevelopmentHttp = allowDevelopmentHttp &&
                Request.HttpContext.RequestServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment() &&
                isLocalConnection;
            bool isLocalNetworkHttp = allowLocalNetworkHttp && isLocalConnection;
            bool isAllowed = Request.IsHttps || isDevelopmentHttp || isLocalNetworkHttp;
            if (isAllowed) return null;
            Response.StatusCode = StatusCodes.Status426UpgradeRequired;
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status426UpgradeRequired, "Login endpoints require HTTPS.");
        }

        [NonAction]
        private string GetV2Context(LoginV2Key key)
        {
            string configured = _configuration["LoginV2:Context"] ?? string.Empty;
            if (!string.IsNullOrWhiteSpace(configured)) return configured;
            byte[] digest = SHA256.HashData(Convert.FromBase64String(key.PublicKey));
            return "hims-" + Convert.ToHexString(digest.AsSpan(0, 12));
        }

        [NonAction]
        private static ApiResponse InvalidV2() =>
            ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Invalid login V2 envelope.");

        [NonAction]
        public async Task<(string menuHideString, List<PageMasterDto> permissions)> GetPermissions(long RoleId)
        {
            List<PageMasterDto> permissions = await _IPermissionService.GetAllModules(RoleId);
            StringBuilder permissionString = new();
            foreach (PageMasterDto objPage in permissions)
            {
                permissionString.Append(objPage.PageCode).Append('|').Append((objPage.IsAdd.HasValue && objPage.IsAdd.Value) ? 1 : 0).Append('|').Append((objPage.IsEdit.HasValue && objPage.IsEdit.Value) ? 1 : 0).Append('|').Append((objPage.IsDelete.HasValue && objPage.IsDelete.Value) ? 1 : 0).Append('|').Append((objPage.IsView.HasValue && objPage.IsView.Value) ? 1 : 0).Append('|').Append((objPage.IsExport.HasValue && objPage.IsExport.Value) ? 1 : 0).Append(',');
            }
            return (permissionString.ToString(), permissions);
        }
        [HttpGet]
        [Route("[action]")]
        [AllowAnonymous]
        [SwaggerOperation(Description = "for see actual captcha open <a target='_blank' href='https://codebeautify.org/base64-to-image-converter'>Link</a> and then paste string in textbox, so you can see actual captcha.")]
        public ApiResponse GetCaptcha()
        {
            ApiResponse? transportError = RequireHttps();
            if (transportError != null) return transportError;
            string[] keys = ApiKeyUtility.DecryptString(AppSettings.Settings.Licence.ApiSecret).Split('|');
            const int width = 200;
            const int height = 60;
            string captchaCode = Captcha.GenerateCaptchaCode();
            CaptchaResult result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            string encrToken = EncryptionUtility.EncryptText(result.CaptchaCode + "|" + AppTime.Now.ToString("yyyy-MM-dd HH:mm"), SecurityKeys.EnDeKey);
            return new ApiResponse() { Data = new { Img = result.CaptchBase64Data, Token = encrToken, Expiry = Convert.ToDateTime(keys[1]) }, StatusCode = 200, StatusText = "OK" };

        }

        [NonAction]
        public bool VerifyCaptcha(string CaptchaCode, string CaptchaToken)
        {
            if (string.IsNullOrWhiteSpace(CaptchaCode) || string.IsNullOrWhiteSpace(CaptchaToken))
            {
                return false;
            }
            else if (CaptchaToken == "ByPassIt")
            {
                return false;
            }

            try
            {
                string[] decrypt = EncryptionUtility.DecryptText(CaptchaToken, SecurityKeys.EnDeKey).Split('|');
                return decrypt.Length == 2 &&
                    CryptographicOperations.FixedTimeEquals(
                        Encoding.UTF8.GetBytes(CaptchaCode), Encoding.UTF8.GetBytes(decrypt[0])) &&
                    Convert.ToDateTime(decrypt[1]).AddMinutes(2) >= AppTime.Now;
            }
            catch (Exception ex) when (ex is FormatException or CryptographicException or ArgumentException)
            {
                return false;
            }
        }


        [HttpGet]
        //[Permission(PageCode = "Administration", Permission = PagePermission.View)]
        [Route("get-menus")]
        [AnyPermission(Permission = PagePermission.View)]
        //[Permission]
        public ApiResponse GetMenus()
        {
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Login Successfully.", _IMenuService.GetMenus(CurrentRoleId, true));
            //return Ok(_IMenuService.GetMenus(CurrentRoleId, true));
        }
        [HttpPost("logout")]
        [SharedAuthenticated]
        public async Task<IActionResult> LogoutAsync()
        {
            string? authHeader = Request.Headers["Authorization"];
            if (authHeader is null || !authHeader.StartsWith("Bearer "))
                return BadRequest("No token provided.");
            if (CurrentUserId > 0)
            {
                LoginManager user = await _userService.GetById(CurrentUserId);

                if ((user?.UserId ?? 0) == 0)
                {
                    return NotFound("User not found.");
                }
                else
                {
                    if (CurrentLoginType == LoginType.Mobile)
                        user.MobileToken = null;
                    else
                        user.UserToken = null;

                    await _userService.UpdateAsync(user, CurrentUserId, CurrentUserName);
                }
            }
            return Ok(new { message = "Logged out successfully." });
        }

        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public async Task<ApiResponse> RefreshToken([FromBody] RefreshAuthenticateModel model)
        {
            int UserId = EncryptionUtility.DecryptText(model.UserId.ToString(), SecurityKeys.EnDeKey).ToInt();
            if (UserId == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status401Unauthorized, "Invalid token");
            }
            LoginManager user = await _userService.GetById(UserId);
            if (user != null && user.MobileToken == model.RefreshToken)
            {
                return await AfterLogin(user, LoginType.Mobile);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status401Unauthorized, "Invalid token,Login failed.");
        }

    }
    //[ApiController]
    //[Route("api/mpesa")]
    //public class MpesaController : ControllerBase
    //{
    //    private readonly MpesaStkService _stkService;

    //    public MpesaController(MpesaStkService service)
    //    {
    //        _stkService = service;
    //    }

    //    [HttpPost("pay")]
    //    public async Task<IActionResult> Pay(string phone, decimal amount, string reference)
    //    {
    //        var result = await _stkService.RegisterUrls();
    //        result = await _stkService.StkPushAsync(phone, amount,
    //           "https://api.airmid.co.in/api/payment/confirmation", reference);

    //        return Ok(result);
    //    }
    //}

}