using HIMS.Services.Utilities;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.Extensions;
using HIMS.Data.Infrastructure;
using HIMS.Data.Models;
using HIMS.Services.Common;
using HIMS.Services.Pharmacy;
using HIMS.Services.Report.OPReports;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WkHtmlToPdfDotNet;

namespace HIMS.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly Data.Models.HIMSDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly IOPBillingReport _OPbilling;
        public readonly IPdfUtility _pdfUtility;
        public ReportService(HIMSDbContext HIMSDbContext, IHostingEnvironment hostingEnvironment, IOPBillingReport OPbilling, IPdfUtility pdfUtility)
        {
            _context = HIMSDbContext;
            _hostingEnvironment = hostingEnvironment;
            _OPbilling = OPbilling;
            _pdfUtility = pdfUtility;
        }

        public string GetReportSetByProc(ReportRequestModel model)
        {
            CommonReportModel commonReportModel = new CommonReportModel();
            string byteFile = string.Empty;

            switch (model.mode)
            {
                #region :: RegistrationReport ::
                case "RegistrationReport":
                    {
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_RegistrationReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        List<SearchModel> fields = SearchFieldExtension.GetSearchFields(model.SearchFields);

                        commonReportModel.FromDate = Convert.ToDateTime(fields.Find(x => x.FieldName?.ToLower() == "fromdate".ToLower())?.FieldValueString);
                        commonReportModel.ToDate = Convert.ToDateTime(fields.Find(x => x.FieldName?.ToLower() == "todate".ToLower())?.FieldValueString);

                        var html = _OPbilling.ViewRegistrationReport(commonReportModel.FromDate, commonReportModel.ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath, model.baseUrl));
                        var tuple = _pdfUtility.GeneratePdfFromHtml(html, model.storageBaseUrl, "RegistrationReport", "RegistrationReport", Orientation.Portrait);
                        byteFile = Convert.ToBase64String(tuple.Item1);
                        break;
                    }
                #endregion

                #region :: AppointmentListReport ::
                case "AppointmentListReport":
                    {

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_AppoitnmentListReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        List<SearchModel> fields = SearchFieldExtension.GetSearchFields(model.SearchFields);

                        commonReportModel.FromDate = Convert.ToDateTime(fields.Find(x => x.FieldName?.ToLower() == "fromdate".ToLower())?.FieldValueString);
                        commonReportModel.ToDate = Convert.ToDateTime(fields.Find(x => x.FieldName?.ToLower() == "todate".ToLower())?.FieldValueString);

                        var html = _OPbilling.ViewOPAppointmentListReport(commonReportModel.FromDate, commonReportModel.ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath, model.baseUrl));
                        var tuple = _pdfUtility.GeneratePdfFromHtml(html, model.storageBaseUrl, "AppointmentListReport", "AppointmentListReport",  Orientation.Portrait);
                        byteFile = Convert.ToBase64String(tuple.Item1);
                        break;
                    }
                #endregion

                #region :: DoctorWiseVisitReport ::
                case "DoctorWiseVisitReport":
                    {

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DoctorWiseVisitReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        List<SearchModel> fields = SearchFieldExtension.GetSearchFields(model.SearchFields);

                        commonReportModel.FromDate = Convert.ToDateTime(fields.Find(x => x.FieldName?.ToLower() == "fromdate".ToLower())?.FieldValueString);
                        commonReportModel.ToDate = Convert.ToDateTime(fields.Find(x => x.FieldName?.ToLower() == "todate".ToLower())?.FieldValueString);

                        var html = _OPbilling.ViewDoctorWiseVisitReport(commonReportModel.FromDate, commonReportModel.ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath, model.baseUrl));
                        var tuple = _pdfUtility.GeneratePdfFromHtml(html, model.storageBaseUrl, "DoctorWiseVisitReport", "DoctorWiseVisitReport", Orientation.Portrait);
                        byteFile = Convert.ToBase64String(tuple.Item1);
                        break;
                    }
                #endregion

                #region :: RefDoctorWiseReport ::
                case "RefDoctorWiseReport":
                    {

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_RefDoctorWiseReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        List<SearchModel> fields = SearchFieldExtension.GetSearchFields(model.SearchFields);

                        commonReportModel.FromDate = Convert.ToDateTime(fields.Find(x => x.FieldName?.ToLower() == "fromdate".ToLower())?.FieldValueString);
                        commonReportModel.ToDate = Convert.ToDateTime(fields.Find(x => x.FieldName?.ToLower() == "todate".ToLower())?.FieldValueString);

                        var html = _OPbilling.ViewRefDoctorWiseReport(commonReportModel.FromDate, commonReportModel.ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath, model.baseUrl));
                        var tuple = _pdfUtility.GeneratePdfFromHtml(html, model.storageBaseUrl, "RefDoctorWiseReport", "RefDoctorWiseReport", Orientation.Portrait);
                        byteFile = Convert.ToBase64String(tuple.Item1);
                        break;
                    }
                #endregion

                #region :: DepartmentWiseCountSummury ::
                case "DepartmentWisecountSummury":
                    {
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DepartmentWiseCountSummury.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        List<SearchModel> fields = SearchFieldExtension.GetSearchFields(model.SearchFields);

                        commonReportModel.FromDate = Convert.ToDateTime(fields.Find(x => x.FieldName?.ToLower() == "fromdate".ToLower())?.FieldValueString);
                        commonReportModel.ToDate = Convert.ToDateTime(fields.Find(x => x.FieldName?.ToLower() == "todate".ToLower())?.FieldValueString);

                        var html = _OPbilling.ViewDepartmentWisecountSummury(commonReportModel.FromDate, commonReportModel.ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath, model.baseUrl));
                        var tuple = _pdfUtility.GeneratePdfFromHtml(html, model.storageBaseUrl, "DepartmentWiseCountSummury", "DepartmentWiseCountSummury", Orientation.Portrait);
                        byteFile = Convert.ToBase64String(tuple.Item1);
                        break;
                    }
                #endregion

                #region :: OPDoctorWiseVisitCountSummary ::
                case "OPDoctorWiseVisitCountSummary":
                    {
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OpDctorwisecountsummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        List<SearchModel> fields = SearchFieldExtension.GetSearchFields(model.SearchFields);

                        commonReportModel.FromDate = Convert.ToDateTime(fields.Find(x => x.FieldName?.ToLower() == "fromdate".ToLower())?.FieldValueString);
                        commonReportModel.ToDate = Convert.ToDateTime(fields.Find(x => x.FieldName?.ToLower() == "todate".ToLower())?.FieldValueString);

                        var html = _OPbilling.ViewOPDoctorWiseVisitCountSummary(commonReportModel.FromDate, commonReportModel.ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath, model.baseUrl));
                        var tuple = _pdfUtility.GeneratePdfFromHtml(html, model.storageBaseUrl, "OPDoctorWiseVisitCountSummary", "OPDoctorWiseVisitCountSummary", Orientation.Portrait);
                        byteFile = Convert.ToBase64String(tuple.Item1);
                        break;
                    }
                #endregion

                #region :: OPAppoinmentListWithServiseAvailed ::
                case "OPAppoinmentListWithServiseAvailed":
                    {
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_AppointmentListWithServiseAvailed.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        List<SearchModel> fields = SearchFieldExtension.GetSearchFields(model.SearchFields);

                        commonReportModel.FromDate = Convert.ToDateTime(fields.Find(x => x.FieldName?.ToLower() == "fromdate".ToLower())?.FieldValueString);
                        commonReportModel.ToDate = Convert.ToDateTime(fields.Find(x => x.FieldName?.ToLower() == "todate".ToLower())?.FieldValueString);

                        var html = _OPbilling.ViewOPAppoinmentListWithServiseAvailed(commonReportModel.FromDate, commonReportModel.ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath, model.baseUrl));
                        var tuple = _pdfUtility.GeneratePdfFromHtml(html, model.storageBaseUrl, "OPAppoinmentListWithServiseAvailed", "OPAppoinmentListWithServiseAvailed", Orientation.Portrait);
                        byteFile = Convert.ToBase64String(tuple.Item1);
                        break;
                    }
                #endregion

                #region :: CrossConsultationReport ::
                case "CrossConsultationReport":
                    {
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_CrossConsultationReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        List<SearchModel> fields = SearchFieldExtension.GetSearchFields(model.SearchFields);

                        commonReportModel.FromDate = Convert.ToDateTime(fields.Find(x => x.FieldName?.ToLower() == "fromdate".ToLower())?.FieldValueString);
                        commonReportModel.ToDate = Convert.ToDateTime(fields.Find(x => x.FieldName?.ToLower() == "todate".ToLower())?.FieldValueString);

                        var html = _OPbilling.ViewCrossConsultationReport(commonReportModel.FromDate, commonReportModel.ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath, model.baseUrl));
                        var tuple = _pdfUtility.GeneratePdfFromHtml(html, model.storageBaseUrl, "CrossConsultationReport", "CrossConsultationReport", Orientation.Portrait);
                        byteFile = Convert.ToBase64String(tuple.Item1);
                        break;
                    }
                #endregion

                #region :: OPDoctorWiseNewOldPatientReport ::
                case "OPDoctorWiseNewOldPatientReport":
                    {
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DoctorWiseNewAndOldPatientReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        List<SearchModel> fields = SearchFieldExtension.GetSearchFields(model.SearchFields);

                        commonReportModel.FromDate = Convert.ToDateTime(fields.Find(x => x.FieldName?.ToLower() == "fromdate".ToLower())?.FieldValueString);
                        commonReportModel.ToDate = Convert.ToDateTime(fields.Find(x => x.FieldName?.ToLower() == "todate".ToLower())?.FieldValueString);

                        var html = _OPbilling.ViewOPDoctorWiseNewOldPatientReport(commonReportModel.FromDate, commonReportModel.ToDate, htmlFilePath, _pdfUtility.GetHeader(htmlHeaderFilePath, model.baseUrl));
                        var tuple = _pdfUtility.GeneratePdfFromHtml(html, model.storageBaseUrl, "OPDoctorWiseNewAndOldPatientReport", "OPDoctorWiseNewAndOldPatientReport", Orientation.Portrait);
                        byteFile = Convert.ToBase64String(tuple.Item1);
                        break;
                    }
                #endregion
                default:
                    break;
            }

            return byteFile;
        }
    }
}
