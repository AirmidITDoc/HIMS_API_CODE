using HIMS.API.Models.Common;

namespace HIMS.API.Extensions
{
    public class RequestContextMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestContextMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var storeId = context.Request.Headers["X-Store-Id"];
            var unitId = context.Request.Headers["X-Unit-Id"];

            if (!string.IsNullOrEmpty(storeId) && !string.IsNullOrEmpty(unitId))
            {
                context.Items["RequestContext"] = new RequestContext
                {
                    StoreId = int.Parse(storeId),
                    UnitId = int.Parse(unitId)
                };
            }

            await _next(context);
        }
    }

}
