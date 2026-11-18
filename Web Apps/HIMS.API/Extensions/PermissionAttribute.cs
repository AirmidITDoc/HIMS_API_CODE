using HIMS.Api.Models.Common;
using HIMS.Core;
using HIMS.Services.Users;
using HIMS.Services.Permissions;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace HIMS.API.Extensions
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PermissionAttribute : AuthenticatedAttribute
    {
        private PagePermission _permission;
        private bool _permissionSpecified;

        public string PageCode { get; set; } = string.Empty;
        public PagePermission Permission
        {
            get => _permission;
            set
            {
                _permission = value;
                _permissionSpecified = true;
            }
        }

        public override async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (!await SessionAuthorization.IsValidAsync(context.HttpContext))
            {
                WriteUnauthorized(context);
                return;
            }

            if (string.IsNullOrWhiteSpace(PageCode) || !_permissionSpecified ||
                !Enum.IsDefined(typeof(PagePermission), _permission))
            {
                WriteForbidden(context);
                return;
            }

            var identity = (System.Security.Claims.ClaimsIdentity)context.HttpContext.User.Identity!;
            SessionAuthorization.TryGetClaim(identity, "Id", out int userId);
            IUserService userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
            IPermissionService permissionService = context.HttpContext.RequestServices.GetRequiredService<IPermissionService>();
            var currentUser = await userService.GetById(userId);
            long roleId = currentUser?.WebRoleId ?? 0;
            if (roleId <= 0)
            {
                WriteForbidden(context);
                return;
            }

            var serverPermissions = await permissionService.GetAllModules(roleId);
            if (!CommonExtensions.CheckPermission(serverPermissions, PageCode, _permission))
                WriteForbidden(context);
        }

        private static void WriteForbidden(AuthorizationFilterContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>()!.ReasonPhrase = "Forbidden";
            context.Result = new JsonResult("Forbidden")
            {
                Value = new
                {
                    Status = ApiStatusCode.Status403Forbidden,
                    Message = "You don't have permission to access this feature."
                }
            };
        }
    }
}