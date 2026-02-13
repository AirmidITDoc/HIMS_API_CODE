using HIMS.Core.Domain.Common;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.ScheduleJobs.Data;
using HIMS.ScheduleJobs.Logic;
using HIMS.Services.Administration;
using HIMS.Services.Report;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HIMS.ScheduleJobs.Jobs
{
    public class PdfJob : IJob
    {
        private readonly IReportService _reportService;
        private readonly IWhatsAppEmailService _whatsAppEmailService;
        public PdfJob(IReportService reportService, IWhatsAppEmailService whatsAppEmailService)
        {
            _reportService = reportService;
            _whatsAppEmailService = whatsAppEmailService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Common.WriteToFile("Pdf Generate Service is running at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                var objReport = await _reportService.GetReportConfigByMode("DailyCollectionSummary", "COMMON Report");
                ReportConfigDto model = new();
                GridRequestModel objGrid = new() { Filters = new List<SearchGrid>() };
                objGrid.Filters.Add(new SearchGrid() { FieldName = "Id", FieldValue = objReport.ReportId.ToString() });
                objGrid.Filters.Add(new SearchGrid() { FieldName = "MenuId", FieldValue = "0" });
                objGrid.First = 0;
                objGrid.Rows = 0;
                IPagedList<MReportListDto> MReportConfigList = await _reportService.MReportListDto(objGrid);
                if (MReportConfigList.Count > 0)
                {
                    List<SearchGrid> searchFields = new()
                    {
                        new SearchGrid() { FieldName = "FromDate", FieldValue = "2026-02-01", OpType = OperatorComparer.Equals },
                        new SearchGrid() { FieldName = "ToDate", FieldValue = "2026-03-01", OpType = OperatorComparer.Equals },
                        new SearchGrid() { FieldName = "UserId", FieldValue = "0", OpType = OperatorComparer.Equals },
                        new SearchGrid() { FieldName = "DoctorId", FieldValue = "0", OpType = OperatorComparer.Equals }
                    };
                    model.colList = MReportConfigList[0].ReportColumn.Split(',');
                    model.SearchFields = searchFields;
                    model.Mode = MReportConfigList[0].ReportMode;
                    model.RepoertName = MReportConfigList[0].ReportName;
                    model.headerList = MReportConfigList[0].ReportHeader.Split(',');
                    model.totalFieldList = MReportConfigList[0].ReportTotalField.Split(',');
                    model.groupByLabel = MReportConfigList[0].ReportGroupByLabel;
                    model.summaryLabel = MReportConfigList[0].SummaryLabel;
                    model.columnWidths = MReportConfigList[0].ReportColumnWidth.Split(',');
                    model.htmlFilePath = MReportConfigList[0].ReportBodyFile;
                    model.htmlHeaderFilePath = MReportConfigList[0].ReportHeaderFile;
                    model.SPName = MReportConfigList[0].ReportSpname;
                    model.FolderName = MReportConfigList[0].ReportFolderName;
                    model.FileName = MReportConfigList[0].ReportFileName;
                    model.vPageOrientation = MReportConfigList[0].ReportPageOrientation;
                }
                model.StorageBaseUrl = AppSettings.Settings.StorageBaseUrl;
                var tuple = _reportService.GetNewReportSetByProc(model);
                TMailOutgoing mailModel = new()
                {
                    ToEmail = "elaunch.vimal@gmail.com",
                    MailSubject = model.RepoertName + " - " + DateTime.Now.ToString("dd/MM/yyyy"),
                    MailBody = "Please find the attached report.",
                    FromEmail = "support@airmidtechinnovations.com",
                    FromName = "Airmid Tech Innovations",
                    AttachmentName = "DailySummaryReport_For_" + DateTime.Now.ToString("dd_MM_yyyy") + ".pdf",
                    AttachmentLink = tuple.Item2,
                    TranNo = 123,
                    PatientId = 0,
                    EmailType = "DailyCollectionSummary",
                    EmailDate = DateTime.Now,
                    Status = -2,
                    Retry = 0,
                    LastTry = DateTime.Now,
                    LastResponse = "",
                    CreatedBy = 0,
                    CreatedOn = DateTime.Now,
                    Cc = "",
                    Bcc = ""
                };
                await _whatsAppEmailService.InsertMailVimal(mailModel);
            }
            catch (Exception ex)
            {
                Common.WriteToFile("Pdf Generate Service errors details : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "error : " + ex.Message);
            }
        }
    }
}
