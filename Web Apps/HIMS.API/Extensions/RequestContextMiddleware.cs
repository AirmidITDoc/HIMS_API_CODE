using HIMS.Api.Models.Common;
using HIMS.API.Models.Common;
using HIMS.Data.Models;
using HIMS.Services.Users;
using System.Security.Claims;
using System.Net;
using System.Text.Json;

namespace HIMS.API.Extensions
{
    public class RequestContextMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            if (context.GetEndpoint()?.Metadata.GetMetadata<Microsoft.AspNetCore.Authorization.IAllowAnonymous>() != null)
            {
                await _next(context);
                return;
            }

            if (context.User.Identity?.IsAuthenticated != true ||
                context.User.Identity is not ClaimsIdentity identity ||
                !SessionAuthorization.TryGetClaim(identity, "Id", out int userId))
            {
                await _next(context);
                return;
            }

            LoginManager? user = await userService.GetById(userId);
            if (user == null || user.IsActive != true)
            {
                await Reject(context, "The authenticated user is not active.");
                return;
            }

            if (!TryResolveHeader(context, "X-Store-Id", out int? requestedStore) ||
                !TryResolveHeader(context, "X-Unit-Id", out int? requestedUnit) ||
                !RequestScopeValidator.TryResolve(user, requestedStore, requestedUnit,
                    out RequestContext? approvedContext, out string reason))
            {
                await Reject(context, "Invalid or unassigned store/unit scope.");
                return;
            }

            context.Items["RequestContext"] = approvedContext!;
            await _next(context);
        }

        private static bool TryResolveHeader(HttpContext context, string name, out int? value)
        {
            value = null;
            if (!context.Request.Headers.TryGetValue(name, out var raw) || raw.Count == 0 ||
                string.IsNullOrWhiteSpace(raw.ToString()))
                return true;
            if (!int.TryParse(raw.ToString(), out int parsed) || parsed <= 0)
                return false;
            value = parsed;
            return true;
        }

        private static async Task Reject(HttpContext context, string message)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                status = ApiStatusCode.Status403Forbidden,
                message
            }));
        }
    }

    public static class RequestScopeValidator
    {
        public static bool TryResolve(LoginManager user, int? requestedStore, int? requestedUnit,
            out RequestContext? context, out string reason)
        {
            HashSet<int> stores = new();
            HashSet<int> units = new();
            if (user.StoreId > 0) stores.Add(checked((int)user.StoreId.Value));
            if (user.UnitId > 0) units.Add(checked((int)user.UnitId.Value));
            foreach (var assignment in user.TLoginStoreDetails ?? Array.Empty<TLoginStoreDetail>())
                if (assignment.StoreId > 0) stores.Add(checked((int)assignment.StoreId));
            foreach (var assignment in user.TLoginUnitDetails ?? Array.Empty<TLoginUnitDetail>())
                if (assignment.UnitId > 0) units.Add(checked((int)assignment.UnitId));

            bool storeResolved = requestedStore.HasValue
                ? stores.Contains(requestedStore.Value)
                : user.StoreId > 0 || stores.Count == 1;
            bool unitResolved = requestedUnit.HasValue
                ? units.Contains(requestedUnit.Value)
                : user.UnitId > 0 || units.Count == 1;

            if (!storeResolved || !unitResolved)
            {
                context = null;
                reason = !storeResolved ? "Store scope is missing or unassigned." : "Unit scope is missing or unassigned.";
                return false;
            }

            context = new RequestContext
            {
                StoreId = requestedStore ?? (user.StoreId > 0
                    ? checked((int)user.StoreId.Value)
                    : stores.Single()),
                UnitId = requestedUnit ?? (user.UnitId > 0
                    ? checked((int)user.UnitId.Value)
                    : units.Single())
            };
            reason = string.Empty;
            return true;
        }
    }
}
