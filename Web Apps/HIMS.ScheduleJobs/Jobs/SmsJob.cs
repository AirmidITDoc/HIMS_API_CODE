using HIMS.ScheduleJobs.Data;
using HIMS.ScheduleJobs.Logic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace HIMS.ScheduleJobs.Jobs
{
    public class SmsJob : IJob
    {
        private readonly ILogger<SmsJob> _logger;

        public SmsJob(ILogger<SmsJob> logger)
        {
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Common.WriteToFile("Service is running at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                await SMSLogic.SendSms();
            }
            catch (Exception ex)
            {
                Common.WriteToFile("Service errors details : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "error : " + ex.Message);
            }
        }
    }
}
