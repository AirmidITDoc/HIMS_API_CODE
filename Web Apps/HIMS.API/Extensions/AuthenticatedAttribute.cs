using HIMS.Api.Models.Common;
using HIMS.Core.Infrastructure;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace HIMS.API.Extensions
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true)]
    public class AuthenticatedAttribute : Attribute, IAsyncAuthorizationFilter
    {
        public virtual async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            if (await SessionAuthorization.IsValidAsync(context.HttpContext))
                return;

            WriteUnauthorized(context);
        }

        protected static void WriteUnauthorized(AuthorizationFilterContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            context.HttpContext.Response.HttpContext.Features.Get<IHttpResponseFeature>()!.ReasonPhrase = "Not Authorized";
            context.Result = new JsonResult("NotAuthorized")
            {
                Value = new
                {
                    Status = ApiStatusCode.Status401Unauthorized,
                    Message = "Authentication Failed! Invalid Token."
                }
            };
        }
    }

    internal static class SessionAuthorization
    {
        internal static async Task<bool> IsValidAsync(HttpContext context)
        {
            ClaimsPrincipal user = context.User;
            if (user.Identity?.IsAuthenticated != true || user.Identity is not ClaimsIdentity identity)
                return false;

            if (IsValidPatient(identity))
                return true;

            if (!TryGetClaim(identity, "Id", out int userId) ||
                !TryGetClaimText(identity, "UserToken", out string userToken) ||
                !TryGetClaimText(identity, "LoginType", out string loginType))
                return false;

            IUserService userService = context.RequestServices.GetRequiredService<IUserService>();
            if (!userService.CheckTokenIsValidAsync(userId, userToken, loginType))
                return false;

            var currentUser = await userService.GetById(userId);
            return currentUser?.IsActive == true;
        }

        internal static bool IsValidPatient(ClaimsIdentity identity)
        {
            string authType = identity.FindFirst("authType")?.Value ?? string.Empty;
            string patientId = identity.FindFirst("patientId")?.Value ?? string.Empty;
            string mobile = identity.FindFirst("mobile")?.Value ?? string.Empty;
            return string.Equals(authType, "PatientOtp", StringComparison.OrdinalIgnoreCase) &&
                   long.TryParse(patientId, out long parsedPatientId) &&
                   parsedPatientId > 0 &&
                   !string.IsNullOrWhiteSpace(mobile);
        }

        internal static bool TryGetClaim(ClaimsIdentity identity, string claimType, out int value)
        {
            value = 0;
            return TryGetClaimText(identity, claimType, out string text) && int.TryParse(text, out value);
        }

        internal static bool TryGetClaimText(ClaimsIdentity identity, string claimType, out string value)
        {
            value = string.Empty;
            string? encrypted = identity.FindFirst(claimType)?.Value;
            if (string.IsNullOrWhiteSpace(encrypted))
                return false;

            try
            {
                value = EncryptionUtility.DecryptText(encrypted, SecurityKeys.EnDeKey);
                return !string.IsNullOrWhiteSpace(value);
            }
            catch
            {
                return false;
            }
        }
    }
}
