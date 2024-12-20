using HIMS.Services.Utilities;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using HIMS.Services.Report.OPReports;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using System.Text;
using WkHtmlToPdfDotNet;
using Microsoft.Data.SqlClient;
using HIMS.Services.OutPatient;

namespace HIMS.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public readonly IPdfUtility _pdfUtility;
        public ReportService(IHostingEnvironment hostingEnvironment, IPdfUtility pdfUtility)
        {
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
        }

        public string GetReportSetByProc(ReportRequestModel model)
        {
            var tuple = new Tuple<byte[], string>(null, string.Empty);
            switch (model.Mode)
            {
                #region :: RegistrationReport ::
                case "RegistrationReport":
                    {
                        string[] colList = {"RegID", "PatientName", "Address", "City", "PinNo", "Age", "GenderName", "MobileNo" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_RegistrationReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptListofRegistration", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "RegistrationReport1", "RegistrationReport1", Orientation.Portrait, PaperKind.A4);
                        break;
                    }
                #endregion
                #region :: RegistrationReport1 ::
                case "RegistrationReport1":
                    {
                        string[] colList = { "RegID", "PatientName","Address", "City", "PinNo", "Age", "GenderName", "MobileNo" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_RegistrationReport1.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptListofRegistration", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "RegistrationReport1", "RegistrationReport1", Orientation.Portrait, PaperKind.A4);
                        break;
                    }
                #endregion

                #region :: CreditReports ::
                case "CreditReports":
                    {
                        string[] colList = {"PBillNo","RegNo"};
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "CommanReport_CreditReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptOP_IP_CreditBills", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "CreditReports", "CreditReports", Orientation.Portrait, PaperKind.A4);
                        break;
                    }
                #endregion

                #region :: AppointmentListReport ::
                case "AppointmentListReport":
                    {
                        string[] colList = { "RegNO", "VisitDate", "PatientName", "AgeYear", "OPDNo", "DoctorName", "RefDocName", "CompanyName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_AppoitnmentListReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptOPAppointmentListReport", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "AppointmentListReport", "AppointmentListReport", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: AppointmentListReport1 ::
                case "AppointmentListReport1":
                    {
                        string[] colList = { "RegNO", "VisitDate", "PatientName", "AgeYear", "OPDNo", "DoctorName", "RefDocName", "CompanyName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_AppoitnmentListReport1.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptOPAppointmentListReport", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "AppointmentListReport1", "AppointmentListReport1", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DoctorWiseVisitReport ::
                case "DoctorWiseVisitReport":
                    {
                        string[] colList = { "RegID", "VisitTime", "PatientName", "DoctorName", "RefDoctorName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DoctorWiseVisitReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDoctorWiseVisitDetails", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DoctorWiseVisitReport", "DoctorWiseVisitReport", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: RefDoctorWiseReport ::
                case "RefDoctorWiseReport":
                    {
                        string[] colList = { "RegNO", "PatientName", "AdmittedDoctorName", "RefDoctorName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_RefDoctorWiseReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("RptRefDoctorWiseAdmission", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "RefDoctorWiseReport", "RefDoctorWiseReport", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DepartmentWiseCountSummury ::
                case "DepartmentWisecountSummury":
                    {
                        string[] colList = { "DepartmentName", "Lbl" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DepartmentWiseCountSummury.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("Rtrv_OPDepartSumry", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentWiseCountSummury", "DepartmentWiseCountSummury", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: OPDoctorWiseVisitCountSummary ::
                case "OPDoctorWiseVisitCountSummary":
                    {
                        string[] colList = { "DocName", "Lbl" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OpDctorwisecountsummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("Rtrv_OPDocSumry", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPDoctorWiseVisitCountSummary", "OPDoctorWiseVisitCountSummary", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: OPAppoinmentListWithServiseAvailed ::
                case "OPAppoinmentListWithServiseAvailed":
                    {
                        string[] colList = { "RegNO", "PatientName", "DoctorName", "RefDoctorName", "Medical", "Pathology", "Radiology" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_AppointmentListWithServiseAvailed.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptAppointListWithService", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPAppoinmentListWithServiseAvailed", "OPAppoinmentListWithServiseAvailed", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: CrossConsultationReport ::
                case "CrossConsultationReport":
                    {
                        string[] colList = { "RegNO", "VisitDate", "PatientName", "DoctorName", "OPDNo", "DepartmentName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_CrossConsultationReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptOPCrossConsultationReport", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "CrossConsultationReport", "CrossConsultationReport", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: OPDoctorWiseNewOldPatientReport ::
                case "OPDoctorWiseNewOldPatientReport":
                    {
                        string[] colList = { "VisitDate", "RegNO", "PatientName", "DepartmentName", "DoctorName", "OPDNo" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DoctorWiseNewAndOldPatientReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptOPDocWiseNewOldPatient", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPDoctorWiseNewAndOldPatientReport", "OPDoctorWiseNewAndOldPatientReport", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: BillReportSummary ::
                case "BillReportSummary":
                    {
                        string[] colList = { "RegId", "PatientName", "BillNo", "BillAmt", "ConcessionAmt", "NetPayableAmt", "PaidAmount", "BalanceAmt", "CashPay" , "ChequePay", "CardPay" , "NeftPay", "PayTMPay" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_BillReportSummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptBillDateWise", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "BillReportSummary", "BillReportSummary", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: BillReportSummarySummary ::
                case "BillReportSummarySummary":
                    {
                        string[] colList = { "RegId", "PatientName", "BillNo", "BillAmt", "ConcessionAmt", "NetPayableAmt", "PaidAmount", "BalanceAmt", "CashPay", "ChequePay", "CardPay", "NeftPay", "PayTMPay" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_BillReportSummarySummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptBillDetails", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "BillReportSummarySummary", "BillReportSummarySummary", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: OPDBillBalanceReport ::
                case "OPDBillBalanceReport":
                    {
                        string[] colList = { "RegId", "BillNo", "PatientName", "NetPayableAmt", "PaidAmount", "BalanceAmt", "TotalAmt"};
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_OPDBillBalanceReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptOPDCreditBills", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPDBillBalanceReport", "OPDBillBalanceReport", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: OPDRefundOfBill ::
                case "OPDRefundOfBill":
                    {
                        string[] colList = { "RefundDate", "RegId", "PatientName", "RefundAmount", "TotalAmt" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_OPDRefundOfBill.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptOPDRefundOfBill", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPDRefundOfBill", "OPDRefundOfBill", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: OPDailyCollectionReport ::
                case "OPDailyCollectionReport":
                    {
                        string[] colList = { "Number", "PaymentTime", "RegNo", "PatientName", "ReceiptNo", "NetPayableAmt" , "CashPayAmount", "ChequePayAmount", "CardPayAmount" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPDailycollectionuserwise.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptOPDailyCollectionReport", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPDailyCollectionReport", "OPDailyCollectionReport", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: OPCollectionSummary ::
                case "OPCollectionSummary":
                    {
                        string[] colList = { "RegNo", "PatientName", "PBillNo", "BillDate", "TotalAmt", "ConcessionAmt", "NetPayableAmt", "CashPayAmount",  "CardPayAmount", "NEFTPayAmount", "PayTMAmount", "RefundAmount", "ConcessionReason", "CompanyName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_OPCollectionReportSummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptOPDCollectionReport", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPCollectionSummary", "OPCollectionSummary", Orientation.Portrait);
                        break;
                    }
                #endregion


                #region :: DayWiseOpdCountDetails ::
                case "DayWiseOpdCountDetails":
                    {
                        string[] colList = { "RegNO", "PatientName", "AgeYear", "CityName", "MobileNo", "DepartmentName", "DoctorName", "RefDoctorName", "CompanyName"};
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DayWiseOpdCountDetails.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDaywiseopdcountdetail", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DayWiseOpdCountDetails", "DayWiseOpdCountDetails", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: DayWiseOpdCountSummry ::
                case "DayWiseOpdCountSummry":
                    {
                        string[] colList = { "VisitDate", "DateWiseCount" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DayWiseOpdCountSummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDaywiseopdcountSummary", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DayWiseOpdCountSummry", "DayWiseOpdCountSummry", Orientation.Portrait);
                        break;
                    }
                #endregion


                #region :: DepartmentWiseOPDCount ::
                case "DepartmentWiseOPDCount":
                    {
                        string[] colList = { "DepartmentName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DepartmentWiseCountSummury.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDaywiseopdcountdetail", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentWiseOPDCount", "DepartmentWiseOPDCount", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DepartmentWiseOpdCountSummary ::
                case "DepartmentWiseOpdCountSummary":
                    {
                        string[] colList = { "DepartmentName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DepartmentWiseOpdCountSummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDepartmentDaywiseopdcountSummary", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentWiseOpdCountSummary", "DepartmentWiseOpdCountSummary", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DrWiseOPDCountDetail ::
                case "DrWiseOPDCountDetail":
                    {
                        string[] colList = { "DepartmentName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DepartmentWiseOpdCountSummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDaywiseopdcountdetail", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DrWiseOPDCountDetail", "DrWiseOPDCountDetail", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: DoctorWiseOpdCountSummary ::
                case "DoctorWiseOpdCountSummary":
                    {
                        string[] colList = { "DoctorName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DoctorWiseOpdCountSummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDaywiseDoctoropdcountSummary", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DoctorWiseOpdCountSummary", "DoctorWiseOpdCountSummary", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DrWiseOPDCollectionDetails ::
                case "DrWiseOPDCollectionDetails":
                    {
                        string[] colList = { "DoctorName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DoctorWiseOpdCountSummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDrwiseopdcollectiondetail", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DrWiseOPDCollectionDetails", "DrWiseOPDCollectionDetails", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: DoctorWiseOpdCollectionSummary ::
                case "DoctorWiseOpdCollectionSummary":
                    {
                        string[] colList = { "DoctorName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DoctorWiseOPDCollectionSummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDrwiseopdcollectionSummary", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DoctorWiseOpdCollectionSummary", "DoctorWiseOpdCollectionSummary", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DepartmentWiseOPDCollectionDetails ::
                case "DepartmentWiseOPDCollectionDetails":
                    {
                        string[] colList = { "DoctorName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DoctorWiseOPDCollectionSummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDrwiseopdcollectiondetail", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentWiseOPDCollectionDetails", "DepartmentWiseOPDCollectionDetails", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DepartmentWiseOpdCollectionSummary ::
                case "DepartmentWiseOpdCollectionSummary":
                    {
                        string[] colList = { "DoctorName", "TotalAmt" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DepartmentWiseOpdCollectionSummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDepartmentwiseopdcollectionSummary", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentWiseOpdCollectionSummary", "DepartmentWiseOpdCollectionSummary", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DepartmentServiceGroupWiseCollectionDetails ::
                case "DepartmentServiceGroupWiseCollectionDetails":
                    {
                        string[] colList = { "DepartmentName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DepartmentWiseOpdCollectionSummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDepartmentservicegroupwisecollectionDetail", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentServiceGroupWiseCollectionDetails", "DepartmentServiceGroupWiseCollectionDetails", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DepartmentServiceGroupWiseCollectionSummary ::
                case "DepartmentServiceGroupWiseCollectionSummary":
                    {
                        string[] colList = { "GroupName" , "NetPayableAmt" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_DepartmentServiceGroupWiseCollectionSummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptservicegroupwisecollectionSummary", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentServiceGroupWiseCollectionSummary", "DepartmentServiceGroupWiseCollectionSummary", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: ViewOPPaymentReceipt ::
                case "ViewOPPaymentReceipt":
                {
                    string[] colList = { "GroupName", "NetPayableAmt" };
                    string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReceipt.html");
                    string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                    var html = GetHTMLView("m_rptOPDPaymentReceiptPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                    tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPPaymentReceipt", "OPPaymentReceipt", Orientation.Portrait);
                    break;
                }
                #endregion

                    

                default:
                    break;

                 
            }
            string byteFile = Convert.ToBase64String(tuple.Item1);
            return byteFile;
        }

        private string GetHTMLView(string sp_Name, ReportRequestModel model, string htmlFilePath, string htmlHeaderFilePath, string[] colList )
        {
            Dictionary<string, string> fields = HIMS.Data.Extensions.SearchFieldExtension.GetSearchFields(model.SearchFields).ToDictionary(e => e.FieldName, e => e.FieldValueString);
            DatabaseHelper odal = new();
            int sp_Para = 0;
            SqlParameter[] para = new SqlParameter[fields.Count];
            foreach (var property in fields)
            {
                var param = new SqlParameter
                {
                    ParameterName = "@" + property.Key,
                    Value = property.Value.ToString()
                };

                para[sp_Para] = param;
                sp_Para++;
            }
            var dt = odal.FetchDataTableBySP(sp_Name, para);


            string htmlHeader = _pdfUtility.GetHeader(htmlHeaderFilePath, model.BaseUrl);

            string html = File.ReadAllText(htmlFilePath);
            html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

            DateTime FromDate = Convert.ToDateTime(model.SearchFields.Find(x => x.FieldName?.ToLower() == "fromdate".ToLower())?.FieldValue ?? null);
            DateTime ToDate = Convert.ToDateTime(model.SearchFields.Find(x => x.FieldName?.ToLower() == "todate".ToLower())?.FieldValue ?? null);

            StringBuilder items = new("");
            double T_Count = 0;
            switch (model.Mode)
            {
                case "RegistrationReport":
                case "CreditReports":

                case "AppointmentListReport":
                case "DepartmentWisecountSummury":
                case "OPDoctorWiseVisitCountSummary":
                case "OPAppoinmentListWithServiseAvailed":
                case "CrossConsultationReport":
                case "OPDoctorWiseNewOldPatientReport":
                    {
                        int k = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            k++;

                            items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                            if (model.Mode == "DepartmentWisecountSummury" || model.Mode == "OPDoctorWiseVisitCountSummary")
                                T_Count += dr["Lbl"].ConvertToDouble();
                        }
                    }
                    break;
                case "DoctorWiseVisitReport":
                case "RefDoctorWiseReport":
                    {
                        string previousLabel = string.Empty;
                        int i = 0, j = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++; j++;
                            if (i == 1)
                            {
                                String Label;
                                Label = dr["DoctorName"].ConvertToString();
                                items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td>");
                                items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(j).Append("</td></tr>");
                            }

                            if (previousLabel != "" && previousLabel != dr["DoctorName"].ConvertToString())
                            {
                                j = 1;
                                items.Append("<tr style='border:1px solid black;color:#241571;background-color:#63C5DA'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append("</td></tr>");
                                items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");
                            }

                            previousLabel = dr["DoctorName"].ConvertToString();

                            items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                        }
                    }
                    break;
                case "ViewOPPaymentReceipt":
                    {
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                        html = html.Replace("{{NewHeader}}", htmlHeader);

                        html = html.Replace("{{BillNo}}", dt.GetColValue("BillNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{CashPayAmount}}", dt.GetColValue("CashPayAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{ChequePayAmount}}", dt.GetColValue("ChequePayAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{ChequeDate}}", dt.GetColValue("ChequeDate").ConvertToDateString("dd/MM/yyyy"));
                        html = html.Replace("{{ChequeNo}}", dt.GetColValue("ChequeNo"));

                        html = html.Replace("{{CardPayAmount}}", dt.GetColValue("CardPayAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{CardDate}}", dt.GetColValue("CardDate").ConvertToDateString("dd/MM/yyyy"));
                        html = html.Replace("{{CardNo}}", dt.GetColValue("CardNo"));
                        html = html.Replace("{{CardBankName}}", dt.GetColValue("CardBankName"));
                        html = html.Replace("{{NEFTPayAmount}}", dt.GetColValue("NEFTPayAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{NEFTNo}}", dt.GetColValue("NEFTNo"));
                        html = html.Replace("{{NEFTBankMaster}}", dt.GetColValue("NEFTBankMaster"));
                        html = html.Replace("{{PayTMAmount}}", dt.GetColValue("PayTMAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{PayTMTranNo}}", dt.GetColValue("PayTMTranNo"));
                        html = html.Replace("{{PaidAmount}}", dt.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{BillDate}}", dt.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{ReceiptNo}}", dt.GetColValue("ReceiptNo"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));
                        html = html.Replace("{{Remark}}", dt.GetColValue("Remark"));
                        html = html.Replace("{{PaymentTime}}", dt.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | HH:mm tt"));


                        html = html.Replace("{{ConsultantDocName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDoctorName"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{Date}}", dt.GetDateColValue("Date").ConvertToDateString());
                        html = html.Replace("{{VisitDate}}", dt.GetColValue("VisitTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        html = html.Replace("{{TotalAmt}}", dt.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{ConcessionAmt}}", dt.GetColValue("ConcessionAmt").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{NetPayableAmt}}", dt.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace());


                        html = html.Replace("{{chkcashflag}}", dt.GetColValue("CashPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkcardflag}}", dt.GetColValue("CardPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkchequeflag}}", dt.GetColValue("ChequePayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkneftflag}}", dt.GetColValue("NEFTPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkpaytmflag}}", dt.GetColValue("PayTMAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkremarkflag}}", dt.GetColValue("Remark").ConvertToDouble() != ' ' ? "table-row " : "none");

                        string finalamt = (dt.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace().ToString()).conversion();
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());
                    }
                    break;
            }


            if (model.Mode == "DepartmentWisecountSummury" || model.Mode == "OPDoctorWiseVisitCountSummary")
                html = html.Replace("{{T_Count}}", T_Count.ToString());

            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

    }
}
