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
using HIMS.Services.Pharmacy;
using HIMS.Services.Common;

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
            services.AddScoped<IContext, HIMSDbContext>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ISalesService, SalesService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IGRNService, GRNService>();
            services.AddHttpContextAccessor();
            services.AddMemoryCache();
        }
    }
}

