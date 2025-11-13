using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Report;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Data;
using WkHtmlToPdfDotNet;

namespace HIMS.Services.Administration
{
    public class WhatsAppEmailService : IWhatsAppEmailService
    {
        private readonly HIMSDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public readonly IPdfUtility _pdfUtility;
        public readonly ReportService _reportServices;    
        public WhatsAppEmailService(HIMSDbContext HIMSDbContext, IHostingEnvironment hostingEnvironment, IPdfUtility pdfUtility,IReportService reportServices)
        {
            _context = HIMSDbContext;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
            //_reportServices = reportServices;
        }

        public virtual async Task InsertAsync(TWhatsAppSmsOutgoing ObjWhatsApp,  int UserId, string Username) //ReportRequestModel model,
        {
            var tuple = new Tuple<byte[], string>(null, string.Empty);
            string vDate = DateTime.Now.ToString("_dd_MM_yyyy_hh_mm_tt");
            try
            {

                //if (ObjWhatsApp.Smstype == "OPBill")
                //{
                //    string[] colList = { };
                //    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OpBillingReceipt.html");
                //    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                //    htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                //    var html = _reportService.GetHTMLView("ps_rptBillPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                //    html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);
                //    tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OpBillReceipt", "OPBillReceipt" + vDate, Orientation.Portrait);
                //    ObjWhatsApp.FilePath = tuple.Item2;
                //}

                DatabaseHelper odal = new();
                string[] rEntity = { "MobileNumber","SourceType","Smsstring", "IsSent", "Smstype", "Smsflag", "Smsdate", "TranNo", "TemplateId", "Smsurl", "FilePath", "CreatedBy","SmsoutGoingId" };

                var lentity = ObjWhatsApp.ToDictionary();
                foreach (var rProperty in lentity.Keys.ToList())
                {
                    if (!rEntity.Contains(rProperty))
                        lentity.Remove(rProperty);
                }
                string vSmsoutGoingId = odal.ExecuteNonQuery("ps_insert_WhatsAppSMS", CommandType.StoredProcedure, "SmsoutGoingId", lentity);
                ObjWhatsApp.SmsoutGoingId = Convert.ToInt32(vSmsoutGoingId);

            }
            catch (Exception ex)
            {
                TWhatsAppSmsOutgoing? objWhatsAppSMS = await _context.TWhatsAppSmsOutgoings.FindAsync(ObjWhatsApp.SmsoutGoingId);
                _context.TWhatsAppSmsOutgoings.Remove(objWhatsAppSMS);
                await _context.SaveChangesAsync();
            }
        }
    }
}
