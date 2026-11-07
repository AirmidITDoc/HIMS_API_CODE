using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace HIMS.API.Extensions
{
    public class SecurityHeadersMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly bool _enableHsts;

        public SecurityHeadersMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _enableHsts = configuration.GetValue<bool>("SecurityHeaders:EnableHsts");
        }

        public async Task Invoke(HttpContext context)
        {
            context.Response.OnStarting(() =>
            {
                var headers = context.Response.Headers;
                // Separate policies are enforced together, preserving any downstream CSP.
                headers.Append("Content-Security-Policy", "frame-ancestors 'self'; base-uri 'self'; form-action 'self';");
                headers["X-Frame-Options"] = "SAMEORIGIN";
                headers["X-Content-Type-Options"] = "nosniff";
                headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
                headers["Permissions-Policy"] = "camera=(self), microphone=(self), geolocation=(), payment=(), usb=(), serial=(), hid=(), display-capture=()";
                if (_enableHsts && context.Request.IsHttps)
                {
                    headers["Strict-Transport-Security"] = "max-age=86400";
                }
                return Task.CompletedTask;
            });
            await _next(context);
        }
    }
}
