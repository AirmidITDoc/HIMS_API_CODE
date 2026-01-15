using HIMS.Api.Models.Login;
using HIMS.API.Extensions;
using HIMS.API.Models.Common;
using HIMS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace HIMS.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public ClaimsPrincipal CurrentUser => HttpContext.User;

        public int CurrentUserId
        {
            get
            {
                if (CurrentUser != null && CurrentUser.HasClaim(c => c.Type == "Id"))
                {
                    return EncryptionUtility.DecryptText(CurrentUser.Claims.FirstOrDefault(c => c.Type == "Id").Value, SecurityKeys.EnDeKey).ToInt();
                }
                else
                {
                    return 0;
                }
            }
        }
        public string CurrentUserName
        {
            get
            {
                if (CurrentUser != null && CurrentUser.HasClaim(c => c.Type == "UserName"))
                {
                    return EncryptionUtility.DecryptText(CurrentUser.Claims.FirstOrDefault(c => c.Type == "UserName").Value, SecurityKeys.EnDeKey);
                }
                else
                {
                    return "";
                }
            }
        }
        public int CurrentRoleId
        {
            get
            {
                if (CurrentUser != null && CurrentUser.HasClaim(c => c.Type == "RoleId"))
                {
                    return int.Parse(EncryptionUtility.DecryptText(CurrentUser.Claims.FirstOrDefault(c => c.Type == "RoleId").Value, SecurityKeys.EnDeKey));
                }
                else
                {
                    return 0;
                }
            }
        }
        public LoginType CurrentLoginType
        {
            get
            {
                if (CurrentUser != null && CurrentUser.HasClaim(c => c.Type == "LoginType"))
                {
                    var decryptedValue = EncryptionUtility.DecryptText(CurrentUser.Claims.FirstOrDefault(c => c.Type == "LoginType").Value, SecurityKeys.EnDeKey);
                    if (int.TryParse(decryptedValue, out int loginTypeInt))
                    {
                        return (LoginType)loginTypeInt;
                    }
                    else
                    {
                        return LoginType.Web;
                    }
                }
                else
                {
                    return LoginType.Web;
                }
            }
        }
        protected RequestContext Context =>
       HttpContext.Items["RequestContext"] as RequestContext
       ?? throw new UnauthorizedAccessException("Session/Unit missing");
    }
}
