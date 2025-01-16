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
using Aspose.Cells;
using Aspose.Cells.Drawing;

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
                        model.RepoertName = "Registration List";
                        string[] headerList = { "Sr.No", "UHID", "Patient Name", "Address", "City", "Pin Code", "Age", "Gender Name", "Mobile No" };
                        string[] colList = {"RegID", "PatientName", "Address", "City", "PinNo", "Age", "GenderName", "MobileNo" };
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
                        string[] headerList = { "Sr.No", "UHID", "Visit Date", "Patient Name", "Age year", "OPD No", "Doctor Name", "RefDoctor Name", "Company Name" };
                        string[] colList = { "Sr.No", "RegNO", "VisitDate", "PatientName", "AgeYear", "OPDNo", "DoctorName", "RefDocName", "CompanyName" };
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
                        string[] colList = { "RegID","PatientName", "DoctorName", "RefDoctorName" };
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
                        string[] headerList = { "Sr.No", "RegNO", "PatientName", "AdmittedDoctorName","AgeYear" };
                        string[] colList = { "RegNo", "PatientName", "AdmittedDoctorName", "AgeYear"};
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
                        string[] colList = { "BillNo", "BillDate", "RegId", "PatientName", "BillAmt", "ConcessionAmt", "NetPayableAmt", "PaidAmount", "BalanceAmt", "CashPay" , "ChequePay", "CardPay" , "NeftPay", "PayTMPay" };
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
                        model.RepoertName = "OP DAILY COLLECTION REPORT";
                        string[] headerList = { "Sr.No", "Bill No", "Date", "UHID", "Patient Name", "Receipt No", "Net Amt", "Cash Amt", "Cheque Amt", "Card Amt", "Online Amt", "User Name" };
                        string[] colList = { "Number", "PaymentTime", "RegNo", "PatientName", "ReceiptNo", "NetPayableAmt" , "CashPayAmount", "ChequePayAmount", "CardPayAmount", "PayTMAmount", "UserName" };
                        string[] totalColList = { "", "", "", "", "", "lableTotal", "NetPayableAmt", "CashPayAmount", "ChequePayAmount", "CardPayAmount", "PayTMAmount", "" };
                        string groupByCol = "Type";
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "MultiTotalReportFormat.html");//OPDailycollectionuserwise
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
                        string[] colList = { "RegNO", "PatientName", "AgeYear", "CityName", "MobileNo", "DepartmentName", "DoctorName", "RefDoctorName", "CompanyName"};
                        string[] totalColList = { "", "lableTotal", "PatientName", "", "", "", "", "", "", ""};
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
                        model.RepoertName = "Department Wise Count Summury";
                        string[] headerList = { "Sr.No", "Department Name", "Count" };
                        string[] colList = { "DepartmentName" };
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
                        model.RepoertName = "Department Wise OPD Count Summary";
                        string[] headerList = { "Sr.No", "Department Name", "Count" };
                        string[] colList = { "DepartmentName" };
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
                        string[] colList = { "DoctorName" ,"Count" };
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
                        string[] colList = { "GroupName" , "NetPayableAmt" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleTotalReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptservicegroupwisecollectionSummary", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentServiceGroupWiseCollectionSummary", "DepartmentServiceGroupWiseCollectionSummary", Orientation.Portrait);
                        break;
                    }
                #endregion

                default:
                    break;

                 
            }
            string byteFile = Convert.ToBase64String(tuple.Item1);
            return byteFile;

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


            string htmlHeader = _pdfUtility.GetHeader(htmlHeaderFilePath, model.BaseUrl);

            string html = File.ReadAllText(htmlFilePath);
            html = html.Replace("{{HospitalHeader}}", htmlHeader);
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
                case "RegistrationReport":
                //case "AppointmentListReport":
                case "DepartmentWisecountSummury":
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

                            items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                            if (model.Mode == "DepartmentWisecountSummury" || model.Mode == "OPDoctorWiseVisitCountSummary")
                                if ( model.Mode == "OPDoctorWiseVisitCountSummary")
                                    T_Count += dr["Lbl"].ConvertToDouble();
                        }
                    }
                    break;
                //case "DepartmentWisecountSummury":
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

                //            items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                //            foreach (var colName in colList)
                //            {
                //                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                //            }
                //            // if (model.Mode == "DepartmentWisecountSummury" || model.Mode == "OPDoctorWiseVisitCountSummary")
                //            if (model.Mode == "DepartmentWisecountSummury")
                //                T_Count += dr["Lbl"].ConvertToDouble();
                //        }
                //        html = html.Replace("{{T_Count}}", T_Count.ToString());


                //    }
                //    break;

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
               
                case "AppointmentListReport":
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
                            if (model.Mode == "AppointmentListReport" || model.Mode == "AppointmentListReport")
                                T_Count += dr["PatientOldNew"].ConvertToDouble();

                        }
                        html = html.Replace("{{T_Count}}", T_Count.ToString());

                    }

                    break;


                case "DepartmentWiseOPDCount":
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
                            if (model.Mode == "DepartmentWiseOPDCount" || model.Mode == "DepartmentWiseOPDCount")
                                T_Count += dr["VisitCount"].ConvertToDouble();

                        }
                        html = html.Replace("{{T_Count}}", T_Count.ToString());

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

                            items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }
                            if (model.Mode == "DepartmentWiseOpdCountSummary" || model.Mode == "DepartmentWiseOpdCountSummary")
                                T_Count += dr["VisitCount"].ConvertToDouble();

                        }
                        html = html.Replace("{{T_Count}}", T_Count.ToString());

                    }
                    break;

                case "DrWiseOPDCountDetail":
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
                            if (model.Mode == "DrWiseOPDCountDetail" || model.Mode == "DrWiseOPDCountDetail")
                                T_Count += dr["VisitCount"].ConvertToDouble();

                        }
                        html = html.Replace("{{T_Count}}", T_Count.ToString());

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
                                // items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(j).Append("</td></tr>");
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
                        int i = 0, j = 0,D=0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++; j++;
                            if (i == 1)
                            {
                                String Label;
                                Label = dr["RefDoctorName"].ConvertToString();
                                items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                               // items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(j).Append("</td></tr>");
                            }

                            if (previousLabel != "" && previousLabel != dr["RefDoctorName"].ConvertToString())
                            {
                                j = 1;
                                //items.Append("<tr style='border:1px solid black;color:#0000;'><td colspan='3' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">").Append("</td>");
                                //items.Append("<td style=\"border: 1px solid #0000; padding: 6px;text-align:center;\">").Append(D).Append("</td></tr>");




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
                        if(totalColList != null)
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

                            items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(k).Append("</td>");
                            foreach (var colName in colList)
                            {
                                if(colName.ToLower().IndexOf("date") == -1)
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

            }




            if (model.Mode == "DepartmentWisecountSummury" || model.Mode == "OPDoctorWiseVisitCountSummary" || model.Mode == "OPDoctorWiseNewOldPatientReport")
                html = html.Replace("{{T_Count}}", T_Count.ToString());

            html = html.Replace("{{HeaderItems}}", HeaderItems.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{ItemsTotal}}", ItemsTotal.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }

    }
}
