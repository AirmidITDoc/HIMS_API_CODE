using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using HIMS.API.Extensions;
using HIMS.Core.Infrastructure;

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

    }
}
