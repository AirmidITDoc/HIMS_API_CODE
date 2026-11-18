using HIMS.API.Extensions;
using HIMS.Core;
using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using HIMS.Services.Permissions;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Security.Claims;

static void Require(bool condition, string message)
{
    if (!condition) throw new InvalidOperationException(message);
}

static AuthorizationFilterContext FilterContext(bool authenticated, IUserService? users = null,
    IPermissionService? permissions = null)
{
    SecurityKeys.Configure(Convert.ToBase64String(new byte[32]));
    var services = new ServiceCollection();
    if (users != null) services.AddSingleton(users);
    if (permissions != null) services.AddSingleton(permissions);
    var http = new DefaultHttpContext
    {
        RequestServices = services.BuildServiceProvider()
    };
    if (authenticated)
    {
        var claims = new[]
        {
            new Claim("Id", EncryptionUtility.EncryptText("17", SecurityKeys.EnDeKey)),
            new Claim("UserToken", EncryptionUtility.EncryptText("session", SecurityKeys.EnDeKey)),
            new Claim("LoginType", EncryptionUtility.EncryptText("Web", SecurityKeys.EnDeKey))
        };
        http.User = new ClaimsPrincipal(new ClaimsIdentity(claims, "test"));
    }

    var action = new ActionContext(http, new RouteData(), new ActionDescriptor(), new ModelStateDictionary());
    return new AuthorizationFilterContext(action, new List<IFilterMetadata>());
}

var apiAssembly = typeof(PermissionAttribute).Assembly;
var actions = apiAssembly.GetTypes()
    .Where(type => typeof(ControllerBase).IsAssignableFrom(type) && !type.IsAbstract)
    .SelectMany(type => type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.DeclaredOnly)
        .Where(method => method.GetCustomAttributes().Any(attribute => attribute is HttpMethodAttribute)))
    .ToList();
Require(actions.Count > 500, "Controller action audit did not discover the API surface.");

foreach (var action in actions)
{
    var permission = action.GetCustomAttribute<PermissionAttribute>();
    if (permission == null) continue;
    Require(!string.IsNullOrWhiteSpace(permission.PageCode),
        $"Bare permission attribute on {action.DeclaringType?.Name}.{action.Name}");
    Require(Enum.IsDefined(typeof(PagePermission), permission.Permission) && (int)permission.Permission > 0,
        $"Missing operation on {action.DeclaringType?.Name}.{action.Name}");
}

var roleController = apiAssembly.GetType("HIMS.API.Controllers.Administration.RoleMasterController")!;
foreach (string methodName in new[] { "GetPermission", "PostPermission" })
{
    var method = roleController.GetMethod(methodName)!;
    Require(method.GetCustomAttribute<AllowAnonymousAttribute>() == null, $"Role permission {methodName} is public.");
    var permission = method.GetCustomAttribute<PermissionAttribute>();
    Require(permission?.PageCode == "RoleTemplateMaster", $"Role permission {methodName} lacks an administrative page code.");
}

var allowlist = actions.Where(action => action.GetCustomAttribute<AllowAnonymousAttribute>() != null)
    .Select(action => $"{action.DeclaringType?.Name}.{action.Name}").OrderBy(name => name).ToList();
var expectedPublic = new HashSet<string>
{
    "LoginController.Authenticate",
    "LoginController.MobileAuthenticate",
    "LoginController.ConfirmAuthenticate",
    "LoginController.Challenge",
    "LoginController.AuthenticateV2",
    "LoginController.ConfirmAuthenticateV2",
    "LoginController.GetCaptcha",
    "LoginController.RefreshToken"
};
Require(expectedPublic.SetEquals(allowlist), $"Unexpected public allowlist: {string.Join(", ", allowlist)}");

var bareUser = new LoginManager { UserId = 17, IsActive = true, WebRoleId = 4 };
var bareContext = FilterContext(true, new FakeUserService(bareUser),
    new FakePermissionService(new List<PageMasterDto>()));
await new PermissionAttribute().OnAuthorizationAsync(bareContext);
Require(bareContext.Result != null && bareContext.HttpContext.Response.StatusCode == StatusCodes.Status403Forbidden,
    "Bare Permission did not fail closed.");

var user = new LoginManager
{
    UserId = 17, IsActive = true, StoreId = 2, UnitId = 8, WebRoleId = 4,
    TLoginStoreDetails = new List<TLoginStoreDetail> { new() { StoreId = 3 } },
    TLoginUnitDetails = new List<TLoginUnitDetail> { new() { UnitId = 9 } }
};
Require(!HIMS.API.Extensions.RequestScopeValidator.TryResolve(user, 99, 8, out _, out _),
    "Unassigned store was accepted.");
Require(!HIMS.API.Extensions.RequestScopeValidator.TryResolve(user, 2, 99, out _, out _),
    "Unassigned unit was accepted.");
Require(HIMS.API.Extensions.RequestScopeValidator.TryResolve(user, 3, 9, out var approved, out _)
        && approved is { StoreId: 3, UnitId: 9 }, "Assigned scope was rejected.");
Require(HIMS.API.Extensions.RequestScopeValidator.TryResolve(user, 2, 8, out _, out _)
        && HIMS.API.Extensions.RequestScopeValidator.TryResolve(
            new LoginManager { StoreId = 2, UnitId = 8 }, null, null, out _, out _),
    "Unambiguous server scope fallback failed.");
Require(!ScopeInputValidator.TryValidate(
        new Dictionary<string, object?> { ["UnitId"] = 99 }, approved!, out _),
    "Action argument bypassed approved unit scope.");

var sessionUser = new FakeUserService(user);
var deniedPermission = new FakePermissionService(new List<PageMasterDto>());
var unknownContext = FilterContext(true, sessionUser, deniedPermission);
await new PermissionAttribute { PageCode = "UnknownPage", Permission = PagePermission.View }
    .OnAuthorizationAsync(unknownContext);
Require(unknownContext.HttpContext.Response.StatusCode == StatusCodes.Status403Forbidden,
    "Unknown page mapping did not fail closed.");

var shared = actions.Where(action => action.GetCustomAttribute<SharedAuthenticatedAttribute>() != null)
    .Select(action => $"{action.DeclaringType?.Name}.{action.Name}")
    .OrderBy(name => name).ToList();
var expectedShared = new HashSet<string>
{
    "DashboardController.DailyDashboard",
    "LoginController.LogoutAsync",
    "NotificationController.List",
    "NotificationController.ReadNotification",
    "QzSigningController.SignMessage"
};
Require(expectedShared.SetEquals(shared),
    $"Unexpected shared-authenticated allowlist: {string.Join(", ", shared)}");
Require(!actions.Any(action => action.GetCustomAttributes()
        .OfType<AuthenticatedAttribute>()
        .Any(attribute => attribute.GetType() == typeof(AuthenticatedAttribute))),
    "Domain controller action retained the general Authenticated attribute.");
var unpermissionedDomainActions = actions
    .Where(action => action.GetCustomAttribute<PermissionAttribute>() == null &&
                     action.GetCustomAttribute<SharedAuthenticatedAttribute>() == null &&
                     action.GetCustomAttribute<AllowAnonymousAttribute>() == null)
    .Select(action => $"{action.DeclaringType?.Name}.{action.Name}")
    .ToList();
Require(unpermissionedDomainActions.Count == 0,
    $"Domain actions without page permission: {string.Join(", ", unpermissionedDomainActions)}");

Console.WriteLine($"PASS: audited {actions.Count} controller actions, public allowlist, administrative permissions, and request scopes.");

sealed class FakeUserService : IUserService
{
    private readonly LoginManager _user;
    public FakeUserService(LoginManager user) => _user = user;
    public Task<LoginManager> CheckLogin(string userName, string password) => Task.FromResult(_user);
    public Task UpdateAsync(LoginManager user, int userId, string username) => Task.CompletedTask;
    public bool CheckTokenIsValidAsync(int userId, string userToken, string loginType) => userId == 17;
    public Task<LoginManager> GetById(int id) => Task.FromResult(_user);
}

sealed class FakePermissionService : IPermissionService
{
    private readonly List<PageMasterDto> _permissions;
    public FakePermissionService(List<PageMasterDto> permissions) => _permissions = permissions;
    public Task<List<PageMasterDto>> GetAllModules(long roleId) => Task.FromResult(_permissions);
}
