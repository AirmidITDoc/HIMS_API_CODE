using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using HIMS.Api.Models.Login;
using HIMS.API.Extensions;
using HIMS.Core;
using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;

static void Require(bool condition, string message)
{
    if (!condition)
        throw new InvalidOperationException(message);
}

foreach (var value in new[] { "", "PAGE|1|0|1|0|1,", "Unicode-\u0915|0|1|0|1|0," })
    Require(PermissionClaimCodec.Decode(PermissionClaimCodec.Encode(value)) == value, "Permission round trip failed.");

var permissions = string.Concat(Enumerable.Range(0, 500)
    .Select(i => $"PAGE{i:D4}|{i % 2}|{(i / 2) % 2}|{(i / 4) % 2}|{(i / 8) % 2}|{(i / 16) % 2},"));
var user = new LoginManager
{
    UserId = 1, WebRoleId = 2, UserName = "regression", FirstName = "Regression",
    UserToken = Guid.NewGuid().ToString(), MobileToken = Guid.NewGuid().ToString()
};
var secret = new string('x', 64);
var handler = new JwtSecurityTokenHandler();
var accessor = new HttpContextAccessor { HttpContext = new DefaultHttpContext() };

foreach (var loginType in new[] { LoginType.Web, LoginType.Mobile })
{
    var token = CommonExtensions.GenerateToken(user, secret, 30, permissions, loginType);
    Require(Encoding.UTF8.GetByteCount("Bearer " + token) < 8192, "Bearer token exceeds the 8 KB regression budget.");
    var principal = handler.ValidateToken(token, new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)),
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    }, out _);
    var claim = principal.FindFirst(PermissionClaimCodec.ClaimType);
    Require(claim != null, "Compressed permission claim missing.");
    Require(PermissionClaimCodec.Decode(EncryptionUtility.DecryptText(claim!.Value, SecurityKeys.EnDeKey)) == permissions,
        "Token permissions were changed.");

    foreach (var legacy in new[] { false, true })
    {
        accessor.HttpContext.User = legacy
            ? new ClaimsPrincipal(new ClaimsIdentity(new[]
            {
                new Claim("RoleId", "2"),
                new Claim("Permissions", EncryptionUtility.EncryptText(permissions, SecurityKeys.EnDeKey))
            }, "test"))
            : principal;
        for (var i = 0; i < 500; i++)
        {
            foreach (var permission in Enum.GetValues<PagePermission>())
            {
                var expected = (i / (1 << ((int)permission - 1))) % 2 == 1;
                Require(CommonExtensions.CheckPermission($"PAGE{i:D4}", permission) == expected,
                    $"Permission mismatch: legacy={legacy}, page={i}, permission={permission}");
            }
        }
        Require(!CommonExtensions.CheckPermission("MISSING", PagePermission.View), "Unknown page was allowed.");
        Require(CommonExtensions.CheckPermission("MISSING|PAGE0008", PagePermission.View), "Multi-page matching failed.");
    }
    Console.WriteLine($"{loginType}: signed token {Encoding.UTF8.GetByteCount(token)} bytes; legacy and compressed authorization checks passed.");
}
accessor.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity());
Require(!CommonExtensions.CheckPermission("PAGE0008", PagePermission.View), "Missing permission claim was allowed.");
Console.WriteLine("All token regression checks passed.");
