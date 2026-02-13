using HIMS.ScheduleJobs.Data;
using HIMS.ScheduleJobs.Logic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace HIMS.ScheduleJobs.Jobs
{
    public class WhatsAppJob : IJob
    {
        private readonly ILogger<WhatsAppJob> _logger;

        public WhatsAppJob(ILogger<WhatsAppJob> logger)
        {
            _logger = logger;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Common.WriteToFile("WhatsApp Service is running at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                await WhatsappLogic.SendWhatsAppSms();
            }
            catch (Exception ex)
            {
                Common.WriteToFile("WhatsApp Service errors details : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "error : " + ex.Message);
            }
        }
    }
}
