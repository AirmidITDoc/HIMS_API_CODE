using HIMS.Core.Domain.Grid;
using HIMS.ScheduleJobs.Data;
using HIMS.ScheduleJobs.Logic;
using HIMS.Services.Report;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace HIMS.ScheduleJobs.Jobs
{
    public class PdfJob : IJob
    {
        private readonly IReportService _reportService;
        private readonly IConfiguration _configuration;
        public PdfJob(IReportService reportService, IConfiguration configuration)
        {
            _reportService = reportService;
            _configuration = configuration;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Common.WriteToFile("WhatsApp Service is running at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                string html = _reportService.GetNewReportSetByProc(new ReportConfigDto() { Mode = "DailyCollectionSummary" }, _configuration["ApiWwwRootFolder"]);
            }
            catch (Exception ex)
            {
                Common.WriteToFile("WhatsApp Service errors details : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "error : " + ex.Message);
            }
        }
    }
}
