using HIMS.Api.Models.Common;
using HIMS.API.Models.Common;
using HIMS.API.Utility;
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
            //string validateLicense = new LicenseService().Validate();
            //if (validateLicense == "Ok")
                await _next(context);
            //else
            //{
            //    HttpResponse response = context.Response;
            //    response.ContentType = "application/json";
            //    response.StatusCode = (int)HttpStatusCode.InternalServerError;
            //    string errorJson = JsonSerializer.Serialize(ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, validateLicense, new { ApiUrl = context.Request.Path.Value }));
            //    await response.WriteAsync(errorJson);
            //}
        }
    }

}
