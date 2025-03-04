using HIMS.Services.Utilities;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Hosting;
using System.Data;
using System.Text;
using WkHtmlToPdfDotNet;
using HIMS.Data;
using Microsoft.Data.SqlClient;
using HIMS.Data.DTO.OPPatient;
using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient.Server;
using System.Globalization;

namespace HIMS.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly Data.Models.HIMSDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public readonly IPdfUtility _pdfUtility;
        public ReportService(HIMSDbContext HIMSDbContext,IHostingEnvironment hostingEnvironment, IPdfUtility pdfUtility)
        {
            _context = HIMSDbContext;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
        }

        public virtual async Task<List<ServiceMasterDTO>> SearchService(string str)
        {
            return await this._context.ServiceMasters.Where(x => (x.ServiceName).ToLower().Contains(str)).Take(25).Select(s => new ServiceMasterDTO() { ServiceId = s.ServiceId, ServiceName = s.ServiceName }).ToListAsync();
        }
        public virtual async Task<List<MDepartmentMaster>> SearchDepartment(string str)
        {
            return await this._context.MDepartmentMasters.Where(x => (x.DepartmentName).ToLower().Contains(str)).Take(25).ToListAsync();
        }
        public virtual async Task<List<CashCounter>> SearchCashCounter(string str)
        {
            return await this._context.CashCounters.Where(x => (x.CashCounterName).ToLower().Contains(str)).Take(25).ToListAsync();
        }

        public string GetReportSetByProc(ReportRequestModel model)
        {
            var tuple = new Tuple<byte[], string>(null, string.Empty);
            switch (model.Mode)
            {
                #region :: RegistrationReport ::
                case "RegistrationReport":
                    {
                        model.RepoertName = "Registration List";
                        string[] headerList = { "Sr.No", "UHID", "Patient Name", "Address", "City", "Pin Code", "Age", "Gender Name", "Mobile No" };
                        string[] colList = { "RegID", "PatientName", "Address", "City", "PinNo", "Age", "GenderName", "MobileNo" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptListofRegistration", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "RegistrationReport", "RegistrationReport", Orientation.Portrait, PaperKind.A4);
                        break;
                    }
                #endregion

                #region :: AppointmentListReport ::
                case "AppointmentListReport":
                    {

                        model.RepoertName = "Appointment List";
                        string[] headerList = { "Sr.No", "UHID", "Visit Date", "Patient Name" };
                        string[] colList = { "RegNo", "VisitDate", "PatientName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptOPAppointmentListReport", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "AppointmentListReport", "AppointmentListReport", Orientation.Portrait);
                        break;
                    }
                #endregion
              

                #region :: DoctorWiseVisitReport ::
                case "DoctorWiseVisitReport":
                    {
                        model.RepoertName = "DoctorWiseVisit Report ";
                        string[] headerList = { "Sr.No", "RegNO", "PatientName", "DoctorName", "RefDoctorName" };
                        string[] colList = { "RegID", "PatientName", "DoctorName", "RefDoctorName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDoctorWiseVisitDetails", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DoctorWiseVisitReport", "DoctorWiseVisitReport", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: RefDoctorWiseReport ::
                case "RefDoctorWiseReport":
                    {
                        model.RepoertName = "RefDoctorWise Report  ";
                        string[] headerList = { "Sr.No", "RegNO", "PatientName", "AdmittedDoctorName", "AgeYear" };
                        string[] colList = { "RegNo", "PatientName", "AdmittedDoctorName", "AgeYear" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("RptRefDoctorWiseAdmission", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "RefDoctorWiseReport", "RefDoctorWiseReport", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DepartmentWiseCountSummury ::
                case "DepartmentWisecountSummury":
                    {
                        model.RepoertName = "Department Wise Count Summury";
                        string[] headerList = { "Sr.No", "Department Name", "Count" };
                        string[] colList = { "DepartmentName", "Lbl" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleTotalReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("Rtrv_OPDepartSumry", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentWiseCountSummury", "DepartmentWiseCountSummury", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: OPDoctorWiseVisitCountSummary ::
                case "OPDoctorWiseVisitCountSummary":
                    {
                        model.RepoertName = "Doctor Wise Count List";
                        string[] headerList = { "Sr.No", "Total Count", "Doctor Name" };
                        string[] colList = { "DocName", "Lbl" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("Rtrv_OPDocSumry", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPDoctorWiseVisitCountSummary", "OPDoctorWiseVisitCountSummary", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: OPAppoinmentListWithServiseAvailed ::
                case "OPAppoinmentListWithServiseAvailed":
                    {
                        model.RepoertName = "ViewAppoinment List With Service";
                        string[] headerList = { "Sr.No", "Reg NO", "Patient Name", "Doctor Name", "RefDoctor Name", "Medical", "Pathology", "Radiology" };
                        string[] colList = { "RegNO", "PatientName", "DoctorName", "RefDoctorName", "Medical", "Pathology", "Radiology" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptAppointListWithService", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPAppoinmentListWithServiseAvailed", "OPAppoinmentListWithServiseAvailed", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: CrossConsultationReport ::
                case "CrossConsultationReport":
                    {
                        model.RepoertName = "Cross Consultation Report";
                        string[] headerList = { "Sr.No", "UHID", "Visit Date", "Patient Name", "Doctor Name", "OPD NO", "Department Name" };
                        string[] colList = { "RegNO", "VisitDate", "PatientName", "DoctorName", "OPDNo", "DepartmentName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptOPCrossConsultationReport", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "CrossConsultationReport", "CrossConsultationReport", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: OPDoctorWiseNewOldPatientReport ::
                case "OPDoctorWiseNewOldPatientReport":
                    {
                        model.RepoertName = "Doctor Wise New And Old Patient Report";
                        string[] headerList = { "Sr.No", "UHID", "Patient Name", "Department Name", "Doctor Name", "OPD NO" };
                        string[] colList = { "RegNO", "PatientName", "DepartmentName", "DoctorName", "OPDNo" };
                        string groupByCol = "PatientOldAndNew";
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "MultiTotalReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptOPDocWiseNewOldPatient_web", model, htmlFilePath, htmlHeaderFilePath, colList, headerList, null, groupByCol);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPDoctorWiseNewAndOldPatientReport", "OPDoctorWiseNewAndOldPatientReport", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: BillReportSummary ::

                case "BillReportSummary":
                    {
                        model.RepoertName = "OP Bill Summary";
                        string[] headerList = { "Sr.No", "Bill No", "Bill Date", "UHID", "Patient Name", "Total Amount", "Con Amt", "Net Amount", "Paid Amount", "Bal Amt", "Cash Pay", "Cheque Pay", "Card Pay", "NEFT Pay", "Online Pa" };
                        string[] colList = { "BillNo", "BillDate", "RegId", "PatientName", "BillAmt", "ConcessionAmt", "NetPayableAmt", "PaidAmount", "BalanceAmt", "CashPay", "ChequePay", "CardPay", "NeftPay", "PayTMPay" };
                        string[] totalColList = { "", "", "", "", "lableTotal", "BillAmt", "ConcessionAmt", "NetPayableAmt", "PaidAmount", "BalanceAmt", "CashPay", "ChequePay", "CardPay", "NeftPay", "PayTMPay" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleTotalReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptBillDateWise", model, htmlFilePath, htmlHeaderFilePath, colList, headerList, totalColList);
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
                        string[] colList = { "RegId", "BillNo", "PatientName", "NetPayableAmt", "PaidAmount", "BalanceAmt", "TotalAmt" };
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

                #region :: OPRefundReceipt ::
                case "OPRefundReceipt":
                    {
                        string[] colList = { "RefundDate", "RegId", "PatientName", "RefundAmount", "TotalAmt" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPRefundReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("m_rptOPRefundofBillPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPRefundReceipt", "OPRefundReceipt", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: OPPaymentReceipt ::
                case "OPPaymentReceipt":
                    {
                        string[] colList = { "RefundDate", "RegId", "PatientName", "RefundAmount", "TotalAmt" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPPaymentReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("m_rptOPDPaymentReceiptPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPPaymentReceipt", "OPPaymentReceipt", Orientation.Portrait);
                        break;
                    }
                #endregion
              
                #region :: AppointmentReceipt ::
                case "AppointmentReceipt":
                    {

                        string[] colList = { "RegNo", "VisitDate", "PatientName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "AppointmentReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptAppointmentPrint1", model, htmlFilePath, htmlHeaderFilePath, colList );
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "AppointmentListReport", "AppointmentListReport", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: OpBillReceipt ::
                case "OpBillReceipt":
                    {

                        string[] colList = { "RegNo", "VisitDate", "PatientName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OpBillingReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptBillPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OpBillReceipt", "OpBillReceipt", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DoctorNotesReceipt ::
                case "DoctorNotesReceipt":
                    {

                        string[] colList = { "RegNo", "VisitDate", "PatientName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "DoctorNoteReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("m_rpt_T_Doctors_Notes", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DoctorNotesReceipt", "DoctorNotesReceipt", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: NursingNotesReceipt ::
                case "NursingNotesReceipt":
                    {

                        string[] colList = { "RegNo", "VisitDate", "PatientName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NursingNoteReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("m_rpt_T_NursingNotesPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "NursingNotesReceipt", "NursingNotesReceipt", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: DoctorPatientHandoverReceipt ::
                case "DoctorPatientHandoverReceipt":
                    {

                        string[] colList = { "RegNo", "VisitDate", "PatientName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "DoctorPatientHandoverReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("m_rpt_T_Doctor_PatientHandover", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DoctorPatientHandoverReceipt", "DoctorPatientHandoverReceipt", Orientation.Portrait);
                        break;
                    }
                #endregion



                #region :: OPDailyCollectionReport ::
                case "OPDailyCollectionReport":
                    {
                        model.RepoertName = "OP DAILY COLLECTION REPORT";
                        string[] headerList = { "Sr.No", "Bill No", "Date", "UHID", "Patient Name", "Receipt No", "Net Amt", "Cash Amt", "Cheque Amt", "Card Amt", "Online Amt", "User Name" };
                        string[] colList = { "Number", "PaymentTime", "RegNo", "PatientName", "ReceiptNo", "NetPayableAmt", "CashPayAmount", "ChequePayAmount", "CardPayAmount", "PayTMAmount", "UserName" };
                        string[] totalColList = { "", "", "", "", "", "lableTotal", "NetPayableAmt", "CashPayAmount", "ChequePayAmount", "CardPayAmount", "PayTMAmount", "" };
                        string groupByCol = "Type";
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "MultiTotalReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptOPDailyCollectionReport", model, htmlFilePath, htmlHeaderFilePath, colList, headerList, totalColList, groupByCol);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPDailyCollectionReport", "OPDailyCollectionReport", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: OPCollectionSummary ::
                case "OPCollectionSummary":
                    {
                        string[] colList = { "RegNo", "PatientName", "PBillNo", "BillDate", "TotalAmt", "ConcessionAmt", "NetPayableAmt", "CashPayAmount", "CardPayAmount", "NEFTPayAmount", "PayTMAmount", "RefundAmount", "ConcessionReason", "CompanyName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPReport_OPCollectionReportSummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptOPDCollectionReport", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPCollectionSummary", "OPCollectionSummary", Orientation.Portrait);
                        break;
                    }
                #endregion
                //#region :: OPCollectionSummary ::
                //case "OPCollectionSummary":
                //    {
                //        string[] headerList = { "RegNo", "UHID", "Patient Name", "PBillNo BillDate", "TotalAmt ", "ConcessionAmt ", "NetPayableAmt ", "CashPayAmount ", "CardPayAmount ", "NEFTPayAmount ", "PayTMAmount", "RefundAmount", "ConcessionReason", "CompanyName" };
                //        string[] colList = { "RegNo", "PatientName", "PBillNo", "BillDate", "TotalAmt", "ConcessionAmt", "NetPayableAmt", "CashPayAmount", "CardPayAmount", "NEFTPayAmount", "PayTMAmount", "RefundAmount", "ConcessionReason", "CompanyName" };
                //        string[] totalColList = { "", "", "PatientName", "", "lableTotal", "", "", "", "","","","","","",""};

                //        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "MultiTotalReportFormat.html");
                //        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                //        var html = GetHTMLView("rptOPDCollectionReport", model, htmlFilePath, htmlHeaderFilePath, colList, headerList, totalColList);
                //        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPCollectionSummary", "OPCollectionSummary", Orientation.Landscape);
                //        break;
                //    }
                //#endregion


                #region :: DayWiseOpdCountDetails ::
                case "DayWiseOpdCountDetails":
                    {
                        model.RepoertName = "Day Wise Opd Details";
                        string[] headerList = { "Sr.No", "UHID", "Patient Name", "Age Year", "City Name", "Mobile No", "Department Name", "Doctor Name", "RefDoctor Name", "Company Name" };
                        string[] colList = { "RegNO", "PatientName", "AgeYear", "CityName", "MobileNo", "DepartmentName", "DoctorName", "RefDoctorName", "CompanyName" };
                        string[] totalColList = { "", "lableTotal", "PatientName", "", "", "", "", "", "", "" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleTotalReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDaywiseopdcountdetail", model, htmlFilePath, htmlHeaderFilePath, colList, headerList, totalColList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DayWiseOpdCountDetails", "DayWiseOpdCountDetails", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: DayWiseOpdCountSummry ::
                case "DayWiseOpdCountSummry":
                    {
                        model.RepoertName = "Day Wise Opd Count Summary";
                        string[] headerList = { "Sr.No", "Visit Date", "Count" };
                        string[] colList = { "VisitDate", "DateWiseCount" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleTotalReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDaywiseopdcountSummary", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DayWiseOpdCountSummry", "DayWiseOpdCountSummry", Orientation.Portrait);
                        break;
                    }
                #endregion


                #region :: DepartmentWiseOPDCount ::
                case "DepartmentWiseOPDCount":
                    {
                        model.RepoertName = "Department WiseCount Details ";
                        string[] headerList = { "Sr.No", "PatientName", "DoctorName", "MobileNo" };
                        string[] colList = { "PatientName", "DoctorName", "MobileNo" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleTotalReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDaywiseopdcountdetail", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentWiseOPDCount", "DepartmentWiseOPDCount", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DepartmentWiseOpdCountSummary ::
                case "DepartmentWiseOpdCountSummary":
                    {
                        model.RepoertName = "Department Wise OPD Count Summary";
                        string[] headerList = { "Sr.No", "Department Name", "Count" };
                        string[] colList = { "DepartmentName", "VisitCount" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleTotalReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDepartmentDaywiseopdcountSummary", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentWiseOpdCountSummary", "DepartmentWiseOpdCountSummary", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DrWiseOPDCountDetail ::
                case "DrWiseOPDCountDetail":
                    {
                        model.RepoertName = "DrWise OPD CountDetail  ";
                        string[] headerList = { "Sr.No", "Department Name", " PatientName", "AgeYear", "CityName", "MobileNo", "RefDoctorName", "CompanyName" };
                        string[] colList = { "DepartmentName", "PatientName", "AgeYear", "CityName", "MobileNo", "RefDoctorName", "CompanyName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleTotalReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDaywiseopdcountdetail", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DrWiseOPDCountDetail", "DrWiseOPDCountDetail", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: DoctorWiseOpdCountSummary ::
                case "DoctorWiseOpdCountSummary":
                    {
                        model.RepoertName = "Doctor Wise Opd Count Summary";
                        string[] headerList = { "Sr.No", "Doctor Name", "Count" };
                        string[] colList = { "DoctorName", "Count" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleTotalReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDaywiseDoctoropdcountSummary", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DoctorWiseOpdCountSummary", "DoctorWiseOpdCountSummary", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DrWiseOPDCollectionDetails ::
                case "DrWiseOPDCollectionDetails":
                    {
                        model.RepoertName = "Doctor Wise Opd Count Summary";
                        string[] headerList = { "Sr.No", "Doctor Name", "Count" };
                        string[] colList = { "DoctorName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleTotalReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDrwiseopdcollectiondetail", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DrWiseOPDCollectionDetails", "DrWiseOPDCollectionDetails", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: DoctorWiseOpdCollectionSummary ::
                case "DoctorWiseOpdCollectionSummary":
                    {
                        model.RepoertName = "Doctor Wise Opd Collection Summary";
                        string[] headerList = { "Sr.No", "Patient Name", "Bill Amount" };
                        string[] colList = { "DoctorName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleTotalReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDrwiseopdcollectionSummary", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DoctorWiseOpdCollectionSummary", "DoctorWiseOpdCollectionSummary", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DepartmentWiseOPDCollectionDetails ::
                case "DepartmentWiseOPDCollectionDetails":
                    {
                        model.RepoertName = "Doctor Wise Opd Collection Summary";
                        string[] headerList = { "Sr.No", "Patient Name", "Bill Amount" };
                        string[] colList = { "DoctorName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleTotalReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDrwiseopdcollectiondetail", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentWiseOPDCollectionDetails", "DepartmentWiseOPDCollectionDetails", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DepartmentWiseOpdCollectionSummary ::
                case "DepartmentWiseOpdCollectionSummary":
                    {
                        model.RepoertName = "Department Wise Opd Collection Summary";
                        string[] headerList = { "Sr.No", "Department Name", "Bill Amount" };
                        string[] colList = { "DoctorName", "TotalAmt" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleTotalReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDepartmentwiseopdcollectionSummary", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentWiseOpdCollectionSummary", "DepartmentWiseOpdCollectionSummary", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DepartmentServiceGroupWiseCollectionDetails ::
                case "DepartmentServiceGroupWiseCollectionDetails":
                    {
                        model.RepoertName = "Department Wise Opd Collection Summary";
                        string[] headerList = { "Sr.No", "Department Name", "Bill Amount" };
                        string[] colList = { "DepartmentName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleTotalReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDepartmentservicegroupwisecollectionDetail", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentServiceGroupWiseCollectionDetails", "DepartmentServiceGroupWiseCollectionDetails", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DepartmentServiceGroupWiseCollectionSummary ::
                case "DepartmentServiceGroupWiseCollectionSummary":
                    {
                        model.RepoertName = "Department Service Group Wise Collection Summary";
                        string[] headerList = { "Sr.No", "Description", "Net Amount" };
                        string[] colList = { "GroupName", "NetPayableAmt" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleTotalReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptservicegroupwisecollectionSummary", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentServiceGroupWiseCollectionSummary", "DepartmentServiceGroupWiseCollectionSummary", Orientation.Portrait);
                        break;
                    }
                #endregion


                    //Ipsection
                #region :: IpCasepaperReport ::
                case "IpCasepaperReport":
                    {

                        string[] colList = { "RegNo", "VisitDate", "PatientName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPCasepaper.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("m_rptAdmissionPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPCasepaper", "IPCasepaper", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: IptemplateCasepaperReport ::
                case "IptemplateCasepaperReport":
                    {

                        string[] colList = { "RegNo", "VisitDate", "PatientName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPCasepaper.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("m_rptAdmissionPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPCasepaper", "IPCasepaper", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: IpMLCCasePaperPrint ::
                case "IpMLCCasePaperPrint":
                    {
                        model.RepoertName = "MLC Case Paper ";
                        string[] headerList = { };
                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_MLCReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("m_MLCCasePaperPrint", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPMLCDetail", "IPMLCDetail", Orientation.Landscape);
                        break;
                    }
                #endregion

                #region :: IpFinalBill ::
                case "IpFinalBill":
                    {

                        string[] colList = { "RegNo", "VisitDate", "PatientName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewIPBillsample.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("m_rptIPD_FINAL_BILL_CLASSWISE", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "NewIPBill", "NewIPBill", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: IpPaymentReceipt ::
                case "IpPaymentReceipt":
                    {

                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SettlementReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("m_rptOPDPaymentReceiptPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPPaymentReceipt", "IPPaymentReceipt", Orientation.Landscape);
                        break;
                    }
                #endregion
                #region :: AdmissionList ::
                case "AdmissionList":
                    {

                        model.RepoertName = "Appointment List";
                        string[] headerList = { "Sr.No", "UHID", "AdmissionDate", "IPDNo", "Patient Name", "Age", "GenderName", "RoomName", "BedName","AdmittedDoctorName", "RefDocName", "ChargesAmount", "AdvanceAmount", "BalPayAmt"};
                        string[] colList = { "RegNo", "AdmissionDate", "IPDNo", "PatientName" , "Age", "GenderName", "RoomName", "BedName","AdmittedDoctorName", "RefDocName", "ChargesAmount", "AdvanceAmount", "BalPayAmt", };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptCurrentAdmittedListReport", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDCurrentAdmittedDoctorWiseCharges", "IPDCurrentAdmittedDoctorWiseCharges", Orientation.Landscape);
                        break;
                    }
                #endregion

                #region :: IpRefundReceipt ::
                case "IpAdvanceRefundReceipt":
                    {

                        model.RepoertName = "Appointment List";
                        string[] headerList = { "Sr.No", "UHID", "AdmissionDate", "IPDNo", "Patient Name", "Age", "GenderName", "RoomName", "BedName", "AdmittedDoctorName", "RefDocName", "ChargesAmount", "AdvanceAmount", "BalPayAmt" };
                        string[] colList = { "RegNo", "AdmissionDate", "IPDNo", "PatientName", "Age", "GenderName", "RoomName", "BedName", "AdmittedDoctorName", "RefDocName", "ChargesAmount", "AdvanceAmount", "BalPayAmt", };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "RefundofAdvanceReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("m_rptIPRefundofAdvancePrint", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDCurrentAdmittedDoctorWiseCharges", "IPDCurrentAdmittedDoctorWiseCharges", Orientation.Landscape);
                        break;
                    }
                #endregion

                #region :: IpRefundReceipt ::
                case "IpBillRefundReceipt":
                    {

                        model.RepoertName = "Appointment List";
                        string[] headerList = { };
                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPRefundBillReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptIPRefundofBillPrint", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDCurrentAdmittedDoctorWiseCharges", "IPDCurrentAdmittedDoctorWiseCharges", Orientation.Landscape);
                        break;
                    }
                #endregion



                #region :: IPDCurrentwardwisecharges ::
                case "IPDCurrentwardwisecharges":
                    {
                        model.RepoertName = "Current Ward Wise Charges Report ";
                        string[] headerList = { "Sr.No", "UHID", "IPDNo", "RoomName", "PatientName", "AdmissionTime", "DoctorName", "RefDoctorName", "BedName", "ChargesAmount", "AdvanceAmount", "BalPayAmt" };
                        string[] colList = { "RegID","IPDNo", "RoomName", "PatientName","AdmissionTime","DoctorName", "RefDoctorName", "BedName", "ChargesAmount", "AdvanceAmount", "BalPayAmt" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptCurrentAdmittedListReportwithCharges", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "wardWisechargesReport", "wardWisechargesReport", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: IPDRefrancefDoctorwise ::
                case "IPDRefrancefDoctorwise":
                    {
                        model.RepoertName = "Ref DoctorWiseVisit Report ";
                        string[] headerList = { "Sr.No", "UHID", "IPDNo", "PatientName","Age", "AdmissionDate", "AdmittedDoctorName" };
                        string[] colList = { "RegNo", "IPDNo","PatientName", "AgeYear" , "AdmissionDate", "AdmittedDoctorName"};
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("RptRefDoctorWiseAdmission", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDrefDoctorwiseReport", "IPDrefDoctorwiseReport", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: IPDCurrentrefDoctorAdmissionList ::
                case "IPDCurrentrefDoctorAdmissionList":
                    {
                        model.RepoertName = "RefDoctorWise Report  ";
                        string[] headerList = { "Sr.No","AdmissionId", "UHID", "PatientName", "DoctorName", "Admission Date" };
                        string[] colList = { "AdmissionID", "RegNo", "PatientName", "DoctorName", "AdmissionDate" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("RptCurrentRefAdmittedReport", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDRefDoctorWiseReport", "IPDRefDoctorWiseReport", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: IPDoctorWiseCountSummary ::
                case "IPDoctorWiseCountSummary":
                    {
                        model.RepoertName = "Doctor Wise Count List";
                        string[] headerList = { "Sr.No", "Doctor Name", "Total Count" };
                        string[] colList = { "DocName", "Lbl" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("Rtrv_IPDocWsSumry", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDoctorWiseVisitCountSummary", "IPDoctorWiseVisitCountSummary", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: Dischargetypewise ::
                case "Dischargetypewise":
                    {
                        model.RepoertName = "Dischargetype wise List";
                        string[] headerList = { "Sr.No", "AdmissionDate", "DischargeTime", "DischargeTypeName", "IPDNo", "PatientName", "MobileNo", "DoctorName", "RefDoctorName", "DepartmentName", "Diagnosis"};
                        string[] colList = { "AdmissionDate", "DischargeTime", "DischargeTypeName", "IPDNo", "PatientName", "MobileNo", "DoctorName", "RefDoctorName", "DepartmentName", "Diagnosis" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDischargeTypeReport", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDDischargeTypeReport", "IPDDischargeTypeReport", Orientation.Landscape);
                        break;
                    }
                #endregion
                #region :: Dischargetypecompanywise ::
                case "Dischargetypecompanywise":
                    {
                        model.RepoertName = "Dischargetype Company wise List";
                        string[] headerList = { "Sr.No", "AdmissionDate", "DischargeTime", "DischargeTypeName", "IPDNo", "PatientName", "MobileNo", "DoctorName", "RefDoctorName", "DepartmentName", "Diagnosis" };
                        string[] colList = { "AdmissionDate", "DischargeTime", "DischargeTypeName", "IPDNo", "PatientName", "MobileNo", "DoctorName", "RefDoctorName", "DepartmentName", "Diagnosis" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDischargeTypeReport", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDDischargeTypeCompanywiseReport", "IPDDischargeTypeCompanywiseReport", Orientation.Landscape);
                        break;
                    }
                #endregion

                #region :: DepartmentwiseCount ::
                case "DepartmentwiseCount":
                    {
                        model.RepoertName = "IPD Department Wise Count List";
                        string[] headerList = { "Sr.No", "Department Name" ,"Total Count", };
                        string[] colList = { "DepartmentName", "Lbl" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("Rtrv_IPDepartWsSumry", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDDepartmentwisecountsummary", "IPDDepartmentwisecountsummary", Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: IPDDischargewithmarkstatus ::
                case "IPDDischargewithmarkstatus":
                    {
                        model.RepoertName = "Discharge Report With Mark Status List";
                        string[] headerList = { "Sr.No", "UHID", "PatientName", "DOA", "DOT", "Mark Date-Time", "DOD", "DOT","Time In Hr" };
                        string[] colList = { "RegNo", "PatientName", "AdmissionDate", "AdmissionTime", "MarkStatus", "DischargeDate", "DischargeTime", "DiffTimeInHr" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptIPDischargeMarkStatusReport", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDDischargewithmarkstatus", "IPDDischargewithmarkstatus", Orientation.Landscape);
                        break;
                    }
                #endregion
                default:

               
                    break;


            }
            string byteFile = Convert.ToBase64String(tuple.Item1);
            return byteFile;

        }

        public string GetNewReportSetByProc(ReportNewRequestModel model)
        {
            var tuple = new Tuple<byte[], string>(null, string.Empty);
            string[] headerList = model.headerList;
            string[] colList = model.colList;
            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", model.htmlFilePath);
            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", model.htmlHeaderFilePath);
            var html = GetHTMLViewer(model.SPName, model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
            tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, model.FolderName, model.FileName, Orientation.Portrait, PaperKind.A4);
            string byteFile = Convert.ToBase64String(tuple.Item1);
            return byteFile;

        }

        private string GetHTMLViewer(string sp_Name, ReportNewRequestModel model, string htmlFilePath, string htmlHeaderFilePath, string[] colList, string[] headerList = null, string[] totalColList = null, string groupByCol = "")
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
                    Value = ((property.Key == "FromDate" || property.Key == "ToDate") ? DateTime.ParseExact(property.Value, "dd-MM-yyyy", CultureInfo.InvariantCulture) : property.Value.ToString())
                };

                para[sp_Para] = param;
                sp_Para++;
            }
            var dt = odal.FetchDataTableBySP(sp_Name, para);


            //string htmlHeader = _pdfUtility.GetHeader(htmlHeaderFilePath, model.BaseUrl);

            string html = File.ReadAllText(htmlFilePath);
            //html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{RepoertName}}", model.RepoertName);

            DateTime FromDate = Convert.ToDateTime(model.SearchFields.Find(x => x.FieldName?.ToLower() == "fromdate".ToLower())?.FieldValue ?? null);
            DateTime ToDate = Convert.ToDateTime(model.SearchFields.Find(x => x.FieldName?.ToLower() == "todate".ToLower())?.FieldValue ?? null);

            StringBuilder HeaderItems = new("");
            StringBuilder items = new("");
            StringBuilder ItemsTotal = new("");
            double T_Count = 0;
            switch (model.htmlFilePath)
            {
                // Simple Report Format
                case "SimpleReportFormat.html":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            k++;

                            items.Append("<tr style=\"text-align: Left; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: Left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                        }
                        html = html.Replace("{{T_Count}}", T_Count.ToString());
                    }
                    break;
                case "SimpleTotalReportFormat.html":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            k++;

                            items.Append("<tr style=\"text-align: Left; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: Left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                            T_Count += dr["Lbl"].ConvertToDouble();
                        }
                        html = html.Replace("{{T_Count}}", T_Count.ToString());
                    }
                    break;
                case "MultiTotalReportFormat.html":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int i = 0, j = 0;
                        double Dcount = 0;
                        string previousLabel = "";
                        int k = 0;
                        var dynamicVariable = new Dictionary<string, double>();
                        if (totalColList != null)
                        {
                            foreach (var colName in totalColList)
                            {
                                if (!string.IsNullOrEmpty(colName))
                                {
                                    dynamicVariable.Add(colName, 0);
                                }
                            }
                        }

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++; j++;
                            if (i == 1)
                            {
                                String Label;
                                Label = dr[groupByCol].ConvertToString();
                                items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                            }
                            if (previousLabel != "" && previousLabel != dr[groupByCol].ConvertToString())
                            {
                                j = 1;
                                items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                                   .Append(Dcount.ToString()).Append("</td></tr>");
                                Dcount = 0;
                                items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["PatientOldAndNew"].ConvertToString()).Append("</td></tr>");
                            }

                            Dcount = Dcount + 1;
                            T_Count = T_Count + 1;

                            previousLabel = dr[groupByCol].ConvertToString();
                            items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                            items.Append("</tr>");
                            if (totalColList != null)
                            {
                                foreach (var colName in totalColList)
                                {
                                    if (!string.IsNullOrEmpty(colName) && colName != "lableTotal")
                                        dynamicVariable[colName] += dr[colName].ConvertToDouble();
                                }
                            }
                            //T_Count += dr["PatientName"].ConvertToDouble();
                            if (totalColList == null)
                            {
                                if (dt.Rows.Count > 0 && dt.Rows.Count == i)
                                {
                                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(Dcount.ToString()).Append("</td></tr>");
                                }
                            }
                        }
                        if (totalColList != null)
                        {
                            ItemsTotal.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'>");
                            foreach (var colName in totalColList)
                            {
                                if (colName == "lableTotal")
                                    ItemsTotal.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">Total</td>");
                                else if (!string.IsNullOrEmpty(colName))
                                    ItemsTotal.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dynamicVariable[colName].ToString()).Append("</td>");
                                else
                                    ItemsTotal.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"></td>");
                            }
                            ItemsTotal.Append("</tr>");
                        }
                    }
                    break;
            }

            if (!string.IsNullOrEmpty(T_Count.ToString()))
                html = html.Replace("{{T_Count}}", T_Count.ToString());

            html = html.Replace("{{HeaderItems}}", HeaderItems.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{ItemsTotal}}", ItemsTotal.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }


        private string GetHTMLView(string sp_Name, ReportRequestModel model, string htmlFilePath, string htmlHeaderFilePath, string[] colList, string[] headerList = null, string[] totalColList = null, string groupByCol = "")
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


            //string htmlHeader = _pdfUtility.GetHeader(htmlHeaderFilePath, model.BaseUrl);

            string html = File.ReadAllText(htmlFilePath);
            //html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{RepoertName}}", model.RepoertName);

            DateTime FromDate = Convert.ToDateTime(model.SearchFields.Find(x => x.FieldName?.ToLower() == "fromdate".ToLower())?.FieldValue ?? null);
            DateTime ToDate = Convert.ToDateTime(model.SearchFields.Find(x => x.FieldName?.ToLower() == "todate".ToLower())?.FieldValue ?? null);

            StringBuilder HeaderItems = new("");
            StringBuilder items = new("");
            StringBuilder ItemsTotal = new("");
            double T_Count = 0;
            switch (model.Mode)
            {
                // Simple Report Format
                //case "RegistrationReport":
                case "AppointmentListReport":
                //case "DepartmentWisecountSummury":
                case "OPDoctorWiseVisitCountSummary":
                case "OPAppoinmentListWithServiseAvailed":
                case "CrossConsultationReport":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            k++;

                            items.Append("<tr style=\"text-align: Left; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: Left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                            if (model.Mode == "DepartmentWisecountSummury" || model.Mode == "OPDoctorWiseVisitCountSummary")
                                if (model.Mode == "OPDoctorWiseVisitCountSummary")
                                    T_Count += dr["Lbl"].ConvertToDouble();
                        }
                        html = html.Replace("{{T_Count}}", T_Count.ToString());

                    }
                    break;
                case "RegistrationReport":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            k++;

                            items.Append("<tr style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                            if (model.Mode == "RegistrationReport" || model.Mode == "RegistrationReport")
                                if (model.Mode == "RegistrationReport") ;
                            //T_Count += dr["PatientName"].ConvertToDouble();
                        }
                    }
                    break;



                case "DayWiseOpdCountSummry":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            k++;

                            items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                            if (model.Mode == "DayWiseOpdCountDetails" || model.Mode == "DayWiseOpdCountSummry")
                                T_Count += dr["DateWiseCount"].ConvertToDouble();

                        }
                        html = html.Replace("{{T_Count}}", T_Count.ToString());

                    }

                    break;


                //Sir Code//
                //case "DepartmentWiseOPDCount":
                //    {
                //        HeaderItems.Append("<tr>");
                //        foreach (var hr in headerList)
                //        {
                //            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                //            HeaderItems.Append(hr.ConvertToString());
                //            HeaderItems.Append("</th>");
                //        }
                //        HeaderItems.Append("</tr>");

                //        int k = 0;
                //        foreach (DataRow dr in dt.Rows)
                //        {
                //            k++;

                //            items.Append("<tr style=\"text-align: Left; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                //            foreach (var colName in colList)
                //            {
                //                items.Append("<td style=\"text-align: Left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                //            }
                //            if (model.Mode == "DepartmentWiseOPDCount" || model.Mode == "DepartmentWiseOPDCount")
                //                T_Count += dr["VisitCount"].ConvertToDouble();

                //        }
                //        html = html.Replace("{{T_Count}}", T_Count.ToString());


                //    }
                //    break;

                //MY CODE//
                case "DepartmentWiseOPDCount":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #000;font-size:20px;padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0, i = 0, D = 0, j = 0;
                        string previousLabel = "";
                        var groupedData = dt.AsEnumerable().GroupBy(row => row["DepartmentName"]).ToList();
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++; j++;
                            if (i == 1)
                            {
                                String Label;
                                Label = dr["DepartmentName"].ConvertToString();
                                items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"4\" style=\"border:1px solid #000;font-weight:bold;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");

                            }

                            if (previousLabel != "" && previousLabel != dr["DepartmentName"].ConvertToString())
                            {
                                j = 1;

                                items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='3' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                                 .Append(D.ToString()).Append("</td></tr>");


                                D = 0;
                                items.Append("<tr style=\"font-size:20px;border-bottom: 1px;border-right: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;font-weight:bold;vertical-align:middle\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td></tr>");
                            }
                            D = D + 1;
                            previousLabel = dr["DepartmentName"].ConvertToString();

                            items.Append("<tr style=\"text-align: center; border: 1px solid #000; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #000; padding: 6px;\">").Append(j).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #000; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }

                            if (dt.Rows.Count > 0 && dt.Rows.Count == i)
                            {

                                items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='3' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                                     .Append(D.ToString()).Append("</td></tr>");

                            }

                        }


                    }
                    break;


                case "DepartmentWiseOpdCountSummary":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            k++;

                            items.Append("<tr style=\"text-align: Left; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: Left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                            if (model.Mode == "DepartmentWiseOpdCountSummary" || model.Mode == "DepartmentWiseOpdCountSummary")
                                T_Count += dr["VisitCount"].ConvertToDouble();

                        }
                        html = html.Replace("{{T_Count}}", T_Count.ToString());

                    }
                    break;

                //case "DrWiseOPDCountDetail":
                //    {
                //        HeaderItems.Append("<tr>");
                //        foreach (var hr in headerList)
                //        {
                //            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                //            HeaderItems.Append(hr.ConvertToString());
                //            HeaderItems.Append("</th>");
                //        }
                //        HeaderItems.Append("</tr>");

                //        int k = 0;
                //        foreach (DataRow dr in dt.Rows)
                //        {
                //            k++;

                //            items.Append("<tr style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                //            foreach (var colName in colList)
                //            {
                //                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                //            }
                //            if (model.Mode == "DrWiseOPDCountDetail" || model.Mode == "DrWiseOPDCountDetail")
                //                T_Count += dr["VisitCount"].ConvertToDouble();

                //        }
                //        html = html.Replace("{{T_Count}}", T_Count.ToString());

                //    }
                //    break;

                case "DrWiseOPDCountDetail":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #000;font-size:20px;padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0, i = 0, D = 0, j = 0;
                        string previousLabel = "";
                        var groupedData = dt.AsEnumerable().GroupBy(row => row["DoctorName"]).ToList();
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++; j++;
                            if (i == 1)
                            {
                                String Label;
                                Label = dr["DoctorName"].ConvertToString();
                                items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"8\" style=\"border:1px solid #000;font-weight:bold;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");

                            }

                            if (previousLabel != "" && previousLabel != dr["DoctorName"].ConvertToString())
                            {
                                j = 1;

                                items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='7' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                                 .Append(D.ToString()).Append("</td></tr>");


                                D = 0;
                                items.Append("<tr style=\"font-size:20px;border-bottom: 1px;border-right: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"8\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;font-weight:bold;vertical-align:middle\">").Append(dr["DoctorName"].ConvertToString()).Append("</td></tr>");
                            }
                            D = D + 1;
                            previousLabel = dr["DoctorName"].ConvertToString();

                            items.Append("<tr style=\"text-align: center; border: 1px solid #000; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #000; padding: 6px;\">").Append(j).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #000; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }

                            if (dt.Rows.Count > 0 && dt.Rows.Count == i)
                            {

                                items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='7' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                                     .Append(D.ToString()).Append("</td></tr>");

                            }

                        }


                    }
                    break;
                case "DoctorWiseOpdCountSummary":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            k++;

                            items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                            if (model.Mode == "DoctorWiseOpdCountSummary" || model.Mode == "DoctorWiseOpdCountSummary")
                                T_Count += dr["VisitCount"].ConvertToDouble();

                        }
                        html = html.Replace("{{T_Count}}", T_Count.ToString());

                    }
                    break;
                case "DrWiseOPDCollectionDetails":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            k++;

                            items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                            if (model.Mode == "DrWiseOPDCollectionDetails" || model.Mode == "DrWiseOPDCollectionDetails")
                                T_Count += dr["NetPayableAmt"].ConvertToDouble();

                        }
                        html = html.Replace("{{T_Count}}", T_Count.ToString());

                    }
                    break;
                case "DoctorWiseOpdCollectionSummary":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            k++;

                            items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                            if (model.Mode == "DoctorWiseOpdCollectionSummary" || model.Mode == "DoctorWiseOpdCollectionSummary")
                                T_Count += dr["Amount"].ConvertToDouble();

                        }
                        html = html.Replace("{{T_Count}}", T_Count.ToString());

                    }
                    break;

                case "DepartmentWiseOPDCollectionDetails":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            k++;

                            items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                            if (model.Mode == "DepartmentWiseOPDCollectionDetails" || model.Mode == "DepartmentWiseOPDCollectionDetails")
                                T_Count += dr["NetPayableAmt"].ConvertToDouble();

                        }
                        html = html.Replace("{{T_Count}}", T_Count.ToString());

                    }
                    break;

                case "DepartmentWiseOpdCollectionSummary":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            k++;

                            items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                            if (model.Mode == "DepartmentWiseOpdCollectionSummary" || model.Mode == "DepartmentWiseOpdCollectionSummary")
                                T_Count += dr["DepartmentName"].ConvertToDouble();

                        }
                        html = html.Replace("{{T_Count}}", T_Count.ToString());

                    }
                    break;

                case "DepartmentServiceGroupWiseCollectionDetails":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            k++;

                            items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                            if (model.Mode == "DepartmentServiceGroupWiseCollectionDetails" || model.Mode == "DepartmentServiceGroupWiseCollectionDetails")
                                T_Count += dr["NetAmount"].ConvertToDouble();

                        }
                        html = html.Replace("{{T_Count}}", T_Count.ToString());

                    }
                    break;
                case "OPCollectionSummary":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0;
                        var dynamicVariable = new Dictionary<string, double>();
                        foreach (var colName in totalColList)
                        {
                            if (!string.IsNullOrEmpty(colName))
                            {
                                dynamicVariable.Add(colName, 0);
                            }
                        }

                        foreach (DataRow dr in dt.Rows)
                        {
                            k++;

                            items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                if (colName.ToLower().IndexOf("date") == -1)
                                    items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                                else
                                    items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                            }
                            items.Append("</tr>");

                            foreach (var colName in totalColList)
                            {
                                if (!string.IsNullOrEmpty(colName) && colName != "lableTotal")
                                    dynamicVariable[colName] += dr[colName].ConvertToDouble();
                            }
                        }

                        ItemsTotal.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'>");
                        foreach (var colName in totalColList)
                        {
                            if (colName == "lableTotal")
                                ItemsTotal.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">Total</td>");
                            else if (!string.IsNullOrEmpty(colName))
                                ItemsTotal.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dynamicVariable[colName].ToString()).Append("</td>");
                            else
                                ItemsTotal.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"></td>");
                        }
                        ItemsTotal.Append("</tr>");
                    }
                    break;




                case "DoctorWiseVisitReport":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #000; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0;
                        string previousLabel = string.Empty;
                        int i = 0, j = 0, D = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++; j++;
                            if (i == 1)
                            {
                                String Label;
                                Label = dr["RefDoctorName"].ConvertToString();
                                items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");

                            }

                            if (previousLabel != "" && previousLabel != dr["RefDoctorName"].ConvertToString())
                            {
                                j = 1;

                                items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                                 .Append(D.ToString()).Append("</td></tr>");


                                D = 0;
                                items.Append("<tr style=\"font-size:20px;border-bottom: 1px;border-right: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");
                            }
                            D = D + 1;
                            previousLabel = dr["RefDoctorName"].ConvertToString();

                            items.Append("<tr style=\"text-align: center; border: 1px solid #000; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #000; padding: 6px;\">").Append(j).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #000; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }

                            if (dt.Rows.Count > 0 && dt.Rows.Count == i)
                            {

                                items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                                     .Append(D.ToString()).Append("</td></tr>");

                            }

                        }
                    }
                    break;
                case "RefDoctorWiseReport":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #000; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0;
                        string previousLabel = string.Empty;
                        int i = 0, j = 0, D = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++; j++;
                            if (i == 1)
                            {
                                String Label;
                                Label = dr["RefDoctorName"].ConvertToString();
                                items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");

                            }

                            if (previousLabel != "" && previousLabel != dr["RefDoctorName"].ConvertToString())
                            {
                                j = 1;

                                items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                                 .Append(D.ToString()).Append("</td></tr>");


                                D = 0;
                                items.Append("<tr style=\"font-size:20px;border-bottom: 1px;border-right: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");
                            }
                            D = D + 1;
                            previousLabel = dr["RefDoctorName"].ConvertToString();

                            items.Append("<tr style=\"text-align: center; border: 1px solid #000; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #000; padding: 6px;\">").Append(j).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #000; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }

                            if (dt.Rows.Count > 0 && dt.Rows.Count == i)
                            {

                                items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='4' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                                     .Append(D.ToString()).Append("</td></tr>");

                            }

                        }
                    }
                    break;
                case "OPDailyCollectionReport":
                case "OPDoctorWiseNewOldPatientReport":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int i = 0, j = 0;
                        double Dcount = 0;
                        string previousLabel = "";
                        int k = 0;
                        var dynamicVariable = new Dictionary<string, double>();
                        if (totalColList != null)
                        {
                            foreach (var colName in totalColList)
                            {
                                if (!string.IsNullOrEmpty(colName))
                                {
                                    dynamicVariable.Add(colName, 0);
                                }
                            }
                        }

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++; j++;
                            if (i == 1)
                            {
                                String Label;
                                Label = dr[groupByCol].ConvertToString();
                                items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                            }
                            if (previousLabel != "" && previousLabel != dr[groupByCol].ConvertToString())
                            {
                                j = 1;
                                items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                                   .Append(Dcount.ToString()).Append("</td></tr>");
                                Dcount = 0;
                                items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["PatientOldAndNew"].ConvertToString()).Append("</td></tr>");
                            }

                            Dcount = Dcount + 1;
                            T_Count = T_Count + 1;

                            previousLabel = dr[groupByCol].ConvertToString();
                            items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                            items.Append("</tr>");
                            if (totalColList != null)
                            {
                                foreach (var colName in totalColList)
                                {
                                    if (!string.IsNullOrEmpty(colName) && colName != "lableTotal")
                                        dynamicVariable[colName] += dr[colName].ConvertToDouble();
                                }
                            }
                            //T_Count += dr["PatientName"].ConvertToDouble();
                            if (totalColList == null)
                            {
                                if (dt.Rows.Count > 0 && dt.Rows.Count == i)
                                {
                                    items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(Dcount.ToString()).Append("</td></tr>");
                                }
                            }
                        }
                        if (totalColList != null)
                        {
                            ItemsTotal.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'>");
                            foreach (var colName in totalColList)
                            {
                                if (colName == "lableTotal")
                                    ItemsTotal.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">Total</td>");
                                else if (!string.IsNullOrEmpty(colName))
                                    ItemsTotal.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dynamicVariable[colName].ToString()).Append("</td>");
                                else
                                    ItemsTotal.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"></td>");
                            }
                            ItemsTotal.Append("</tr>");
                        }
                    }
                    break;
                case "OPPaymentReceipt":
                    {
                      

                        int i = 0, j = 0;
                        double Dcount = 0;
                        string previousLabel = "";
                        int k = 0;
                        var dynamicVariable = new Dictionary<string, double>();
                        foreach (DataRow dr in dt.Rows)
                        {

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




                            string finalamt = conversion(dt.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace().ToString());
                            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());

                        }




                    }
                    break;
                case "OpBillReceipt":
                    {


                        int i = 0, j = 0;
                        double Dcount = 0;
                        string previousLabel = "";
                        int k = 0;
                        var dynamicVariable = new Dictionary<string, double>();
                        foreach (DataRow dr in dt.Rows)
                        {

                            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                            //html = html.Replace("{{NewHeader}}", htmlHeader);
                            html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                            html = html.Replace("{{TotalBillAmount}}", dt.GetColValue("TotalBillAmount").ConvertToDouble().To2DecimalPlace());
                            html = html.Replace("{{ConcessionAmt}}", dt.GetColValue("ConcessionAmt").ConvertToDouble().To2DecimalPlace());
                            html = html.Replace("{{ConsultantDocName}}", dt.GetColValue("ConsultantDocName"));
                            html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                            html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName"));
                            html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                            html = html.Replace("{{BillNo}}", dt.GetColValue("BillNo"));
                            html = html.Replace("{{BillDate}}", dt.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | H:mm tt"));
                            html = html.Replace("{{PayMode}}", dt.GetColValue("PayMode"));
                            html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                            html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                            html = html.Replace("{{ExtMobileNo}}", dt.GetColValue("ExtMobileNo"));
                            html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                            html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                            html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                            html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                            html = html.Replace("{{Date}}", dt.GetDateColValue("Date").ConvertToDateString());
                            html = html.Replace("{{VisitDate}}", dt.GetColValue("VisitTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                            html = html.Replace("{{PhoneNo}}", dt.GetColValue("PhoneNo"));
                            html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                            html = html.Replace("{{OPDNo}}", dt.GetColValue("OPDNo"));

                            double T_NetAmount = 0;
                            {
                                i++;
                                items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size:15;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Price"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");

                                T_NetAmount += dr["NetAmount"].ConvertToDouble();
                            }
                            T_NetAmount = Math.Round(T_NetAmount);

                            html = html.Replace("{{Items}}", items.ToString());


                            html = html.Replace("{{T_NetAmount}}", T_NetAmount.ConvertToDouble().To2DecimalPlace());
                            html = html.Replace("{{TotalBillAmount}}", dt.GetColValue("TotalBillAmount").ConvertToDouble().To2DecimalPlace());

                            html = html.Replace("{{BalanceAmt}}", dt.GetColValue("BalanceAmt").ConvertToDouble().To2DecimalPlace());
                            html = html.Replace("{{PaidAmount}}", dt.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace());
                            html = html.Replace("{{Price}}", dt.GetColValue("Price").ConvertToDouble().To2DecimalPlace());
                            html = html.Replace("{{TotalGst}}", dt.GetColValue("TotalGst").ConvertToDouble().To2DecimalPlace());
                            html = html.Replace("{{NetPayableAmt}}", dt.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace());
                            html = html.Replace("{{UserName}}", dt.GetColValue("AddedByName").ConvertToString());
                            html = html.Replace("{{HospitalName}}", dt.GetColValue("HospitalName").ConvertToString());
                            html = html.Replace("{{DiscComments}}", dt.GetColValue("DiscComments").ConvertToString());


                            html = html.Replace("{{chkpaidflag}}", dt.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                            html = html.Replace("{{chkbalflag}}", dt.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "table-row " : "none");

                            html = html.Replace("{{chkdiscflag}}", dt.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");

                            //html = html.Replace("{{chkRefdrflag}}", Bills.GetColValue("chkRefdrflag").ConvertToDouble() != ' ' ? "table-row " : "none");
                            //html = html.Replace("{{chkCompanyflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() != ' ' ? "table-row " : "none");


                            string finalamt = conversion(T_NetAmount.ToString());
                            html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


                            return html;

                        }




                    }
                    break;
                case "AppointmentReceipt":
                    {


                        int i = 0, j = 0;
                        double Dcount = 0;
                        string previousLabel = "";
                        int k = 0;
                        var dynamicVariable = new Dictionary<string, double>();
                        foreach (DataRow dr in dt.Rows)
                        {


                            html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                            html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                            html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                            html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                            html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                            html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                            html = html.Replace("{{VisitDate}}", dt.GetColValue("VisitTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                            html = html.Replace("{{OPDNo}}", dt.GetColValue("OPDNo"));
                            html = html.Replace("{{ConsultantDoctorName}}", dt.GetColValue("ConsultantDoctorName"));

                            html = html.Replace("{{Address}}", dt.GetColValue("Address"));
                            html = html.Replace("{{Expr1}}", dt.GetColValue("Expr1"));
                            html = html.Replace("{{MobileNo}}", dt.GetColValue("MobileNo"));
                            html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                            html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));


                            html = html.Replace("{{AdmittedDoctorName}}", dt.GetColValue("AdmittedDoctorName"));
                            html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName"));

                            html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                            html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));

                            html = html.Replace("{{RelativeName}}", dt.GetColValue("RelativeName"));
                            html = html.Replace("{{RelativePhoneNo}}", dt.GetColValue("RelativePhoneNo"));

                            html = html.Replace("{{RelationshipName}}", dt.GetColValue("RelationshipName"));
                            html = html.Replace("{{OPDNo}}", dt.GetColValue("OPDNo"));
                            html = html.Replace("{{IsMLC}}", dt.GetColValue("IsMLC"));

                            html = html.Replace("{{AdmittedDoctor2}}", dt.GetColValue("AdmittedDoctor2"));
                            html = html.Replace("{{LoginUserSurname}}", dt.GetColValue("LoginUserSurname"));
;

                        }




                    }
                    break;

                case "DoctorNotesReceipt":
                    {


                        int i = 0, j = 0;
                        double Dcount = 0;
                        string previousLabel = "";
                        int k = 0;
                        var dynamicVariable = new Dictionary<string, double>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr style\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TTime"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["DoctorsNotes"].ConvertToString()).Append("</td></tr>");

                        }
                        html = html.Replace("{{Items}}", items.ToString());
                            html = html.Replace("{{AdmID}}", dt.GetColValue("AdmId"));
                            html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                            html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                            html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                            html = html.Replace("{{TDate}}", dt.GetColValue("TDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                            html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                            html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                            html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                            html = html.Replace("{{WardName}}", dt.GetColValue("WardName"));
                            html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                            html = html.Replace("{{BedNo}}", dt.GetColValue("BedNo"));
                            html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                            html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                            html = html.Replace("{{RefDoctorName}}", dt.GetColValue("RefDoctorName"));
                            html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                            html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));

                            return html;


                            //string finalamt = conversion(dt.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace().ToString());
                            //html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());

                        }


                 break;

                case "NursingNotesReceipt":
                    {


                        int i = 0, j = 0;
                        double Dcount = 0;
                        string previousLabel = "";
                        int k = 0;
                        var dynamicVariable = new Dictionary<string, double>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr style\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TTime"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["NursingNotes"].ConvertToString()).Append("</td></tr>");

                            html = html.Replace("{{Items}}", items.ToString());
                            html = html.Replace("{{AdmId}}", dt.GetColValue("AdmId"));
                            html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                            //   html = html.Replace("{{AdmId}}", Bills.GetColValue("AdmId"));
                            html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                            html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                            html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                            html = html.Replace("{{TTime}}", dt.GetColValue("TTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                            html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                            html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                            html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                            html = html.Replace("{{WardName}}", dt.GetColValue("WardName"));
                            html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                            html = html.Replace("{{BedNo}}", dt.GetColValue("BedNo"));
                            html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                            html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                            html = html.Replace("{{RefDoctorName}}", dt.GetColValue("RefDoctorName"));
                            html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                            html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));

                            return html;


                            //string finalamt = conversion(dt.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace().ToString());
                            //html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());

                        }




                    }
                    break;


                case "DoctorPatientHandoverReceipt":
                    {


                        int i = 0, j = 0;
                        double Dcount = 0;
                        string previousLabel = "";
                        int k = 0;
                        var dynamicVariable = new Dictionary<string, double>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr style\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TTime"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ShiftInfo"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatHand_I"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatHand_S"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatHand_B"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatHand_A"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["PatHand_R"].ConvertToString()).Append("</td></tr>");

                        }
                        html = html.Replace("{{Items}}", items.ToString());
                        html = html.Replace("{{AdmID}}", dt.GetColValue("AdmId"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{TDate}}", dt.GetColValue("TDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{WardName}}", dt.GetColValue("WardName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{BedNo}}", dt.GetColValue("BedNo"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{RefDoctorName}}", dt.GetColValue("RefDoctorName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));


                        return html;



                    }


                    break;
                case "OPRefundReceipt":
                    {
                        
                        int i = 0, j = 0;
                        var Bills = dt;

                        html = html.Replace("{{BillNo}}", Bills.GetColValue("BillNo"));
                        html = html.Replace("{{PatientName}}", Bills.GetColValue("PatientName"));
                        html = html.Replace("{{RegNo}}", Bills.GetColValue("RegNo"));
                        html = html.Replace("{{CashPayAmount}}", Bills.GetColValue("CashPayAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{ChequePayAmount}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{ChequeDate}}", Bills.GetColValue("ChequeDate").ConvertToDateString("dd/MM/yyyy"));
                        html = html.Replace("{{ChequeNo}}", Bills.GetColValue("ChequeNo"));

                        html = html.Replace("{{CardPayAmount}}", Bills.GetColValue("CardPayAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{CardDate}}", Bills.GetColValue("CardDate").ConvertToDateString("dd/MM/yyyy"));
                        html = html.Replace("{{CardNo}}", Bills.GetColValue("CardNo"));
                        html = html.Replace("{{CardBankName}}", Bills.GetColValue("CardBankName"));
                        html = html.Replace("{{NEFTPayAmount}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{NEFTNo}}", Bills.GetColValue("NEFTNo"));
                        html = html.Replace("{{NEFTBankMaster}}", Bills.GetColValue("NEFTBankMaster"));
                        html = html.Replace("{{PayTMAmount}}", Bills.GetColValue("PayTMAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{PayTMTranNo}}", Bills.GetColValue("PayTMTranNo"));
                        html = html.Replace("{{PaidAmount}}", Bills.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{BillDate}}", Bills.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{ReceiptNo}}", Bills.GetColValue("ReceiptNo"));
                        html = html.Replace("{{UserName}}", Bills.GetColValue("UserName"));
                        html = html.Replace("{{Remark}}", Bills.GetColValue("Remark"));
                        html = html.Replace("{{PaymentTime}}", Bills.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | HH:mm tt"));


                        html = html.Replace("{{ConsultantDocName}}", Bills.GetColValue("DoctorName"));
                        html = html.Replace("{{DepartmentName}}", Bills.GetColValue("DepartmentName"));
                        html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDoctorName"));
                        html = html.Replace("{{AgeYear}}", Bills.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", Bills.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", Bills.GetColValue("AgeDay"));
                        html = html.Replace("{{GenderName}}", Bills.GetColValue("GenderName"));
                        html = html.Replace("{{Date}}", Bills.GetDateColValue("Date").ConvertToDateString());
                        html = html.Replace("{{VisitDate}}", Bills.GetColValue("VisitTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        html = html.Replace("{{TotalAmt}}", Bills.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{ConcessionAmt}}", Bills.GetColValue("ConcessionAmt").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{NetPayableAmt}}", Bills.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace());


                        html = html.Replace("{{chkcashflag}}", Bills.GetColValue("CashPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkcardflag}}", Bills.GetColValue("CardPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkchequeflag}}", Bills.GetColValue("ChequePayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkneftflag}}", Bills.GetColValue("NEFTPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkpaytmflag}}", Bills.GetColValue("PayTMAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkremarkflag}}", Bills.GetColValue("Remark").ConvertToDouble() != ' ' ? "table-row " : "none");

                        //string finalamt = conversion(Bills.GetColValue("RefundAmount").ConvertToDouble().To2DecimalPlace().ToString());
                        //html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());



                    }
                    break;


                case "BillReportSummary":
                case "DayWiseOpdCountDetails":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0;
                        var dynamicVariable = new Dictionary<string, double>();
                        foreach (var colName in totalColList)
                        {
                            if (!string.IsNullOrEmpty(colName))
                            {
                                dynamicVariable.Add(colName, 0);
                            }
                        }

                        foreach (DataRow dr in dt.Rows)
                        {
                            k++;

                            items.Append("<tr style=\"text-align: Left; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                if (colName.ToLower().IndexOf("date") == -1)
                                    items.Append("<td style=\"text-align: Left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                                else
                                    items.Append("<td style=\"text-align: Left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                            }
                            items.Append("</tr>");

                            foreach (var colName in totalColList)
                            {
                                if (!string.IsNullOrEmpty(colName) && colName != "lableTotal")
                                    dynamicVariable[colName] += dr[colName].ConvertToDouble();
                            }
                        }

                        ItemsTotal.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'>");
                        foreach (var colName in totalColList)
                        {
                            if (colName == "lableTotal")
                                ItemsTotal.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">Total</td>");
                            else if (!string.IsNullOrEmpty(colName))
                                ItemsTotal.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dynamicVariable[colName].ToString()).Append("</td>");
                            else
                                ItemsTotal.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"></td>");
                        }
                        ItemsTotal.Append("</tr>");
                    }
                    break;


                //Ip Section

                case "IpCasepaperReport":
                    {


                        foreach (DataRow dr in dt.Rows)
                        {
                            //html = html.Replace("{{DataContent}}", htmlHeader);


                            html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                            html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));

                            html = html.Replace("{{Address}}", dt.GetColValue("Address"));
                            html = html.Replace("{{MobileNo}}", dt.GetColValue("MobileNo"));
                            html = html.Replace("{{PhoneNo}}", dt.GetColValue("PhoneNo"));

                            html = html.Replace("{{DOT}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yy hh:mm tt"));
                            html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));

                            html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                            html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));

                            html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                            html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                            html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                            html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));


                            html = html.Replace("{{AdmittedDoctorName}}", dt.GetColValue("AdmittedDoctorName"));
                            html = html.Replace("{{RefDoctorName}}", dt.GetColValue("RefDoctorName"));

                            html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                            html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));

                            html = html.Replace("{{RelativeName}}", dt.GetColValue("RelativeName"));
                            html = html.Replace("{{RelativePhoneNo}}", dt.GetColValue("RelativePhoneNo"));

                            html = html.Replace("{{RelationshipName}}", dt.GetColValue("RelationshipName"));
                            html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                            html = html.Replace("{{IsMLC}}", dt.GetColValue("IsMLC"));
                            html = html.Replace("{{AdmittedDoctor1}}", dt.GetColValue("AdmittedDoctor1"));
                            html = html.Replace("{{AdmittedDoctorName}}", dt.GetColValue("AdmittedDoctorName"));

                            html = html.Replace("{{MaritalStatusName}}", dt.GetColValue("MaritalStatusName"));
                            html = html.Replace("{{AadharcardNo}}", dt.GetColValue("AadharcardNo"));
                            html = html.Replace("{{TariffName}}", dt.GetColValue("TariffName"));
                            html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));

                            html = html.Replace("{{AdmittedDoctor1}}", dt.GetColValue("AdmittedDoctor1"));

                            //html = html.Replace("{{chkMLCflag}}", dt.GetColValue("IsMLC").ToBool() == true ? "table-row " : "none");
                            //html = html.Replace("{{chkMLCflag1}}", dt.GetColValue("IsMLC").ToBool() == false ? "table-row " : "none");

                            html = html.Replace("{{DOA}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));

                            html = html.Replace("{{AdmittedDoctor2}}", dt.GetColValue("AdmittedDoctor2"));
                            html = html.Replace("{{LoginUserSurname}}", dt.GetColValue("LoginUserSurname"));

                            return html;




                        }




                    }
                    break;



                case "IptemplateCasepaperReport":
                    {

                        string htmlHeade = "";
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                        
                        int i = 0;

                        html = html.Replace("{{DataContent}}", htmlHeade);


                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));

                        html = html.Replace("{{Address}}", dt.GetColValue("Address"));
                        html = html.Replace("{{MobileNo}}", dt.GetColValue("MobileNo"));
                        html = html.Replace("{{PhoneNo}}", dt.GetColValue("PhoneNo"));

                        html = html.Replace("{{DOT}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yy hh:mm tt"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));

                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));

                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));


                        html = html.Replace("{{AdmittedDoctorName}}", dt.GetColValue("AdmittedDoctorName"));
                        html = html.Replace("{{RefDoctorName}}", dt.GetColValue("RefDoctorName"));

                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));

                        html = html.Replace("{{RelativeName}}", dt.GetColValue("RelativeName"));
                        html = html.Replace("{{RelativePhoneNo}}", dt.GetColValue("RelativePhoneNo"));

                        html = html.Replace("{{RelationshipName}}", dt.GetColValue("RelationshipName"));
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{IsMLC}}", dt.GetColValue("IsMLC"));
                        html = html.Replace("{{AdmittedDoctor1}}", dt.GetColValue("AdmittedDoctor1"));
                        html = html.Replace("{{AdmittedDoctorName}}", dt.GetColValue("AdmittedDoctorName"));

                        html = html.Replace("{{MaritalStatusName}}", dt.GetColValue("MaritalStatusName"));
                        html = html.Replace("{{AadharcardNo}}", dt.GetColValue("AadharcardNo"));
                        html = html.Replace("{{TariffName}}", dt.GetColValue("TariffName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));

                        html = html.Replace("{{AdmittedDoctor1}}", dt.GetColValue("AdmittedDoctor1"));

                        //html = html.Replace("{{chkMLCflag}}", dt.GetColValue("IsMLC").ToBool() == true ? "table-row " : "none");
                        //html = html.Replace("{{chkMLCflag1}}", dt.GetColValue("IsMLC").ToBool() == false ? "table-row " : "none");

                        html = html.Replace("{{DOA}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));

                        html = html.Replace("{{AdmittedDoctor2}}", dt.GetColValue("AdmittedDoctor2"));
                        html = html.Replace("{{LoginUserSurname}}", dt.GetColValue("LoginUserSurname"));



                        //html = html.Replace("{{chkyearflag}}", dt.GetColValue("AgeYear").ToInt() == 0 ? "none" : "visible");
                        //html = html.Replace("{{chkmonthflag}}", dt.GetColValue("AgeMonth").ToInt() == 0 ? "none" : "visible");
                        //html = html.Replace("{{chkdayflag}}", dt.GetColValue("AgeDay").ToInt() == 0 ? "none" : "visible");

                        return html;


                        }

                    
                    break;

                case "IpFinalBill":
                    {
                       // StringBuilder items = new StringBuilder("");
                        int i = 0, j = 0;
                        String[] GroupName;
                        object GroupName1 = "";
                        Boolean chkcommflag = false, chkpaidflag = false, chkbalflag = false, chkdiscflag = false, chkAdvflag = false, chkadminchargeflag = false;
                        double T_NetAmount = 0, TotalNetPayAmt = 0, Tot_Advamt = 0, balafteradvuseAmount = 0, BalancewdudcAmt = 0;

                        string GroupLable = "";
                        string ClassLable = "";
                        String FinalLabel = "";
                        double T_TotAmount = 0, ChargesTotalamt = 0, T_TotalAmount = 0, F_TotalAmount = 0.0, AdminChares = 0, Tot_paidamt = 0;
                        double Tot = 0;
                        int length = dt.Rows.Count - 1;

                        foreach (DataRow dr in dt.Rows)
                        {

                            i++; j++;

                            T_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                            F_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                            ChargesTotalamt += dr["ChargesTotalAmt"].ConvertToDouble();

                            items.Append("<tr style=\"font-family: 'Helvetica Neue','Helvetica', Helvetica, Arial, sans-serif;font-size:22px;\"><td style=\"border: 1px solid #000; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ClassName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: right; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");

                            if (dt.Rows.Count > 0 && dt.Rows.Count == i && i != length + 1)
                            {
                                items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\"><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                                 .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

                            }

                            if (i == length + 1)
                            {

                                T_TotalAmount = T_TotalAmount - Tot;
                                items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\"><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                                .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

                            }


                            ClassLable = dr["ClassName"].ConvertToString();
                            GroupLable = dr["GroupName"].ConvertToString();

                            TotalNetPayAmt = dr["NetPayableAmt"].ConvertToDouble();
                            Tot_Advamt = dr["AdvanceUsedAmount"].ConvertToDouble();
                            Tot_paidamt = dr["PaidAmount"].ConvertToDouble();

                            if (Tot_Advamt.ConvertToDouble() < TotalNetPayAmt.ConvertToDouble())
                            {
                                BalancewdudcAmt = (TotalNetPayAmt - Tot_Advamt - Tot_paidamt).ConvertToDouble();
                            }

                        }


                        html = html.Replace("{{Items}}", items.ToString());

                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));

                        string finalamt = conversion(dt.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace().ToString());
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


                        html = html.Replace("{{BillNo}}", dt.GetColValue("PBillNo"));
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo").ToString());

                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{Age}}", dt.GetColValue("Age"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));


                        html = html.Replace("{{AdmissionDate}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName"));

                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));

                        html = html.Replace("{{DischargeDate}}", dt.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{BillDate}}", dt.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{PayMode}}", dt.GetColValue("PayMode"));
                        html = html.Replace("{{TotalBillAmount}}", dt.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{AdvanceUsedAmount}}", dt.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{PaidAmount}}", dt.GetColValue("PaidAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{TotalBillAmount}}", dt.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{BalanceAmt}}", dt.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{TotalAmt}}", dt.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{TaxAmount}}", dt.GetColValue("TaxAmount").ConvertToDouble().ToString("0.00"));

                        html = html.Replace("{{CardPayAmount}}", dt.GetColValue("CardPayAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{CashPayAmount}}", dt.GetColValue("CashPayAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{ChequePayAmount}}", dt.GetColValue("ChequePayAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{NEFTPayAmount}}", dt.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("0.00"));
                        // html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{PayTMPayAmount}}", dt.GetColValue("OnlinePayAmount").ConvertToDouble().ToString("0.00"));

                        html = html.Replace("{{TotalAdvanceAmount}}", dt.GetColValue("TotalAdvanceAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{AdvanceUsedAmount}}", dt.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{AdvanceBalAmount}}", dt.GetColValue("AdvanceBalAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{AdvanceRefundAmount}}", dt.GetColValue("AdvanceRefundAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{ConcessionAmount}}", dt.GetColValue("ConcessionAmt").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{BalanceAmt}}", dt.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));


                        html = html.Replace("{{T_NetAmount}}", dt.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));

                        html = html.Replace("{{ChargesTotalamt}}", ChargesTotalamt.ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{BalancewdudcAmt}}", BalancewdudcAmt.ConvertToDouble().ToString("0.00"));

                        html = html.Replace("{{Qty}}", dt.GetColValue("Qty"));
                        html = html.Replace("{{Phone}}", dt.GetColValue("Phone"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{F_TotalAmount}}", F_TotalAmount.ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{balafteradvuseAmount}}", balafteradvuseAmount.ConvertToDouble().ToString("0.00"));

                        html = html.Replace("{{UseName}}", dt.GetColValue("UseName"));
                        html = html.Replace("{{TDSAmount}}", dt.GetColValue("TDSAmount"));
                        html = html.Replace("{{AffilAmount}}", dt.GetColValue("AffilAmount"));


                        html = html.Replace("{{chkpaidflag}}", dt.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkAdvflag}}", dt.GetColValue("AdvanceUsedAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkdiscflag}}", dt.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkbalflag}}", dt.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkadminchargeflag}}", AdminChares.ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkbalafterdudcflag}}", BalancewdudcAmt.ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chktdsflag}}", dt.GetColValue("TDSAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkWrfAmountflag}}", dt.GetColValue("AffilAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkadminflag}}", dt.GetColValue("TaxAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                    }

            
                    break;


                case "AdmissionList":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #000;font-size:20px;padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0, i = 0, D = 0, j = 0;
                        string previousLabel = "";
                        var groupedData = dt.AsEnumerable().GroupBy(row => row["AdmittedDoctorName"]).ToList();
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++; j++;
                            if (i == 1)
                            {
                                String Label;
                                Label = dr["AdmittedDoctorName"].ConvertToString();
                                items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"4\" style=\"border:1px solid #000;font-weight:bold;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");

                            }

                            if (previousLabel != "" && previousLabel != dr["AdmittedDoctorName"].ConvertToString())
                            {
                                j = 1;

                                items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='13' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                                 .Append(D.ToString()).Append("</td></tr>");


                                D = 0;
                                items.Append("<tr style=\"font-size:20px;border-bottom: 1px;border-right: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;font-weight:bold;vertical-align:middle\">").Append(dr["AdmittedDoctorName"].ConvertToString()).Append("</td></tr>");
                            }
                            D = D + 1;
                            previousLabel = dr["AdmittedDoctorName"].ConvertToString();

                            items.Append("<tr style=\"text-align: center; border: 1px solid #000; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #000; padding: 6px;\">").Append(j).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #000; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }

                            if (dt.Rows.Count > 0 && dt.Rows.Count == i)
                            {

                                items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='13' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                                     .Append(D.ToString()).Append("</td></tr>");

                            }

                        }

                    }


                    break;



                case "IpPaymentReceipt":
                    {
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                        //html = html.Replace("{{NewHeader}}", htmlHeader);
                      
                        int i = 0;
                        html = html.Replace("{{PBillNo}}", dt.GetColValue("PBillNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{PaidAmount}}", dt.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));

                        html = html.Replace("{{CashPayAmount}}", dt.GetColValue("CashPayAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{PaymentTime}}", dt.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{BillDate}}", dt.GetColValue("BillDate").ConvertToDateString());
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{DischargeDate}}", dt.GetColValue("DischargeDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));


                        html = html.Replace("{{TotalAmt}}", dt.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{Remark}}", dt.GetColValue("Remark"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));
                        html = html.Replace("{{ReceiptNo}}", dt.GetColValue("ReceiptNo"));

                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{Age}}", dt.GetColValue("Age"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));


                        html = html.Replace("{{PayTMPayAmount}}", dt.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{CashPayAmount}}", dt.GetColValue("CashPayAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{CardPayAmount}}", dt.GetColValue("CardPayAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{ChequePayAmount}}", dt.GetColValue("ChequePayAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{NEFTPayAmount}}", dt.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{OnlinePayAmount}}", dt.GetColValue("OnlinePayAmount").ConvertToDouble().ToString("0.00"));


                        //string finalamt = conversion(Bills.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace().ToString());
                        //html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


                    }


                    break;


                case "IpAdvanceRefundReceipt":
                    {

                        html = html.Replace("{{Remark}}", dt.GetColValue("Remark"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{RefundNo}}", dt.GetColValue("RefundNo"));
                        html = html.Replace("{{Addedby}}", dt.GetColValue("Addedby"));

                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{AdmissinDate}}", dt.GetColValue("AdmissinDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{AdvanceAmount}}", dt.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace().ToString());
                        html = html.Replace("{{RefundAmount}}", dt.GetColValue("RefundAmount").ConvertToDouble().To2DecimalPlace().ToString());
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{Phone}}", dt.GetColValue("Phone"));

                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{PBillNo}}", dt.GetColValue("PBillNo"));
                        html = html.Replace("{{RefundAmount}}", dt.GetColValue("RefundAmount"));
                        html = html.Replace("{{AdvanceUsedAmount}}", dt.GetColValue("AdvanceUsedAmount"));

                        html = html.Replace("{{BalanceAmount}}", dt.GetColValue("BalanceAmount"));
                        html = html.Replace("{{CashPayAmount}}", dt.GetColValue("CashPayAmount"));
                        html = html.Replace("{{CardPayAmount}}", dt.GetColValue("CardPayAmount"));
                        html = html.Replace("{{ChequePayAmount}}", dt.GetColValue("ChequePayAmount"));
                        html = html.Replace("{{NEFTPayAmount}}", dt.GetColValue("NEFTPayAmount"));
                        html = html.Replace("{{PayTMPayAmount}}", dt.GetColValue("PayTMPayAmount"));


                        html = html.Replace("{{DischargeDate}}", dt.GetColValue("DischargeDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        html = html.Replace("{{RefundTime}}", dt.GetColValue("RefundTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{PaymentTime}}", dt.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AddedBy}}", dt.GetColValue("AddedBy"));

                        //string finalamt = conversion(Bills.GetColValue("RefundAmount").ConvertToDouble().To2DecimalPlace().ToString());
                        //html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


                    }


                    break;


                case "IpBillRefundReceipt":
                    {

                        html = html.Replace("{{Remark}}", dt.GetColValue("Remark"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{RefundNo}}", dt.GetColValue("RefundNo"));
                        html = html.Replace("{{Addedby}}", dt.GetColValue("AddedBy"));
                        html = html.Replace("{{Age}}", dt.GetColValue("Age"));
                        html = html.Replace("{{AdmissinDate}}", dt.GetColValue("AdmissinDate"));
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{NetPayableAmt}}", dt.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{RefundAmount}}", dt.GetColValue("RefundAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{Phone}}", dt.GetColValue("Phone"));
                        html = html.Replace("{{PBillNo}}", dt.GetColValue("PBillNo"));

                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));

                        html = html.Replace("{{DischargeDate}}", dt.GetColValue("DischargeDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{RefundTime}}", dt.GetColValue("RefundTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{PaymentTime}}", dt.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));

                        //string finalamt = conversion(Bills.GetColValue("RefundAmount").ConvertToDouble().To2DecimalPlace().ToString());
                        //html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());

                      

                    }


                    break;


                case "IPDCurrentwardwisecharges":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #000; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int i = 0, j = 0;
                        string previousLabel = "";
                        double TotalCollection = 0;
                        double T_ChargesAmount = 0, T_BalAmount = 0, T_AdvAmount = 0, G_ChargesAmount = 0, G_BalAmount = 0, G_AdvAmount = 0;
                        double FinalTotal = 0;


                        foreach (DataRow dr in dt.Rows)
                        {

                            i++; j++;


                            if (i == 1)
                            {
                                String Label;
                                Label = dr["RoomName"].ConvertToString();
                               // items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center\">").Append(j).Append("</td>");
                                items.Append("<tr style=\"font-size:20px;border: 1px;\"><td colspan=\"12\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                            }
                            if (previousLabel != "" && previousLabel != dr["RoomName"].ConvertToString())
                            {
                                j = 1;
                                items.Append("<tr style='border:1px solid black;'><td colspan='11' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                               .Append(T_ChargesAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                               .Append(T_AdvAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")

                               .Append(T_BalAmount.To2DecimalPlace()).Append("</td></tr>");
                                T_ChargesAmount = 0; T_BalAmount = 0; T_AdvAmount = 0;

                                items.Append("<tr style=\"font-size:20px;border-bottom: 1px;\"><td colspan=\"12\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["RoomName"].ConvertToString()).Append("</td></tr>");

                            }


                            T_ChargesAmount += dr["ChargesAmount"].ConvertToDouble();
                            T_AdvAmount += dr["AdvanceAmount"].ConvertToDouble();
                            T_BalAmount += dr["BalPayAmt"].ConvertToDouble();


                            previousLabel = dr["RoomName"].ConvertToString();
                          
                            items.Append("<tr style=\"font-size:15px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border-bottom: 1px;\"><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center\">").Append(j).Append("</td>");
                          
                            items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["RegID"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["IPDNo"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["RoomName"].ConvertToString()).Append("</td>");
                            
                            items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: right;\">").Append(dr["PatientName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">").Append(dr["AdmissionTime"].ConvertToDateString("dd/MM/yy")).Append("</td>");

                            items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;padding:3px;height:10px;\">").Append(dr["DoctorName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;padding:3px;height:10px;\">").Append(dr["BedName"].ConvertToString()).Append("</td>");
                            
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["ChargesAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["AdvanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BalPayAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");

                            if (dt.Rows.Count > 0 && dt.Rows.Count == i)
                            {
                                items.Append("<tr style='border:1px solid black; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='10' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle;margin-center:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                    .Append(T_ChargesAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                                    .Append(T_AdvAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;border-top:1px solid #000;border-left:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                                      .Append(T_BalAmount.To2DecimalPlace()).Append("</td></tr>");

                            }



                        }



                        html = html.Replace("{{Items}}", items.ToString());

                        html = html.Replace("{{T_ChargesAmount}}", T_ChargesAmount.To2DecimalPlace());
                        html = html.Replace("{{T_AdvAmount}}", T_AdvAmount.To2DecimalPlace());
                        html = html.Replace("{{T_BalAmount}}", T_BalAmount.To2DecimalPlace());

                    }
                    break;
                case "IPDRefrancefDoctorwise":
                    {
                      
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #000; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0;
                        string previousLabel = string.Empty;
                        int i = 0, j = 0, D = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++; j++;
                            if (i == 1)
                            {
                                String Label;
                                Label = dr["RefDoctorName"].ConvertToString();
                                items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"7\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");

                            }

                            if (previousLabel != "" && previousLabel != dr["RefDoctorName"].ConvertToString())
                            {
                                j = 1;

                                items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='6' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                                 .Append(D.ToString()).Append("</td></tr>");


                                D = 0;
                                items.Append("<tr style=\"font-size:20px;border-bottom: 1px;border-right: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"7\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");
                            }
                            D = D + 1;
                            previousLabel = dr["RefDoctorName"].ConvertToString();

                            items.Append("<tr style=\"text-align: center; border: 1px solid #000; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #000; padding: 6px;\">").Append(j).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #000; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }

                            if (dt.Rows.Count > 0 && dt.Rows.Count == i)
                            {

                                items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='6' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                                     .Append(D.ToString()).Append("</td></tr>");

                            }

                        }


                    }
                    break;


                  
                case "IPDDischargewithmarkstatus":

                case "DepartmentwiseCount":
                case "Dischargetypewise":
                case "IPDDoctorWiseCountSummaryList":
                case "IPDCurrentrefDoctorAdmissionList":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        int k = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            k++;

                            items.Append("<tr style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: left; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                         }

                    }
                    break;

                case "IpMLCCasePaperPrint":
                    {


                        var dynamicVariable = new Dictionary<string, double>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            html = html.Replace("{{reason}}", dt.GetColValue("reason"));
                            html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                            html = html.Replace("{{AdvanceNo}}", dt.GetColValue("AdvanceNo"));
                            html = html.Replace("{{Addedby}}", dt.GetColValue("Addedby"));

                            html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                            html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                            html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                            html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                            html = html.Replace("{{AdvanceAmount}}", dt.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace());
                            html = html.Replace("{{Phone}}", dt.GetColValue("Phone"));
                            html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                            html = html.Replace("{{ReportingDate}}", dt.GetColValue("ReportingTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                            html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));


                            html = html.Replace("{{MLCNo}}", dt.GetColValue("MLCNo").ConvertToString());

                            html = html.Replace("{{AuthorityName}}", dt.GetColValue("AuthorityName").ConvertToString());
                            html = html.Replace("{{BuckleNo}}", dt.GetColValue("BuckleNo").ConvertToString());
                            html = html.Replace("{{PoliceStation}}", dt.GetColValue("PoliceStation").ConvertToString());

                            html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                            html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                            html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                            html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                            html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                            html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                            html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                            html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                            html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName"));
                            html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                            //html = html.Replace("{{Remark}}", Bills.GetColValue("Remark"));
                            //html = html.Replace("{{DetailGiven}}", Bills.GetColValue("DetailGiven"));


                            //html = html.Replace("{{chkRemarkflag}}", Bills.GetColValue("Remark") != null ? "table-row " : "none");

                            //html = html.Replace("{{chkgivenflag}}", Bills.GetColValue("DetailGiven").ConvertToString() != "" ? "table -row " : "none");

                            html = html.Replace("{{chkcashflag}}", dt.GetColValue("CashPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                            html = html.Replace("{{chkchequeflag}}", dt.GetColValue("ChequePayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                            html = html.Replace("{{chkcardflag}}", dt.GetColValue("CardPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                            html = html.Replace("{{chkneftflag}}", dt.GetColValue("NEFTPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                            html = html.Replace("{{chkpaytmflag}}", dt.GetColValue("PayTMAmount").ConvertToDouble() > 0 ? "table-row " : "none");


                            return html;


                            //string finalamt = conversion(dt.GetColValue("PaidAmount").ConvertToDouble().To2DecimalPlace().ToString());
                            //html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());

                        }




                    }
                    break;


            }
             
            if (model.Mode == "DepartmentWisecountSummury" || model.Mode == "OPDoctorWiseVisitCountSummary" || model.Mode == "OPDoctorWiseNewOldPatientReport" || model.Mode == "IPDCurrentrefDoctorAdmissionList" || model.Mode == "IPDDoctorWiseCountSummaryList" || model.Mode == "Dischargetypewise" || model.Mode == "Dischargetypecompanywise" || model.Mode == "DepartmentwiseCount" || model.Mode == "IPDDischargewithmarkstatus")
                html = html.Replace("{{T_Count}}", T_Count.ToString());
             
            html = html.Replace("{{HeaderItems}}", HeaderItems.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{ItemsTotal}}", ItemsTotal.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

        public string conversion(string amount)
        {
            double m = Convert.ToInt64(Math.Floor(Convert.ToDouble(amount)));
            double l = Convert.ToDouble(amount);

            double j = (l - m) * 100;
            //string Word = " ";

            var beforefloating = ConvertNumbertoWords(Convert.ToInt64(m));
            var afterfloating = ConvertNumbertoWords(Convert.ToInt64(j));

            // Word = beforefloating + '.' + afterfloating;

            var Content = beforefloating + ' ' + " RUPEES" + ' ' + " only";

            return Content;
        }

        public string ConvertNumbertoWords(long number)
        {
            if (number == 0) return "ZERO";
            if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            string words = "";
            if ((number / 1000000) > 0)
            {
                words += ConvertNumbertoWords(number / 100000) + " LAKES ";
                number %= 1000000;
            }
            if ((number / 1000) > 0)
            {
                words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
                number %= 1000;
            }
            if ((number / 100) > 0)
            {
                words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
                number %= 100;
            }
            //if ((number / 10) > 0)  
            //{  
            // words += ConvertNumbertoWords(number / 10) + " RUPEES ";  
            // number %= 10;  
            //}  
            if (number > 0)
            {
                if (words != "") words += "AND ";
                var unitsMap = new[]
           {
            "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };
                var tensMap = new[]
           {
            "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"
        };
                if (number < 20) words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0) words += " " + unitsMap[number % 10];
                }
            }
            return words;
        }

    }
}
