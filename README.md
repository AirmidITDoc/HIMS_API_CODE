# HIMS_API_CODE
HIMS New Version API Code 

## HTTP security headers

The first middleware, `SecurityHeadersMiddleware`, adds these headers to responses
including static files, Swagger, API endpoints, redirects, and application errors:

- `Content-Security-Policy: frame-ancestors 'self'; base-uri 'self'; form-action 'self';`
- `X-Frame-Options: SAMEORIGIN`
- `X-Content-Type-Options: nosniff`
- `Referrer-Policy: strict-origin-when-cross-origin`
- `Permissions-Policy: camera=(self), microphone=(self), geolocation=(), payment=(), usb=(), serial=(), hid=(), display-capture=()`

A separate CSP header preserves any downstream policy; browsers enforce both.
This CSP is a framing/base/form baseline, not a full script/XSS policy.
Test stricter script/resource restrictions in report-only mode before enforcing,
including Swagger, static HTML, PDF responses, and external integrations.

HSTS is opt-in because production HTTPS availability has not been verified.
After confirming valid certificates and HTTPS on all routes/clients, set
`SecurityHeaders__EnableHsts=true` in the deployment environment (configuration
key `SecurityHeaders:EnableHsts`). The middleware sends
`Strict-Transport-Security: max-age=86400` only when `Request.IsHttps` is true.
It does not enable HTTPS or redirects. Start with this one-day rollout; increase
the duration after validation. No `includeSubDomains` or preload is enabled.
Reject credential requests over HTTP at the public host.

If TLS terminates at a reverse proxy, configure HSTS there, or configure
forwarded-proto handling restricted to known/trusted proxies. Never accept
arbitrary client-supplied forwarding headers as proof of HTTPS.

Deploy/restart the API to apply this change. Verify document GET responses and
`curl.exe -I https://<api-host>/swagger/index.html` through the public reverse
proxy. Ensure proxies preserve these headers and add equivalent protections to
their own redirects/errors. A separately hosted Angular frontend needs its own
HTTP headers; API headers do not protect its HTML pages.

Run the package-free security-header regression executable with:

```powershell
dotnet run --project "Tests\HIMS.SecurityHeadersRegression\HIMS.SecurityHeadersRegression.csproj"
```

It hosts the actual middleware without starting the database-backed API, and
checks headers on success, redirects, 404/500 responses, preservation of a
downstream CSP, and both enabled/disabled HSTS on HTTP and simulated HTTPS.

## Login token size and HTTP 400 errors

Permission lists are Brotli-compressed before encryption into the signed JWT's
`PermissionsBr` claim. Authorization decodes the original permission list before
applying the existing checks. Legacy tokens with an encrypted `Permissions` claim
remain supported. The separate permissions response used by the UI is unchanged.

Deploy the updated API to every instance before issuing new tokens: older API
instances cannot authorize the new claim format. Users must sign out and sign in
after deployment to replace existing oversized tokens. A frontend reload alone
does not replace a token already in local storage.

An HTML HTTP 400 response saying "Request Too Long" can be emitted by the HTTP
server before the API runs when request headers exceed its limit. Do not truncate
tokens or remove authorization headers. Verify authenticated requests after a
fresh login on the deployed API.

Run the dependency-free regression executable with:

```powershell
dotnet run --project "Tests\HIMS.TokenRegression\HIMS.TokenRegression.csproj"
```

It verifies signed web/mobile tokens remain below 8 KB for 500 permission entries,
lossless permission round trips, legacy-token compatibility, all five permission
flags, multi-page matching, and denial for unknown pages or missing claims.
