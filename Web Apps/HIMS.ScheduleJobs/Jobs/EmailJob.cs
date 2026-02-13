using HIMS.ScheduleJobs.Data;
using HIMS.ScheduleJobs.Logic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace HIMS.ScheduleJobs.Jobs
{
    public class EmailJob : IJob
    {
        private readonly ILogger<EmailJob> _logger;
        public EmailJob(ILogger<EmailJob> logger)
        {
            _logger = logger;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Common.WriteToFile("Email Service is running at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                await EmailLogic.SendEmail();
            }
            catch (Exception ex)
            {
                Common.WriteToFile("Email Service errors details : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "error : " + ex.Message);
            }
        }
    }
}
