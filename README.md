# HIMS_API_CODE
HIMS New Version API Code 

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
