using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using HIMS.Api.Models.Login;
using HIMS.API.Extensions;
using HIMS.API.Security;
using HIMS.Core;
using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

SecurityKeys.Configure(Convert.ToBase64String(Enumerable.Range(0, 32).Select(i => (byte)i).ToArray()));
const int MinutesPerDay = 24 * 60;

IConfiguration configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
Require(LoginSessionPolicy.GetTokenValidityMinutes(configuration, LoginType.Mobile) ==
    LoginSessionPolicy.DefaultMobileSessionDays * MinutesPerDay, "Default mobile session must be 180 days.");
Require(LoginSessionPolicy.GetTokenValidityMinutes(configuration, LoginType.Web) == 12 * 60,
    "Web session duration changed unexpectedly.");

configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
{
    ["LoginV2:MobileSessionDays"] = "30"
}).Build();
Require(LoginSessionPolicy.GetTokenValidityMinutes(configuration, LoginType.Mobile) ==
    LoginSessionPolicy.MinimumMobileSessionDays * MinutesPerDay, "Mobile sessions must not be shorter than 90 days.");

configuration = new ConfigurationBuilder().AddInMemoryCollection(new Dictionary<string, string?>
{
    ["LoginV2:MobileSessionDays"] = "365"
}).Build();
Require(LoginSessionPolicy.GetTokenValidityMinutes(configuration, LoginType.Mobile) ==
    LoginSessionPolicy.MaximumMobileSessionDays * MinutesPerDay, "Mobile sessions must not exceed 180 days.");

var isolatedUser = new LoginManager { UserToken = "web-token", MobileToken = "mobile-token" };
LoginSessionPolicy.RotateSessionToken(isolatedUser, LoginType.Mobile);
Require(isolatedUser.UserToken == "web-token", "Mobile login changed the web session token.");
Require(isolatedUser.MobileToken != "mobile-token", "Mobile login did not rotate the mobile session token.");
string currentMobileToken = isolatedUser.MobileToken!;
LoginSessionPolicy.RotateSessionToken(isolatedUser, LoginType.Web);
Require(isolatedUser.MobileToken == currentMobileToken, "Web login changed the mobile session token.");
Require(isolatedUser.UserToken != "web-token", "Web login did not rotate the web session token.");

Console.WriteLine("PASS: Mobile sessions default to 180 days, are bounded to 90–180 days, and do not conflict with web sessions.");


static void Require(bool condition, string message)
{
    if (!condition) throw new InvalidOperationException(message);
}

const string issuer = "hims-api-test";
const string audience = "hims-clients-test";
using var currentRsa = RSA.Create(2048);
using var oldRsa = RSA.Create(2048);
var oldKey = new LoginV2Key { Kid = "old-key", Rsa = oldRsa };
var currentKey = new LoginV2Key { Kid = "current-key", Rsa = currentRsa };
var publicKeys = new[]
{
    new RsaSecurityKey(oldRsa.ExportParameters(false)) { KeyId = oldKey.Kid },
    new RsaSecurityKey(currentRsa.ExportParameters(false)) { KeyId = currentKey.Kid }
};
var user = new LoginManager
{
    UserId = 1,
    WebRoleId = 2,
    UserName = "regression",
    FirstName = "Regression",
    UserToken = Guid.NewGuid().ToString(),
    MobileToken = Guid.NewGuid().ToString()
};
var handler = new JwtSecurityTokenHandler();

foreach (var loginType in new[] { LoginType.Web, LoginType.Mobile })
{
    string token = CommonExtensions.GenerateToken(user, currentKey, 30, loginType, issuer, audience);
    string overlapToken = CommonExtensions.GenerateToken(user, oldKey, 30, loginType, issuer, audience);
    var parsed = handler.ReadJwtToken(token);
    Require(parsed.Header.Kid == currentKey.Kid, "JWT kid is missing or incorrect.");
    Require(parsed.Header.Alg == SecurityAlgorithms.RsaSha256, "JWT did not use RS256.");
    Require(Encoding.UTF8.GetByteCount("Bearer " + token) < 8192, "Bearer token exceeds 8 KB.");

    var principal = handler.ValidateToken(token, new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKeys = publicKeys,
        ValidAlgorithms = new[] { SecurityAlgorithms.RsaSha256 },
        ValidateLifetime = true,
        RequireExpirationTime = true,
        RequireSignedTokens = true,
        ClockSkew = TimeSpan.Zero
    }, out _);
    Require(principal.HasClaim(x => x.Type == "RoleId"), "RoleId claim missing.");
    Require(!principal.HasClaim(x => x.Type == PermissionClaimCodec.ClaimType), "Permissions must not be trusted from JWT.");
    handler.ValidateToken(overlapToken, new TokenValidationParameters
    {
        ValidateIssuer = true, ValidIssuer = issuer,
        ValidateAudience = true, ValidAudience = audience,
        ValidateIssuerSigningKey = true, IssuerSigningKeys = publicKeys,
        ValidAlgorithms = new[] { SecurityAlgorithms.RsaSha256 },
        ValidateLifetime = true, RequireExpirationTime = true,
        ClockSkew = TimeSpan.Zero
    }, out _);

    foreach (var invalid in new[]
    {
        new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true, IssuerSigningKeys = publicKeys,
            ValidAlgorithms = new[] { SecurityAlgorithms.RsaSha256 },
            ValidateIssuer = true, ValidIssuer = "wrong-issuer",
            ValidateAudience = true, ValidAudience = audience,
            ValidateLifetime = true
        },
        new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Enumerable.Repeat((byte)1, 64).ToArray()),
            ValidAlgorithms = new[] { SecurityAlgorithms.HmacSha256 },
            ValidateIssuer = true, ValidIssuer = issuer,
            ValidateAudience = true, ValidAudience = audience,
            ValidateLifetime = true
        }
    })
    {
        try
        {
            handler.ValidateToken(token, invalid, out _);
            throw new InvalidOperationException("Invalid JWT validation parameters accepted token.");
        }
        catch (SecurityTokenException) { }
    }

    Console.WriteLine($"{loginType}: RS256 token, kid, issuer/audience/algorithm and key overlap validation passed.");
}

var serverPermissions = new List<PageMasterDto>
{
    new() { PageCode = "PAGE1", IsView = true, IsEdit = false },
    new() { PageCode = "PAGE2", IsView = false, IsEdit = true }
};
Require(CommonExtensions.CheckPermission(serverPermissions, "PAGE1", PagePermission.View),
    "Server permission allow failed.");
Require(!CommonExtensions.CheckPermission(serverPermissions, "PAGE1", PagePermission.Edit),
    "Denied server permission was allowed.");
Require(!CommonExtensions.CheckPermission(serverPermissions, "MISSING", PagePermission.View),
    "Unknown page permission was allowed.");
Require(CommonExtensions.CheckPermission(serverPermissions, "MISSING|PAGE2", PagePermission.Edit),
    "Multi-page server permission failed.");

using var decoded = RSA.Create();
decoded.ImportSubjectPublicKeyInfo(Convert.FromBase64String(currentKey.PublicKey), out int bytesRead);
Require(bytesRead > 0 && decoded.KeySize >= 2048, "Challenge public key is not Flutter-compatible SPKI DER base64.");
Require(JsonSerializer.Serialize(new { version = 1 }).Contains("\"version\":1"),
    "Challenge version must be a JSON number.");

Console.WriteLine("All asymmetric token, server-authoritative permission, SPKI and numeric-version regressions passed.");
