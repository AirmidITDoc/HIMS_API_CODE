using HIMS.ScheduleJobs.Jobs;
using HIMS.ScheduleJobs.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using System;

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


                // Bind settings
                var quartzSettings = new QuartzSettings();
                configuration.GetSection("QuartzSettings").Bind(quartzSettings);

                services.AddSingleton(quartzSettings);

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
