using HIMS.API.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

foreach (bool enableHsts in new[] { false, true })
{
    var builder = WebApplication.CreateBuilder();
    builder.Logging.ClearProviders();
    builder.WebHost.UseUrls("http://127.0.0.1:0");
    if (enableHsts)
        builder.Configuration["SecurityHeaders:EnableHsts"] = "true";

    await using var app = builder.Build();
    // Simulate TLS termination without trusting forwarding headers in production.
    app.Use(async (context, next) =>
    {
        if (context.Request.Path.StartsWithSegments("/https"))
            context.Request.Scheme = "https";
        await next();
    });
    app.UseMiddleware<SecurityHeadersMiddleware>();
    app.Use(async (context, next) =>
    {
        try { await next(); }
        catch { context.Response.StatusCode = 500; }
    });
    app.MapGet("/ok", () => "OK");
    app.MapGet("/https", () => "OK");
    app.MapGet("/redirect", (HttpContext context) => context.Response.Redirect("/ok"));
    app.MapGet("/error", (HttpContext context) => { throw new InvalidOperationException("Test error"); });
    app.MapGet("/csp", (HttpContext context) =>
    {
        context.Response.Headers["Content-Security-Policy"] = "script-src 'self';";
        return "OK";
    });
    await app.StartAsync();
    using var client = new HttpClient(new HttpClientHandler { AllowAutoRedirect = false })
    {
        BaseAddress = new Uri(app.Urls.Single())
    };
    foreach (var (path, status) in new[] { ("/ok", 200), ("/https", 200), ("/redirect", 302), ("/missing", 404), ("/error", 500), ("/csp", 200) })
    {
        using var response = await client.GetAsync(path);
        Require((int)response.StatusCode == status, $"Unexpected status for {path}");
        var csp = string.Join(",", response.Headers.GetValues("Content-Security-Policy"));
        Require(csp.Contains("frame-ancestors 'self'; base-uri 'self'; form-action 'self';"), "Missing CSP");
        if (path == "/csp")
            Require(csp.Contains("script-src 'self';"), "Downstream CSP overwritten");
        Require(response.Headers.GetValues("X-Frame-Options").Single() == "SAMEORIGIN", "Missing framing fallback");
        Require(response.Headers.GetValues("X-Content-Type-Options").Single() == "nosniff", "Missing nosniff");
        Require(response.Headers.GetValues("Referrer-Policy").Single() == "strict-origin-when-cross-origin", "Missing referrer policy");
        Require(response.Headers.GetValues("Permissions-Policy").Single() == "camera=(self), microphone=(self), geolocation=(), payment=(), usb=(), serial=(), hid=(), display-capture=()", "Unexpected permissions policy");
        bool hasHsts = response.Headers.Contains("Strict-Transport-Security");
        Require(hasHsts == (enableHsts && path == "/https"), "HSTS must require opt-in and HTTPS");
        if (hasHsts)
            Require(response.Headers.GetValues("Strict-Transport-Security").Single() == "max-age=86400", "Unexpected HSTS rollout duration");
    }
    await app.StopAsync();
}
Console.WriteLine("PASS: headers on success, redirects, 404/500, preserved CSP, and HTTPS-only opt-in HSTS.");

static void Require(bool condition, string message)
{
    if (!condition) throw new InvalidOperationException(message);
}
