using Quartz;

namespace HIMS.ScheduleJobs
{
    public static class QuartzExtensions
    {
        public static void AddJobWithCron<T>(this IServiceCollectionQuartzConfigurator q, string jobName, string cron) where T : IJob
        {
            var key = new JobKey(jobName);

            q.AddJob<T>(opts => opts.WithIdentity(key));

            q.AddTrigger(opts => opts
                .ForJob(key)
                .WithIdentity($"{jobName}-trigger")
                .WithCronSchedule(cron));
        }
    }
}
