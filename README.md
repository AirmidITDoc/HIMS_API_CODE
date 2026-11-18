# HIMS_API_CODE
HIMS New Version API Code 

## Empty OP bill receipts

If `OpBillReceipt` renders only its header and zero totals, check
`dbo.ps_rptBillPrint` before changing the Angular bill identifier. Duplicate
`M_LoginAccessConfig` entries for `IsHospitalStamp` can cause SQL error 512 in
the procedure's scalar stamp-configuration lookup, even when the bill and its
charges exist.

Run [FixOpBillReceiptStampLookup.sql](Web%20Apps/HIMS.API/DbData/FixOpBillReceiptStampLookup.sql)
against the affected API database. It changes only that lookup from scalar
equality to set membership, retains existing procedure customizations and
configuration rows, and can be rerun. Unexpected definitions fail explicitly
instead of being overwritten. Take the normal database procedure backup and
use the normal shared-database deployment process.

The SQL correction takes effect on the next report request without an API
restart. Close and reopen an existing preview: its already-loaded PDF will
not refresh automatically.

Deploy/restart the API to enable the empty-OP-receipt guard as well: failed or
empty receipt queries must report an error rather than generate a misleading
zero-valued PDF. Reprint an existing bill; no new bill or payment is required.

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

## Secure Login V2 deployment and configuration (support handoff)

These uncommitted login changes introduce V2 challenge-based authentication,
RSA-signed JWT validation, password hashing upgrades, and stricter transport
requirements. Apply this checklist in deployment order.

1. **Database update (required before API rollout)**
   - Execute once per environment:
     [HIMS_Secure_Login_V2.sql](Web%20Apps/HIMS.API/DbData/HIMS_Secure_Login_V2.sql)
   - The script creates `dbo.HimsLoginChallenge` and `dbo.HimsLoginConfirmation`
     plus expiry indexes. Without it, V2 login challenge/confirm endpoints fail.

2. **Set required secrets/configuration**
   - **API and ScheduleJobs encryption key**
     - Set `HIMS_ENCRYPTION_KEY` (preferred) or `EncryptionKey` config value.
     - Startup now fails if encryption key is missing.
   - **JWT issuer/audience (required)**
     - Set `AuthenticationSettings:Issuer` and `AuthenticationSettings:Audience`.
   - **Login V2 RSA key material (required in production)**
     - Set `HIMS_LOGIN_V2_KID` and exactly one private-key input:
       - `HIMS_LOGIN_V2_PRIVATE_KEY_PKCS8_BASE64` (preferred, unencrypted PKCS#8 DER encoded as Base64), or
       - `HIMS_LOGIN_V2_PRIVATE_KEY_PEM`, or
       - `HIMS_LOGIN_V2_PRIVATE_KEY_PATH` (the IIS application-pool identity must have read access).
     - The RSA key must be at least 2048 bits. The key `kid` must match an active
       signing/validation key and remain identical on every API instance.
     - Optional multi-key rotation uses `LoginV2:Keys[]` + `LoginV2:CurrentKid`.
   - **Rate limit and transport switches**
     - `LoginSecurity:RequestsPerMinute` (default `60`)
     - `LoginSecurity:AllowHttpDevelopment` must remain `false` in production.
   - For IIS, provision these as **system-level environment variables** (or an
     approved deployment secret store), recycle the application pool, and do not
     store private keys in the checked-in JSON files. User-level variables are
     not reliably visible to the IIS worker process.

3. **Infrastructure/proxy requirements**
   - Login endpoints now enforce HTTPS and return `426 Upgrade Required` on HTTP.
   - If TLS terminates at a proxy/load balancer, pass trusted
     `X-Forwarded-Proto`/`X-Forwarded-For` headers so HTTPS is detected correctly.

4. **Rollout sequencing**
   - Run SQL script first.
   - Deploy API update to all instances.
   - Restart ScheduleJobs with the same encryption key setup.
   - Have users sign out/sign in to refresh tokens under new signing/validation.

5. **Post-deployment verification**
   - Open `/swagger/index.html` in a browser to confirm the API starts.
   - `GET /api/Login/Challenge?loginType=Web` should return `200` with
     `kid`, `publicKey`, `challengeId`, `nonce`, `context`, and captcha image.
   - `POST /api/Login/AuthenticateV2` should reject reused/expired challenge IDs.
   - Repeated login bursts should eventually return `429 Too Many Requests`.
   - If IIS returns `500.30` or `500.31`, verify the .NET 6 Hosting Bundle/runtime
     first, then run Event Viewer > Windows Logs > Application and inspect
     ASP.NET Core Module/`IIS AspNetCore Module V2` entries.
   - For the current login update, a startup error containing `Login V2 is not
     configured` or `Program.cs:line 157` means the RSA key environment variables
     were not visible to the IIS application-pool process. Set them at system
     scope, then recycle the pool.
   - If logs do not show the exception, temporarily set `stdoutLogEnabled="true"`
     in the deployed `web.config`, ensure the `logs\stdout` directory is writable
     by the pool identity, reproduce once, then disable stdout logging again.
   - Run the regression utility:

```powershell
dotnet run --project "Tests\HIMS.LoginV2Regression\HIMS.LoginV2Regression.csproj"
```

It validates V2 envelope encryption/decryption, AAD/tamper rejection, loginType
JSON contract, and password-hash behavior.
