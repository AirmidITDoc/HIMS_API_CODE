using Microsoft.Extensions.DependencyInjection;
using HIMS.API.Extensions;
using HIMS.API.Extensions;
using HIMS.Data;
using HIMS.Services;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Users;
using System.Security;
using HIMS.Services.Permissions;

namespace HIMS.API.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public static class DependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        public static void Register(IServiceCollection services)
        {
            // services.AddScoped<IDataProvider, MsSqlDataProvider>();
            //services.AddScoped(typeof(IRepository<>), typeof(EntityRepository<>));
            services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));
            services.AddScoped<IContext, SSDB_JSS_WEB_25SepContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
        }
    }
}

