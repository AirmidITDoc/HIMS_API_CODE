using HIMS.Core.Domain.Common;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.ScheduleJobs.Data;
using HIMS.ScheduleJobs.Logic;
using HIMS.Services.Administration;
using HIMS.Services.Masters;
using HIMS.Services.Report;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HIMS.ScheduleJobs.Jobs
{
    public class PdfJob : IJob
    {
        private readonly IReportService _reportService;
        private readonly IWhatsAppEmailService _whatsAppEmailService;
        private readonly IEmailTemplateService _emailTemplateService;
        public PdfJob(IReportService reportService, IWhatsAppEmailService whatsAppEmailService, IEmailTemplateService emailTemplateService)
        {
            _reportService = reportService;
            _whatsAppEmailService = whatsAppEmailService;
            _emailTemplateService = emailTemplateService;
        }
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                Common.WriteToFile("Pdf Generate Service is running at " + DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                List<SearchGrid> searchFields = new()
                    {
                        new SearchGrid() { FieldName = "FromDate", FieldValue =DateTime.Now.ToString("yyyy-MM-dd"), OpType = OperatorComparer.Equals },
                        new SearchGrid() { FieldName = "ToDate", FieldValue = DateTime.Now.ToString("yyyy-MM-dd"), OpType = OperatorComparer.Equals },
                        new SearchGrid() { FieldName = "UserId", FieldValue = "0", OpType = OperatorComparer.Equals },
                        new SearchGrid() { FieldName = "DoctorId", FieldValue = "0", OpType = OperatorComparer.Equals }
                    };
                List<Tuple<byte[], string, string>> Attachments = new()
                {
                    await GenerateReportAndSendMail("COMMON Report", "DailyCollectionSummary", searchFields),
                    await GenerateReportAndSendMail("Pharmacy Report", "SalesSummaryReport", searchFields),
                    await GenerateReportAndSendMail("Pharmacy Report", "PharmacyDailyCollectionSummaryDayandUserWise", searchFields)
                };
                await PrepareMail("DailyCollectionSummary", Attachments);
            }
            catch (Exception ex)
            {
                Common.WriteToFile("Pdf Generate Service errors details : " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + "error : " + ex.Message);
            }
        }
        public async Task<Tuple<byte[], string, string>> GenerateReportAndSendMail(string Section, string Mode, List<SearchGrid> searchFields)
        {
            var objReport = await _reportService.GetReportConfigByMode(Mode, Section);
            ReportConfigDto model = new();
            GridRequestModel objGrid = new() { Filters = new List<SearchGrid>() };
            objGrid.Filters.Add(new SearchGrid() { FieldName = "Id", FieldValue = objReport.ReportId.ToString() });
            objGrid.Filters.Add(new SearchGrid() { FieldName = "MenuId", FieldValue = "0" });
            objGrid.First = 0;
            objGrid.Rows = 0;
            IPagedList<MReportListDto> MReportConfigList = await _reportService.MReportListDto(objGrid);
            if (MReportConfigList.Count > 0)
            {
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
            return new(tuple.Item1, tuple.Item2, Mode);
        }
        public async Task PrepareMail(string EmailTemplateCode, List<Tuple<byte[], string, string>> Attachments)
        {
            EmailTemplateMaster objEmailTemplate = await _emailTemplateService.GetTemplateByCode(EmailTemplateCode);
            string AttachmentLink = string.Empty;
            string AttachmentName = string.Empty;
            foreach (var item in Attachments)
            {
                AttachmentLink += item.Item2 + ",";
                AttachmentName += $"{item.Item3}_For_{DateTime.Now:dd_MM_yyyy}.pdf,";
            }
            TMailOutgoing mailModel = new()
            {
                ToEmail = objEmailTemplate.ToMail,
                MailSubject = objEmailTemplate.MailSubject.Replace("{{Date}}", DateTime.Now.ToString("dd/MM/yyyy")),
                MailBody = objEmailTemplate.MailBody.Replace("{{Name}}", objEmailTemplate.FromName),
                FromEmail = objEmailTemplate.FromEmail,
                FromName = objEmailTemplate.FromName,
                AttachmentName = AttachmentName.Trim(','),
                AttachmentLink = AttachmentLink.Trim(','),
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
                Cc = objEmailTemplate.Cc,
                Bcc = objEmailTemplate.Bcc
            };
            await _whatsAppEmailService.InsertMailVimal(mailModel);
        }
    }
}
