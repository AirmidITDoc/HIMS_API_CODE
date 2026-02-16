using HIMS.Core.Domain.Common;
using HIMS.Core.Infrastructure;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.ScheduleJobs.Jobs;
using HIMS.ScheduleJobs.Models;
using HIMS.Services.Administration;
using HIMS.Services.Masters;
using HIMS.Services.Report;
using HIMS.Services.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using System;
using WkHtmlToPdfDotNet;
using WkHtmlToPdfDotNet.Contracts;

namespace HIMS.ScheduleJobs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)

            // 👉 Run as Windows Service
            .UseWindowsService()

            // 👉 Load config from service folder
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                config.SetBasePath(System.IO.Path.Combine(AppContext.BaseDirectory));
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            })

            .ConfigureServices((hostContext, services) =>
            {
                IConfiguration configuration = hostContext.Configuration;

                // =========================
                // EXISTING SERVICES (KEEP AS IT IS FOR NOW)
                // =========================
                // ✅ ADD THIS - DB CONTEXT REGISTRATION
                // services.AddDbContext<HIMSDbContext>(options => options.UseSqlServer(configuration.GetValue<string>("CONNECTION_STRING")));
                AppSettings.Initialize(configuration);
                services.AddDbContextPool<HIMSDbContext>((provider, options) =>
                {
                    options.UseSqlServer(AppSettings.Settings.CONNECTION_STRING);
                });
                ConnectionStrings.SetConnectionString(AppSettings.Settings.CONNECTION_STRING);
                // Bind settings
                var quartzSettings = new QuartzSettings();
                configuration.GetSection("QuartzSettings").Bind(quartzSettings);

                services.AddSingleton(typeof(IConverter), new SynchronizedConverter(new PdfTools()));

                services.AddSingleton(quartzSettings);
                services.AddScoped<IContext, HIMSDbContext>();
                services.AddScoped<IPdfUtility, PdfUtility>();
                services.AddScoped<IReportService, ReportService>();
                services.AddScoped<IWhatsAppEmailService, WhatsAppEmailService>();
                services.AddScoped<IBarcodeConfigService, BarcodeConfigService>();
                services.AddScoped<IEmailTemplateService, EmailTemplateService>();

                services.AddQuartz(q =>
                {
                    q.UseMicrosoftDependencyInjectionJobFactory();

                    q.AddJobWithCron<SmsJob>("SmsJob", quartzSettings.Jobs["SmsJob"]);
                    q.AddJobWithCron<EmailJob>("EmailJob", quartzSettings.Jobs["EmailJob"]);
                    q.AddJobWithCron<WhatsAppJob>("WhatsAppJob", quartzSettings.Jobs["WhatsAppJob"]);
                    q.AddJobWithCron<PdfJob>("PdfJob", quartzSettings.Jobs["PdfJob"]);
                });

                // Quartz background hosted service
                services.AddQuartzHostedService(options =>
                {
                    options.WaitForJobsToComplete = true;
                });
            });
    }
}
