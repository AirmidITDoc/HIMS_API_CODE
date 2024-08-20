using Microsoft.Extensions.DependencyInjection;
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
using HIMS.Services.Report;
using HIMS.Services.Report.OPReports;
using HIMS.Services.Utilities;
using WkHtmlToPdfDotNet.Contracts;
using WkHtmlToPdfDotNet;
using HIMS.Services.Masters;
using HIMS.Services.Dashboard;
using HIMS.Services.Inventory;
using HIMS.Services.OutPatient;

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
            services.AddScoped<IMenuService, MenuService>();
            services.AddScoped<IPermissionService, PermissionService>();
            services.AddScoped<ISalesService, SalesService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IGRNService, GRNService>();

            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IOPBillingReport, OPBillingReport>();
            services.AddTransient<IPdfUtility, PdfUtility>();
            services.AddScoped<IFavouriteService, FavouriteService>();

            services.AddScoped<IDashboardService, DashboardService>();
            services.AddScoped<IIndentService, IndentService>();

            services.AddScoped<IRegistrationService, RegistrationService>();
            services.AddScoped<IAppointmentService, AppointmentService>();

            services.AddScoped<IOPBillingService, OPBillingService>();

            services.AddHttpContextAccessor();
            services.AddMemoryCache();
        }
    }
}

