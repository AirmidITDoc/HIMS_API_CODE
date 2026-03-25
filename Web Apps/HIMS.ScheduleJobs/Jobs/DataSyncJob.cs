using HIMS.ScheduleJobs.Data;
using HIMS.ScheduleJobs.Logic;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.ScheduleJobs.Jobs
{
    public class DataSyncJob : IJob
    {
        private readonly ILogger<DataSyncJob> _logger;

        public DataSyncJob(ILogger<DataSyncJob> logger)
        {
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Common.WriteToFile("Service is running at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                await DataSyncLogic.RunDataSync();
            }
            catch (Exception ex)
            {
                Common.WriteToFile("Service errors details : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "error : " + ex.Message);
            }
        }
    }
}
