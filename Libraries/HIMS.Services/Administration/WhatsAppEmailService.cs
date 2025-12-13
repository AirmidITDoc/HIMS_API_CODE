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
using System.Text.RegularExpressions;
using WkHtmlToPdfDotNet;

namespace HIMS.Services.Administration
{
    public class WhatsAppEmailService : IWhatsAppEmailService
    {
        private readonly HIMSDbContext _context;
        public readonly IPdfUtility _pdfUtility;
        public readonly IReportService _reportServices;
        private static readonly string[] AllowedFields =
       {
            "MobileNumber", "SourceType", "Smsstring", "IsSent", "Smstype","FilePath",
            "Smsflag", "Smsdate", "TranNo", "TemplateId", "Smsurl",
            "CreatedBy", "SmsoutGoingId","PatientId"
        };
        private static readonly string[] EmailAllowedFields =
      {
            "FromEmail", "FromName", "ToEmail", "Cc", "Bcc","MailSubject",
            "MailBody", "AttachmentName", "AttachmentLink","TranNo",
            "EmailType", "Id", "CreatedBy","PatientId"
        };
        public WhatsAppEmailService(HIMSDbContext HIMSDbContext, IPdfUtility pdfUtility, IReportService reportServices)
        {
            _context = HIMSDbContext;
            _pdfUtility = pdfUtility;
            _reportServices = reportServices;
        }

        public virtual async Task InsertAsync(TWhatsAppSmsOutgoing ObjWhatsApp, IConfiguration _configuration, long Id, int UserId, string Username) //ReportRequestModel model,
        {
            var tuple = new Tuple<byte[], string>(null, string.Empty);
            string vDate = DateTime.Now.ToString("_ddMMyyyy_hhmmtt");

            var reg = await _context.Registrations.Where(r => r.RegNo == ObjWhatsApp.PatientId.ToString()).Select(r => new { FirstName = r.FirstName, LastName = r.LastName }).FirstOrDefaultAsync();
            string firstName = Regex.Replace(reg?.FirstName ?? "", @"\s+", " ").Trim();
            // Merge first + last name
            string fullName = $"{reg?.FirstName}_{reg?.LastName}".Trim();
            // Take first 4 characters safely
            string first4 = (firstName.Length >= 4 ? firstName.Substring(0, 4) : firstName).ToUpper();
            // Current year (4-digit)
            string currentYear = DateTime.Now.Year.ToString();
            string UserPassword = first4 + currentYear;

            // Need to pass Pdf Generation type
            var mType = await _context.SmspdfConfigs.Where(r => r.Type == ObjWhatsApp.Smstype).Select(r => new { mBillType = r.Type, mPdfModeName = r.PdfModeName, mFieldName = r.FieldName }).FirstOrDefaultAsync();

            try
            {
                string FilePath = "";
                if (ObjWhatsApp.Smstype == mType.mBillType)
                {
                    ReportRequestModel model = new()
                    {
                        Mode = mType.mPdfModeName,
                        SearchFields = new()
                    {
                        new() { FieldName = mType.mFieldName, FieldValue = Id.ToString(), OpType = OperatorComparer.Equals }
                    },
                        BaseUrl = Convert.ToString(_configuration["BaseUrl"]),
                        StorageBaseUrl = Convert.ToString(_configuration["StorageBaseUrl"])
                    };
                    var byteFile = await _reportServices.GetReportSetByProc(model, _configuration["PdfFontPath"]);
                    FilePath = _pdfUtility.GeneratePasswordProtectedPdf(byteFile.Item1, UserPassword, model.StorageBaseUrl, model.Mode, "Bill_" + fullName + "_"+Id+vDate);
                }
                ObjWhatsApp.FilePath = FilePath;
                var entityData = ObjWhatsApp.ToDictionary().Where(kvp => AllowedFields.Contains(kvp.Key)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                using var dbHelper = new DatabaseHelper();
                var resultId = dbHelper.ExecuteNonQuery("ps_insert_WhatsAppSMS", CommandType.StoredProcedure, "SmsoutGoingId", entityData);

                ObjWhatsApp.SmsoutGoingId = Convert.ToInt32(resultId);

            }
            catch (Exception ex)
            {
                TWhatsAppSmsOutgoing? objWhatsAppSMS = await _context.TWhatsAppSmsOutgoings.FindAsync(ObjWhatsApp.SmsoutGoingId);
                _context.TWhatsAppSmsOutgoings.Remove(objWhatsAppSMS);
                await _context.SaveChangesAsync();
            }
        }

        public virtual async Task InsertEmailAsync(TMailOutgoing ObjEmail, IConfiguration _configuration, long Id, int UserId, string Username) //ReportRequestModel model,
        {
            var tuple = new Tuple<byte[], string>(null, string.Empty);
            string vDate = DateTime.Now.ToString("_ddMMyyyy_hhmmtt");

            var reg = await _context.Registrations.Where(r => r.RegNo == ObjEmail.PatientId.ToString()).Select(r => new { FirstName = r.FirstName , LastName = r.LastName}).FirstOrDefaultAsync();
            string firstName = Regex.Replace(reg?.FirstName ?? "", @"\s+", " ").Trim();
            // Merge first + last name
            string fullName = $"{reg?.FirstName}_{reg?.LastName}".Trim();
            // Take first 4 characters safely
            string first4 = (firstName.Length >= 4 ? firstName.Substring(0, 4) : firstName).ToUpper();
            // Current year (4-digit)
            string currentYear = DateTime.Now.Year.ToString();
            string UserPassword = first4 + currentYear;
            
            // Need to pass Pdf Generation type
            var mType = await _context.SmspdfConfigs.Where(r => r.Type == ObjEmail.EmailType).Select(r=> new { mBillType=r.Type, mPdfModeName =r.PdfModeName , mFieldName =r.FieldName }).FirstOrDefaultAsync();
            
            try
            {
                string FilePath = "";
                if (ObjEmail.EmailType == mType.mBillType)
                {
                    ReportRequestModel model = new()
                    {
                        Mode = mType.mPdfModeName,
                        SearchFields = new()
                    {
                        new() { FieldName = mType.mFieldName, FieldValue = Id.ToString(), OpType = OperatorComparer.Equals }
                    },
                        BaseUrl = Convert.ToString(_configuration["BaseUrl"]),
                        StorageBaseUrl = Convert.ToString(_configuration["StorageBaseUrl"])
                    };
                    var byteFile = await _reportServices.GetReportSetByProc(model, _configuration["PdfFontPath"]);
                    FilePath = _pdfUtility.GeneratePasswordProtectedPdf(byteFile.Item1, UserPassword, model.StorageBaseUrl, model.Mode, "Bill_" + fullName + "_" + Id + vDate);
                }
                ObjEmail.AttachmentName = FilePath;
                var entityData = ObjEmail.ToDictionary().Where(kvp => EmailAllowedFields.Contains(kvp.Key)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                using var dbHelper = new DatabaseHelper();
                var resultId = dbHelper.ExecuteNonQuery("ps_insert_Mail_Outgoing_1", CommandType.StoredProcedure, "Id", entityData);

                ObjEmail.Id = Convert.ToInt32(resultId);

            }
            catch (Exception ex)
            {
                TMailOutgoing? objWhatsAppSMS = await _context.TMailOutgoings.FindAsync(ObjEmail.Id);
                _context.TMailOutgoings.Remove(objWhatsAppSMS);
                await _context.SaveChangesAsync();
            }
        }
    }
}
