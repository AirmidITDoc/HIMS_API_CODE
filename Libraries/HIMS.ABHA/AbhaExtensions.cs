using HIMS.ABHA.Helper;
using HIMS.ABHA.Interface;
using HIMS.ABHA.Services;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.ABHA
{
    public static class AbhaExtensions
    {
        /// <summary>
        /// Call this in Program.cs / Startup.cs of any project.
        /// Reads from the project's own appsettings.json automatically.
        /// </summary>
        public static IServiceCollection AddAbhaServices(this IServiceCollection services, int RequestTimeoutSeconds)
        {
            var retryPolicy = HttpPolicyExtensions.HandleTransientHttpError()
                .WaitAndRetryAsync(HIMS.ABHA.Configuration.AppSettings.Current.RetryCount, attempt => TimeSpan.FromSeconds(Math.Pow(2, attempt)));

            services.AddHttpClient<AbdmHttpClient>(c =>
            {
                c.Timeout = TimeSpan.FromSeconds(RequestTimeoutSeconds);
            }).AddPolicyHandler(retryPolicy);
            // Typed HttpClients
            services.AddHttpClient<IAbdmTokenService, AbdmTokenService>(c =>
            {
                c.Timeout = TimeSpan.FromSeconds(RequestTimeoutSeconds);
            }).AddPolicyHandler(retryPolicy);

            services.AddScoped<IAbhaService, AbhaService>();
            services.AddScoped<IAbdmAuthService, AbdmAuthService>();
            services.AddScoped<IHipLinkingService, HipLinkingService>();
            services.AddScoped<IUserLinkingService, UserLinkingService>();
            services.AddScoped<IDataTransferService, DataTransferService>();
            services.AddScoped<IAbdmAuthService, AbdmAuthService>();
            services.AddScoped<IHipLinkingService, HipLinkingService>();

            return services;
        }
    }
}
