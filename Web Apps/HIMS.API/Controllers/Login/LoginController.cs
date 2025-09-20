using Asp.Versioning;
using DocumentFormat.OpenXml.Spreadsheet;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.Api.Models.Login;
using HIMS.API.Extensions;
using HIMS.API.Utility;
using HIMS.Core.Infrastructure;
using HIMS.Core.Utilities;
using HIMS.Data.Models;
using HIMS.Services.Permissions;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Security;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace HIMS.API.Controllers.Login
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class LoginController : BaseController
    {
        protected readonly IUserService _userService;
        private readonly IConfiguration _Configuration;
        private readonly IPermissionService _IPermissionService;
        private readonly IMenuService _IMenuService;
        public LoginController(IUserService userService, IConfiguration configuration, IPermissionService permission, IMenuService iMenuService)
        {
            _userService = userService;
            _Configuration = configuration;
            _IPermissionService = permission;
            _IMenuService = iMenuService;
        }


        [HttpPost]
        [Route("[action]")]
        [SwaggerOperation(Description = "for get CaptchaCode & CaptchaToken call GetCaptcha (Next) API.")]
        public async Task<ApiResponse> Authenticate([FromBody] AuthenticateModel model)
        {
            //string id = Guid.NewGuid().ToString();
            //string secret = ApiKeyUtility.EncryptString(id + "|" + DateTime.Now.AddYears(1).ToString("yyyy-MM-dd"));
            //string secret = ConfigurationHelper.config.GetSection("Licence:ApiSecret").Value;
            //string apiKey = ConfigurationHelper.config.GetSection("Licence:ApiKey").Value;
            //string[] keys = ApiKeyUtility.DecryptString(secret).Split('|');
            //if (apiKey == keys[0] && Convert.ToDateTime(keys[1]) >= DateTime.Now)
            //{
            if (string.IsNullOrWhiteSpace(model.CaptchaCode))
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Captcha code is required");
            }
            else
            {
                //model.Username = EncryptionUtility.DecryptFromAngular(model.Username);
                //model.Password = EncryptionUtility.DecryptFromAngular(model.Password);
                if (string.IsNullOrWhiteSpace(model.Username))
                {
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Username is required");
                }
                else
                {
                    if (VerifyCaptcha(model.CaptchaCode, model.CaptchaToken) || model.Password.Trim().Length == 0)
                    {
                        LoginManager user = await _userService.CheckLogin(model.Username, model.Password);
                        if (user == null)
                        {
                            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Authentication Failed! Invalid username or password.");
                        }
                        else if (!string.IsNullOrWhiteSpace(user.UserToken) && user.UserId > 0)
                        {
                            string encrToken = EncryptionUtility.EncryptText(user.UserName + "|" + user.Password + "|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), SecurityKeys.EnDeKey);
                            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { token = encrToken, status = "Already Active", Msg = "There is another session is already active. Do you want to continue?" });
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
            //}
            //else {
            //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "API product key expired.");
            //}
        }
        [HttpPost]
        [Route("[action]")]
        [SwaggerOperation(Description = "for get CaptchaCode & CaptchaToken call GetCaptcha (Next) API.")]
        public async Task<ApiResponse> ConfirmAuthenticate([FromBody] ADAuthenticateModel model)
        {

            string[] decrypt = EncryptionUtility.DecryptText(model.Token, SecurityKeys.EnDeKey).Split('|');
            if (Convert.ToDateTime(decrypt[2]).AddMinutes(10) >= DateTime.Now)
            {
                LoginManager user = await _userService.CheckLogin(decrypt[0], decrypt[1]);
                if (user == null)
                {
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Authentication Failed! Invalid username or password.");
                }
                else
                {
                    return await AfterLogin(user, model.LoginType);
                }
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Token is expired.");
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
                Permissions = EncryptionUtility.EncryptForAngular(JsonConvert.SerializeObject(permissions)),
                Token = CommonExtensions.GenerateToken(user, Convert.ToString(_Configuration["AuthenticationSettings:SecretKey"]), 720, permissionString, loginType),
                UserName = user.FirstName + " " + user.LastName,
                user.UserId,
                RefreshToken = loginType == LoginType.Mobile ? user.MobileToken : null,
                DeviceId = loginType == LoginType.Mobile ? EncryptionUtility.EncryptText(user.UserId.ToString(), SecurityKeys.EnDeKey) : null,
                User = user // This includes all fields of the user object
            });
        }
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
        [SwaggerOperation(Description = "for see actual captcha open <a target='_blank' href='https://codebeautify.org/base64-to-image-converter'>Link</a> and then paste string in textbox, so you can see actual captcha.")]
        public ApiResponse GetCaptcha()
        {
            const int width = 200;
            const int height = 60;
            string captchaCode = Captcha.GenerateCaptchaCode();
            CaptchaResult result = Captcha.GenerateCaptchaImage(width, height, captchaCode);
            string encrToken = EncryptionUtility.EncryptText(result.CaptchaCode + "|" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), SecurityKeys.EnDeKey);
            return new ApiResponse() { Data = new { Img = result.CaptchBase64Data, Token = encrToken }, StatusCode = 200, StatusText = "OK" };
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

            string[] decrypt = EncryptionUtility.DecryptText(CaptchaToken, SecurityKeys.EnDeKey).Split('|');
            return CaptchaCode == decrypt[0] && Convert.ToDateTime(decrypt[1]).AddMinutes(2) >= DateTime.Now;
        }


        [HttpGet]
        [Route("get-menus")]
        //[Permission]
        public ApiResponse GetMenus()
        {
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Login Successfully.", _IMenuService.GetMenus(CurrentRoleId, true));
            //return Ok(_IMenuService.GetMenus(CurrentRoleId, true));
        }
        [HttpPost("logout")]
        [Permission]
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
                    user.UserToken = null;
                    await _userService.UpdateAsync(user, CurrentUserId, CurrentUserName);
                }
            }
            return Ok(new { message = "Logged out successfully." });
        }

        [HttpPost("RefreshToken")]
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
}
