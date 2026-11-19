using HIMS.Api.Models.Common;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Services.Permissions;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace HIMS.API.Extensions
{
    [AttributeUsage(AttributeTargets.Method)]
    public sealed class AnyPermissionAttribute : AuthenticatedAttribute
    {
        private PagePermission _permission;
        private bool _permissionSpecified;

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

            if (!_permissionSpecified || !Enum.IsDefined(typeof(PagePermission), _permission))
            {
                WriteForbidden(context);
                return;
            }

            var identity = (System.Security.Claims.ClaimsIdentity)context.HttpContext.User.Identity!;
            SessionAuthorization.TryGetClaim(identity, "Id", out int userId);
            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
            var permissionService = context.HttpContext.RequestServices.GetRequiredService<IPermissionService>();
            LoginManager currentUser = await userService.GetById(userId);
            if ((currentUser?.WebRoleId ?? 0) <= 0)
            {
                WriteForbidden(context);
                return;
            }

            var permissions = await permissionService.GetAllModules(currentUser.WebRoleId!.Value);
            if (!permissions.Any(permission => HasPermission(permission, _permission)))
                WriteForbidden(context);
        }

        private static bool HasPermission(PageMasterDto permission, PagePermission required)
        {
            return required switch
            {
                PagePermission.Add => permission.IsAdd == true,
                PagePermission.Edit => permission.IsEdit == true,
                PagePermission.Delete => permission.IsDelete == true,
                PagePermission.Export => permission.IsExport == true,
                _ => permission.IsView == true
            };
        }

        private static void WriteForbidden(AuthorizationFilterContext context)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>()!.ReasonPhrase = "Forbidden";
            context.Result = new JsonResult(new
            {
                Status = ApiStatusCode.Status403Forbidden,
                Message = "You don't have permission to access this feature."
            });
        }
    }
}
