using Asp.Versioning;
using HIMS.Api.Models.Common;
using HIMS.Api.Models.Login;
using HIMS.API.Extensions;
using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using HIMS.Services.Permissions;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using System.Security;
using System.Text;

namespace HIMS.API.Controllers.Login
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class LoginController : ControllerBase
    {
        protected readonly IUserService _userService;
        private readonly IConfiguration _Configuration;
        private readonly IPermissionService _IPermissionService;
        public LoginController(IUserService userService, IConfiguration configuration, IPermissionService permission)
        {
            _userService = userService;
            _Configuration = configuration;
            _IPermissionService = permission;
        }
        [HttpPost]
        [Route("[action]")]
        [SwaggerOperation(Description = "for get CaptchaCode & CaptchaToken call GetCaptcha (Next) API.")]
        public async Task<ApiResponse> Authenticate([FromBody] AuthenticateModel model)
        {
            if (string.IsNullOrWhiteSpace(model.CaptchaCode))
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Captcha code is required");
            }
            else if (string.IsNullOrWhiteSpace(model.Username))
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
                    else
                    {
                        user.UserToken = Guid.NewGuid().ToString();
                        await _userService.UpdateAsync(user, 0, "");
                        (var permissionString, var permissions) = await GetPermissions(user.RoleId.Value);
                        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Login Successfully.", new
                        {
                            user.UserToken,
                            Permissions = HIMS.Services.Utilities.AESEncrytDecry.EncryptStringAES(JsonConvert.SerializeObject(permissions)),
                            Token = CommonExtensions.GenerateToken(user, Convert.ToString(_Configuration["AuthenticationSettings:SecretKey"]), 30, permissionString),
                            UserName = user.FirstName + " " + user.LastName,
                            user.UserId
                        });
                    }
                }
                else
                {
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Captcha code is expired OR Invalid");
                }
            }
        }
        [NonAction]
        public async Task<(string menuHideString, List<PageMaster> permissions)> GetPermissions(long RoleId)
        {
            List<PageMaster> permissions = await _IPermissionService.GetAllModules(RoleId);
            StringBuilder permissionString = new();
            foreach (PageMaster objPage in permissions)
            {
                permissionString.Append(objPage.PageCode).Append('|').Append(objPage.IsAdd ? 1 : 0).Append('|').Append(objPage.IsEdit ? 1 : 0).Append('|').Append(objPage.IsDelete ? 1 : 0).Append('|').Append(objPage.IsView ? 1 : 0).Append(',');
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

    }
}
