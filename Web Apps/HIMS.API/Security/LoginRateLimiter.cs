using System.Collections.Concurrent;

namespace HIMS.API.Security
{
    public sealed class LoginRateLimitMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly LoginRateLimiter _limiter;

        public LoginRateLimitMiddleware(RequestDelegate next, LoginRateLimiter limiter)
        {
            _next = next;
            _limiter = limiter;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path.StartsWithSegments("/api", out var remaining) &&
                remaining.Value?.Contains("/Login/", StringComparison.OrdinalIgnoreCase) == true &&
                _limiter.IsLimited(context))
            {
                context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                context.Response.Headers["Retry-After"] = "60";
                await context.Response.WriteAsJsonAsync(new { statusCode = 429, message = "Too many login attempts." });
                return;
            }
            await _next(context);
        }
    }

    public sealed class LoginRateLimiter
    {
        private readonly ConcurrentDictionary<string, Window> _windows = new();
        private readonly int _limit;
        private readonly TimeSpan _period;

        public LoginRateLimiter(IConfiguration configuration)
        {
            _limit = Math.Max(1, configuration.GetValue("LoginSecurity:RequestsPerMinute", 60));
            _period = TimeSpan.FromMinutes(1);
        }

        public bool IsLimited(HttpContext context)
        {
            string ip = context.Connection.RemoteIpAddress?.ToString() ?? "unknown";
            string key = ip + "|" + context.Request.Path.Value;
            long now = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var window = _windows.GetOrAdd(key, _ => new Window(now, 0));
            lock (window)
            {
                if (now - window.Start >= _period.TotalSeconds)
                {
                    window.Start = now;
                    window.Count = 0;
                }
                window.Count++;
                return window.Count > _limit;
            }

        }

        private sealed class Window
        {
            public Window(long start, int count) { Start = start; Count = count; }
            public long Start { get; set; }
            public int Count { get; set; }
        }
    }
}
