using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Report;
using HIMS.Services.Utilities;
using LinqToDB;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
        public readonly IReportService _reportServices;
        public WhatsAppEmailService(HIMSDbContext HIMSDbContext, IHostingEnvironment hostingEnvironment, IPdfUtility pdfUtility, IReportService reportServices)
        {
            _context = HIMSDbContext;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
            _reportServices = reportServices;
        }

        public virtual async Task InsertAsync(TWhatsAppSmsOutgoing ObjWhatsApp, IConfiguration _configuration, int Id, int UserId, string Username) //ReportRequestModel model,
        {
            var tuple = new Tuple<byte[], string>(null, string.Empty);
            string vDate = DateTime.Now.ToString("_dd_MM_yyyy_hh_mm_tt");
            try
            {
                string FilePath = "";
                if (ObjWhatsApp.Smstype == "OPBill")
                {
                    ReportRequestModel model = new()
                    {
                        Mode = "OpBillReceipt",
                        SearchFields = new()
                    {
                        new() { FieldName = "BillNo", FieldValue = Id.ToString(), OpType = OperatorComparer.Equals }
                    },
                        BaseUrl = Convert.ToString(_configuration["BaseUrl"]),
                        StorageBaseUrl = Convert.ToString(_configuration["StorageBaseUrl"])
                    };
                    var byteFile = await _reportServices.GetReportSetByProc(model, _configuration["PdfFontPath"]);
                    FilePath = _pdfUtility.GeneratePasswordProtectedPdf(byteFile.Item1, "Test123", model.StorageBaseUrl, "Bill", "Bill_" + Id);
                }

                DatabaseHelper odal = new();
                string[] rEntity = { "MobileNumber", "SourceType", "Smsstring", "IsSent", "Smstype", "Smsflag", "Smsdate", "TranNo", "TemplateId", "Smsurl", "CreatedBy", "SmsoutGoingId" };
                ObjWhatsApp.FilePath = FilePath;
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
