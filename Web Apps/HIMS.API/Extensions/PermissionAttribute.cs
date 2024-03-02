using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using HIMS.API.Models.Common;
using HIMS.Api.Models.Common;
using HIMS.Services.Users;
using HIMS.Core;
using HIMS.Core.Infrastructure;

namespace HIMS.API.Extensions
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PermissionAttribute : Attribute, IAuthorizationFilter
    {
        public string PageCode { get; set; }
        public PagePermission Permission { get; set; }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            bool isAuthorised = false;
            bool isPermission = true;

            IPrincipal user = context.HttpContext.User;
            UserService IUserMasterRepository = context.HttpContext.RequestServices.GetService(typeof(IUserService)) as UserService;
            if ((user.Identity as ClaimsIdentity).Claims.Any())
            {
                string UserToken = GetTokenValue(user, "UserToken");
                string UserId = GetTokenValue(user, "Id");
                if (IUserMasterRepository.CheckTokenIsValidAsync(UserId.ToInt(), UserToken) && user.Identity.IsAuthenticated)
                {
                    isAuthorised = true;
                    if (DateTime.Compare(CommonExtensions.FromUnixTime(long.Parse((user.Identity as ClaimsIdentity).Claims.FirstOrDefault(c => c.Type == "exp").Value)), DateTime.UtcNow) < 0)
                    {
                        isAuthorised = false;
                    }
                    else if ((PageCode?.Trim() ?? "") != "")
                    {
                        isPermission = CommonExtensions.CheckPermission(PageCode, Permission);
                    }
                }
            }
            if (!isAuthorised)
            {
                string actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;
                if (actionName.ToUpper() != "REFRESHTOKEN")
                {
                    context.HttpContext.Response.StatusCode = 401;
                    context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Not Authorized";
                    context.Result = new JsonResult("NotAuthorized")
                    {
                        Value = new
                        {
                            Status = ApiStatusCode.Status401Unauthorized,
                            Message = "Authentication Failed! Invalid Token."
                        },
                    };
                }
            }
            else if (!isPermission)
            {
                string actionName = ((Microsoft.AspNetCore.Mvc.Controllers.ControllerActionDescriptor)context.ActionDescriptor).ActionName;
                if (actionName.ToUpper() != "LOGOUT")
                {
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                    context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>().ReasonPhrase = "Bad Request";
                    context.Result = new JsonResult("Forbidden")
                    {
                        Value = new
                        {
                            Status = ApiStatusCode.Status403Forbidden,
                            Message = "You don't have permission to access this feature."
                        },
                    };
                }
            }
        }
        public string GetTokenValue(IPrincipal user, string climType)
        {
            return EncryptionUtility.DecryptText((user.Identity as ClaimsIdentity).Claims.FirstOrDefault(c => c.Type == climType).Value, SecurityKeys.EnDeKey);
        }
    }
}