using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Utilities;
using LinqToDB.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using WkHtmlToPdfDotNet;





namespace HIMS.Services.Report
{
    public class ReportService : IReportService
    {
        private readonly Data.Models.HIMSDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;
        public readonly IPdfUtility _pdfUtility;
        //public readonly IFileUtility _FileUtility;
        public ReportService(HIMSDbContext HIMSDbContext, IHostingEnvironment hostingEnvironment, IPdfUtility pdfUtility)
        {
            _context = HIMSDbContext;
            _hostingEnvironment = hostingEnvironment;
            _pdfUtility = pdfUtility;
            //_FileUtility = fileUtility;
        }

        public virtual async Task<List<DoctorMaster>> SearchDoctor(string str)
        {
            return await this._context.DoctorMasters.Where(x => (x.FirstName + " " + x.LastName).ToLower().Contains(str)).Take(25).ToListAsync();
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

        public virtual async Task<List<RoomMaster>> SearchWard(string str)
        {
            return await this._context.RoomMasters.Where(x => (x.RoomName).ToLower().Contains(str)).Take(25).ToListAsync();
        }

        //public virtual async Task<List<Admission>> SearchAdmission(string str)
        //{
        //    return await this._context.Admissions.Where(x => (x.AdmissionId).ToLower().Contains(str)).Take(25).ToListAsync();
        //}
        public virtual async Task<List<CompanyMaster>> SearchCompany(string str)
        {
            return await this._context.CompanyMasters.Where(x => (x.CompanyName).ToLower().Contains(str)).Take(25).ToListAsync();
        }

        public virtual async Task<List<DischargeTypeMaster>> SearchDischargeType(string str)
        {
            return await this._context.DischargeTypeMasters.Where(x => (x.DischargeTypeName).ToLower().Contains(str)).Take(25).ToListAsync();
        }

        public virtual async Task<List<GroupMaster>> SearchGroupMaster(string str)
        {
            return await this._context.GroupMasters.Where(x => (x.GroupName).ToLower().Contains(str)).Take(25).ToListAsync();
        }

        public virtual async Task<List<ClassMaster>> SearchClassMaster(string str)
        {
            return await this._context.ClassMasters.Where(x => (x.ClassName).ToLower().Contains(str)).Take(25).ToListAsync();
        }

        public virtual async Task<List<MStoreMaster>> SearchMStoreMaster(string str)
        {
            return await this._context.MStoreMasters.Where(x => (x.StoreName).ToLower().Contains(str)).Take(25).ToListAsync();
        }

        public virtual async Task<List<MSupplierMaster>> SearchMSupplierMaster(string str)
        {
            return await this._context.MSupplierMasters.Where(x => (x.SupplierName).ToLower().Contains(str)).Take(25).ToListAsync();
        }

        //public virtual async Task<List<Payment>> SearchPayment(string str)
        //{
        //    return await this._context.Payments.Where(x => (x.PaymentId).ToLower().Contains(str)).Take(25).ToListAsync();
        //}

        public virtual async Task<List<MItemDrugTypeMaster>> SearchMItemDrugTypeMaster(string str)
        {
            return await this._context.MItemDrugTypeMasters.Where(x => (x.DrugTypeName).ToLower().Contains(str)).Take(25).ToListAsync();
        }

        public virtual async Task<List<MCreditReasonMaster>> SearchMCreditReasonMaster(string str)
        {
            return await this._context.MCreditReasonMasters.Where(x => (x.CreditReason).ToLower().Contains(str)).Take(25).ToListAsync();
        }

        public virtual async Task<List<MItemMaster>> SearchMItemMaster(string str)
        {
            return await this._context.MItemMasters.Where(x => (x.ItemName).ToLower().Contains(str)).Take(25).ToListAsync();
        }

        public virtual async Task<List<MModeOfPayment>> SearchMModeOfPayment(string str)
        {
            return await this._context.MModeOfPayments.Where(x => (x.ModeOfPayment).ToLower().Contains(str)).Take(25).ToListAsync();
        }

     

        public virtual async Task<IPagedList<MReportListDto>> MReportListDto(GridRequestModel model)
        {
            return await DatabaseHelper.GetGridDataBySp<MReportListDto>(model, "ps_ReportList");
        }


        public string GetReportSetByProc(ReportRequestModel model, string PdfFontPath = "")
        {

            var tuple = new Tuple<byte[], string>(null, string.Empty);
            string vDate = DateTime.Now.ToString("_dd_MM_yyyy_hh_mm_tt");

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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "RegistrationReport", "RegistrationReport" + vDate, Orientation.Portrait, PaperKind.A4);


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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "AppointmentListReport", "AppointmentListReport" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DoctorWiseVisitReport", "DoctorWiseVisitReport" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "RefDoctorWiseReport", "RefDoctorWiseReport" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentWiseCountSummury", "DepartmentWiseCountSummury" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPDoctorWiseVisitCountSummary", "OPDoctorWiseVisitCountSummary" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPAppoinmentListWithServiseAvailed", "OPAppoinmentListWithServiseAvailed" + vDate, Orientation.Portrait);

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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "CrossConsultationReport", "CrossConsultationReport" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPDoctorWiseNewAndOldPatientReport", "OPDoctorWiseNewAndOldPatientReport" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "BillReportSummary", "BillReportSummary" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "BillReportSummarySummary", "BillReportSummarySummary" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPDBillBalanceReport", "OPDBillBalanceReport" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPDRefundOfBill", "OPDRefundOfBill" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: OPRefundReceipt ::
                case "OPRefundReceipt":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPRefundReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("ps_rptOPRefundofBillPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPRefundReceipt", "OPRefundReceipt" + vDate, Orientation.Portrait);
                        break;

                    }
                #endregion
                #region :: OPPaymentReceipt ::
                case "OPPaymentReceipt":
                    {
                        string[] colList = { "RefundDate", "RegId", "PatientName", "RefundAmount", "TotalAmt" };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPPaymentReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rptOPDPaymentReceiptPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPPaymentReceipt", "OPPaymentReceipt" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: AppointmentReceipt ::
                case "AppointmentReceipt":
                    {

                        string[] colList = { "RegNo", "VisitDate", "PatientName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "AppointmentReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptAppointmentPrint1", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "AppointmentReceipt", "AppointmentReceipt", Orientation.Portrait);
                        break;



                        //string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "AppointmentReceipt.html");
                        //string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        //htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath, model.BaseUrl);
                        //var html = GetHTMLView("rptAppointmentPrint1", model, htmlFilePath, htmlHeaderFilePath, colList);
                        //html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        //tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "AppointmentReceipt", "AppointmentReceipt", Orientation.Portrait);
                        //break;
                    }
                #endregion
                #region :: OpBillReceipt ::
                case "OpBillReceipt":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OpBillingReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("ps_rptBillPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OpBillReceipt", "OpBillReceipt" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: OPCasePaper ::
                case "OPCasePaper":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPCasePaper.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptAppointmentPrint1", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPCasePaper", "OPCasePaper" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: OPStickerPrint ::
                case "OPStickerPrint":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPPatientBarCodeSticker.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptAppointmentPrint1", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        //tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPStickerPrint", "Sticker" + vDate, Orientation.Portrait);
                        tuple = _pdfUtility.GeneratePdfFromHtmlBarCode(html, model.StorageBaseUrl, "OPStickerPrint", "Sticker" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: OPDSpineCasePaper ::
                case "OPDSpineCasePaper":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPDCASEPAPERSpineClinic.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptAppointmentPrint1", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPDSpineCasePaper", "OPDSpineCasePaper" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: OPPrescription ::
                case "OPPrescription":
                    {

                        model.RepoertName = "OPPrescription ";

                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPPrescriptionNew.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rptOPDPrecriptionPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);
                        html = html.Replace("{{Signature}}", "");
                        html = SetFonts(html, PdfFontPath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPPrescription", "OPPrescription" + vDate, Orientation.Portrait);


                        break;
                    }
                #endregion

                #region :: OPPrescriptionwithoutHeader ::
                case "OPPrescriptionwithoutHeader":
                    {

                        model.RepoertName = "OPPrescriptionwithoutHeader ";

                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OPPrescriptionwithoutheader.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rptOPDPrecriptionPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);
                        html = html.Replace("{{Signature}}", "");
                        html = SetFonts(html, PdfFontPath);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPPrescription", "OPPrescriptionwithoutHeader" + vDate, Orientation.Portrait);

                        break;
                    }
                #endregion

                #region :: CertificateInformationReport ::
                case "CertificateInformationReport":
                    {

                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "CertificateInformationPrint.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rpt_CertificateInformationPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "CertificateInformation", "CertificateInformation" + vDate, Orientation.Portrait);
                        break;

                    }
                #endregion

                #region :: Certificate ::
                case "Certificate":
                    {

                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "CertificateInformationPrint1.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rpt_CertificateInformationPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "Certificate", "CertificateInformation" + vDate, Orientation.Portrait);
                        break;

                    }
                #endregion


                #region :: EmergencyPrint ::
                case "EmergencyPrint":
                    {

                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "Emergencyprint.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("ps_rptEmergencyAdmPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "EmergencyPrint", "EmergencyPrint" + vDate, Orientation.Portrait);
                        break;

                    }
                #endregion


                #region :: EmergencyPrescription ::
                case "EmergencyPrescription":
                    {

                        model.RepoertName = "EmergencyPrescription ";

                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "EmergencyPresciption.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rptEmergencyAdmPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);
                        //html = html.Replace("{{Signature}}", "");
                        //html = SetFonts(html, PdfFontPath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "EmergencyPrescription", "EmergencyPrescription" + vDate, Orientation.Portrait);






                        //string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PathologyResultTest.html");
                        //string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");


                        break;
                    }
                #endregion


                #region :: ConsentInformation ::
                case "ConsentInformation":
                    {

                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "ConsentInformation.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rpt_TConsentInformation", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "ConsentInformation", "ConsentInformation" + vDate, Orientation.Portrait);
                        break;

                    }
                #endregion

                #region :: OPBillWithPackagePrint ::
                case "OPBillWithPackagePrint":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OpBillWithPackagePrint.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("ps_rptOP_BillWithPackage_Print", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPBillWithPackagePrint", "OPBillWithPackagePrint" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: GRNReport ::
                case "GRNReport":
                    {

                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GRNReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptPrintGRN", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "GRNReport", "GRNReport" + vDate, Orientation.Portrait);
                        break;



                    }
                #endregion
                #region :: GRNReturnReport ::
                case "GRNReturnReport":
                    {

                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "GRNReturn.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("ps_rptGRNReturnPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "GRNReturnReport", "GRNReturnReport" + vDate, Orientation.Portrait);
                        break;



                    }
                #endregion

                #region :: IndentwiseReport ::
                case "IndentwiseReport":
                    {

                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IndentReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptPrintIndent", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IndentwiseReport", "IndentwiseReport" + vDate, Orientation.Portrait);
                        break;


                    }
                #endregion


                #region :: DoctorNotesReceipt ::
                case "DoctorNotesReceipt":
                    {

                        //string[] colList = { "RegNo", "VisitDate", "PatientName" };
                        //string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "DoctorNoteReceipt.html");
                        //string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        //var html = GetHTMLView("m_rpt_T_Doctors_Notes", model, htmlFilePath, htmlHeaderFilePath, colList);
                        //tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DoctorNotesReceipt", "DoctorNotesReceipt" + vDate, Orientation.Portrait);
                        //break;


                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "DoctorNoteReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rpt_T_Doctors_Notes", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DoctorNotesReceipt", "DoctorNotesReceipt" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: NursingNotesReceipt ::
                case "NursingNotesReceipt":
                    {


                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NursingNoteReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rpt_T_NursingNotesPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "NursingNotesReceipt", "NursingNotesReceipt" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion


                #region :: DoctorNoteandNursingNoteReport ::
                case "DoctorNoteandNursingNoteReport":
                    {


                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "DoctorAndNursingNoteReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rpt_T_NursingNotesAndT_Doctors_NotesPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "NursingNotesReceipt", "NursingNotesReceipt" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: DoctorPatientHandoverReceipt ::
                case "DoctorPatientHandoverReceipt":
                    {

                        string[] colList = { "RegNo", "VisitDate", "PatientName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "DoctorPatientHandoverReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("ps_rpt_T_Doctor_PatientHandover", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DoctorPatientHandoverReceipt", "DoctorPatientHandoverReceipt" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPDailyCollectionReport", "OPDailyCollectionReport" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OPCollectionSummary", "OPCollectionSummary" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DayWiseOpdCountDetails", "DayWiseOpdCountDetails" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DayWiseOpdCountSummry", "DayWiseOpdCountSummry" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentWiseOPDCount", "DepartmentWiseOPDCount" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentWiseOpdCountSummary", "DepartmentWiseOpdCountSummary" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DrWiseOPDCountDetail", "DrWiseOPDCountDetail" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: DoctorWiseOpdCountSummary ::
                case "DoctorWiseOpdCountSummary":
                    {
                        model.RepoertName = "Doctor Wise Opd Count Summary";
                        string[] colList = { "DoctorName", "Count" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleTotalReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDaywiseDoctoropdcountSummary", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DoctorWiseOpdCountSummary", "DoctorWiseOpdCountSummary" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DrWiseOPDCollectionDetails", "DrWiseOPDCollectionDetails" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DoctorWiseOpdCollectionSummary", "DoctorWiseOpdCollectionSummary" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentWiseOPDCollectionDetails", "DepartmentWiseOPDCollectionDetails" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentWiseOpdCollectionSummary", "DepartmentWiseOpdCollectionSummary" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentServiceGroupWiseCollectionDetails", "DepartmentServiceGroupWiseCollectionDetails" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DepartmentServiceGroupWiseCollectionSummary", "DepartmentServiceGroupWiseCollectionSummary" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion



                //Ipsection
                #region :: AdmissionList ::
                case "AdmissionList":
                    {

                        model.RepoertName = "Appointment List";
                        string[] headerList = { "Sr.No", "UHID", "Date", "IPDNo", "Patient Name", "Age", "Gender", "Ward", "Bed", /*"AdmDoctor Name",*/ "RefDocName", "ChargesAmt", "AdvAmt", "BalPayAmt" };
                        string[] colList = { "RegNo", "DOA", "IPDNo", "PatientName", "Age", "GenderName", "RoomName", "BedName",/* "AdmittedDoctorName",*/ "RefDocName", "ChargesAmount", "AdvanceAmount", "BalPayAmt", };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptCurrentAdmittedListReport", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDCurrentAdmittedDoctorWiseCharges", "IPDCurrentAdmittedDoctorWiseCharges", Orientation.Landscape);
                        break;
                    }
                #endregion
                #region :: IpCasepaperReport ::
                case "IpCasepaperReport":
                    {

                        string[] colList = { "RegNo", "VisitDate", "PatientName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPCasepaper.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rptAdmissionPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPCasepaper", "IPCasepaper" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion

                //#region :: IpCasepaperReport1 ::
                //case "IpCasepaperReport1":
                //    {

                //        string[] colList = { "RegNo", "VisitDate", "PatientName" };
                //        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPCasepaper.html");
                //        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                //        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath, model.BaseUrl);
                //        var html = GetHTMLView("m_rptAdmissionPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                //        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                //        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPCasepaper", "IPCasepaper", Orientation.Portrait);
                //        break;
                //    }
                //#endregion
                #region :: IptemplateCasepaperReport ::
                case "IptemplateCasepaperReport":
                    {

                        string[] colList = { "RegNo", "VisitDate", "PatientName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPCasepaper.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("m_rptAdmissionPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPCasepaper", "IPCasepaper" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: IpMLCCasePaperPrint ::
                case "IpMLCCasePaperPrint":
                    {
                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_MLCReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_MLCCasePaperPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPMLCDetail", "IPMLCDetail" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: IPCompanyFinalBillWithSR ::
                case "IPCompanyFinalBillWithSR":
                    {

                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPCompanyBillWithSR.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptIPComBlPrtSummary_WithAdd_SR", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPCompanyFinalBillWithSR", "IPCompanyFinalBillWithSR" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion


                #region :: IpCreditBill ::
                case "IpCreditBill":
                    {

                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPInterimBill.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("m_rptIPDInterimBill", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IpCreditBill", "IpCreditBill" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: IpInterimBill ::
                case "IPDInterimBill":
                    {


                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPInterimBill.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("ps_rptIPDInterimBill", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDInterimBill", "IPDInterimBill" + vDate, Orientation.Portrait);
                        break;

                    }
                #endregion




                #region :: RegistrationForm ::
                case "RegistrationForm":
                    {

                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "RegistrationForm.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("ps_rptRegistrationPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "RegistrationForm", "RegistrationForm" + vDate, Orientation.Portrait);
                        break;

                    }
                #endregion

                #region :: IPStickerPrint ::
                case "IPStickerPrint":
                    {

                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PatientDetailsStickerA4Page.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rptAdmissionPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPStickerPrint", "Sticker" + vDate, Orientation.Portrait);
                        break;

                    }
                #endregion
                #region :: IpDraftBillGroupWise ::
                case "IpDraftBillGroupWise":
                    {

                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPDraftBillGroupWise.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rptIPD_DraftBillClassWise_Print", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IpDraftBill", "IpDraftBillGroupWise" + vDate, Orientation.Portrait);
                        break;

                    }
                #endregion
                #region :: IpDraftBillClassWise ::
                case "IpDraftBillClassWise":
                    {

                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IpDraftBillClassWise.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rptIPD_DraftBillSummary_Print", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IpDraftBill", "IpDraftBillClassWise" + vDate, Orientation.Portrait);
                        break;

                    }
                #endregion
                #region :: IPFinalBillClassWise ::
                case "IPFinalBillClassWise":
                    {

                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPFinalBillClassWise.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rptIPD_FINAL_BILL_CLASSWISE", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPFinalBill", "IPDFinalBillClassWise" + vDate, Orientation.Portrait);
                        break;

                    }
                #endregion

                #region :: IPFinalBillClassServiceWise ::
                case "IPFinalBillClassServiceWise":
                    {

                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPFinalBillClassServiceWise.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rptIPD_FINAL_BILL_CLASSWISE", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPFinalBill", "IPFinalBillClassServiceWise" + vDate, Orientation.Portrait);
                        break;

                    }
                #endregion

                //#region :: IpFinalBillNew ::
                //case "IpFinalBillNew":
                //    {

                //        string[] colList = { };

                //        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPFinalBillReceiptNew.html");
                //        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                //        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath, model.BaseUrl);
                //        var html = GetHTMLView("rptIPComBlPrtSummary_WithAdd_SR", model, htmlFilePath, htmlHeaderFilePath, colList);
                //        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                //        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "NewIPBillClassservicewise", "NewIPBillClassservicewise", Orientation.Portrait);
                //        break;

                //    }
                //#endregion
                //#region :: IpFinalClasswiseBill ::
                //case "IpFinalClasswiseBill":
                //    {

                //        string[] colList = { "RegNo", "VisitDate", "PatientName" };

                //        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPReport_IPBillingReceiptclasswise.html");
                //        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                //        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath, model.BaseUrl);
                //        var html = GetHTMLView("m_rptIPD_FINAL_BILL_CLASSWISE", model, htmlFilePath, htmlHeaderFilePath, colList);
                //        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                //        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPFinalBill", "NewIPBilClasswise"+vDate, Orientation.Portrait);
                //        break;
                //    }
                //#endregion
                #region :: IPFinalBillGroupwise ::
                case "IPFinalBillGroupwise":
                    {

                        string[] colList = { "RegNo", "VisitDate", "PatientName" };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPFinalBillGroupwise.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rptIPD_FINAL_BILL_GROUPWISE", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPFinalBill", "IPFinalBillGroupwise" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: IpPaymentReceipt ::
                case "IpPaymentReceipt":
                    {

                        string[] colList = Array.Empty<string>();

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SettlementReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptIPDPaymentReceiptPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPPaymentReceipt", "IPPaymentReceipt", Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: IpAdvanceReceipt ::
                case "IpAdvanceReceipt":
                    {

                        string[] colList = Array.Empty<string>();

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "AdvanceReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rptIPDAdvancePrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IpAdvanceReceipt", "IpAdvanceReceipt" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: IpAdvanceStatement ::
                case "IpAdvanceStatement":
                    {

                        string[] colList = Array.Empty<string>();

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPAdvanceStatement.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptIPAdvanceSummary", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IpAdvanceStatement", "IpAdvanceStatement" + vDate, Orientation.Landscape);
                        break;
                    }
                #endregion

                #region :: IpAdvanceRefundReceipt ::
                case "IpAdvanceRefundReceipt":
                    {

                        model.RepoertName = "Ip Advance Refund  Receipt";
                        string[] headerList = { };
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "RefundofAdvanceReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rptIPRefundofAdvancePrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IpAdvanceRefundReceipt", "IpAdvanceRefundReceipt" + vDate, Orientation.Portrait);
                        break;


                    }
                #endregion

                #region :: IpBillRefundReceipt ::
                case "IpBillRefundReceipt":
                    {

                        model.RepoertName = "Ip Advance Refund  Receipt";
                        string[] headerList = { };
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPRefundBillReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptIPRefundofBillPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IpAdvanceRefundReceipt", "IpAdvanceRefundReceipt" + vDate, Orientation.Portrait);
                        break;


                    }
                #endregion


                #region :: IpDischargeSummaryReport ::
                case "IpDischargeSummaryReport":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPDischargeSummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLViewWithTwoSPs("m_rptDischargeSummaryPrint_New", "m_Rtrv_IP_Prescription_Discharge", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IpDischargeSummaryReport", "IpDischargeSummaryReport" + vDate, Orientation.Portrait);
                        break;

                    }
                #endregion
                #region :: IpDischargeSummaryReportWithoutHeader ::
                case "IpDischargeSummaryReportWithoutHeader":
                    {

                        model.RepoertName = "DischargeSummaryWithoutHeader";
                        string[] headerList = { };
                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPDischargeSummaryWithoutHeader.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        //GetHTMLView("m_rptDischargeSummaryPrint_New", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        var html = GetHTMLViewWithTwoSPs("m_rptDischargeSummaryPrint_New", "m_Rtrv_IP_Prescription_Discharge", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IpDischargeSummarywithoutHeaderReport", "IpDischargeSummarywithoutHeaderReport" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: IpDischargeSummaryTemplate ::
                case "IpDischargeSummaryTemplate":
                    {


                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPDischargesummaryTemplate.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLViewWithTwoSPs("m_rptDischargeSummaryPrint_New", "m_Rtrv_IP_Prescription_Discharge", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IpDischargeSummaryTemplate", "IpDischargeSummaryTemplate" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: IpDischargeSummaryTemplateWithoutHeader ::
                case "IpDischargeSummaryTemplateWithoutHeader":
                    {

                        model.RepoertName = "IpDischargeSummaryTemplateWithoutHeader";
                        string[] headerList = { };
                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPDischargesummaryTemplateWithoutHeader.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        //GetHTMLView("m_rptDischargeSummaryPrint_New", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        var html = GetHTMLViewWithTwoSPs("m_rptDischargeSummaryPrint_New", "m_Rtrv_IP_Prescription_Discharge", model, htmlFilePath, htmlHeaderFilePath, colList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IpDischargeSummaryTemplate", "IpDischargeSummaryTemplateWithoutHeader" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: IpDischargeReceipt ::
                case "IpDischargeReceipt":
                    {
                        model.RepoertName = "Discharge Slip";
                        string[] headerList = { "Sr.No", "UHID", "IPDNo", "RoomName", "PatientName", "AdmissionTime", "DoctorName", "RefDoctorName", "BedName", "ChargesAmount", "AdvanceAmount", "BalPayAmt" };
                        string[] colList = { "RegID", "IPDNo", "RoomName", "PatientName", "AdmissionTime", "DoctorName", "RefDoctorName", "BedName", "ChargesAmount", "AdvanceAmount", "BalPayAmt" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPDischarge.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("ps_rptDischargeCheckOutSlip", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IpDischargeCheckOut", "IpDischargeReceipt" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: IPDCurrentwardwisecharges ::
                case "IPDCurrentwardwisecharges":
                    {
                        model.RepoertName = "Current Ward Wise Charges Report ";
                        string[] headerList = { "Sr.No", "UHID", "IPDNo", "RoomName", "PatientName", "AdmissionTime", "DoctorName", "RefDoctorName", "BedName", "ChargesAmount", "AdvanceAmount", "BalPayAmt" };
                        string[] colList = { "RegID", "IPDNo", "RoomName", "PatientName", "AdmissionTime", "DoctorName", "RefDoctorName", "BedName", "ChargesAmount", "AdvanceAmount", "BalPayAmt" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptCurrentAdmittedListReportwithCharges", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "wardWisechargesReport", "wardWisechargesReport" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: IPDRefrancefDoctorwise ::
                case "IPDRefrancefDoctorwise":
                    {
                        model.RepoertName = "Ref DoctorWiseVisit Report ";
                        string[] headerList = { "Sr.No", "UHID", "IPDNo", "PatientName", "Age", "AdmissionDate", "AdmittedDoctorName" };
                        string[] colList = { "RegNo", "IPDNo", "PatientName", "AgeYear", "AdmissionDate", "AdmittedDoctorName" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("RptRefDoctorWiseAdmission", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDrefDoctorwiseReport", "IPDrefDoctorwiseReport" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: IPDCurrentrefDoctorAdmissionList ::
                case "IPDCurrentrefDoctorAdmissionList":
                    {
                        model.RepoertName = "RefDoctorWise Report  ";
                        string[] headerList = { "Sr.No", "AdmissionId", "UHID", "PatientName", "DoctorName", "Admission Date" };
                        string[] colList = { "AdmissionID", "RegNo", "PatientName", "DoctorName", "AdmissionDate" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("RptCurrentRefAdmittedReport", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDRefDoctorWiseReport", "IPDRefDoctorWiseReport" + vDate, Orientation.Portrait);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDoctorWiseVisitCountSummary", "IPDoctorWiseVisitCountSummary" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: Dischargetypewise ::
                case "Dischargetypewise":
                    {
                        model.RepoertName = "Dischargetype wise List";
                        string[] headerList = { "Sr.No", "AdmissionDate", "DischargeTime", "DischargeTypeName", "IPDNo", "PatientName", "MobileNo", "DoctorName", "RefDoctorName", "DepartmentName", "Diagnosis" };
                        string[] colList = { "AdmissionDate", "DischargeTime", "DischargeTypeName", "IPDNo", "PatientName", "MobileNo", "DoctorName", "RefDoctorName", "DepartmentName", "Diagnosis" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptDischargeTypeReport", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDDischargeTypeReport", "IPDDischargeTypeReport" + vDate, Orientation.Landscape);
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
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDDischargeTypeCompanywiseReport", "IPDDischargeTypeCompanywiseReport" + vDate, Orientation.Landscape);
                        break;
                    }
                #endregion

                #region :: DepartmentwiseCount ::
                case "DepartmentwiseCount":
                    {
                        model.RepoertName = "IPD Department Wise Count List";
                        string[] headerList = { "Sr.No", "Department Name", "Total Count", };
                        string[] colList = { "DepartmentName", "Lbl" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("Rtrv_IPDepartWsSumry", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDDepartmentwisecountsummary", "IPDDepartmentwisecountsummary" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: IPDDischargewithmarkstatus ::
                case "IPDDischargewithmarkstatus":
                    {
                        model.RepoertName = "Discharge Report With Mark Status List";
                        string[] headerList = { "Sr.No", "UHID", "PatientName", "DOA", "DOT", "Mark Date-Time", "DOD", "DOT", "Time In Hr" };
                        string[] colList = { "RegNo", "PatientName", "AdmissionDate", "AdmissionTime", "MarkStatus", "DischargeDate", "DischargeTime", "DiffTimeInHr" };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SimpleReportFormat.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        var html = GetHTMLView("rptIPDischargeMarkStatusReport", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPDDischargewithmarkstatus", "IPDDischargewithmarkstatus" + vDate, Orientation.Landscape);
                        break;
                    }
                #endregion

                #region :: DischargSummary ::
                case "DischargSummary":
                    {
                        //model.RepoertName = "DischargeSummaryWithTemplate";
                        //string[] headerList = { "Sr.No", "UHID", "PatientName" };
                        //string[] colList = { "RegNo", "PatientName" };
                        //string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "DischargeSummary.html");
                        //string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        //var html = GetHTMLView("m_rptDischargeSummaryPrint_New", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        //tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DischargeSummaryWithTemplate", "DischargeSummaryWithTemplate", Orientation.Landscape);
                        //break;

                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "DischargeSummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rptDischargeSummaryPrint_New", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "DischargeSummaryReport", "DischargeSummaryReport" + vDate, Orientation.Portrait);
                        break;

                    }

                #endregion





                //Pathology
                #region :: PathologyReportWithHeader ::
                case "PathologyReportWithHeader":
                    {

                        model.RepoertName = "PathologyReport ";
                        string[] headerList = Array.Empty<string>();
                        string[] colList = Array.Empty<string>();
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PathologyResultTest.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rptPathologyReportPrintMultiple", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        //html.Replace("{{Signature}}", signature);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "PathologyReportWithHeader", "PathologyReportWithHeader" + vDate, Orientation.Portrait);


                        break;
                    }
                #endregion
                #region :: PathologyReportWithOutHeader ::
                case "PathologyReportWithOutHeader":

                    {

                        model.RepoertName = "PathologyReportWithHeader";
                        string[] headerList = Array.Empty<string>();
                        string[] colList = Array.Empty<string>();
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PathTestReportHospitalheader.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rptPathologyReportPrintMultiple", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);
                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "PathologyReportWithOutHeader", "PathologyReportWithOutHeader" + vDate, Orientation.Portrait);

                        break;
                    }
                #endregion

                #region :: PathologyReportTemplateWithOutHeader ::
                case "PathologyReportTemplateWithOutHeader":
                    {

                     
                        model.RepoertName = "PathologyReportTemplate";
                        string[] headerList = Array.Empty<string>();
                        string[] colList = Array.Empty<string>();
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PathTemplate.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rptPrintPathologyReportTemplate", model, htmlFilePath, htmlHeaderFilePath, colList, headerList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "PathologyReportTemplateWithOutHeader", "PathologyReportTemplateWithOutHeader" + vDate, Orientation.Portrait);

                        break;

                    }
                #endregion
                #region :: PathologyReportTemplateWithHeader ::
                case "PathologyReportTemplateWithHeader":
                    {

                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PathTemplateHeader.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rptPrintPathologyReportTemplate", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "PathologyReportTemplateWithHeader", "PathologyReportTemplateWithHeader" + vDate, Orientation.Portrait);
                        break;

                    }
                #endregion


                //Radiology
                #region :: RadiologyTemplateReport ::
                case "RadiologyTemplateReport":
                    {

                        model.RepoertName = "Radiology  Template Report ";
                        string[] headerList = Array.Empty<string>();
                        string[] colList = Array.Empty<string>();

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "RadiologyTemplateReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptRadiologyReportPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "RadiologyTemplateReport", "RadiologyTemplateReport" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion



                //Nursing
                #region :: NurMaterialConsumption ::
                case "NurMaterialConsumption":
                    {

                        string[] colList = { };
                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "MaterialConsumptionReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptPrintMaterialConsumption", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "NurMaterialConsumption", "NurMaterialConsumption" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion


                #region :: NurLabRequestTest ::
                case "NurLabRequestTest":
                    {

                        model.RepoertName = "Nursing Lab Request Report ";
                        string[] headerList = Array.Empty<string>();
                        string[] colList = Array.Empty<string>();

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "LabRequest.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptLabRequestList", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "NurLabRequest", "NurLabRequest" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: CanteenRequestprint ::
                case "CanteenRequestprint":
                    {

                        model.RepoertName = "CanteenRequest print";
                        string[] headerList = Array.Empty<string>();
                        string[] colList = Array.Empty<string>();

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "CanteenRequest.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("ps_rptCanteenRequestPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "CanteenRequestprint", "CanteenRequestprint" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion


                #region :: NurIPprescriptionReport ::
                case "NurIPprescriptionReport":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IpPrescription.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptIPDPrecriptionPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IpPrescription", "IpPrescription" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion



                #region :: NurIPprescriptionReturnReport ::
                case "NurIPprescriptionReturnReport":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IPPrescriptionReturn.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptIPPrescriptionReturnListPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IPPrescriptionReturn", "IPPrescriptionReturn" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion


                #region :: Purchaseorder ::
                case "Purchaseorder":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PurchaseorderNew.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptPrintPurchaseOrder", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "PurchaseOrder", "PurchaseOrder" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion



                #region :: IndentWiseReport ::
                case "IndentWiseReport":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IndentReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptPrintIndent", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IndentWise", "IndentWise" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: IndentverifyReport ::
                case "IndentverifyReport":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "IndentReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptPrintIndent", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "IndentWise", "IndentWise" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: Issutodeptissuewise ::
                case "Issutodeptissuewise":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InvIssuetoDept.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptPrintIssueToDepartment", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "Issuetodept", "Issuetodept" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: Issutodeptsummarywise ::
                case "Issutodeptsummarywise":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "InvIssuetodeptsummary.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptIssueToDepartmentSummary", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "Issuetodept", "Issuetodept" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: OpeningBalance ::
                case "OpeningBalance":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OpeningBalance.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("m_rpt_Opening_Balance", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OpeningBalance", "OpeningBalance" + vDate, Orientation.Landscape);
                        break;
                    }
                #endregion
                #region :: WorkOrder ::
                case "WorkOrder":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PurWorkorder.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptWorkOrderPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "WorkOrder", "WorkOrder" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: MaterialReceivedByDept ::
                case "MaterialReceivedByDept":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PhrmaMaterialReceivedfrDept.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptPrintIssueToDepartment", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "ReceivedBydept", "ReceivedBydept" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion




                //Pharmacy
                #region :: PharmacyPatientStatement ::
                case "PharmacyPatientStatement":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmacySalesStatementReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("ps_rptIPPatientSalesSummary", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "PharmacyPatientStatement", "pharmacyPatientStatementReport" + vDate, Orientation.Landscape);
                        break;
                    }
                #endregion

                #region :: PharmacySalesDetails ::
                case "PharmacySalesDetails":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmacySalesBillDetailReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("ps_rptIPSalesBill", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "pharmacySalesDetails", "pharmacySalesDetailsReport" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: PharamcySalesBill ::
                case "PharamcySalesBill":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesTaxReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("ps_rptSalesPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "PharamcySalesBill", "PharamcySalesBill" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion



                #region :: PharamcySalesReturn ::
                case "PharamcySalesReturn":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SalesReturnReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("ps_rptSalesReturnPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "SalesReturnPrint", "SalesReturnPrint" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
                #region :: PharamcyAdvanceReceipt ::
                case "PharamcyAdvanceReceipt":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaAdvanceReceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptIPDPharAdvancePrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "PharamcyAdvanceReceipt", "PharamcyAdvanceReceipt" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: PharamcyAdvanceReturnReceipt ::
                case "PharamcyAdvanceReturnReceipt":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PharmaRefundofadvancereceipt.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptIPRefundofPharAdvancePrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "PharamcyAdvanceReturnReceipt", "PharamcyAdvanceReturnReceipt" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion
              

                #region :: SupplierPaymentReciept ::
                case "SupplierPaymentReciept":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "SupplierPaymentReciept.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("Rtrv_GrnSupPayList", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "SupplierPaymentReciept", "SupplierPaymentReciept" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion


                #region :: ExpenseVoucharPrint ::
                case "ExpenseVoucharPrint":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "ExpenseVoucharReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptExpVoucharPrint", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "ExpenseVoucharPrint", "ExpenseVoucharPrint" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: OTRequest ::
                case "OTRequest":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OT_RequestReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("Rtrv_OTRequest", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OTRequest", "OTRequest" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion

                #region :: OTReservationReport ::
                case "OTReservationReport":
                    {
                        string[] colList = { };

                        string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "OTReservationReport.html");
                        string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "NewHeader.html");
                        htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);
                        var html = GetHTMLView("rptRtrv_OTReservation", model, htmlFilePath, htmlHeaderFilePath, colList);
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);

                        tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, "OTReservationReport", "OTReservationReport" + vDate, Orientation.Portrait);
                        break;
                    }
                #endregion

                default:


                    break;


            }
            string byteFile = Convert.ToBase64String(tuple.Item1);
            return byteFile;

        }
        public string SetFonts(string html, string PdfFontPath)
        {
            string font = "";
            int c = 0;
            string fonts = "";
            foreach (string path in PdfFontPath.Split(','))
            {
                c++;
                font += "@font-face {font-family: 'NotoSans" + c + "';src: url('" + path + "') format('truetype');}";
                fonts += "'NotoSans" + c + "', ";
            }
            font += "\nbody {font-family: " + fonts + " sans-serif;}";
            return html.Replace("{{LoadFont}}", font);
        }
        public string GetNewReportSetByProc(ReportConfigDto model)
        {

            string vDate = DateTime.Now.ToString("_dd_MM_yyyy_hh_mm_tt");

            string[] headerList = model.headerList;
            string[] colList = model.colList;
            string[] totalList = model.totalFieldList;
            //   string[] columnAlignments = model.columnAlignments;
            // string[] columnWidths = model.columnWidths;
            //string[] groupbyList = model.groupbyList;  //"Type,SectionType";

            //Convert vPageOrientation from string to the appropriate Orientation enum
            Orientation vPageOrg;
            if (!Enum.TryParse(model.vPageOrientation, true, out vPageOrg))
            {
                // If conversion fails, set default value (for example, Portrait)
                vPageOrg = Orientation.Portrait;
            }

            string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", model.htmlFilePath);

            string htmlHeaderFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", model.htmlHeaderFilePath);
            htmlHeaderFilePath = _pdfUtility.GetHeader(htmlHeaderFilePath);

            var html = GetHTMLViewer(model.SPName, model, htmlFilePath, htmlHeaderFilePath, colList, headerList, totalList, model.groupByLabel, model.columnWidths /*model.columnAlignments*/);
            //var html = GetHTMLViewerGroupBy(model.SPName, model, htmlFilePath, htmlHeaderFilePath, colList, headerList, totalList, groupbyList, model.groupByLabel);

            html = html.Replace("{{HospitalHeader}}", htmlHeaderFilePath);

            var tuple = _pdfUtility.GeneratePdfFromHtml(html, model.StorageBaseUrl, model.FolderName, model.FileName + vDate, vPageOrg, PaperKind.A4);
            string byteFile = Convert.ToBase64String(tuple.Item1);
            return byteFile;

        }

        private static string GetHTMLViewer(string sp_Name, ReportConfigDto model, string htmlFilePath, string htmlHeaderFilePath, string[] colList, string[] headerList = null, string[] totalColList = null, string groupByCol = "", string[] columnWidths = null, string[] columnAlignments = null)
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
                    Value = ((property.Key == "FromDate" || property.Key == "ToDate") ? DateTime.ParseExact(property.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture) : property.Value.ToString())
                };

                para[sp_Para] = param;
                sp_Para++;
            }
            var dt = odal.FetchDataTableBySP(sp_Name, para, true);

            string html = File.ReadAllText(htmlFilePath);
            html = html.Replace("{{HospitalHeader}}", htmlHeaderFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{RepoertName}}", model.RepoertName);

            DateTime FromDate = Convert.ToDateTime(model.SearchFields.Find(x => x.FieldName?.ToLower() == "fromdate".ToLower())?.FieldValue ?? null);
            DateTime ToDate = Convert.ToDateTime(model.SearchFields.Find(x => x.FieldName?.ToLower() == "todate".ToLower())?.FieldValue ?? null);

            StringBuilder HeaderItems = new("");
            StringBuilder items = new("");
            StringBuilder ItemsTotal = new("");
            StringBuilder ItemsNetTotal = new("");
            StringBuilder itemsGroup = new("");
            //dont remove code, vimal is creating common method for all reports..
            //string final = GetCommonHtmlTableReports(dt, headerList, model.colList, totalColList, model.groupByLabel.Split(','));
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
                case "MultiTotalReportFormat.html":
                    {

                        HeaderItems.Append(GetCommonHtmlTableHeader(dt, headerList, columnWidths));
                        items.Append(GetCommonHtmlTableReports(dt, headerList, model.colList, totalColList, model.groupByLabel.Split(',').Where(x => x != "").ToArray()));
                        if (model.summaryLabel.Split(',').Where(x => x != "").Any()) // if need to display summary 
                                                                                     //  if (model.groupByLabel.Split(',').Where(x => x != "").Any())
                            ItemsTotal.Append(CreateSummary(dt, totalColList, model.summaryLabel.Split(',')));


                        else
                            ItemsTotal.Append(CreateGrandTotal(dt, totalColList.Where(x => !string.IsNullOrWhiteSpace(x)).ToArray(), model.groupByLabel.Split(',').Where(x => !string.IsNullOrWhiteSpace(x)).ToArray()));
                        if (model.Mode == "DailyCollectionSummary")
                            ItemsTotal.Append(CreateSummaryIncome(dt, headerList, model.groupByLabel.Split(',').Where(x => x != "").ToArray(), totalColList));
                    }
                    break;

            }

            if (!string.IsNullOrEmpty(T_Count.ToString()))
                html = html.Replace("{{T_Count}}", T_Count.ToString());

            html = html.Replace("{{HeaderItems}}", HeaderItems.ToString());
            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{itemsGroup}}", itemsGroup.ToString());

            html = html.Replace("{{ItemsTotal}}", ItemsTotal.ToString());

            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yyyy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yyyy"));
            return html;

        }

        // Create  by Ashutosh 24 Jun 2025
        public static string GetCommonHtmlTableHeader(DataTable dt, string[] headers, string[] columnWidths = null)
        {
            StringBuilder table = new();
            table.Append("<tr>");
            for (int i = 0; i < headers.Length; i++)
            {
                string widthStyle = (columnWidths != null && i < columnWidths.Length && !string.IsNullOrWhiteSpace(columnWidths[i]))
                    ? $"width: {columnWidths[i]}%;"
                    : "";

                table.Append($"<th style=\"border: 1px solid #d4c3c3; padding: 6px; {widthStyle}\">");
                table.Append(headers[i]);
                table.Append("</th>");
            }
            table.Append("</tr>");
            return table.ToString();
        }
        /// changes 4 july 2025
        //public static string GetCommonHtmlTableHeader(DataTable dt, string[] headers,string[] columnWidths = null,string[] columnAlignments = null)
        //{
        //    StringBuilder table = new();
        //    table.Append("<tr>");

        //    for (int i = 0; i < headers.Length; i++)
        //    {
        //        string widthStyle = (columnWidths != null && i < columnWidths.Length && !string.IsNullOrWhiteSpace(columnWidths[i]))
        //            ? $"width: {columnWidths[i].Trim()}%;"
        //            : "";

        //        string alignStyle = (columnAlignments != null && i < columnAlignments.Length && !string.IsNullOrWhiteSpace(columnAlignments[i]))
        //            ? $"text-align: {columnAlignments[i].Trim().ToLower()};"
        //            : ""; // default

        //        table.Append($"<th style=\"border: 1px solid #d4c3c3; padding: 6px; {widthStyle} {alignStyle}\">");
        //        table.Append(headers[i]);
        //        table.Append("</th>");
        //    }

        //    table.Append("</tr>");
        //    return table.ToString();
        //}




        public static string GetCommonHtmlTableReports(DataTable dt, string[] headers, string[] columnDataNames, string[] footer, string[] groupBy)
        {
            StringBuilder table = new();
            //groupBy = new string[2] { "PaymentAddedByName", "PatientName" };
            int RowNo = 1;
            if (groupBy.Length > 0)
            {
                var groups1 = dt.AsEnumerable().Select(row => row.Field<string>(groupBy[0])).Distinct().ToList();
                foreach (string group1 in groups1)
                {
                    var group1Data = dt.Select(groupBy[0] + "='" + group1 + "'");
                    table.Append("<tr style='font-size:20px;color:black;'><th style='border:1px solid #000;padding:3px;text-align:left;' colspan='").Append(headers.Length).Append("'>").Append(group1).Append("</th></tr>");
                    if (groupBy.Length > 1)
                    {
                        var groups2 = group1Data.AsEnumerable().Select(row => row.Field<string>(groupBy[1])).Distinct().ToList();
                        foreach (string group2 in groups2)
                        {
                            var group2Data = group1Data.Where(x => x[groupBy[1]].ToString().ToLower() == group2.ToLower());
                            table.Append("<tr style='font-size:18px;color:black;'><th style='border:1px solid #000;padding:3px;text-align:left;' colspan='").Append(headers.Length).Append("'>").Append(group2).Append("</th></tr>");
                            if (groupBy.Length > 2)
                            {
                                var groups3 = group2Data.AsEnumerable().Select(row => row.Field<string>(groupBy[2])).Distinct().ToList();
                                foreach (string group3 in groups3)
                                {
                                    var group3Data = group2Data.Where(x => x[groupBy[2]].ToString().ToLower() == group3.ToLower());
                                    table.Append("<tr style='font-size:16px;color:black;'><th style='border:1px solid #000;padding:3px;text-align:left;' colspan='").Append(headers.Length).Append("'>").Append(group3).Append("</th></tr>");
                                    if (groupBy.Length > 3)
                                    {
                                        var groups4 = group3Data.AsEnumerable().Select(row => row.Field<string>(groupBy[3])).Distinct().ToList();
                                        foreach (string group4 in groups4)
                                        {
                                            table.Append("<tr style='font-size:14px;color:black;'><th style='border:1px solid #000;padding:3px;text-align:left;' colspan='").Append(headers.Length).Append("'>").Append(group4).Append("</th></tr>");
                                            CreateRows(group3Data.Where(x => x[groupBy[3]].ToString().ToLower() == group4.ToLower()), table, headers, columnDataNames, ref RowNo);
                                        }
                                    }
                                    else
                                    {
                                        CreateRows(group3Data, table, headers, columnDataNames, ref RowNo);
                                    }
                                    CreateFooterGroupBy(group3Data, table, footer, group3);
                                }
                            }
                            else
                            {
                                CreateRows(group2Data, table, headers, columnDataNames, ref RowNo);
                            }
                            if (footer != null && footer.Any(x => !string.IsNullOrWhiteSpace(x)))
                                CreateFooterGroupBy(group2Data, table, footer, group2);
                        }
                    }
                    else
                    {
                        CreateRows(group1Data, table, headers, columnDataNames, ref RowNo);
                    }
                    if (footer != null && footer.Any(x => !string.IsNullOrWhiteSpace(x)))
                        CreateFooterGroupBy(group1Data, table, footer, group1);
                }
                if (footer != null && footer.Any(x => !string.IsNullOrWhiteSpace(x)))
                    CreateFooterGroupBy(dt.AsEnumerable(), table, footer, "Total", true);
            }
            else
            {
                CreateRows(dt.AsEnumerable(), table, headers, columnDataNames, ref RowNo);
            }
            return table.ToString();
        }

        public static void CreateRows(IEnumerable<DataRow> group2Data, StringBuilder table, string[] headers, string[] columnDataNames, ref int RowNo)
        {
            foreach (var row in group2Data)
            {
                table.Append("<tr style='text-align: center; border: 1px solid #d4c3c3;'>");

                if (headers.Contains("Sr.No"))
                {
                    table.Append("<th style='border: 1px solid #d4c3c3; padding: 6px; font-family: Calibri, \"Helvetica Neue\", Helvetica, sans-serif; font-size:16px;'>");
                    table.Append(RowNo);
                    table.Append("</th>");
                    RowNo++;
                }

                foreach (var hr in columnDataNames)
                {
                    table.Append("<td style='border: 1px solid #d4c3c3; font-family: Calibri, \"Helvetica Neue\", Helvetica, sans-serif; font-size:16px; word-break: break-word; white-space: normal; padding: 4px;'>");
                    table.Append(row.Table.Columns.Contains(hr) ? row[hr].ToString() : "");
                    table.Append("</td>");
                }

                table.Append("</tr>");
            }
        }



        ///  chnages by 4 july
        //public static void CreateRows(IEnumerable<DataRow> group2Data, StringBuilder table, string[] headers, string[] columnDataNames, ref int RowNo)
        //{
        //    foreach (var row in group2Data)
        //    {
        //        table.Append("<tr style='border: 1px solid #d4c3c3;'>");

        //        // Sr.No Column
        //        if (headers.Contains("Sr.No"))
        //        {
        //            table.Append("<td style='border: 1px solid #d4c3c3; padding: 6px; text-align: center;'>");
        //            table.Append(RowNo);
        //            table.Append("</td>");
        //            RowNo++;
        //        }

        //        foreach (var hr in columnDataNames)
        //        {
        //            string value = row.Table.Columns.Contains(hr) ? row[hr]?.ToString() ?? "" : "";

        //            string alignment = "left"; // default alignment

        //            // Determine alignment
        //            if (DateTime.TryParse(value, out _))
        //                alignment = "center";
        //            else if (double.TryParse(value, out _))
        //                alignment = "right";

        //            table.Append($"<td style='border: 1px solid #d4c3c3; text-align: {alignment}; padding: 6px;'>");
        //            table.Append(value);
        //            table.Append("</td>");
        //        }

        //        table.Append("</tr>");
        //    }
        //}


        //public static void CreateFooterGroupBy(IEnumerable<DataRow> groupData, StringBuilder table, string[] footer, string groupName, bool isTotal = false)
        //{
        //    table.Append("<tr style='border:1px solid black;color:black;background-color:#f9f9f9; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'>");
        //    int col = 1;
        //    int colspan = 1;
        //    foreach (var hr in footer)
        //    {
        //        string total = "";
        //        if (hr.ToLower() == "space")
        //            colspan++;
        //        else if (hr.ToLower() == "labletotal")
        //            total = isTotal ? "Total" : ("Sub Total for " + groupName);
        //        else
        //            total = groupData.Sum(row => row.IsNull(hr) ? 0 : Convert.ToDecimal(row[hr])).ToString();
        //        if (!string.IsNullOrWhiteSpace(total) || footer.Length == col)
        //        {
        //            table.Append("<th style='border: 1px solid #d4c3c3; padding: 6px;' colspan='").Append(colspan).Append("'>").Append(total).Append("</th>");
        //            colspan = 1;
        //        }
        //        col++;
        //    }
        //    table.Append("</tr>");
        //}


        public static void CreateFooterGroupBy(IEnumerable<DataRow> groupData, StringBuilder table, string[] footer, string groupName, bool isTotal = false)
        {
            // Row-level styling
            table.Append("<tr style='border:1px solid black; color:black; background-color:#f9f9f9; " + "font-family: Calibri, \"Helvetica Neue\", Helvetica, Arial, sans-serif; font-size:15px;'>");

            int col = 1;
            int colspan = 1;

            foreach (var hr in footer)
            {
                string total = "";
                if (hr.ToLower() == "space")
                {
                    colspan++;
                }
                else if (hr.ToLower() == "labletotal")
                {
                    // Apply different font + bold for label
                    total = isTotal
                        ? "<span style='font-family: Times New Roman, serif; font-size:15px; font-weight:bold;'>Total</span>"
                        : "<span style='font-family: Times New Roman, serif; font-size:15px; font-weight:bold;'>Sub Total for " + groupName + "</span>";
                }
                else
                {
                    // Numeric totals stay with default font
                    total = groupData.Sum(row => row.IsNull(hr) ? 0 : Convert.ToDecimal(row[hr])).ToString();
                }

                if (!string.IsNullOrWhiteSpace(total) || footer.Length == col)
                {
                    table.Append("<th style='border: 1px solid #d4c3c3; padding: 6px; " +
                                 "font-family: Calibri, \"Helvetica Neue\", Helvetica, Arial, sans-serif; font-size:15px;' " +
                                 "colspan='").Append(colspan).Append("'>").Append(total).Append("</th>");
                    colspan = 1;
                }
                col++;
            }

            table.Append("</tr>");
        }


        public static string CreateSummary(DataTable dt, string[] totalColList, string[] summaries)
        {
            StringBuilder table = new();
            int col = 1; int colspan = 1;
            foreach (var summary in summaries)
            {
                var groups = dt.AsEnumerable().Select(row => row.Field<string>(summary)).Distinct().ToList();
                foreach (var group in groups)
                {
                    table.Append("<tr style='border:1px solid black; color:black; background-color:#e6ffe6; font-weight:bold;'>");
                    col = 1; colspan = 1;
                    foreach (var colName in totalColList)
                    {
                        if (colName == "space")
                        {
                            if (totalColList.Length == col)
                                table.Append("<td style='text-align:center; border:1px solid #d4c3c3; padding:6px;' colspan='").Append(colspan).Append("'></td>");
                            else
                                colspan++;
                        }
                        else if (colName == "lableTotal")
                        {
                            table.Append("<td style='text-align:center; border:1px solid #d4c3c3; padding:6px;' colspan='").Append(colspan).Append("'>Total " + group + "</td>");
                            colspan = 1;
                        }
                        else
                        {
                            string total = dt.Select(summaries[0] + "='" + group + "'").Sum(row => row.IsNull(colName) ? 0 : Convert.ToDecimal(row[colName])).ToString();
                            table.Append("<td style='text-align:center; border:1px solid #d4c3c3; padding:6px;'>" + total + "</td>");
                        }
                        col++;
                    }
                    table.Append("</tr>");
                }
            }
            return table.ToString();
        }


        public static string CreateSummaryIncome(DataTable dt, string[] headers, string[] groupCol, string[] totalColList)
        {



            StringBuilder table = new();

            // Define expected groups
            string[] groupNames = { "Income", "Expense" };

            // Dictionary to store totals for each group
            Dictionary<string, Dictionary<string, decimal>> groupTotals = new();
            table.Append("<tr style='font-size:20px;color:black;'><th style='border:0;padding-top:10px;padding-bottom:10px;text-align:left;' colspan='").Append(headers.Length).Append("'>").Append("Summary").Append("</th></tr>");
            int col = 1; int colspan = 1;
            foreach (var group in groupNames)
            {
                groupTotals[group] = new Dictionary<string, decimal>();

                // Get distinct subgroups for this main group
                var subGroups = dt.AsEnumerable()
                                  .Where(row => row[groupCol[0]].ToString() == group)
                                  .Select(row => row[groupCol[1]].ToString())
                                  .Distinct();


                // Group Total row
                table.Append("<tr style='border:1px solid black; color:black; background-color:#e6ffe6; font-weight:bold;'>");
                col = 1; colspan = 1;
                foreach (var colName in totalColList)
                {
                    if (colName == "space")
                    {
                        if (totalColList.Length == col)
                            table.Append("<td style='text-align:center; border:1px solid #d4c3c3; padding:6px;' colspan='").Append(colspan).Append("'></td>");
                        else
                            colspan++;
                    }
                    else if (colName == "lableTotal")
                    {
                        table.Append($"<td style='text-align:center; border:1px solid #d4c3c3; padding:6px;' colspan='").Append(colspan).Append("'>Total ").Append(group).Append("</td>");
                        colspan = 1;
                    }
                    else
                    {
                        decimal total = dt.Select($"{groupCol[0]} = '{group}'")
                                          .Sum(row => row.IsNull(colName) ? 0 : Convert.ToDecimal(row[colName]));
                        groupTotals[group][colName] = total;

                        table.Append($"<td style='text-align:right; border:1px solid #d4c3c3; padding:6px;'>{total:N2}</td>");
                    }
                    col++;
                }

                table.Append("</tr>");

                // Start new table for each group
                foreach (var subGroup in subGroups)
                {
                    table.Append("<tr style='border:1px solid black; color:black; background-color:#f0f0f0;'>");
                    col = 1; colspan = 1;
                    foreach (var colName in totalColList)
                    {
                        if (colName == "space")
                        {
                            if (totalColList.Length == col)
                                table.Append("<td style='text-align:center; border:1px solid #d4c3c3; padding:6px;' colspan='").Append(colspan).Append("'></td>");
                            else
                                colspan++;
                        }
                        else if (colName == "lableTotal")
                        {
                            table.Append($"<td style='text-align:left; border:1px solid #d4c3c3; padding:6px;' colspan='").Append(colspan).Append("'>").Append(subGroup).Append(" Sub Total</td>");
                            colspan = 1;
                        }
                        else
                        {
                            decimal subTotal = dt.Select($"{groupCol[0]} = '{group}' AND {groupCol[1]} = '{subGroup}'")
                                                 .Sum(row => row.IsNull(colName) ? 0 : Convert.ToDecimal(row[colName]));
                            table.Append($"<td style='text-align:right; border:1px solid #d4c3c3; padding:6px;'>{subTotal:N2}</td>");
                        }
                        col++;
                    }

                    table.Append("</tr>");
                }
            }

            // Grand Total row (Income - Expense)
            table.Append("<tr style='border:1px solid black; color:black; background-color:#ccffff; font-weight:bold;'>");
            col = 1; colspan = 1;
            decimal grandTotalSum = 0; // ✅ To accumulate the final sum
            foreach (var colName in totalColList)
            {
                if (colName == "space")
                {
                    if (totalColList.Length == col)
                        table.Append("<td style='text-align:center; border:1px solid #d4c3c3; padding:6px;' colspan='").Append(colspan).Append("'></td>");
                    else
                        colspan++;
                }
                else if (colName == "lableTotal")
                {
                    table.Append("<td style='text-align:center; border:1px solid #d4c3c3; padding:6px;'  colspan='").Append(colspan).Append("'>Grand Total</td>");
                    colspan = 1;
                }
                else
                {
                    decimal income = groupTotals.ContainsKey("Income") && groupTotals["Income"].ContainsKey(colName)
                        ? groupTotals["Income"][colName] : 0;
                    decimal expense = groupTotals.ContainsKey("Expense") && groupTotals["Expense"].ContainsKey(colName)
                        ? groupTotals["Expense"][colName] : 0;
                    decimal net = income - expense;

                    // ✅ Add to grand total sum
                    grandTotalSum += net;

                    table.Append($"<td style='text-align:right; border:1px solid #d4c3c3; padding:6px;'>{net:N2}</td>");
                }
                col++;
            }

            // ✅ Add new line (new row) showing overall Grand Total
            table.Append("<tr style='border:2px solid black; background-color:#99ffff; font-weight:bold;'>");
            table.Append("<td colspan='").Append(totalColList.Length - 2)
                 .Append("' style='text-align:right; padding:6px;'>Overall Grand Total :</td>");
            table.Append($"<td colspan='3' style='text-align:right; padding:6px;'>{grandTotalSum:N2}</td>");

            table.Append("</tr>");

            return table.ToString();


        }

        public static string CreateGrandTotal(DataTable dt, string[] totalColList, string[] summaries)
        {
            // Add Grand Total Row (without grouping)
            StringBuilder table = new();
            int colspan = 1;
            if (totalColList.Length > 0)
            {
                int col = 1;
                table.Append("<tr style='border:1px solid black; color:black; background-color:#f9f9f9; font-weight:bold;'>");
                foreach (var colName in totalColList)
                {
                    if (colName == "space")
                    {
                        if (totalColList.Length == col)
                            table.Append("<td style='text-align:center; border:1px solid #d4c3c3; padding:6px;' colspan='").Append(colspan).Append("'></td>");
                        else
                            colspan++;
                    }
                    else if (colName == "lableTotal")
                    {
                        table.Append("<td style='text-align:center; border:1px solid #d4c3c3; padding:6px;'colspan='").Append(colspan).Append("'>Total Amount</td>");
                        colspan++;
                    }
                    else
                    {
                        string grandTotal = dt.AsEnumerable()
                                              .Sum(row => row.IsNull(colName) ? 0 : Convert.ToDecimal(row[colName]))
                                              .ToString();
                        table.Append("<td style='text-align:center; border:1px solid #d4c3c3; padding:6px;'>" + grandTotal + "</td>");
                    }
                    col++;
                }
                table.Append("</tr>");
            }
            return table.ToString();
        }
        private static string GetHTMLViewerGroupBy(string sp_Name, ReportConfigDto model, string htmlFilePath, string htmlHeaderFilePath, string[] colList, string[] headerList = null, string[] totalColList = null, string[] groupbyList = null, string groupByLabel = "", string[] columnWidths = null, string[] columnAlignments = null)
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
                    Value = ((property.Key == "FromDate" || property.Key == "ToDate") ? DateTime.ParseExact(property.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture) : property.Value.ToString())
                };

                para[sp_Para] = param;
                sp_Para++;
            }
            var dt = odal.FetchDataTableBySP(sp_Name, para);

            string html = File.ReadAllText(htmlFilePath);
            html = html.Replace("{{HospitalHeader}}", htmlHeaderFilePath);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{RepoertName}}", model.RepoertName);

            DateTime FromDate = Convert.ToDateTime(model.SearchFields.Find(x => x.FieldName?.ToLower() == "fromdate".ToLower())?.FieldValue ?? null);
            DateTime ToDate = Convert.ToDateTime(model.SearchFields.Find(x => x.FieldName?.ToLower() == "todate".ToLower())?.FieldValue ?? null);

            StringBuilder HeaderItems = new("");
            StringBuilder items = new("");
            StringBuilder ItemsTotal = new("");
            StringBuilder ItemsNetTotal = new("");
            StringBuilder itemsGroup = new("");

            double T_Count = 0;
            switch (model.htmlFilePath)
            {
                // Simple Report Format
                case "MultiGroupByTotalReportFormat.html":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #d4c3c3; padding: 6px;\">");
                            HeaderItems.Append(hr.ConvertToString());
                            HeaderItems.Append("</th>");
                        }
                        HeaderItems.Append("</tr>");

                        var dynamicVariableSub = new Dictionary<string, double>();
                        var dynamicVariableGrandTotal = new Dictionary<string, double>();

                        // Initialize both dictionaries
                        foreach (var colName in totalColList)
                        {
                            if (!string.IsNullOrEmpty(colName) && colName != "lableTotal" && colName != "space")
                            {
                                dynamicVariableSub[colName] = 0;
                                dynamicVariableGrandTotal[colName] = 0;
                            }
                        }

                        List<string> groupByList = groupbyList?.ToList() ?? new List<string>();

                        var dataRows = dt.AsEnumerable();

                        // First group level
                        var groupedData = dataRows
                            .GroupBy(r => string.Join("|", groupByList.Select(g => r[g].ConvertToString())))
                            .OrderBy(g => g.Key);

                        foreach (var group in groupedData)
                        {
                            var groupKeys = group.Key.Split('|');

                            // Print group headers
                            for (int level = 0; level < groupByList.Count; level++)
                            {
                                var label = groupKeys[level];
                                items.Append("<tr style='font-size:20px;color:black;'>")
                                     .Append("<td colspan='13' style='border:1px solid #000;padding:3px;text-align:left;'>")
                                     .Append(groupByList[level] + ": " + label).Append("</td></tr>");
                            }

                            // Reset subtotal accumulator
                            foreach (var colName in totalColList)
                            {
                                if (!string.IsNullOrEmpty(colName) && colName != "space" && colName != "lableTotal")
                                    dynamicVariableSub[colName] = 0;
                                dynamicVariableGrandTotal[colName] = 0;
                            }

                            int rowIndex = 0;

                            // Print rows in this group
                            foreach (var dr in group)
                            {
                                rowIndex++;
                                items.Append("<tr style='text-align: center; border: 1px solid #d4c3c3;'>")
                                     .Append("<td style='border: 1px solid #d4c3c3;'>").Append(rowIndex).Append("</td>");

                                foreach (var colName in colList)
                                {
                                    items.Append("<td style='border: 1px solid #d4c3c3;'>")
                                         .Append(dr[colName].ConvertToString()).Append("</td>");
                                }

                                items.Append("</tr>");

                                // Add to subtotal
                                foreach (var colName in totalColList)
                                {
                                    if (!string.IsNullOrEmpty(colName) && colName != "space" && colName != "lableTotal")
                                        //double value = dr[colName].ConvertToDouble();
                                        //dynamicVariableSub[colName] += value;
                                        //dynamicVariableGrandTotal[colName] += value;
                                        dynamicVariableSub[colName] += dr[colName].ConvertToDouble();
                                    dynamicVariableGrandTotal[colName] += dr[colName].ConvertToDouble();

                                }
                            }

                            // Print subtotal for this group
                            items.Append("<tr style='border:1px solid black;color:black;background-color:#f9f9f9;'>");
                            foreach (var colName in totalColList)
                            {
                                if (colName == "space")
                                    items.Append("<td style='border: 1px solid #d4c3c3; padding: 6px;'></td>");
                                else if (colName == "lableTotal")
                                    items.Append("<td style='border: 1px solid #d4c3c3; padding: 6px; font-weight:bold;'>Sub Total</td>");
                                else
                                    items.Append("<td style='border: 1px solid #d4c3c3; padding: 6px;'>")
                                          .Append(dynamicVariableSub[colName].ToString("F2")).Append("</td>");
                            }
                            items.Append("</tr>");
                        }
                        // Grand Total Row
                        items.Append("<tr style='border:2px solid black;color:black;background-color:#e8e8e8;font-weight:bold;'>");
                        foreach (var colName in totalColList)
                        {
                            if (colName == "space")
                                items.Append("<td style='border: 1px solid #d4c3c3; padding: 6px;'></td>");
                            else if (colName == "lableTotal")
                                items.Append("<td style='border: 1px solid #d4c3c3; padding: 6px;'>Grand Total</td>");
                            else
                                items.Append("<td style='border: 1px solid #d4c3c3; padding: 6px;'>")
                                      .Append(dynamicVariableGrandTotal[colName].ToString("F2")).Append("</td>");
                        }
                        items.Append("</tr>");


                        //////string previousLabel = "";
                        //////int i = 0, j = 0, Dcount = 0;

                        //////foreach (DataRow dr in dt.Rows)
                        //////{

                        //////    i++; j++;

                        //////    string currentLabel = dr[groupByCol].ConvertToString();

                        //////    // Group changed? Print subtotal for previous group
                        //////    if (!string.IsNullOrEmpty(previousLabel) && previousLabel != currentLabel)
                        //////    {
                        //////        // Append subtotal row
                        //////        items.Append("<tr style='border:1px solid black;color:black;background-color:#f9f9f9;'>");
                        //////        foreach (var colName in totalColList)
                        //////        {
                        //////            if (colName == "space")
                        //////                items.Append("<td style='border: 1px solid #d4c3c3; padding: 6px;'></td>");
                        //////            else if (colName == "lableTotal")
                        //////                items.Append("<td style='border: 1px solid #d4c3c3; padding: 6px; font-weight:bold;'>Sub Total for ")
                        //////                      .Append(previousLabel).Append("</td>");
                        //////            else
                        //////                items.Append("<td style='border: 1px solid #d4c3c3; padding: 6px;'>")
                        //////                      .Append(dynamicVariableSub[colName].ToString("F2")).Append("</td>");
                        //////        }
                        //////        items.Append("</tr>");

                        //////        // Reset group subtotals
                        //////        foreach (var key in totalColList)
                        //////        {
                        //////            if (!string.IsNullOrEmpty(key) && key != "space" && key != "lableTotal")
                        //////                dynamicVariableSub[key] = 0;
                        //////        }

                        //////        Dcount = 0;

                        //////        // New group header
                        //////        items.Append("<tr style='font-size:20px;color:black;'>")
                        //////             .Append("<td colspan='13' style='border:1px solid #000;padding:3px;text-align:left;'>")
                        //////             .Append(currentLabel).Append("</td></tr>");
                        //////    }

                        //////    // First group
                        //////    if (i == 1)
                        //////    {
                        //////        items.Append("<tr style='font-size:20px;color:black;'>")
                        //////             .Append("<td colspan='13' style='border:1px solid #000;padding:3px;text-align:left;'>")
                        //////             .Append(currentLabel).Append("</td></tr>");
                        //////    }

                        //////    // Append row data
                        //////    items.Append("<tr style='text-align: center; border: 1px solid #d4c3c3;'>")
                        //////         .Append("<td style='border: 1px solid #d4c3c3;'>").Append(i).Append("</td>");
                        //////    foreach (var colName in colList)
                        //////    {
                        //////        items.Append("<td style='border: 1px solid #d4c3c3;'>")
                        //////             .Append(dr[colName].ConvertToString()).Append("</td>");
                        //////    }
                        //////    items.Append("</tr>");

                        //////    previousLabel = currentLabel;
                        //////    Dcount++;

                        //////    // Accumulate both group and total values
                        //////    foreach (var colName in totalColList)
                        //////    {
                        //////        if (!string.IsNullOrEmpty(colName) && colName != "space" && colName != "lableTotal")
                        //////        {
                        //////            double value = dr[colName].ConvertToDouble();
                        //////            //dynamicVariable[colName] += value;
                        //////            dynamicVariableSub[colName] += value;
                        //////        }
                        //////    }
                        //////}
                        //////if (!string.IsNullOrEmpty(previousLabel))
                        //////{
                        //////    items.Append("<tr style='border:1px solid black;color:black;background-color:#f9f9f9;'>");
                        //////    foreach (var colName in totalColList)
                        //////    {
                        //////        if (colName == "space")
                        //////            items.Append("<td style='border: 1px solid #d4c3c3; padding: 6px;'></td>");
                        //////        else if (colName == "lableTotal")
                        //////            items.Append("<td style='border: 1px solid #d4c3c3; padding: 6px; font-weight:bold;'>Sub Total for ")
                        //////                  .Append(previousLabel).Append("</td>");
                        //////        else
                        //////            items.Append("<td style='border: 1px solid #d4c3c3; padding: 6px;'>")
                        //////                  .Append(dynamicVariableSub[colName].ToString("F2")).Append("</td>");
                        //////    }
                        //////    items.Append("</tr>");
                        //////}
                        //////if (totalColList.Count() > 0 && totalColList != null)
                        //////{
                        //////    ItemsTotal.Append("<tr style='border:1px solid black;color:black;background-color:#f9f9f9; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'>");
                        //////    foreach (var colName in totalColList)
                        //////    {
                        //////        if (colName == "space")
                        //////            ItemsTotal.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"></td>");
                        //////        else if (colName == "lableTotal")
                        //////            ItemsTotal.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">Total</td>");
                        //////        else if (!string.IsNullOrEmpty(colName))
                        //////            ItemsTotal.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dynamicVariableSub[colName].ToString("F2")).Append("</td>");
                        //////        else
                        //////            ItemsTotal.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\"></td>");
                        //////    }
                        //////    ItemsTotal.Append("</tr>");
                        //////}

                    }
                    break;

            }

            if (!string.IsNullOrEmpty(T_Count.ToString()))
                html = html.Replace("{{T_Count}}", T_Count.ToString());

            html = html.Replace("{{HeaderItems}}", HeaderItems.ToString());
            html = html.Replace("{{Items}}", items.ToString());

            html = html.Replace("{{itemsGroup}}", itemsGroup.ToString());

            html = html.Replace("{{ItemsTotal}}", ItemsTotal.ToString());

            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

        }
        private static string GetHTMLViewWithTwoSPs(string sp_Name1, string sp_Name2, ReportRequestModel model, string htmlFilePath, string htmlHeaderFilePath, string[] colList, string[] headerList = null, string[] totalColList = null, string groupByCol = "")
        {
            // Build parameter list from search fields
            Dictionary<string, string> fields = HIMS.Data.Extensions.SearchFieldExtension.GetSearchFields(model.SearchFields).ToDictionary(e => e.FieldName, e => e.FieldValueString);

            DatabaseHelper odal = new();
            SqlParameter[] para = fields.Select(f => new SqlParameter("@" + f.Key, f.Value)).ToArray();

            // Fetch data from first stored procedure
            var dt = odal.FetchDataTableBySP(sp_Name1, para);

            // Fetch data from second stored procedure
            var dt2 = odal.FetchDataTableBySP(sp_Name2, para);

            string html = File.ReadAllText(htmlFilePath);
            //html = html.Replace("{{HospitalHeader}}", htmlHeader);
            html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
            html = html.Replace("{{RepoertName}}", model.RepoertName);

            DateTime FromDate = Convert.ToDateTime(model.SearchFields.Find(x => x.FieldName?.ToLower() == "fromdate".ToLower())?.FieldValue ?? null);
            DateTime ToDate = Convert.ToDateTime(model.SearchFields.Find(x => x.FieldName?.ToLower() == "todate".ToLower())?.FieldValue ?? null);

            StringBuilder HeaderItems = new("");
            StringBuilder items = new("");
            StringBuilder ItemsTotal = new("");

            switch (model.Mode)
            {
                // Simple Report Format
                //case "RegistrationReport":
                case "AppointmentListReport":
                case "IpDischargeSummaryReport":
                    {

                        int i = 0;
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{DischargeTime}}", dt.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{Followupdate}}", dt.GetColValue("Followupdate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        
                         html = html.Replace("{{DischargeSummaryTime}}", dt.GetColValue("DischargeSummaryTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));


                        html = html.Replace("{{History}}", dt.GetColValue("History"));
                        html = html.Replace("{{Diagnosis}}", dt.GetColValue("Diagnosis"));
                        html = html.Replace("{{ClinicalFinding}}", dt.GetColValue("ClinicalFinding"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{OpertiveNotes}}", dt.GetColValue("OpertiveNotes"));
                        html = html.Replace("{{TreatmentGiven}}", dt.GetColValue("TreatmentGiven"));
                        html = html.Replace("{{Investigation}}", dt.GetColValue("Investigation"));

                        html = html.Replace("{{p}}", dt.GetColValue("History"));
                        html = html.Replace("{{R}}", dt.GetColValue("Diagnosis"));
                        html = html.Replace("{{SPO2}}", dt.GetColValue("ClinicalFinding"));
                        html = html.Replace("{{RS}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{PA}}", dt.GetColValue("OpertiveNotes"));
                        html = html.Replace("{{CVS}}", dt.GetColValue("TreatmentGiven"));
                        html = html.Replace("{{CNS}}", dt.GetColValue("Investigation"));

                        html = html.Replace("{{DischargeTypeName}}", dt.GetColValue("DischargeTypeName"));
                        html = html.Replace("{{TreatmentAdvisedAfterDischarge}}", dt.GetColValue("TreatmentAdvisedAfterDischarge"));

                        html = html.Replace("{{ClinicalConditionOnAdmisssion}}", dt.GetColValue("ClinicalConditionOnAdmisssion"));

                        html = html.Replace("{{OtherConDrOpinions}}", dt.GetColValue("OtherConDrOpinions"));

                        html = html.Replace("{{PainManagementTechnique}}", dt.GetColValue("PainManagementTechnique"));
                        html = html.Replace("{{LifeStyle}}", dt.GetColValue("LifeStyle"));

                        html = html.Replace("{{WarningSymptoms}}", dt.GetColValue("WarningSymptoms"));
                        html = html.Replace("{{ConditionAtTheTimeOfDischarge}}", dt.GetColValue("ConditionAtTheTimeOfDischarge"));




                        html = html.Replace("{{DoctorAssistantName}}", dt.GetColValue("DoctorAssistantName"));
                        html = html.Replace("{{PreOthNumber}}", dt.GetColValue("PreOthNumber"));
                        html = html.Replace("{{SurgeryProcDone}}", dt.GetColValue("SurgeryProcDone"));
                        html = html.Replace("{{ICD10CODE}}", dt.GetColValue("ICD10CODE"));
                        html = html.Replace("{{Radiology}}", dt.GetColValue("Radiology"));
                        html = html.Replace("{{IsNormalOrDeath}}", dt.GetColValue("IsNormalOrDeath"));

                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{RefDoctorName}}", dt.GetColValue("RefDoctorName"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{AddedBy}}", dt.GetColValue("AddedBy"));
                        html = html.Replace("{{DischargeDoctor2}}", dt.GetColValue("DiscDoctor2"));

                        //border: 1px solid #d4c3c3;

                        foreach (DataRow dr in dt2.Rows)
                        {
                            i++;
                            items.Append("<tr style=\" text-align: center; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, sans-serif;font-size: 18px;\"><td style=\"border-left: 1px solid #d4c3c3;text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: left;font-size: 18px; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("<br>").Append(dr["ItemGenericName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: center;font-size: 18px; padding: 6px;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3;text-align: center;font-size: 18px; padding: 6px;\">").Append(dr["DoseNameInEnglish"].ConvertToString()).Append("</td>");
                            //items.Append("<td style=\"border-right: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("(").Append(dr["DoseNameInMarathi"].ConvertToString()).Append(")").Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3;  text-align: center;font-size: 18px; padding: 6px;\">").Append(dr["Days"].ConvertToString()).Append("</td></tr>");


                        }

                        html = html.Replace("{{Items}}", items.ToString());

                        html = html.Replace("{{chkHistoryflag}}", dt.GetColValue("History").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkHistoryflag}}", dt.GetColValue("History").ConvertToString() != "" ? "block" : "table-row");
                        html = html.Replace("{{chkRadiologyflag}}", dt.GetColValue("Radiology").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkRadiologyflag}}", dt.GetColValue("Radiology").ConvertToString() != "" ? "block" : "table-row");
                        html = html.Replace("{{chkDignosflag}}", dt.GetColValue("Diagnosis").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkClfindingflag}}", dt.GetColValue("ClinicalFinding").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkOprativeflag}}", dt.GetColValue("OpertiveNotes").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkTreatmentflag}}", dt.GetColValue("TreatmentGiven").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkInvestigationflag}}", dt.GetColValue("Investigation").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chktreatadviceflag}}", dt.GetColValue("TreatmentAdvisedAfterDischarge").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkTreatmentflag}}", dt.GetColValue("TreatmentGiven").ConvertToString() != " " ? "table-row" : "none");
                        html = html.Replace("{{chkInvestigationflag}}", dt.GetColValue("Investigation").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkClinicalconditionflag}}", dt.GetColValue("ClinicalConditionOnAdmisssion").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkotherconditionflag}}", dt.GetColValue("OtherConDrOpinions").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkpainmanageflag}}", dt.GetColValue("PainManagementTechnique").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkConondiscflag}}", dt.GetColValue("ConditionAtTheTimeOfDischarge").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkLifeStyleflag}}", dt.GetColValue("LifeStyle").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkWarningSymptomsflag}}", dt.GetColValue("WarningSymptoms").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkSurgeryProcDoneflag}}", dt.GetColValue("SurgeryProcDone").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkidischragetypeflag}}", dt.GetColValue("DischargeTypeName").ConvertToString() != "NULL" ? "table-row" : "none");
                        html = html.Replace("{{chkSurgeryProcDoneflag}}", dt2.GetColValue("SurgeryProcDone").ConvertToString() != "" ? "table-row" : "none");
                        //html = html.Replace("{{chkSurgeryPrescriptionflag}}", length != 0 ? "table-row" : "none");
                    }
                    break;
                case "IpDischargeSummaryReportWithoutHeader":
                    {

                        int i = 0;
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{DischargeTime}}", dt.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{Followupdate}}", dt.GetColValue("Followupdate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{DischargeSummaryTime}}", dt.GetColValue("DischargeSummaryTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        html = html.Replace("{{History}}", dt.GetColValue("History"));
                        html = html.Replace("{{Diagnosis}}", dt.GetColValue("Diagnosis"));
                        html = html.Replace("{{ClinicalFinding}}", dt.GetColValue("ClinicalFinding"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{OpertiveNotes}}", dt.GetColValue("OpertiveNotes"));
                        html = html.Replace("{{TreatmentGiven}}", dt.GetColValue("TreatmentGiven"));
                        html = html.Replace("{{Investigation}}", dt.GetColValue("Investigation"));

                        html = html.Replace("{{p}}", dt.GetColValue("History"));
                        html = html.Replace("{{R}}", dt.GetColValue("Diagnosis"));
                        html = html.Replace("{{SPO2}}", dt.GetColValue("ClinicalFinding"));
                        html = html.Replace("{{RS}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{PA}}", dt.GetColValue("OpertiveNotes"));
                        html = html.Replace("{{CVS}}", dt.GetColValue("TreatmentGiven"));
                        html = html.Replace("{{CNS}}", dt.GetColValue("Investigation"));

                        html = html.Replace("{{DischargeTypeName}}", dt.GetColValue("DischargeTypeName"));
                        html = html.Replace("{{TreatmentAdvisedAfterDischarge}}", dt.GetColValue("TreatmentAdvisedAfterDischarge"));

                        html = html.Replace("{{ClinicalConditionOnAdmisssion}}", dt.GetColValue("ClinicalConditionOnAdmisssion"));

                        html = html.Replace("{{OtherConDrOpinions}}", dt.GetColValue("OtherConDrOpinions"));

                        html = html.Replace("{{PainManagementTechnique}}", dt.GetColValue("PainManagementTechnique"));
                        html = html.Replace("{{LifeStyle}}", dt.GetColValue("LifeStyle"));

                        html = html.Replace("{{WarningSymptoms}}", dt.GetColValue("WarningSymptoms"));
                        html = html.Replace("{{ConditionAtTheTimeOfDischarge}}", dt.GetColValue("ConditionAtTheTimeOfDischarge"));




                        html = html.Replace("{{DoctorAssistantName}}", dt.GetColValue("DoctorAssistantName"));
                        html = html.Replace("{{PreOthNumber}}", dt.GetColValue("PreOthNumber"));
                        html = html.Replace("{{SurgeryProcDone}}", dt.GetColValue("SurgeryProcDone"));
                        html = html.Replace("{{ICD10CODE}}", dt.GetColValue("ICD10CODE"));
                        html = html.Replace("{{Radiology}}", dt.GetColValue("Radiology"));
                        html = html.Replace("{{IsNormalOrDeath}}", dt.GetColValue("IsNormalOrDeath"));

                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{RefDoctorName}}", dt.GetColValue("RefDoctorName"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{AddedBy}}", dt.GetColValue("AddedBy"));
                        html = html.Replace("{{DischargeDoctor2}}", dt.GetColValue("DiscDoctor2"));

                        //border: 1px solid #d4c3c3;

                        foreach (DataRow dr in dt2.Rows)
                        {
                            i++;
                            items.Append("<tr style=\" text-align: center; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, sans-serif;font-size: 18px;\"><td style=\"border-left: 1px solid #d4c3c3;text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: left;font-size: 18px; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("<br>").Append(dr["ItemGenericName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: center;font-size: 18px; padding: 6px;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3;text-align: center;font-size: 18px; padding: 6px;\">").Append(dr["DoseNameInEnglish"].ConvertToString()).Append("</td>");
                            //items.Append("<td style=\"border-right: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("(").Append(dr["DoseNameInMarathi"].ConvertToString()).Append(")").Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3;  text-align: center;font-size: 18px; padding: 6px;\">").Append(dr["Days"].ConvertToString()).Append("</td></tr>");


                        }

                        html = html.Replace("{{Items}}", items.ToString());

                        html = html.Replace("{{chkHistoryflag}}", dt.GetColValue("History").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkHistoryflag}}", dt.GetColValue("History").ConvertToString() != "" ? "block" : "table-row");
                        html = html.Replace("{{chkRadiologyflag}}", dt.GetColValue("Radiology").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkRadiologyflag}}", dt.GetColValue("Radiology").ConvertToString() != "" ? "block" : "table-row");
                        html = html.Replace("{{chkDignosflag}}", dt.GetColValue("Diagnosis").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkClfindingflag}}", dt.GetColValue("ClinicalFinding").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkOprativeflag}}", dt.GetColValue("OpertiveNotes").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkTreatmentflag}}", dt.GetColValue("TreatmentGiven").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkInvestigationflag}}", dt.GetColValue("Investigation").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chktreatadviceflag}}", dt.GetColValue("TreatmentAdvisedAfterDischarge").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkTreatmentflag}}", dt.GetColValue("TreatmentGiven").ConvertToString() != " " ? "table-row" : "none");
                        html = html.Replace("{{chkInvestigationflag}}", dt.GetColValue("Investigation").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkClinicalconditionflag}}", dt.GetColValue("ClinicalConditionOnAdmisssion").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkotherconditionflag}}", dt.GetColValue("OtherConDrOpinions").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkpainmanageflag}}", dt.GetColValue("PainManagementTechnique").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkConondiscflag}}", dt.GetColValue("ConditionAtTheTimeOfDischarge").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkLifeStyleflag}}", dt.GetColValue("LifeStyle").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkWarningSymptomsflag}}", dt.GetColValue("WarningSymptoms").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkSurgeryProcDoneflag}}", dt.GetColValue("SurgeryProcDone").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkidischragetypeflag}}", dt.GetColValue("DischargeTypeName").ConvertToString() != "NULL" ? "table-row" : "none");
                        html = html.Replace("{{chkSurgeryProcDoneflag}}", dt2.GetColValue("SurgeryProcDone").ConvertToString() != "" ? "table-row" : "none");
                        //html = html.Replace("{{chkSurgeryPrescriptionflag}}", length != 0 ? "table-row" : "none");
                    }
                    break;
                case "IpDischargeSummaryTemplate":
                    {
                        int i = 0;

                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{DischargeTime}}", dt.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{Followupdate}}", dt.GetColValue("Followupdate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{DischargeSummaryTime}}", dt.GetColValue("DischargeSummaryTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        html = html.Replace("{{History}}", dt.GetColValue("History"));
                        html = html.Replace("{{Diagnosis}}", dt.GetColValue("Diagnosis"));
                        html = html.Replace("{{ClinicalFinding}}", dt.GetColValue("ClinicalFinding"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{OpertiveNotes}}", dt.GetColValue("OpertiveNotes"));
                        html = html.Replace("{{TreatmentGiven}}", dt.GetColValue("TreatmentGiven"));
                        html = html.Replace("{{Investigation}}", dt.GetColValue("Investigation"));

                        html = html.Replace("{{p}}", dt.GetColValue("History"));
                        html = html.Replace("{{R}}", dt.GetColValue("Diagnosis"));
                        html = html.Replace("{{SPO2}}", dt.GetColValue("ClinicalFinding"));
                        html = html.Replace("{{RS}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{PA}}", dt.GetColValue("OpertiveNotes"));
                        html = html.Replace("{{CVS}}", dt.GetColValue("TreatmentGiven"));
                        html = html.Replace("{{CNS}}", dt.GetColValue("Investigation"));

                        html = html.Replace("{{DischargeTypeName}}", dt.GetColValue("DischargeTypeName"));
                        html = html.Replace("{{TreatmentAdvisedAfterDischarge}}", dt.GetColValue("TreatmentAdvisedAfterDischarge"));

                        html = html.Replace("{{ClinicalConditionOnAdmisssion}}", dt.GetColValue("ClinicalConditionOnAdmisssion"));

                        html = html.Replace("{{OtherConDrOpinions}}", dt.GetColValue("OtherConDrOpinions"));

                        html = html.Replace("{{PainManagementTechnique}}", dt.GetColValue("PainManagementTechnique"));
                        html = html.Replace("{{LifeStyle}}", dt.GetColValue("LifeStyle"));

                        html = html.Replace("{{WarningSymptoms}}", dt.GetColValue("WarningSymptoms"));
                        html = html.Replace("{{ConditionAtTheTimeOfDischarge}}", dt.GetColValue("ConditionAtTheTimeOfDischarge"));


                        html = html.Replace("{{IsNormalOrDeath}}", dt.GetColValue("IsNormalOrDeath"));

                        html = html.Replace("{{DoctorAssistantName}}", dt.GetColValue("DoctorAssistantName"));
                        html = html.Replace("{{PreOthNumber}}", dt.GetColValue("PreOthNumber"));
                        html = html.Replace("{{SurgeryProcDone}}", dt.GetColValue("SurgeryProcDone"));
                        html = html.Replace("{{ICD10CODE}}", dt.GetColValue("ICD10CODE"));
                        html = html.Replace("{{Radiology}}", dt.GetColValue("Radiology"));


                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{RefDoctorName}}", dt.GetColValue("RefDoctorName"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{AddedBy}}", dt.GetColValue("AddedBy"));
                        html = html.Replace("{{TemplateDescriptionHtml}}", dt.GetColValue("TemplateDescriptionHtml"));
                        html = html.Replace("{{DischargeDoctor2}}", dt.GetColValue("DiscDoctor2"));

                        //html = html.Replace("{{chkSurgeryPrescriptionflag}}", length != 0 ? "table-row" : "none");
                        string chkSurgeryPrescriptionflag = (dt2.Rows.Count > 0) ? "table-row" : "none";
                        html = html.Replace("{{chkSurgeryPrescriptionflag}}", chkSurgeryPrescriptionflag);

                        //border: 1px solid #d4c3c3;

                        foreach (DataRow dr in dt2.Rows)
                        {
                            i++;
                            items.Append("<tr style=\" text-align: center; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, sans-serif;font-size: 24px;\"><td style=\"border-left: 1px solid #d4c3c3;text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("<br>").Append(dr["ItemGenericName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3;text-align: center; padding: 6px;\">").Append(dr["DoseNameInEnglish"].ConvertToString()).Append("</td>");

                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3;  text-align: center; padding: 6px;\">").Append(dr["Days"].ConvertToString()).Append("</td></tr>");


                            items.Append("<tr style=\"text-align: center; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size: 24px;\"><td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td>");
                            //items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("(").Append(dr["DoseNameInMarathi"].ConvertToString()).Append(")").Append("</td>");
                            items.Append("<td style=\"border-right: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td></tr>");


                        }
                    }
                    break;
                case "IpDischargeSummaryTemplateWithoutHeader":
                    {
                        int i = 0;

                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{DischargeTime}}", dt.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{Followupdate}}", dt.GetColValue("Followupdate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{DischargeSummaryTime}}", dt.GetColValue("DischargeSummaryTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        html = html.Replace("{{History}}", dt.GetColValue("History"));
                        html = html.Replace("{{Diagnosis}}", dt.GetColValue("Diagnosis"));
                        html = html.Replace("{{ClinicalFinding}}", dt.GetColValue("ClinicalFinding"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{OpertiveNotes}}", dt.GetColValue("OpertiveNotes"));
                        html = html.Replace("{{TreatmentGiven}}", dt.GetColValue("TreatmentGiven"));
                        html = html.Replace("{{Investigation}}", dt.GetColValue("Investigation"));

                        html = html.Replace("{{p}}", dt.GetColValue("History"));
                        html = html.Replace("{{R}}", dt.GetColValue("Diagnosis"));
                        html = html.Replace("{{SPO2}}", dt.GetColValue("ClinicalFinding"));
                        html = html.Replace("{{RS}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{PA}}", dt.GetColValue("OpertiveNotes"));
                        html = html.Replace("{{CVS}}", dt.GetColValue("TreatmentGiven"));
                        html = html.Replace("{{CNS}}", dt.GetColValue("Investigation"));

                        html = html.Replace("{{DischargeTypeName}}", dt.GetColValue("DischargeTypeName"));
                        html = html.Replace("{{TreatmentAdvisedAfterDischarge}}", dt.GetColValue("TreatmentAdvisedAfterDischarge"));

                        html = html.Replace("{{ClinicalConditionOnAdmisssion}}", dt.GetColValue("ClinicalConditionOnAdmisssion"));

                        html = html.Replace("{{OtherConDrOpinions}}", dt.GetColValue("OtherConDrOpinions"));

                        html = html.Replace("{{PainManagementTechnique}}", dt.GetColValue("PainManagementTechnique"));
                        html = html.Replace("{{LifeStyle}}", dt.GetColValue("LifeStyle"));

                        html = html.Replace("{{WarningSymptoms}}", dt.GetColValue("WarningSymptoms"));
                        html = html.Replace("{{ConditionAtTheTimeOfDischarge}}", dt.GetColValue("ConditionAtTheTimeOfDischarge"));


                        html = html.Replace("{{IsNormalOrDeath}}", dt.GetColValue("IsNormalOrDeath"));

                        html = html.Replace("{{DoctorAssistantName}}", dt.GetColValue("DoctorAssistantName"));
                        html = html.Replace("{{PreOthNumber}}", dt.GetColValue("PreOthNumber"));
                        html = html.Replace("{{SurgeryProcDone}}", dt.GetColValue("SurgeryProcDone"));
                        html = html.Replace("{{ICD10CODE}}", dt.GetColValue("ICD10CODE"));
                        html = html.Replace("{{Radiology}}", dt.GetColValue("Radiology"));


                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{RefDoctorName}}", dt.GetColValue("RefDoctorName"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{AddedBy}}", dt.GetColValue("AddedBy"));
                        html = html.Replace("{{TemplateDescriptionHtml}}", dt.GetColValue("TemplateDescriptionHtml"));
                        html = html.Replace("{{DischargeDoctor2}}", dt.GetColValue("DiscDoctor2"));

                        //html = html.Replace("{{chkSurgeryPrescriptionflag}}", length != 0 ? "table-row" : "none");
                        string chkSurgeryPrescriptionflag = (dt2.Rows.Count > 0) ? "table-row" : "none";
                        html = html.Replace("{{chkSurgeryPrescriptionflag}}", chkSurgeryPrescriptionflag);

                        //border: 1px solid #d4c3c3;

                        foreach (DataRow dr in dt2.Rows)
                        {
                            i++;
                            items.Append("<tr style=\" text-align: center; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, sans-serif;font-size: 24px;\"><td style=\"border-left: 1px solid #d4c3c3;text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("<br>").Append(dr["ItemGenericName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3;text-align: center; padding: 6px;\">").Append(dr["DoseNameInEnglish"].ConvertToString()).Append("</td>");

                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3;  text-align: center; padding: 6px;\">").Append(dr["Days"].ConvertToString()).Append("</td></tr>");


                            items.Append("<tr style=\"text-align: center; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size: 24px;\"><td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td>");
                            //items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("(").Append(dr["DoseNameInMarathi"].ConvertToString()).Append(")").Append("</td>");
                            items.Append("<td style=\"border-right: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("</td></tr>");


                        }
                    }
                    break;


            }
            html = html.Replace("{{HeaderItems}}", HeaderItems.ToString());
            html = html.Replace("{{Items}}", items.ToString());
            html = html.Replace("{{ItemsTotal}}", ItemsTotal.ToString());
            html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
            html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
            return html;

            //return (dt1, dt2);
        }


        //private static string GetHTMLViewWithSPHTML(string sp_Name, ReportRequestModel model, string htmlFilePath, string htmlHeaderFilePath, string[] colList, string[] headerList = null, string[] totalColList = null, string groupByCol = "")
        //{
        //    Dictionary<string, string> fields = HIMS.Data.Extensions.SearchFieldExtension.GetSearchFields(model.SearchFields).ToDictionary(e => e.FieldName, e => e.FieldValueString);
        //    DatabaseHelper odal = new();
        //    int sp_Para = 0;
        //    SqlParameter[] para = new SqlParameter[fields.Count];
        //    foreach (var property in fields)
        //    {
        //        var param = new SqlParameter
        //        {
        //            ParameterName = "@" + property.Key,
        //            Value = property.Value.ToString()
        //        };

        //        para[sp_Para] = param;
        //        sp_Para++;
        //    }
        //    var dt = odal.FetchDataTableBySP(sp_Name, para);


        //    //string htmlHeader = _pdfUtility.GetHeader(htmlHeaderFilePath, model.BaseUrl);

        //    string html = File.ReadAllText(htmlFilePath);
        //    //html = html.Replace("{{HospitalHeader}}", htmlHeader);
        //    html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
        //    html = html.Replace("{{RepoertName}}", model.RepoertName);

        //    DateTime FromDate = Convert.ToDateTime(model.SearchFields.Find(x => x.FieldName?.ToLower() == "fromdate".ToLower())?.FieldValue ?? null);
        //    DateTime ToDate = Convert.ToDateTime(model.SearchFields.Find(x => x.FieldName?.ToLower() == "todate".ToLower())?.FieldValue ?? null);

        //    StringBuilder HeaderItems = new("");
        //    StringBuilder items = new("");
        //    StringBuilder ItemsTotal = new("");
        //    double T_Count = 0;
        //    switch (model.Mode)
        //    {
        //        // Simple Report Format
        //        //case "RegistrationReport":
        //        case "AppointmentListReport":
        //        case "IpDischargeSummaryReport":
        //            {

        //                int i = 0;
        //                html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
        //                html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
        //                html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
        //                html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
        //                html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
        //                html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
        //                html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
        //                html = html.Replace("{{DischargeTime}}", dt.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
        //                html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
        //                html = html.Replace("{{Followupdate}}", dt.GetColValue("Followupdate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

        //                html = html.Replace("{{History}}", dt.GetColValue("History"));
        //                html = html.Replace("{{Diagnosis}}", dt.GetColValue("Diagnosis"));
        //                html = html.Replace("{{ClinicalFinding}}", dt.GetColValue("ClinicalFinding"));
        //                html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
        //                html = html.Replace("{{OpertiveNotes}}", dt.GetColValue("OpertiveNotes"));
        //                html = html.Replace("{{TreatmentGiven}}", dt.GetColValue("TreatmentGiven"));
        //                html = html.Replace("{{Investigation}}", dt.GetColValue("Investigation"));

        //                html = html.Replace("{{p}}", dt.GetColValue("History"));
        //                html = html.Replace("{{R}}", dt.GetColValue("Diagnosis"));
        //                html = html.Replace("{{SPO2}}", dt.GetColValue("ClinicalFinding"));
        //                html = html.Replace("{{RS}}", dt.GetColValue("DoctorName"));
        //                html = html.Replace("{{PA}}", dt.GetColValue("OpertiveNotes"));
        //                html = html.Replace("{{CVS}}", dt.GetColValue("TreatmentGiven"));
        //                html = html.Replace("{{CNS}}", dt.GetColValue("Investigation"));

        //                html = html.Replace("{{DischargeTypeName}}", dt.GetColValue("DischargeTypeName"));
        //                html = html.Replace("{{TreatmentAdvisedAfterDischarge}}", dt.GetColValue("TreatmentAdvisedAfterDischarge"));

        //                html = html.Replace("{{ClinicalConditionOnAdmisssion}}", dt.GetColValue("ClinicalConditionOnAdmisssion"));

        //                html = html.Replace("{{OtherConDrOpinions}}", dt.GetColValue("OtherConDrOpinions"));

        //                html = html.Replace("{{PainManagementTechnique}}", dt.GetColValue("PainManagementTechnique"));
        //                html = html.Replace("{{LifeStyle}}", dt.GetColValue("LifeStyle"));

        //                html = html.Replace("{{WarningSymptoms}}", dt.GetColValue("WarningSymptoms"));
        //                html = html.Replace("{{ConditionAtTheTimeOfDischarge}}", dt.GetColValue("ConditionAtTheTimeOfDischarge"));




        //                html = html.Replace("{{DoctorAssistantName}}", dt.GetColValue("DoctorAssistantName"));
        //                html = html.Replace("{{PreOthNumber}}", dt.GetColValue("PreOthNumber"));
        //                html = html.Replace("{{SurgeryProcDone}}", dt.GetColValue("SurgeryProcDone"));
        //                html = html.Replace("{{ICD10CODE}}", dt.GetColValue("ICD10CODE"));
        //                html = html.Replace("{{Radiology}}", dt.GetColValue("Radiology"));
        //                html = html.Replace("{{IsNormalOrDeath}}", dt.GetColValue("IsNormalOrDeath"));

        //                html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
        //                html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
        //                html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
        //                html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
        //                html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
        //                html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
        //                html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
        //                html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
        //                html = html.Replace("{{RefDoctorName}}", dt.GetColValue("RefDoctorName"));
        //                html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
        //                html = html.Replace("{{AddedBy}}", dt.GetColValue("AddedBy"));
        //                html = html.Replace("{{DischargeDoctor2}}", dt.GetColValue("DischargeDoctor2"));

        //                //border: 1px solid #d4c3c3;

        //                foreach (DataRow dr in dt.Rows)
        //                {
        //                    i++;
        //                    items.Append("<tr style=\" text-align: center; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, sans-serif;font-size: 24px;\"><td style=\"border-left: 1px solid #d4c3c3;text-align: center; padding: 6px;\">").Append(i).Append("</td>");
        //                    //items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("<br>").Append(dr["ItemGenericName"].ConvertToString()).Append("</td>");
        //                    //items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
        //                    //items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3;text-align: center; padding: 6px;\">").Append(dr["DoseNameInEnglish"].ConvertToString()).Append("</td>");
        //                    //items.Append("<td style=\"border-right: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("(").Append(dr["DoseNameInMarathi"].ConvertToString()).Append(")").Append("</td>");
        //                    //items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3;  text-align: center; padding: 6px;\">").Append(dr["Days"].ConvertToString()).Append("</td></tr>");


        //                }

        //                html = html.Replace("{{Items}}", items.ToString());

        //                html = html.Replace("{{chkHistoryflag}}", dt.GetColValue("History").ConvertToString() != "" ? "table-row" : "none");

        //                html = html.Replace("{{chkHistoryflag}}", dt.GetColValue("History").ConvertToString() != "" ? "block" : "table-row");

        //                html = html.Replace("{{chkRadiologyflag}}", dt.GetColValue("Radiology").ConvertToString() != "" ? "table-row" : "none");

        //                html = html.Replace("{{chkRadiologyflag}}", dt.GetColValue("Radiology").ConvertToString() != "" ? "block" : "table-row");


        //                html = html.Replace("{{chkDignosflag}}", dt.GetColValue("Diagnosis").ConvertToString() != "" ? "table-row" : "none");

        //                html = html.Replace("{{chkClfindingflag}}", dt.GetColValue("ClinicalFinding").ConvertToString() != "" ? "table-row" : "none");

        //                html = html.Replace("{{chkOprativeflag}}", dt.GetColValue("OpertiveNotes").ConvertToString() != "" ? "table-row" : "none");

        //                html = html.Replace("{{chkTreatmentflag}}", dt.GetColValue("TreatmentGiven").ConvertToString() != "" ? "table-row" : "none");


        //                html = html.Replace("{{chkInvestigationflag}}", dt.GetColValue("Investigation").ConvertToString() != "" ? "table-row" : "none");



        //                html = html.Replace("{{chktreatadviceflag}}", dt.GetColValue("TreatmentAdvisedAfterDischarge").ConvertToString() != "" ? "table-row" : "none");

        //                html = html.Replace("{{chkTreatmentflag}}", dt.GetColValue("TreatmentGiven").ConvertToString() != " " ? "table-row" : "none");


        //                html = html.Replace("{{chkInvestigationflag}}", dt.GetColValue("Investigation").ConvertToString() != "" ? "table-row" : "none");

        //                html = html.Replace("{{chkClinicalconditionflag}}", dt.GetColValue("ClinicalConditionOnAdmisssion").ConvertToString() != "" ? "table-row" : "none");



        //                html = html.Replace("{{chkotherconditionflag}}", dt.GetColValue("OtherConDrOpinions").ConvertToString() != "" ? "table-row" : "none");

        //                html = html.Replace("{{chkpainmanageflag}}", dt.GetColValue("PainManagementTechnique").ConvertToString() != "" ? "table-row" : "none");


        //                html = html.Replace("{{chkConondiscflag}}", dt.GetColValue("ConditionAtTheTimeOfDischarge").ConvertToString() != "" ? "table-row" : "none");


        //                html = html.Replace("{{chkLifeStyleflag}}", dt.GetColValue("LifeStyle").ConvertToString() != "" ? "table-row" : "none");

        //                html = html.Replace("{{chkWarningSymptomsflag}}", dt.GetColValue("WarningSymptoms").ConvertToString() != "" ? "table-row" : "none");


        //                html = html.Replace("{{chkSurgeryProcDoneflag}}", dt.GetColValue("SurgeryProcDone").ConvertToString() != "" ? "table-row" : "none");
        //                html = html.Replace("{{chkidischragetypeflag}}", dt.GetColValue("DischargeTypeName").ConvertToString() != "NULL" ? "table-row" : "none");


        //                html = html.Replace("{{chkSurgeryProcDoneflag}}", dt.GetColValue("SurgeryProcDone").ConvertToString() != "" ? "table-row" : "none");
        //                //html = html.Replace("{{chkSurgeryPrescriptionflag}}", length != 0 ? "table-row" : "none");
        //            }
        //            break;
        //    }
        //    html = html.Replace("{{HeaderItems}}", HeaderItems.ToString());
        //    html = html.Replace("{{Items}}", items.ToString());
        //    html = html.Replace("{{ItemsTotal}}", ItemsTotal.ToString());
        //    html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
        //    html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
        //    return html;

        //}

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

                case "RegistrationForm":
                    {

                        int i = 0;


                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{RegId}}", dt.GetColValue("RegId"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{RegDate}}", dt.GetColValue("RegTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{City}}", dt.GetColValue("City"));
                        html = html.Replace("{{CityName}}", dt.GetColValue("CityName"));
                        html = html.Replace("{{PinNo}}", dt.GetColValue("PinNo"));

                        html = html.Replace("{{Address}}", dt.GetColValue("Address"));

                        html = html.Replace("{{MobileNo}}", dt.GetColValue("MobileNo"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{AddedBy}}", dt.GetColValue("AddedBy"));


                        html = html.Replace("{{UpdatedBy}}", dt.GetColValue("UpdatedBy"));
                        html = html.Replace("{{PhoneNo}}", dt.GetColValue("PhoneNo"));
                        html = html.Replace("{{CountryName}}", dt.GetColValue("CountryName"));

                        
                        // New
                        html = html.Replace("{{EmgContactPersonName}}", dt.GetColValue("EmgContactPersonName"));
                        html = html.Replace("{{EmgRelationshipId}}", dt.GetColValue("EmgRelationshipId"));
                        html = html.Replace("{{RelationshipName}}", dt.GetColValue("RelationshipName"));
                        html = html.Replace("{{EmgMobileNo}}", dt.GetColValue("EmgMobileNo"));
                        html = html.Replace("{{EmgAadharCardNo}}", dt.GetColValue("EmgAadharCardNo"));
                        html = html.Replace("{{EngAddress}}", dt.GetColValue("EngAddress"));
                        html = html.Replace("{{MedTourismPassportNo}}", dt.GetColValue("MedTourismPassportNo"));
                        html = html.Replace("{{MedTourismNationalityID}}", dt.GetColValue("MedTourismNationalityID"));
                        html = html.Replace("{{MedTourismResidentialAddress}}", dt.GetColValue("MedTourismResidentialAddress"));
                        html = html.Replace("{{MedTourismOfficeWorkAddress}}", dt.GetColValue("MedTourismOfficeWorkAddress"));
                        html = html.Replace("{{MedTourismCitizenship}}", dt.GetColValue("MedTourismCitizenship"));


                        html = html.Replace("{{chkEmgContactPersonNameflag}}", dt.GetColValue("EmgContactPersonName").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkEmgRelationshipIdflag}}", dt.GetColValue("EmgRelationshipId").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkRelationshipNameflag}}", dt.GetColValue("RelationshipName").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkEmgMobileNoflag}}", dt.GetColValue("EmgMobileNo").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkEmgAadharCardNoflag}}", dt.GetColValue("EmgAadharCardNo").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkEngAddressflag}}", dt.GetColValue("EngAddress").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkMedTourismPassportNoflag}}", dt.GetColValue("MedTourismPassportNo").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkMedTourismNationalityIDflag}}", dt.GetColValue("MedTourismNationalityID").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkMedTourismResidentialAddressflag}}", dt.GetColValue("MedTourismResidentialAddress").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkMedTourismOfficeWorkAddressflag}}", dt.GetColValue("MedTourismOfficeWorkAddress").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkMedTourismCitizenshipflag}}", dt.GetColValue("MedTourismCitizenship").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkCountryNameflag}}", dt.GetColValue("CountryName").ConvertToString() != "" ? "table-row" : "none");








                        return html;

                    }
                    break;

                case "IPStickerPrint":
                    {


                        foreach (DataRow dr in dt.Rows)
                        {
                            //html = html.Replace("{{DataContent}}", htmlHeader);


                            html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName").ToUpper());
                            html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName").ToUpper());

                            html = html.Replace("{{Address}}", dt.GetColValue("Address").ToUpper());
                            html = html.Replace("{{MobileNo}}", dt.GetColValue("MobileNo"));
                            html = html.Replace("{{PhoneNo}}", dt.GetColValue("PhoneNo"));

                            html = html.Replace("{{DOT}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yy hh:mm tt"));
                            html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType").ToUpper());

                            html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName").ToUpper());
                            html = html.Replace("{{BedName}}", dt.GetColValue("BedName").ToUpper());

                            html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                            html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                            html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                            html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));


                            html = html.Replace("{{AdmittedDoctorName}}", dt.GetColValue("AdmittedDoctorName").ToUpper());
                            html = html.Replace("{{RefDoctorName}}", dt.GetColValue("RefDoctorName").ToUpper());

                            html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName").ToUpper());
                            html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName").ToUpper());

                            html = html.Replace("{{RelativeName}}", dt.GetColValue("RelativeName").ToUpper());
                            html = html.Replace("{{RelativePhoneNo}}", dt.GetColValue("RelativePhoneNo"));

                            html = html.Replace("{{RelationshipName}}", dt.GetColValue("RelationshipName").ToUpper());
                            html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                            html = html.Replace("{{IsMLC}}", dt.GetColValue("IsMLC"));
                            html = html.Replace("{{AdmittedDoctor1}}", dt.GetColValue("AdmittedDoctor1").ToUpper());
                            html = html.Replace("{{AdmittedDoctorName}}", dt.GetColValue("AdmittedDoctorName").ToUpper());

                            html = html.Replace("{{MaritalStatusName}}", dt.GetColValue("MaritalStatusName").ToUpper());
                            html = html.Replace("{{AadharcardNo}}", dt.GetColValue("AadharcardNo"));
                            html = html.Replace("{{TariffName}}", dt.GetColValue("TariffName").ToUpper());
                            html = html.Replace("{{AdmittedDoctor2}}", dt.GetColValue("AdmittedDoctor2").ToUpper());
                            html = html.Replace("{{LoginUserSurname}}", dt.GetColValue("LoginUserSurname").ToUpper());


                            //html = html.Replace("{{chkMLCflag}}", dt.GetColValue("IsMLC").ToBool() == true ? "table-row " : "none");
                            //html = html.Replace("{{chkMLCflag1}}", dt.GetColValue("IsMLC").ToBool() == false ? "table-row " : "none");



                            //html = html.Replace("{{chkMLCflag}}", Convert.ToBoolean(dt.GetColValue("IsMLC")) ? "table-row" : "none");
                            //changes by Ashu 27 May 2025
                            bool isMLC = Convert.ToBoolean(dt.GetColValue("IsMLC"));

                            html = html.Replace("{{chkMLCflag}}", isMLC ? "table-row" : "none");
                            html = html.Replace("{{chkMLCflag1}}", !isMLC ? "table-row" : "none");



                            html = html.Replace("{{DOA}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));



                            return html;

                        }

                    }
                    break;

                case "EmergencyPrint":
                    {
                        int i = 0;
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{RegId}}", dt.GetColValue("RegId"));
                        html = html.Replace("{{EmgId}}", dt.GetColValue("EmgId"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{RegDate}}", dt.GetColValue("RegTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{CityName}}", dt.GetColValue("CityName"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{Address}}", dt.GetColValue("Address"));
                        html = html.Replace("{{MobileNo}}", dt.GetColValue("MobileNo"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{EmgDate}}", dt.GetColValue("EmgDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{EmgTime}}", dt.GetColValue("EmgTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{PhoneNo}}", dt.GetColValue("PhoneNo"));
                        html = html.Replace("{{TariffName}}", dt.GetColValue("TariffName"));
                        html = html.Replace("{{ClassName}}", dt.GetColValue("ClassName"));
                        html = html.Replace("{{RefDoctorName}}", dt.GetColValue("RefDoctorName"));
                        html = html.Replace("{{AttendingDoctorName}}", dt.GetColValue("AttendingDoctorName"));
                        html = html.Replace("{{IsMLC}}", dt.GetColValue("IsMLC"));
                        html = html.Replace("{{IsConverted}}", dt.GetColValue("IsConverted"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));
                    

                        return html;

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
                            html = html.Replace("{{OPDNo}}", dt.GetColValue("OPDNo"));
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
                            html = html.Replace("{{PaidAmount}}", dt.GetColValue("NetPayableAmt").ConvertToDouble().To2DecimalPlace());
                            html = html.Replace("{{BillDate}}", dt.GetColValue("BillDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                            html = html.Replace("{{ReceiptNo}}", dt.GetColValue("ReceiptNo"));
                            html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));
                            html = html.Replace("{{Remark}}", dt.GetColValue("Remark"));
                            html = html.Replace("{{PaymentTime}}", dt.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | HH:mm tt"));

                            html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));

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


                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                        //html = html.Replace("{{NewHeader}}", htmlHeader);
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{TotalBillAmount}}", dt.GetColValue("TotalBillAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{ConcessionAmt}}", dt.GetColValue("ConcessionAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{NetPayableAmt}}", dt.GetColValue("NetPayableAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{ConsultantDocName}}", dt.GetColValue("ConsultantDocName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{BillNo}}", dt.GetColValue("PBillNo"));
                        html = html.Replace("{{BillDate}}", dt.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | H:mm tt"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
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
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;
                            items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;font-size:15;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Price"].ConvertToDouble().ToString("F2")).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChargesTotalAmount"].ConvertToDouble().ToString("F2")).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ConcessionAmount"].ConvertToDouble().ToString("F2")).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().ToString("F2")).Append("</td></tr>");

                            T_NetAmount += dr["NetAmount"].ConvertToDouble();
                        }
                        T_NetAmount = Math.Round(T_NetAmount);

                        html = html.Replace("{{Items}}", items.ToString());


                        html = html.Replace("{{T_NetAmount}}", T_NetAmount.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{TotalBillAmount}}", dt.GetColValue("TotalBillAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{BalanceAmt}}", dt.GetColValue("BalanceAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{PaidAmount}}", dt.GetColValue("PaidAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{Price}}", dt.GetColValue("Price").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{TotalGst}}", dt.GetColValue("TotalGst").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{NetPayableAmt}}", dt.GetColValue("NetPayableAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("AddedByName").ConvertToString());
                        html = html.Replace("{{HospitalName}}", dt.GetColValue("HospitalName").ConvertToString());
                        html = html.Replace("{{DiscComments}}", dt.GetColValue("DiscComments").ConvertToString());
                        html = html.Replace("{{CashPay}}", dt.GetColValue("CashPay").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{ChequePay}}", dt.GetColValue("ChequePay").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{CardPay}}", dt.GetColValue("CardPay").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{NeftPay}}", dt.GetColValue("NeftPay").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{PayTMPay}}", dt.GetColValue("PayTMPay").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{PaymentMode}}", dt.GetColValue("PaymentMode").ConvertToString());
                        html = html.Replace("{{RefundInfo}}", dt.GetColValue("RefundInfo"));
                        html = html.Replace("{{chkpaidflag}}", dt.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkbalflag}}", dt.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkdiscflag}}", dt.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkRefundflag}}", dt.GetColValue("RefundAmt").ConvertToDouble() > 0 ? "table-row " : "none");

                        string finalamt = conversion(T_NetAmount.ToString());
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


                        return html;

                    }
                    break;
                case "OPBillWithPackagePrint":
                    {


                        int i = 0, j = 0;
                        double Dcount = 0;
                        string previousLabel = "";
                        int k = 0;
                        var dynamicVariable = new Dictionary<string, double>();


                        //   string html = File.ReadAllText(htmlFilePath);
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                        //    html = html.Replace("{{NewHeader}}", htmlHeader);
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{TotalBillAmount}}", dt.GetColValue("TotalBillAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{ConcessionAmt}}", dt.GetColValue("ConcessionAmt").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{PaymentMode}}", dt.GetColValue("PaymentMode").ConvertToString());
                        html = html.Replace("{{ConsultantDocName}}", dt.GetColValue("ConsultantDocName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName"));
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
                       
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            // Start a new row
                            items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size:15;\">");

                            // For the first row: Show serial number, Service Name in bold, and Doctor's Name
                            if (i == 1)
                            {
                                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px; font-weight: bold;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");

                            }
                            else
                            {
                                // For the subsequent rows: Skip serial number and doctor’s name, indent Service Name slightly
                                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">&nbsp;</td>"); // Empty cell for serial number
                                items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding-left: 20px; font-size: 12px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                            }

                            //// Continue with the rest of the data (Price, Qty, NetAmount)
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Price"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChargesTotalAmount"].ConvertToDouble().ToString("F2")).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ConcessionAmount"].ConvertToDouble().ToString("F2")).Append("</td>");
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
                        html = html.Replace("{{PaymentMode}}", dt.GetColValue("PaymentMode").ConvertToString());


                        html = html.Replace("{{chkpaidflag}}", dt.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkbalflag}}", dt.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkdiscflag}}", dt.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");

                        //html = html.Replace("{{chkRefdrflag}}", Bills.GetColValue("chkRefdrflag").ConvertToDouble() != ' ' ? "table-row " : "none");
                        //html = html.Replace("{{chkCompanyflag}}", Bills.GetColValue("PaidAmount").ConvertToDouble() != ' ' ? "table-row " : "none");


                        string finalamt = conversion(T_NetAmount.ToString());
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


                        return html;

                    }
                    break; 
                case "OPCasePaper":
                    {

                        int i = 0;


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
                        html = html.Replace("{{BP}}", dt.GetColValue("BP"));
                        html = html.Replace("{{Pulse}}", dt.GetColValue("Pulse"));
                        html = html.Replace("{{Height}}", dt.GetColValue("Height"));
                        html = html.Replace("{{Weight}}", dt.GetColValue("PWeight"));
                        html = html.Replace("{{Temp}}", dt.GetColValue("Temp"));
                        html = html.Replace("{{BP}}", dt.GetColValue("BP"));
                        html = html.Replace("{{BSL}}", dt.GetColValue("BSL"));
                        html = html.Replace("{{BMI}}", dt.GetColValue("BMI"));
                        html = html.Replace("{{SpO2}}", dt.GetColValue("SpO2"));



                        html = html.Replace("{{chkBPflag}}", dt.GetColValue("BP").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkPulseflag}}", dt.GetColValue("Pulse").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkHeightflag}}", dt.GetColValue("Height").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkWeightflag}}", dt.GetColValue("PWeight").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkTempflag}}", dt.GetColValue("Temp").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkBSLflag}}", dt.GetColValue("BSL").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkBMIflag}}", dt.GetColValue("BMI").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkSpO2flag}}", dt.GetColValue("SpO2").ConvertToString() != "" ? "visible" : "none");



                        return html;
                    }
                    break;

                case "OPStickerPrint":
                    {

                        int i = 0;


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
                        html = html.Replace("{{BP}}", dt.GetColValue("BP"));
                        html = html.Replace("{{Pulse}}", dt.GetColValue("Pulse"));
                        html = html.Replace("{{Height}}", dt.GetColValue("Height"));
                        html = html.Replace("{{Weight}}", dt.GetColValue("PWeight"));
                        html = html.Replace("{{Temp}}", dt.GetColValue("Temp"));
                        html = html.Replace("{{BP}}", dt.GetColValue("BP"));
                        html = html.Replace("{{BSL}}", dt.GetColValue("BSL"));
                        html = html.Replace("{{BMI}}", dt.GetColValue("BMI"));
                        html = html.Replace("{{SpO2}}", dt.GetColValue("SpO2"));



                        html = html.Replace("{{chkBPflag}}", dt.GetColValue("BP").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkPulseflag}}", dt.GetColValue("Pulse").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkHeightflag}}", dt.GetColValue("Height").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkWeightflag}}", dt.GetColValue("PWeight").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkTempflag}}", dt.GetColValue("Temp").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkBSLflag}}", dt.GetColValue("BSL").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkBMIflag}}", dt.GetColValue("BMI").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkSpO2flag}}", dt.GetColValue("SpO2").ConvertToString() != "" ? "visible" : "none");



                        return html;
                    }
                    break;
                case "OPDSpineCasePaper":
                    {

                        int i = 0;


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
                        html = html.Replace("{{BP}}", dt.GetColValue("BP"));
                        html = html.Replace("{{Pulse}}", dt.GetColValue("Pulse"));
                        html = html.Replace("{{Height}}", dt.GetColValue("Height"));
                        html = html.Replace("{{Weight}}", dt.GetColValue("PWeight"));
                        html = html.Replace("{{Temp}}", dt.GetColValue("Temp"));
                        html = html.Replace("{{BP}}", dt.GetColValue("BP"));
                        html = html.Replace("{{BSL}}", dt.GetColValue("BSL"));
                        html = html.Replace("{{BMI}}", dt.GetColValue("BMI"));
                        html = html.Replace("{{SpO2}}", dt.GetColValue("SpO2"));



                        html = html.Replace("{{chkBPflag}}", dt.GetColValue("BP").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkPulseflag}}", dt.GetColValue("Pulse").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkHeightflag}}", dt.GetColValue("Height").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkWeightflag}}", dt.GetColValue("PWeight").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkTempflag}}", dt.GetColValue("Temp").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkBSLflag}}", dt.GetColValue("BSL").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkBMIflag}}", dt.GetColValue("BMI").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkSpO2flag}}", dt.GetColValue("SpO2").ConvertToString() != "" ? "visible" : "none");



                        return html;

                    }
                    break;

                case "OPPrescription":
                    {

                        int i = 0, j = 0;
                        string previousLabel = "";
                        String Label = "", Label1 = "", Label2 = "";

                        //foreach (DataRow dr in Bills.Rows)
                        //{
                        //    i++;

                        //    items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size:15;\"><td style=\"border-left: 1px solid black;border-bottom:1px solid #000;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
                        //    items.Append("<td style=\"border-left:1px solid #000;padding:3px;border-bottom:1px solid #000;height:10px;text-align:left;vertical-align:middle;\">").Append(dr["DrugName"].ConvertToString()).Append("</td>");
                        //    items.Append("<td style=\"border-left:1px solid #000;padding:3px;border-bottom:1px solid #000;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
                        //    items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["TotalQty"].ConvertToString()).Append("</td></tr>");

                        //}





                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;
                            //if (i == 1 || Label != previousLabel)
                            //{
                            //    j = 1;
                            //    Label = dr["DrugName"].ConvertToString();
                            //    Label1 = dr["GenericName"].ConvertToString();
                            //    Label2 = dr["OldClassName"].ConvertToString();

                            //    items.Append("<tr style=\"font-size:22px; font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"2\" style=\";padding:6px;height:10px;text-align:left;font-weight:bold;vertical-align:middle;padding-left:60px;\">").Append(Label2).Append("-----").Append(Label).Append("</td><td  style=\"padding:6px;height:10px;text-align:left;vertical-align:middle\">").Append(Label1).Append("</td></tr>");
                            //}
                            //previousLabel = dr["DrugName"].ConvertToString();

                            //if (Label == previousLabel)
                            //{

                            //    i++;
                            items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size:20px;\">");
                            items.Append("<td style=\"vertical-align: top; padding: 6px; height: 20px; text-align: center; font-size:16px; font-weight:bold;\">" + i + ")" + "</td>");
                            items.Append("<td style=\"vertical-align: top; padding: 6px; height: 20px; text-align:left; font-weight: bold; font-size:16px; padding-left:7px;\">").Append(dr["DrugName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"padding: 6px;height:10px;vertical-align:middle;text-align:left;font-size:20px;padding-left:10px;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"padding: 6px;height:10px;vertical-align:middle;text-align:left;font-size:20px;padding-left:10px;\">").Append(dr["Instruction"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"vertical-align:middle;padding: 6px;height:10px;text-align:left;font-size:20px;padding-left:10px;\">").Append(dr["TotalQty"].ConvertToString()).Append("</td></tr>");
                            items.Append("</tr>");
                            if (dr["ItemGenericName"].ConvertToString() != null)
                            {
                                items.Append("<tr style=\"font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border: 1px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle;padding-left:50px;\">").Append("Composition :").Append(dr["ItemGenericName"].ConvertToString()).Append("</td></tr>");

                            }

                            items.Append("<tr style=\"font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border:1px;border-bottom: 1px solid #d4c3c3;padding-bottom:20px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle;padding-left:50px;padding-bottom:20px;\">").Append("Timing :").Append(dr["DoseNameInEnglish"].ConvertToString()).Append(dr["DoseNameInMarathi"].ConvertToString()).Append("</td></tr>");





                        }


                        html = html.Replace("{{Items}}", items.ToString());
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{ConsultantDocName}}", dt.GetColValue("ConsultantDocName"));
                        html = html.Replace("{{ReferDrName}}", dt.GetColValue("ReferDrName"));

                        html = html.Replace("{{OPDNo}}", dt.GetColValue("OPDNo"));
                        html = html.Replace("{{PDate}}", dt.GetColValue("PTime").ConvertToDateString("dd/MM/yyyy|hh:mm tt"));
                        html = html.Replace("{{VisitTime}}", dt.GetColValue("VisitTime").ConvertToDateString("dd/MM/yyyy|hh:mm tt"));
                        html = html.Replace("{{FollowupDate}}", dt.GetColValue("FollowupDate").ConvertToDateString("dd/MM/yyyy"));


                        html = html.Replace("{{PrecriptionId}}", dt.GetColValue("PrecriptionId"));

                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{WardName}}", dt.GetColValue("WardName"));
                        html = html.Replace("{{PreparedBy}}", dt.GetColValue("PreparedBy"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{Address}}", dt.GetColValue("Address"));
                        html = html.Replace("{{ChiefComplaint}}", dt.GetColValue("ChiefComplaint"));
                        html = html.Replace("{{Diagnosis}}", dt.GetColValue("Diagnosis"));
                        html = html.Replace("{{Examination}}", dt.GetColValue("Examination"));
                        html = html.Replace("{{SecondRefDoctorName}}", dt.GetColValue("SecondRefDoctorName"));
                        html = html.Replace("{{RequestList}}", dt.GetColValue("RequestList"));
                        html = html.Replace("{{BP}}", dt.GetColValue("BP"));
                        html = html.Replace("{{Pulse}}", dt.GetColValue("Pulse"));
                        html = html.Replace("{{Height}}", dt.GetColValue("Height"));
                        html = html.Replace("{{Weight}}", dt.GetColValue("PWeight"));
                        html = html.Replace("{{Temp}}", dt.GetColValue("Temp"));
                        html = html.Replace("{{BP}}", dt.GetColValue("BP"));
                        html = html.Replace("{{BSL}}", dt.GetColValue("BSL"));
                        html = html.Replace("{{BMI}}", dt.GetColValue("BMI"));
                        html = html.Replace("{{SpO2}}", dt.GetColValue("SpO2"));
                        html = html.Replace("{{Allergy}}", dt.GetColValue("Allergy"));
                        html = html.Replace("{{BloodGroup}}", dt.GetColValue("BloodGroup"));

                        html = html.Replace("{{Signature}}", dt.GetColValue("Signature"));

                        html = html.Replace("{{PathResultDr1}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{MahRegNo}}", dt.GetColValue("MahRegNo"));
                        html = html.Replace("{{Education}}", dt.GetColValue("Education"));
                        html = html.Replace("{{Advice}}", dt.GetColValue("Advice"));

                        html = html.Replace("{{chkBPflag}}", dt.GetColValue("BP").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkPulseflag}}", dt.GetColValue("Pulse").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkHeightflag}}", dt.GetColValue("Height").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkWeightflag}}", dt.GetColValue("PWeight").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkTempflag}}", dt.GetColValue("Temp").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkBSLflag}}", dt.GetColValue("BSL").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkBMIflag}}", dt.GetColValue("BMI").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkSpO2flag}}", dt.GetColValue("SpO2").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkBloodGroupflag}}", dt.GetColValue("BloodGroup").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkAllergyflag}}", dt.GetColValue("Allergy").ConvertToString() != "" ? "visible" : "none");


                        html = html.Replace("{{chkEdu}}", dt.GetColValue("PathResultDr1").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkRegNo}}", dt.GetColValue("PathResultDr1").ConvertToString() != "" ? "table-row" : "none");

                        html = html.Replace("{{chkSignature}}", dt.GetColValue("Signature").ConvertToString() != "" ? "table-row" : "none");


                        return html;
                    }
                    break;

                case "OPPrescriptionwithoutHeader":
                    {

                        int i = 0, j = 0;
                        string previousLabel = "";
                        String Label = "", Label1 = "", Label2 = "";

                        //foreach (DataRow dr in Bills.Rows)
                        //{
                        //    i++;

                        //    items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size:15;\"><td style=\"border-left: 1px solid black;border-bottom:1px solid #000;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
                        //    items.Append("<td style=\"border-left:1px solid #000;padding:3px;border-bottom:1px solid #000;height:10px;text-align:left;vertical-align:middle;\">").Append(dr["DrugName"].ConvertToString()).Append("</td>");
                        //    items.Append("<td style=\"border-left:1px solid #000;padding:3px;border-bottom:1px solid #000;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
                        //    items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["TotalQty"].ConvertToString()).Append("</td></tr>");

                        //}





                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;
                           
                            items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif; font-size:20px;\">");
                            items.Append("<td style=\"vertical-align: top; padding: 6px; height: 20px; text-align: center; font-size:16px; font-weight:bold;\">" + i + ")" + "</td>");
                            items.Append("<td style=\"vertical-align: top; padding: 6px; height: 20px; text-align:left; font-weight: bold; font-size:16px; padding-left:7px;\">").Append(dr["DrugName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"padding: 6px;height:10px;vertical-align:middle;text-align:left;font-size:20px;padding-left:10px;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"padding: 6px;height:10px;vertical-align:middle;text-align:left;font-size:20px;padding-left:10px;\">").Append(dr["Instruction"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"vertical-align:middle;padding: 6px;height:10px;text-align:left;font-size:20px;padding-left:10px;\">").Append(dr["TotalQty"].ConvertToString()).Append("</td></tr>");
                            items.Append("</tr>");
                            if (dr["ItemGenericName"].ConvertToString() != null)
                            {
                                items.Append("<tr style=\"font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border: 1px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle;padding-left:50px;\">").Append("Composition :").Append(dr["ItemGenericName"].ConvertToString()).Append("</td></tr>");

                            }

                            items.Append("<tr style=\"font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border:1px;border-bottom: 1px solid #d4c3c3;padding-bottom:20px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle;padding-left:50px;padding-bottom:20px;\">").Append("Timing :").Append(dr["DoseNameInEnglish"].ConvertToString()).Append(dr["DoseNameInMarathi"].ConvertToString()).Append("</td></tr>");


                         


                        }


                        html = html.Replace("{{Items}}", items.ToString());
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{ConsultantDocName}}", dt.GetColValue("ConsultantDocName"));
                        html = html.Replace("{{ReferDrName}}", dt.GetColValue("ReferDrName"));

                        html = html.Replace("{{OPDNo}}", dt.GetColValue("OPDNo"));
                        html = html.Replace("{{PDate}}", dt.GetColValue("PTime").ConvertToDateString("dd/MM/yyyy|hh:mm tt"));
                        html = html.Replace("{{VisitTime}}", dt.GetColValue("VisitTime").ConvertToDateString("dd/MM/yyyy|hh:mm tt"));
                        html = html.Replace("{{FollowupDate}}", dt.GetColValue("FollowupDate").ConvertToDateString("dd/MM/yyyy"));


                        html = html.Replace("{{PrecriptionId}}", dt.GetColValue("PrecriptionId"));

                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{WardName}}", dt.GetColValue("WardName"));
                        html = html.Replace("{{PreparedBy}}", dt.GetColValue("PreparedBy"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{Address}}", dt.GetColValue("Address"));
                        html = html.Replace("{{ChiefComplaint}}", dt.GetColValue("ChiefComplaint"));
                        html = html.Replace("{{Diagnosis}}", dt.GetColValue("Diagnosis"));
                        html = html.Replace("{{Examination}}", dt.GetColValue("Examination"));
                        html = html.Replace("{{SecondRefDoctorName}}", dt.GetColValue("SecondRefDoctorName"));
                        html = html.Replace("{{RequestList}}", dt.GetColValue("RequestList"));
                        html = html.Replace("{{BP}}", dt.GetColValue("BP"));
                        html = html.Replace("{{Pulse}}", dt.GetColValue("Pulse"));
                        html = html.Replace("{{Height}}", dt.GetColValue("Height"));
                        html = html.Replace("{{Weight}}", dt.GetColValue("PWeight"));
                        html = html.Replace("{{Temp}}", dt.GetColValue("Temp"));
                        html = html.Replace("{{BP}}", dt.GetColValue("BP"));
                        html = html.Replace("{{BSL}}", dt.GetColValue("BSL"));
                        html = html.Replace("{{BMI}}", dt.GetColValue("BMI"));
                        html = html.Replace("{{SpO2}}", dt.GetColValue("SpO2"));
                        html = html.Replace("{{Allergy}}", dt.GetColValue("Allergy"));
                        html = html.Replace("{{BloodGroup}}", dt.GetColValue("BloodGroup"));

                        html = html.Replace("{{PathResultDr1}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{MahRegNo}}", dt.GetColValue("MahRegNo"));
                        html = html.Replace("{{Education}}", dt.GetColValue("Education"));
                        html = html.Replace("{{Advice}}", dt.GetColValue("Advice"));

                        html = html.Replace("{{chkBPflag}}", dt.GetColValue("BP").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkPulseflag}}", dt.GetColValue("Pulse").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkHeightflag}}", dt.GetColValue("Height").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkWeightflag}}", dt.GetColValue("PWeight").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkTempflag}}", dt.GetColValue("Temp").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkBSLflag}}", dt.GetColValue("BSL").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkBMIflag}}", dt.GetColValue("BMI").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkSpO2flag}}", dt.GetColValue("SpO2").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkBloodGroupflag}}", dt.GetColValue("BloodGroup").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkAllergyflag}}", dt.GetColValue("Allergy").ConvertToString() != "" ? "visible" : "none");


                        html = html.Replace("{{chkEdu}}", dt.GetColValue("PathResultDr1").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkRegNo}}", dt.GetColValue("PathResultDr1").ConvertToString() != "" ? "table-row" : "none");

                        html = html.Replace("{{chkSignature}}", dt.GetColValue("Signature").ConvertToString() != "" ? "table-row" : "none");


                        return html;
                    }
                    break;



                case "CertificateInformationReport":
                    {

                        int i = 0;
                        string previousLabel = "";
                        String Label = "", Label1 = "", Label2 = "";
                        double Dcount = 0;
                        var dynamicVariable = new Dictionary<string, double>();



                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            //    items.Append("<tr style\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["CertificateText"].ConvertToString()).Append("</td></tr>");
                        }


                        html = html.Replace("{{Items}}", items.ToString());
                        html = html.Replace("{{CertificateId}}", dt.GetColValue("CertificateId"));

                        html = html.Replace("{{PatientName}}", "<b>" + dt.GetColValue("PatientName") + "</b>");
                        html = html.Replace("{{AgeYear}}", "<b>" + dt.GetColValue("AgeYear") + "</b>");
                        html = html.Replace("{{DoctorName}}", "<b>" + dt.GetColValue("DoctorName") + "</b>");
                        html = html.Replace("{{DepartmentName}}", "<b>" + dt.GetColValue("DepartmentName") + "</b>");
                        html = html.Replace("{{RefDoctorName}}", "<b>" + dt.GetColValue("RefDoctorName") + "</b>");

                        html = html.Replace("{{GenderName}}", "<b>" + dt.GetColValue("GenderName") + "</b>");
                        html = html.Replace("{{OPDNo}}", "<b>" + dt.GetColValue("OPDNo") + "</b>");
                        html = html.Replace("{{IPDNo}}", "<b>" + dt.GetColValue("IPDNo") + "</b>");
                        html = html.Replace("{{BedName}}", "<b>" + dt.GetColValue("BedName") + "</b>");
                        html = html.Replace("{{RoomName}}", "<b>" + dt.GetColValue("RoomName") + "</b>");


                        html = html.Replace("{{CertificateName}}", dt.GetColValue("CertificateName"));

                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{RequestId}}", dt.GetColValue("RequestId"));
                        html = html.Replace("{{OPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{ReqDate}}", dt.GetColValue("ReqDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        html = html.Replace("{{CreatedDate}}", dt.GetColValue("CreatedDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{OPDNo}}", dt.GetColValue("OPDNo"));

                        html = html.Replace("{{PathResultDr1}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{MahRegNo}}", dt.GetColValue("MahRegNo"));
                        html = html.Replace("{{Education}}", dt.GetColValue("Education"));
                        html = html.Replace("{{Signature}}", dt.GetColValue("Signature"));


                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{CertificateText}}", dt.GetColValue("CertificateText"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{OP_IP_Type}}", dt.GetColValue("OP_IP_Type"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));
                        html = html.Replace("{{chkEdu}}", dt.GetColValue("PathResultDr1").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkRegNo}}", dt.GetColValue("PathResultDr1").ConvertToString() != "" ? "table-row" : "none");

                        html = html.Replace("{{chkSignature}}", dt.GetColValue("Signature").ConvertToString() != "" ? "table-row" : "none");


                        return html;
                    }
                    break;


                case "Certificate":
                    {

                        int i = 0;
                        string previousLabel = "";
                        String Label = "", Label1 = "", Label2 = "";
                        double Dcount = 0;
                        var dynamicVariable = new Dictionary<string, double>();



                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr style\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["CertificateText"].ConvertToString()).Append("</td></tr>");
                        }


                        html = html.Replace("{{Items}}", items.ToString());
                        html = html.Replace("{{CertificateId}}", dt.GetColValue("CertificateId"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{CertificateName}}", dt.GetColValue("CertificateName"));

                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{RequestId}}", dt.GetColValue("RequestId"));
                        html = html.Replace("{{OPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{ReqDate}}", dt.GetColValue("ReqDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        html = html.Replace("{{CreatedDate}}", dt.GetColValue("CreatedDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{OPDNo}}", dt.GetColValue("OPDNo"));

                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{CertificateText}}", dt.GetColValue("CertificateText"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{OP_IP_Type}}", dt.GetColValue("OP_IP_Type"));
                        html = html.Replace("{{RefDoctorName}}", dt.GetColValue("RefDoctorName"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));


                        return html;
                    }
                    break;

               
                case "GRNReport":
                    {

                        double T_TotalAmount = 0, T_TotalVatAmount = 0, T_TotalDiscAmount = 0, T_TotalNETAmount = 0, T_TotalBalancepay = 0, T_TotalCGST = 0, T_TotalSGST = 0, T_TotalIGST = 0, T_ItemNetAmount = 0, T_GrandTotalAount = 0;
                        int i = 0, j = 0;
                        double Dcount = 0;
                        string previousLabel = "";

                        int k = 0;
                        var dynamicVariable = new Dictionary<string, double>();
                        foreach (DataRow dr in dt.Rows)
                        {

                            i++;
                            items.Append("<tr  style=\"font-size:15px;border: 1px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;color: #101828 \"><td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["UnitofMeasurementName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["MRP"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ReceiveQty"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["FreeQty"].ConvertToString()).Append("</td>");

                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Rate"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            //items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0px;height:10px;text-align: center;border-bottom:1px solid #000;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DiscPercentage"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["CGSTPer"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["SGSTPer"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VatAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                            T_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                            T_TotalVatAmount += dr["VatAmount"].ConvertToDouble();
                            T_TotalDiscAmount += dr["TotalDiscAmount"].ConvertToDouble();
                            T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                            //T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();
                            T_TotalCGST += dr["CGSTAmt"].ConvertToDouble();
                            T_TotalSGST += dr["SGSTAmt"].ConvertToDouble();
                            T_TotalIGST += dr["IGSTAmt"].ConvertToDouble();
                            T_GrandTotalAount += dr["GrandTotalAount"].ConvertToDouble();


                            //T_ItemNetAmount += dr["ItemNetAmount"].ConvertToDouble();

                        }

                        //| currency:'INR':'symbol-narrow':'0.2'
                        html = html.Replace("{{Items}}", items.ToString());
                        //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
                        //html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
                        //html = html.Replace("{{TotalAmount}}", T_TotalAmount.To2DecimalPlace());
                        //html = html.Replace("{{TotalVatAmount}}", T_TotalVatAmount.To2DecimalPlace());
                        html = html.Replace("{{TotalDiscAmount}}", dt.GetColValue("TotalDiscAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{TotalNETAmount}}", dt.GetColValue("GrnReturnAmount").ConvertToDouble().ToString("F2"));


                        html = html.Replace("{{TotCGSTAmt}}", dt.GetColValue("TotCGSTAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{GrandTotalAount}}", dt.GetColValue("GrandTotalAount").ConvertToDouble().ToString("F2"));


                        html = html.Replace("{{TotalAmount}}", dt.GetColValue("TotalAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{HandlingCharges}}", dt.GetColValue("HandlingCharges").ConvertToDouble().To2DecimalPlace());

                        html = html.Replace("{{TransportChanges}}", dt.GetColValue("TransportChanges").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{DiscAmount}}", dt.GetColValue("DiscAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{TotSGSTAmt}}", dt.GetColValue("TotSGSTAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{TotalDiscAmount}}", dt.GetColValue("TotalDiscAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{OtherCharge}}", dt.GetColValue("OtherCharge").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{CreditNote}}", dt.GetColValue("CreditNote").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{AddedByName}}", dt.GetColValue("AddedByName").ConvertToString());
                        html = html.Replace("{{DebitNote}}", dt.GetColValue("DebitNote").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{Remark}}", dt.GetColValue("Remark").ConvertToString());
                        html = html.Replace("{{TotalVATAmount}}", dt.GetColValue("TotalVATAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{NetPayble}}", dt.GetColValue("NetPayble").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{T_TotalCGST}}", T_TotalCGST.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_TotalSGST}}", T_TotalSGST.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_GrandTotalAount}}", T_GrandTotalAount.ConvertToDouble().To2DecimalPlace());


                        html = html.Replace("{{GRNDate}}", dt.GetColValue("GRNDate").ConvertToDateString("dd/MM/yyyy"));
                        html = html.Replace("{{GRNTime}}", dt.GetColValue("GRNReturnTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
                        html = html.Replace("{{PurchaseTime}}", dt.GetColValue("PurchaseTime").ConvertToDateString("dd/MM/yyyy"));

                        html = html.Replace("{{InvDate}}", dt.GetColValue("InvDate").ConvertToDateString("dd/MM/yyyy"));

                        html = html.Replace("{{GateEntryNo}}", dt.GetColValue("GateEntryNo"));

                        html = html.Replace("{{GrnNumber}}", dt.GetColValue("GrnNumber"));
                        html = html.Replace("{{EwayBillDate}}", dt.GetColValue("EwayBillDate").ConvertToDateString("dd/MM/yyyy"));
                        html = html.Replace("{{GSTNo}}", dt.GetColValue("GSTNo").ConvertToString());
                        html = html.Replace("{{EwayBillNo}}", dt.GetColValue("EwayBillNo"));


                        html = html.Replace("{{SupplierName}}", dt.GetColValue("SupplierName").ConvertToString());
                        html = html.Replace("{{PONo}}", dt.GetColValue("PONo").ConvertToString());
                        html = html.Replace("{{InvoiceNo}}", dt.GetColValue("InvoiceNo").ConvertToString());
                        html = html.Replace("{{StoreName}}", dt.GetColValue("StoreName"));
                        html = html.Replace("{{Address}}", dt.GetColValue("Address"));

                        html = html.Replace("{{Email}}", dt.GetColValue("Email").ConvertToString());
                        html = html.Replace("{{GateEntryNo}}", dt.GetColValue("GateEntryNo").ConvertToString());
                        html = html.Replace("{{Mobile}}", dt.GetColValue("Mobile"));
                        html = html.Replace("{{Phone}}", dt.GetColValue("Phone"));
                        html = html.Replace("{{VatAmount}}", dt.GetColValue("VatAmount").ConvertToString());
                        html = html.Replace("{{Mobile}}", dt.GetColValue("Mobile"));
                        html = html.Replace("{{Phone}}", dt.GetColValue("Phone"));
                        html = html.Replace("{{PONo}}", dt.GetColValue("PONo"));
                        html = html.Replace("{{GrnNumber}}", dt.GetColValue("GrnNumber").ConvertToString());

                        

                        html = html.Replace("{{PrintStoreName}}", dt.GetColValue("PrintStoreName"));

                        string finalamt = conversion(dt.GetColValue("NetPayble").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());

                        //string finalamt = ConvertNumbertoWords(dt.GetColValue("TotalAmount").ConvertToDouble().To2DecimalPlace().Count());
                        //html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());

                        html = html.Replace("{{chkdiscflag}}", dt.GetColValue("T_TotalDiscAmount").ConvertToDouble() > 0 ? "block" : "none");

                        html = html.Replace("{{chkponoflag}}", dt.GetColValue("PONo").ConvertToDouble() != 0 ? "table-row " : "none");

                        return html;


                    }
                    break;
                case "IndentwiseReport":
                    {


                        int i = 0, j = 0;
                        double Dcount = 0;
                        string previousLabel = "";
                        int k = 0;
                        //var dynamicVariable = new Dictionary<string, double>();

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;border-bottom: 1px solid black;text-align:center;font-size:18px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;border-bottom: 1px solid black;font-size:18px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0;border-right:1px solid #000;height:10px;text-align:center;border-bottom: 1px solid black;font-size:18px;\">").Append(dr["Qty"].ConvertToString()).Append("</td></tr>");

                        }



                        html = html.Replace("{{Items}}", items.ToString());
                        //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
                        //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
                        html = html.Replace("{{FromStoreName}}", dt.GetColValue("FromStoreName"));

                        html = html.Replace("{{VLabel}}", dt.GetColValue("VLabel"));
                        html = html.Replace("{{Remark}}", dt.GetColValue("Comments"));
                        html = html.Replace("{{ToStoreName}}", dt.GetColValue("ToStoreName"));
                        html = html.Replace("{{IndentNo}}", dt.GetColValue("IndentNo"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));
                        html = html.Replace("{{IndentTime}}", dt.GetColValue("IndentTime").ConvertToDateString("dd.MM.yyyy hhLmm tt"));
                        html = html.Replace("{{Isverify}}", dt.GetColValue("Isverify"));
                        html = html.Replace("{{Comments}}", dt.GetColValue("Comments"));

                        html = html.Replace("{{PrintStoreName}}", dt.GetColValue("PrintStoreName"));
                        html = html.Replace("{{StoreAddress}}", dt.GetColValue("StoreAddress"));
                        return html;
                    }
                    break;


                case "GRNReturnReport":
                    {


                        int i = 0;
                        Boolean chkdiscflag = false;
                        Boolean chkponoflag = false;

                        double T_TotalAmount = 0, T_TotalVatAmount = 0, T_TotalDiscAmount = 0, T_TotalNETAmount = 0, T_TotalBalancepay = 0, T_TotalCGST = 0, T_TotalSGST = 0, T_TotalIGST = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr  style=\"font-size:15px;border: 1px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;color: #101828 \"><td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["Conversion"].ConvertToString()).Append("</td>");

                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchNo"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchExpiryDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["LandedRate"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ReturnQty"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["LandedTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");

                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DiscPercentage"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["DiscAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalDiscAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                            T_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                            T_TotalVatAmount += dr["DiscAmount"].ConvertToDouble();
                            T_TotalDiscAmount += dr["TotalDiscAmount"].ConvertToDouble();
                            T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                            //T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();
                            //T_TotalCGST += dr["CGSTAmt"].ConvertToDouble();
                            //T_TotalSGST += dr["SGSTAmt"].ConvertToDouble();
                            //T_TotalIGST += dr["IGSTAmt"].ConvertToDouble();


                        }

                        //| currency:'INR':'symbol-narrow':'0.2'
                        html = html.Replace("{{Items}}", items.ToString());
                        //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
                        //html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));
                        html = html.Replace("{{TotalAmount}}", T_TotalAmount.To2DecimalPlace());
                        html = html.Replace("{{TotalVatAmount}}", T_TotalVatAmount.To2DecimalPlace());
                        html = html.Replace("{{NetAmount}}", T_TotalNETAmount.ConvertToDouble().ToString("F2"));

                        html = html.Replace("{{TotalDiscAmount}}", dt.GetColValue("TotalDiscAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{TotalNETAmount}}", dt.GetColValue("GrnReturnAmount").ConvertToDouble().To2DecimalPlace());


                        html = html.Replace("{{TotCGSTAmt}}", dt.GetColValue("TotCGSTAmt").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{GrandTotalAount}}", dt.GetColValue("TotalAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{TotalAmount}}", dt.GetColValue("TotalAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{HandlingCharges}}", dt.GetColValue("HandlingCharges").ConvertToDouble().To2DecimalPlace());

                        html = html.Replace("{{TransportChanges}}", dt.GetColValue("TransportChanges").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{DiscAmount}}", dt.GetColValue("DiscAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{TotSGSTAmt}}", dt.GetColValue("TotSGSTAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{TotalDiscAmount}}", dt.GetColValue("TotalDiscAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{OtherCharge}}", dt.GetColValue("OtherCharge").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{CreditNote}}", dt.GetColValue("CreditNote").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{AddedByName}}", dt.GetColValue("AddedByName").ConvertToString());
                        html = html.Replace("{{DebitNote}}", dt.GetColValue("DebitNote").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{Remark}}", dt.GetColValue("Remark").ConvertToString());
                        html = html.Replace("{{TotalVATAmount}}", dt.GetColValue("TotalVATAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{NetPayble}}", dt.GetColValue("GrnReturnAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{NetAmount}}", dt.GetColValue("NetAmount").ConvertToDouble().ToString("F2"));


                        html = html.Replace("{{T_TotalCGST}}", T_TotalCGST.ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{T_TotalSGST}}", T_TotalSGST.ConvertToDouble().To2DecimalPlace());

                        html = html.Replace("{{GRNDate}}", dt.GetColValue("GRNDate").ConvertToDateString("dd/MM/yyyy"));
                        html = html.Replace("{{GRNReturnDate}}", dt.GetColValue("GRNReturnDate").ConvertToDateString("dd/MM/yyyy"));

                        html = html.Replace("{{GRNTime}}", dt.GetColValue("GRNReturnTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
                        html = html.Replace("{{PurchaseTime}}", dt.GetColValue("PurchaseTime").ConvertToDateString("dd/MM/yyyy"));

                        html = html.Replace("{{InvDate}}", dt.GetColValue("InvDate").ConvertToDateString("dd/MM/yyyy"));

                        html = html.Replace("{{GateEntryNo}}", dt.GetColValue("GateEntryNo"));

                        html = html.Replace("{{GrnNumber}}", dt.GetColValue("GRNReturnNo"));
                        html = html.Replace("{{EwayBillDate}}", dt.GetColValue("EwayBillDate").ConvertToDateString("dd/MM/yyyy"));
                        html = html.Replace("{{GSTNo}}", dt.GetColValue("GSTNo").ConvertToString());
                        html = html.Replace("{{EwayBillNo}}", dt.GetColValue("EwayBillNo"));


                        html = html.Replace("{{SupplierName}}", dt.GetColValue("SupplierName").ConvertToString());
                        html = html.Replace("{{PONo}}", dt.GetColValue("PONo").ConvertToString());
                        html = html.Replace("{{InvoiceNo}}", dt.GetColValue("InvoiceNo").ConvertToString());
                        html = html.Replace("{{StoreName}}", dt.GetColValue("StoreName"));
                        html = html.Replace("{{Address}}", dt.GetColValue("Address"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));

                        

                        html = html.Replace("{{Email}}", dt.GetColValue("Email").ConvertToString());
                        html = html.Replace("{{GateEntryNo}}", dt.GetColValue("GateEntryNo").ConvertToString());
                        html = html.Replace("{{Mobile}}", dt.GetColValue("Mobile"));
                        html = html.Replace("{{Phone}}", dt.GetColValue("Phone"));
                        html = html.Replace("{{VatAmount}}", dt.GetColValue("VatAmount").ConvertToString());
                        html = html.Replace("{{Mobile}}", dt.GetColValue("Mobile"));
                        html = html.Replace("{{Phone}}", dt.GetColValue("Phone"));
                        html = html.Replace("{{PONo}}", dt.GetColValue("PONo"));

                        html = html.Replace("{{PrintStoreName}}", dt.GetColValue("PrintStoreName"));

                        string finalamt = conversion(dt.GetColValue("NetAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());
                        //string finalamt = ConvertNumbertoWords(dt.GetColValue("GrnReturnAmount").ConvertToDouble().To2DecimalPlace().Count());
                        //html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());
                        string NetAmount = ConvertNumbertoWords(dt.GetColValue("NetAmount").ConvertToDouble().To2DecimalPlace().Count());
                        html = html.Replace("{{NetAmount}}", finalamt.ToString().ToUpper());

                        html = html.Replace("{{chkdiscflag}}", dt.GetColValue("T_TotalDiscAmount").ConvertToDouble() > 0 ? "block" : "none");


                        html = html.Replace("{{chkponoflag}}", dt.GetColValue("PONo").ConvertToDouble() != 0 ? "table-row " : "none");



                        return html;


                    }
                    break;




                case "EmergencyPrescription":
                    {

                        int i = 0, j = 0;
                        string previousLabel = "";
                        String Label = "", Label1 = "", Label2 = "";


                        html = html.Replace("{{Items}}", items.ToString());
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{ReferDrName}}", dt.GetColValue("ReferDrName"));

                        html = html.Replace("{{EmgId}}", dt.GetColValue("EmgId"));
                        html = html.Replace("{{PDate}}", dt.GetColValue("PTime").ConvertToDateString("dd/MM/yyyy|hh:mm tt"));
                        html = html.Replace("{{EmgTime}}", dt.GetColValue("EmgTime").ConvertToDateString("dd/MM/yyyy|hh:mm tt"));
                        html = html.Replace("{{FollowupDate}}", dt.GetColValue("FollowupDate").ConvertToDateString("dd/MM/yyyy"));


                        html = html.Replace("{{PrecriptionId}}", dt.GetColValue("PrecriptionId"));

                        html = html.Replace("{{MobileNo}}", dt.GetColValue("MobileNo"));
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{Address}}", dt.GetColValue("Address"));
                        html = html.Replace("{{PreparedBy}}", dt.GetColValue("PreparedBy"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{Address}}", dt.GetColValue("Address"));
                        html = html.Replace("{{ChiefComplaint}}", dt.GetColValue("ChiefComplaint"));
                        html = html.Replace("{{Diagnosis}}", dt.GetColValue("Diagnosis"));
                        html = html.Replace("{{Examination}}", dt.GetColValue("Examination"));
                        html = html.Replace("{{SecondRefDoctorName}}", dt.GetColValue("SecondRefDoctorName"));
                        html = html.Replace("{{RequestList}}", dt.GetColValue("RequestList"));
                        html = html.Replace("{{BP}}", dt.GetColValue("BP"));
                        html = html.Replace("{{Pulse}}", dt.GetColValue("Pulse"));
                        html = html.Replace("{{Height}}", dt.GetColValue("Height"));
                        html = html.Replace("{{Weight}}", dt.GetColValue("PWeight"));
                        html = html.Replace("{{Temp}}", dt.GetColValue("Temp"));
                        html = html.Replace("{{BP}}", dt.GetColValue("BP"));
                        html = html.Replace("{{BSL}}", dt.GetColValue("BSL"));
                        html = html.Replace("{{BMI}}", dt.GetColValue("BMI"));
                        html = html.Replace("{{SpO2}}", dt.GetColValue("SpO2"));

                        html = html.Replace("{{PathResultDr1}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{MahRegNo}}", dt.GetColValue("MahRegNo"));
                        html = html.Replace("{{Education}}", dt.GetColValue("Education"));
                        html = html.Replace("{{Advice}}", dt.GetColValue("Advice"));

                        html = html.Replace("{{chkBPflag}}", dt.GetColValue("BP").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkPulseflag}}", dt.GetColValue("Pulse").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkHeightflag}}", dt.GetColValue("Height").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkWeightflag}}", dt.GetColValue("PWeight").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkTempflag}}", dt.GetColValue("Temp").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkBSLflag}}", dt.GetColValue("BSL").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkBMIflag}}", dt.GetColValue("BMI").ConvertToString() != "" ? "visible" : "none");
                        html = html.Replace("{{chkSpO2flag}}", dt.GetColValue("SpO2").ConvertToString() != "" ? "visible" : "none");

                        html = html.Replace("{{chkEdu}}", dt.GetColValue("PathResultDr1").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkRegNo}}", dt.GetColValue("PathResultDr1").ConvertToString() != "" ? "table-row" : "none");

                        html = html.Replace("{{chkSignature}}", dt.GetColValue("Signature").ConvertToString() != "" ? "table-row" : "none");


                        return html;
                    }
                    break;


                case "ConsentInformation":
                    {

                        int i = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            //      items.Append("<tr style\"font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ConsentText"].ConvertToString()).Append("</td></tr>");


                        }

                        html = html.Replace("{{Items}}", items.ToString());
                        html = html.Replace("{{ConsentId}}", dt.GetColValue("ConsentId"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{ConsentName}}", dt.GetColValue("ConsentName"));


                        //html = html.Replace("{{RequestId}}", Bills.GetColValue("RequestId"));
                        //html = html.Replace("{{OPDNo}}", Bills.GetColValue("IPDNo"));
                        //html = html.Replace("{{ReqDate}}", Bills.GetColValue("ReqDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{ConsentTime}}", dt.GetColValue("ConsentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));


                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));

                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{ConsentText}}", dt.GetColValue("ConsentText"));
                        //html = html.Replace("{{BedName}}", Bills.GetColValue("BedName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        //html = html.Replace("{{PatientType}}", Bills.GetColValue("PatientType"));
                        html = html.Replace("{{OP_IP_Type}}", dt.GetColValue("OP_IP_Type"));
                        html = html.Replace("{{OPIPID}}", dt.GetColValue("OPIPID"));

                        //html = html.Replace("{{RefDocName}}", Bills.GetColValue("RefDocName"));
                        //html = html.Replace("{{CompanyName}}", Bills.GetColValue("CompanyName"));


                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));




                        return html;
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

                            html = html.Replace("{{BP}}", dt.GetColValue("BP"));
                            html = html.Replace("{{Pulse}}", dt.GetColValue("Pulse"));
                            html = html.Replace("{{Height}}", dt.GetColValue("Height"));
                            html = html.Replace("{{Weight}}", dt.GetColValue("PWeight"));
                            html = html.Replace("{{Temp}}", dt.GetColValue("Temp"));
                            html = html.Replace("{{BP}}", dt.GetColValue("BP"));
                            html = html.Replace("{{BSL}}", dt.GetColValue("BSL"));
                            html = html.Replace("{{BMI}}", dt.GetColValue("BMI"));
                            html = html.Replace("{{SpO2}}", dt.GetColValue("SpO2"));



                            html = html.Replace("{{chkBPflag}}", dt.GetColValue("BP").ConvertToString() != "" ? "visible" : "none");
                            html = html.Replace("{{chkPulseflag}}", dt.GetColValue("Pulse").ConvertToString() != "" ? "visible" : "none");
                            html = html.Replace("{{chkHeightflag}}", dt.GetColValue("Height").ConvertToString() != "" ? "visible" : "none");
                            html = html.Replace("{{chkWeightflag}}", dt.GetColValue("PWeight").ConvertToString() != "" ? "visible" : "none");
                            html = html.Replace("{{chkTempflag}}", dt.GetColValue("Temp").ConvertToString() != "" ? "visible" : "none");
                            html = html.Replace("{{chkBSLflag}}", dt.GetColValue("BSL").ConvertToString() != "" ? "visible" : "none");
                            html = html.Replace("{{chkBMIflag}}", dt.GetColValue("BMI").ConvertToString() != "" ? "visible" : "none");
                            html = html.Replace("{{chkSpO2flag}}", dt.GetColValue("SpO2").ConvertToString() != "" ? "visible" : "none");
                            

                        }

                        return html;


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

                            items.Append("<tr style\"font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;font-size:18px;\">").Append(dr["TTime"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;font-size:18px;\">").Append(dr["DoctorsNotes"].ConvertToString()).Append("</td></tr>");

                        }
                        html = html.Replace("{{Items}}", items.ToString());
                        html = html.Replace("{{AdmID}}", dt.GetColValue("AdmId"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{TDate}}", dt.GetColValue("TTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

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
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        return html;

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

                            items.Append("<tr style\"font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;font-size:20px;\"><td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;font-size:22px;\">").Append(dr["TTime"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;font-size:22px;\">").Append(dr["NursingNotes"].ConvertToString()).Append("</td></tr>");

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


                        }


                    }
                    break;

                case "DoctorNoteandNursingNoteReport":
                    {


                        int i = 0, j = 0;
                        double Dcount = 0;
                        string previousLabel = "";
                        int k = 0;
                        var dynamicVariable = new Dictionary<string, double>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr style\"font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;font-size:22px;\">").Append(dr["TTime"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;font-size:22px;\">").Append(dr["DoctorsNotes"].ConvertToString()).Append("</td></tr>");

                        }
                        html = html.Replace("{{Items}}", items.ToString());
                        html = html.Replace("{{AdmID}}", dt.GetColValue("AdmId"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{TDate}}", dt.GetColValue("TTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

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
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                       
                 
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr style\"font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;font-size:20px;\"><td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;font-size:22px;\">").Append(dr["TTime"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;font-size:22px;\">").Append(dr["NursingNotes"].ConvertToString()).Append("</td></tr>");

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


                        }


                    }
                    break;


                case "DoctorPatientHandoverReceipt":
                    {


                        int i = 0, j = 0;
                        double Dcount = 0;
                        string previousLabel = "";
                        string htmlHeader = "";
                        int k = 0;
                        var dynamicVariable = new Dictionary<string, double>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr style\"font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TTime"].ConvertToString()).Append("</td>");
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
                        html = html.Replace("{{AdmissionDate}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

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


                        
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{RefundNo}}", dt.GetColValue("RefundNo"));
                        

                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AdmissinDate}}", dt.GetColValue("AdmissinDate"));
                        html = html.Replace("{{OPDNo}}", dt.GetColValue("OPDNo"));
                        html = html.Replace("{{NetPayableAmt}}", dt.GetColValue("NetPayableAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{RefundAmount}}", dt.GetColValue("RefundAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName").ConvertToString());
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{MobileNo}}", dt.GetColValue("PhoneNo"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{ConsultantDocName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDoctorName"));

                        html = html.Replace("{{ReceiptNo}}", dt.GetColValue("ReceiptNo"));
                        
                        html = html.Replace("{{RefundDateTime}}", dt.GetColValue("RefundDateTime").ConvertToString());
                        html = html.Replace("{{RefundTime}}", dt.GetColValue("RefundTime").ConvertToDateString("dd/mm/yyyy | hh:mm tt"));
                        html = html.Replace("{{PaymentTime}}", dt.GetColValue("PaymentTime").ConvertToDateString("dd/mm/yyyy | hh:mm tt"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));

                        html = html.Replace("{{PBillNo}}", dt.GetColValue("PBillNo").ConvertToString());
                        html = html.Replace("{{BillDate}}", dt.GetColValue("BillTime").ConvertToDateString("dd/mm/yyyy | hh:mm tt"));
                        html = html.Replace("{{NetPayableAmt}}", dt.GetColValue("NetPayableAmt").ConvertToDouble().ToString("F2"));
                        //html = html.Replace("{{CashPayAmount}}", dt.GetColValue("CashPayAmount").ConvertToDouble().To2DecimalPlace());
                        //html = html.Replace("{{ChequePayAmount}}", dt.GetColValue("ChequePayAmount").ConvertToDouble().To2DecimalPlace());
                        //html = html.Replace("{{CardPayAmount}}", dt.GetColValue("CardPayAmount").ConvertToDouble().To2DecimalPlace());
                        //html = html.Replace("{{NEFTPayAmount}}", dt.GetColValue("NEFTPayAmount").ConvertToDouble().To2DecimalPlace());
                        //html = html.Replace("{{PayTMAmount}}", dt.GetColValue("PayTMAmount").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{Addedby}}", dt.GetColValue("Addedby"));
                        html = html.Replace("{{Remark}}", dt.GetColValue("Remark"));
                        html = html.Replace("{{VisitDate}}", dt.GetColValue("VisitTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
                        html = html.Replace("{{PaymentMode}}", dt.GetColValue("PaymentMode").ToString());
                        string finalamt = conversion(dt.GetColValue("RefundAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());



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
                                if (!colName.ToLower().Contains("date", StringComparison.CurrentCulture))
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

                        ItemsTotal.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'>");
                        foreach (var colName in totalColList)
                        {
                            if (colName == "lableTotal")
                                ItemsTotal.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">Total</td>");
                            else if (!string.IsNullOrEmpty(colName))
                                ItemsTotal.Append("<td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dynamicVariable[colName]).Append("</td>");
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


                            html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName").ToUpper());
                            html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName").ToUpper());

                            html = html.Replace("{{Address}}", dt.GetColValue("Address").ToUpper());
                            html = html.Replace("{{MobileNo}}", dt.GetColValue("MobileNo"));
                            html = html.Replace("{{PhoneNo}}", dt.GetColValue("PhoneNo"));

                            html = html.Replace("{{DOT}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yy hh:mm tt"));
                            html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType").ToUpper());

                            html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName").ToUpper());
                            html = html.Replace("{{BedName}}", dt.GetColValue("BedName").ToUpper());

                            html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                            html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                            html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                            html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));


                            html = html.Replace("{{AdmittedDoctorName}}", dt.GetColValue("AdmittedDoctorName").ToUpper());
                            html = html.Replace("{{RefDoctorName}}", dt.GetColValue("RefDoctorName").ToUpper());

                            html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName").ToUpper());
                            html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName").ToUpper());

                            html = html.Replace("{{RelativeName}}", dt.GetColValue("RelativeName").ToUpper());
                            html = html.Replace("{{RelativePhoneNo}}", dt.GetColValue("RelativePhoneNo"));

                            html = html.Replace("{{RelationshipName}}", dt.GetColValue("RelationshipName").ToUpper());
                            html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                            html = html.Replace("{{IsMLC}}", dt.GetColValue("IsMLC"));
                            html = html.Replace("{{AdmittedDoctor1}}", dt.GetColValue("AdmittedDoctor1").ToUpper());
                            html = html.Replace("{{AdmittedDoctorName}}", dt.GetColValue("AdmittedDoctorName").ToUpper());

                            html = html.Replace("{{MaritalStatusName}}", dt.GetColValue("MaritalStatusName").ToUpper());
                            html = html.Replace("{{AadharcardNo}}", dt.GetColValue("AadharcardNo"));
                            html = html.Replace("{{TariffName}}", dt.GetColValue("TariffName").ToUpper());
                            html = html.Replace("{{AdmittedDoctor2}}", dt.GetColValue("AdmittedDoctor2").ToUpper());
                            html = html.Replace("{{LoginUserSurname}}", dt.GetColValue("LoginUserSurname").ToUpper());


                            //html = html.Replace("{{chkMLCflag}}", dt.GetColValue("IsMLC").ToBool() == true ? "table-row " : "none");
                            //html = html.Replace("{{chkMLCflag1}}", dt.GetColValue("IsMLC").ToBool() == false ? "table-row " : "none");



                            //html = html.Replace("{{chkMLCflag}}", Convert.ToBoolean(dt.GetColValue("IsMLC")) ? "table-row" : "none");
                            //changes by Ashu 27 May 2025
                            bool isMLC = Convert.ToBoolean(dt.GetColValue("IsMLC"));

                            html = html.Replace("{{chkMLCflag}}", isMLC ? "table-row" : "none");
                            html = html.Replace("{{chkMLCflag1}}", !isMLC ? "table-row" : "none");



                            html = html.Replace("{{DOA}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));



                            return html;

                        }

                    }
                    break;
                //case "IpCasepaperReport1":
                //    {


                //        foreach (DataRow dr in dt.Rows)
                //        {
                //            //html = html.Replace("{{DataContent}}", htmlHeader);


                //            html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName").ToUpper());
                //            html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName").ToUpper());

                //            html = html.Replace("{{Address}}", dt.GetColValue("Address").ToUpper());
                //            html = html.Replace("{{MobileNo}}", dt.GetColValue("MobileNo"));
                //            html = html.Replace("{{PhoneNo}}", dt.GetColValue("PhoneNo"));

                //            html = html.Replace("{{DOT}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yy hh:mm tt"));
                //            html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType").ToUpper());

                //            html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName").ToUpper());
                //            html = html.Replace("{{BedName}}", dt.GetColValue("BedName").ToUpper());

                //            html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                //            html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                //            html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                //            html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));


                //            html = html.Replace("{{AdmittedDoctorName}}", dt.GetColValue("AdmittedDoctorName").ToUpper());
                //            html = html.Replace("{{RefDoctorName}}", dt.GetColValue("RefDoctorName").ToUpper());

                //            html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName").ToUpper());
                //            html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName").ToUpper());

                //            html = html.Replace("{{RelativeName}}", dt.GetColValue("RelativeName").ToUpper());
                //            html = html.Replace("{{RelativePhoneNo}}", dt.GetColValue("RelativePhoneNo"));

                //            html = html.Replace("{{RelationshipName}}", dt.GetColValue("RelationshipName").ToUpper());
                //            html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                //            html = html.Replace("{{IsMLC}}", dt.GetColValue("IsMLC"));
                //            html = html.Replace("{{AdmittedDoctor1}}", dt.GetColValue("AdmittedDoctor1").ToUpper());
                //            html = html.Replace("{{AdmittedDoctorName}}", dt.GetColValue("AdmittedDoctorName").ToUpper());

                //            html = html.Replace("{{MaritalStatusName}}", dt.GetColValue("MaritalStatusName").ToUpper());
                //            html = html.Replace("{{AadharcardNo}}", dt.GetColValue("AadharcardNo"));
                //            html = html.Replace("{{TariffName}}", dt.GetColValue("TariffName").ToUpper());
                //            html = html.Replace("{{AdmittedDoctor2}}", dt.GetColValue("AdmittedDoctor2").ToUpper());
                //            html = html.Replace("{{LoginUserSurname}}", dt.GetColValue("LoginUserSurname").ToUpper());


                //            //html = html.Replace("{{chkMLCflag}}", dt.GetColValue("IsMLC").ToString() == "False" ? "table-row " : "none");
                //            //html = html.Replace("{{chkMLCflag1}}", dt.GetColValue("IsMLC").ToString() == "True" ? "table-row " : "none");
                //            //html = html.Replace("{{chkpaidflag}}", dt.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");


                //            html = html.Replace("{{chkMLCflag}}", dt.GetColValue("IsMLC").ToString() == "True" ? "table-row " : "none");
                //            html = html.Replace("{{chkMLCflag1}}", dt.GetColValue("IsMLC").ToString() == "false" ? "table-row " : "none");


                //            html = html.Replace("{{DOA}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));



                //            return html;

                //        }

                //    }
                //    break;



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


                        return html;


                    }


                    break;

                case "IPDInterimBill":
                    {

                        int i = 0;
                        String[] GroupName;
                        object GroupName1 = "";
                        Boolean chkcommflag = false, chkpaidflag = false;
                        double T_NetAmount = 0;

                        int j = 0;

                        string htmlHeader = "";
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

                        html = html.Replace("{{BillNo}}", dt.GetColValue("PBillNo"));
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo").ToString());
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName").ToString());

                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{Age}}", dt.GetColValue("Age"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));

                        html = html.Replace("{{AdmissionDate}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{BillDateTime}}", dt.GetColValue("BillDateTime"));
                        html = html.Replace("{{BillDate}}", dt.GetColValue("BillDateTime").ConvertToDateString("dd/MM/yyyy"));
                        html = html.Replace("{{BillTime}}", dt.GetColValue("BillTime").ConvertToDateString("hh:mm tt"));
                        html = html.Replace("{{PayMode}}", dt.GetColValue("PayMode"));

                        //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
                        //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
                        html = html.Replace("{{TotalBillAmount}}", dt.GetColValue("TotalAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{AdvanceUsedAmount}}", dt.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{PaidAmount}}", dt.GetColValue("PaidAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{BalanceAmt}}", dt.GetColValue("BalanceAmt").ConvertToDouble().ToString("F2"));

                        html = html.Replace("{{TotalBillAmount}}", dt.GetColValue("TotalAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{PayTMPayAmount}}", dt.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{CashPayAmount}}", dt.GetColValue("CashPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{CardPayAmount}}", dt.GetColValue("CardPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{ChequePayAmount}}", dt.GetColValue("ChequePayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{NEFTPayAmount}}", dt.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{OnlinePayAmount}}", dt.GetColValue("OnlinePayAmount").ConvertToDouble().ToString("F2"));

                        html = html.Replace("{{TotalAdvanceAmount}}", dt.GetColValue("TotalAdvanceAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{AdvanceUsedAmount}}", dt.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{AdvanceBalAmount}}", dt.GetColValue("AdvanceBalAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{AdvanceRefundAmount}}", dt.GetColValue("AdvanceRefundAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{ConcessionAmt}}", dt.GetColValue("ConcessionAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{DiscComments}}", dt.GetColValue("DiscComments").ConvertToString());
                        html = html.Replace("{{T_NetAmount}}", dt.GetColValue("NetPayableAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{Qty}}", dt.GetColValue("Qty"));
                        html = html.Replace("{{Phone}}", dt.GetColValue("Phone"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{ConcessionReason}}", dt.GetColValue("ConcessionReason").ConvertToString()); ;
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{chkpaidflag}}", dt.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkbalflag}}", dt.GetColValue("BalanceAmt").ConvertToDouble().ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkdiscflag}}", dt.GetColValue("ConcessionAmt").ConvertToDouble().ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkCompanyNameflag}}", dt.GetColValue("CompanyName").ConvertToString() != "" ? "visible" : "none");


                        string previousLabel = "";


                        double T_TotalAmount = 0, F_TotalAmount = 0, ChargesTotalamt = 0;


                        foreach (DataRow dr in dt.Rows)
                        {

                            i++; j++;


                            if (i == 1)
                            {
                                String Label;
                                Label = dr["GroupName"].ConvertToString();
                                items.Append("<tr style=\"font-size:20px;border: 1px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-weight:bold;\">").Append(Label).Append("</td></tr>");
                            }
                            if (previousLabel != "" && previousLabel != dr["GroupName"].ConvertToString())
                            {
                                j = 1;
                                items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:right;font-weight:bold;\">")

                               .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                                T_TotalAmount = 0;

                                items.Append("<tr style=\"font-size:20px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;border-bottom: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-weight:bold;\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");

                            }


                            T_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                            F_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                            ChargesTotalamt += dr["ChargesTotalAmt"].ConvertToDouble();


                            previousLabel = dr["GroupName"].ConvertToString();

                            items.Append("<tr style=\"font-family:'Helvetica Neue', 'Helvetica',, Arial, sans-serif;font-size:18px;\"><td style=\"border: 1px solid #000; text-align: center; padding: 6px;\">").Append(j).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:18px;font-family:'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:18px;font-family:'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:18px;font-family:'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["Price"].ConvertToDouble().ToString("F2")).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:18px;font-family:'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: right; padding: 6px;font-size:18px;font-family:'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble().ToString("F2")).Append("</td></tr>");

                            if (dt.Rows.Count > 0 && dt.Rows.Count == i)
                            {

                                items.Append("<tr style='border:1px solid black;font-weight:bold;font-family:'Helvetica Neue', 'Helvetica',, Arial, sans-serif;font-size:20px;'><td colspan='5' style=\"border:1px solid #000;border-bottom:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;font-size:20px;\">Group Wise Amount</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;font-size:20px;\">")
                                    .Append(T_TotalAmount.ConvertToDouble().ToString("F2")).Append("</td></tr>");

                            }

                        }

                        html = html.Replace("{{Items}}", items.ToString());

                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));

                        string finalamt = conversion(dt.GetColValue("NetPayableAmt").ConvertToDouble().ToString(/*"0.00"*/));
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


                        html = html.Replace("{{chkpaidflag}}", dt.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkbalflag}}", dt.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkdiscflag}}", dt.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");



                    }


                    break;
                case "IpDraftBillGroupWise":
                    {

                        int i = 0, j = 0;
                        double T_NetAmount = 0;
                        string previousLabel = "";
                        String finalLabel = "";
                        int rowlength = 0;
                        rowlength = dt.Rows.Count;
                        double Tot_AfterAdvused = 0, Tot_Wothoutdedu = 0, Tot_Balamt = 0, Tot_Advamt = 0, Tot_Advusedamt = 0, T_TotalAmount = 0, F_TotalAmount = 0, balafteradvuseAmount = 0, BalancewdudcAmt = 0, AdminChares = 0, TotalNetPayAmt = 0;
                        double T_TotAmount = 0;


                        foreach (DataRow dr in dt.Rows)
                        {

                            i++; j++;


                            if (i == 1)
                            {
                                String Label;
                                Label = dr["GroupName"].ConvertToString();
                                items.Append("<tr style=\"font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-weight:bold;\">").Append(Label).Append("</td></tr>");
                            }
                            if (previousLabel != "" && previousLabel != dr["GroupName"].ConvertToString())
                            {
                                j = 1;
                                items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")

                               .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                                T_TotalAmount = 0;

                                items.Append("<tr style=\"font-size:18px;border-bottom: 1px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-weight:bold;\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");

                            }


                            T_TotalAmount += dr["TotalAmt"].ConvertToDouble();
                            F_TotalAmount += dr["TotalAmt"].ConvertToDouble();

                            previousLabel = dr["GroupName"].ConvertToString();


                            items.Append("<tr  style=\"font-family: 'Helvetica Neue','Helvetica', Helvetica, Arial, sans-serif;font-size:18px;\"><td style=\"border: 1px solid #000; text-align: center; padding: 6px;\">").Append(j).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: right; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td></tr>");


                            TotalNetPayAmt = dr["NetPayableAmt"].ConvertToDouble();
                            Tot_Advamt = dr["AdvanceAmount"].ConvertToDouble();

                            if (Tot_Advamt.ConvertToDouble() > TotalNetPayAmt.ConvertToDouble())
                            {
                                balafteradvuseAmount = (Tot_Advamt - TotalNetPayAmt).ConvertToDouble();
                            }
                            if (Tot_Advamt.ConvertToDouble() < TotalNetPayAmt.ConvertToDouble())
                            {
                                BalancewdudcAmt = (TotalNetPayAmt - Tot_Advamt).ConvertToDouble();
                            }



                            if (dt.Rows.Count > 0 && dt.Rows.Count == i)
                            {

                                items.Append("<tr style='border:1px solid black;font-weight:bold'><td colspan='5' style=\"font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border:1px solid #000;border-collapse: collapse;padding:3px;border-bottom:1px solid #000;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">Grand Total Amount</td><td style=\"border-right:1px solid #000;border-bottom:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">")
                                    .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                                items.Append("<tr style='border:1px solid black;'><td colspan='5' style=\"font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;border-right:1px solid #000;border-top:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-center:20px;font-weight:bold;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">Total</td><td style=\"border-right:1px solid #000;font-weight:bold;border-top:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">")
                                .Append(F_TotalAmount.To2DecimalPlace()).Append("</td></tr>");


                            }

                        }



                        html = html.Replace("{{Items}}", items.ToString());

                        html = html.Replace("{{BillNo}}", dt.GetColValue("BillNo"));

                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo").ToString());
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo").ToString());

                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName").ToString());
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear").ToString());

                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName").ToString());

                        html = html.Replace("{{AdmissionDate}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName").ToString());
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDoctorName"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("AdmittedDoctorName"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{DischargeDate}}", dt.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{BillDate}}", dt.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{PayMode}}", dt.GetColValue("PayMode").ToString());

                        html = html.Replace("{{TotalBillAmount}}", dt.GetColValue("TotalBillAmt").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{TotalBillAmount}}", dt.GetColValue("TotalBillAmt").ConvertToDouble().ToString("0.00"));

                        html = html.Replace("{{AdvanceUsedAmount}}", dt.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{AdvanceBalAmount}}", dt.GetColValue("AdvanceBalAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{balafteradvuseAmount}}", balafteradvuseAmount.ConvertToDouble().ToString("0.00"));

                        html = html.Replace("{{BalancewdudcAmt}}", BalancewdudcAmt.ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{AdvanceAmount}}", dt.GetColValue("AdvanceAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{TaxAmount}}", dt.GetColValue("TaxAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{ConcessionAmount}}", dt.GetColValue("ConcessionAmt").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{T_NetAmount}}", dt.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{Qty}}", dt.GetColValue("Qty"));
                        html = html.Replace("{{Phone}}", dt.GetColValue("Phone"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));
                        string finalamt = conversion(dt.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());
                        //html = html.Replace("{{balafteradvuseAmount}}", balafteradvuseAmount.ToString());
                        //html = html.Replace("{{BalancewdudcAmt}}", BalancewdudcAmt.ToString());

                        html = html.Replace("{{AdvanceRefundAmount}}", dt.GetColValue("AdvanceRefundAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{CardPayAmount}}", dt.GetColValue("CardPayAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{CashPayAmount}}", dt.GetColValue("CashPayAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{ChequePayAmount}}", dt.GetColValue("ChequePayAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{NEFTPayAmount}}", dt.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("0.00"));

                        html = html.Replace("{{PayTMPayAmount}}", dt.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("0.00"));

                        html = html.Replace("{{AddedBy}}", dt.GetColValue("AddedBy"));

                        html = html.Replace("{{chkAftBalflag}}", balafteradvuseAmount.ConvertToDouble() > 0 ? "table-row" : "none");
                        html = html.Replace("{{chkBalflag}}", BalancewdudcAmt.ConvertToDouble() > 0 ? "table-row" : "none");

                        html = html.Replace("{{chkCompanyNameflag}}", dt.GetColValue("CompanyName").ConvertToString() != "" ? "visible" : "none");

                        html = html.Replace("{{chkdiscflag}}", dt.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row" : "none");

                        html = html.Replace("{{chkpaidflag}}", dt.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkAdvflag}}", dt.GetColValue("AdvanceAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkdiscflag}}", dt.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkbalflag}}", balafteradvuseAmount.ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkbalafterdudcflag}}", BalancewdudcAmt.ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkadminflag}}", dt.GetColValue("TaxAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkadminchargeflag}}", AdminChares.ConvertToDouble() > 0 ? "table-row " : "none");



                    }


                    break;
                case "IpDraftBillClassWise":
                    {

                        int i = 0, j = 0;
                        double T_NetAmount = 0;
                        string previousLabel = "";
                        String finalLabel = "";
                        int rowlength = 0;
                        rowlength = dt.Rows.Count;
                        double Tot_AfterAdvused = 0, Tot_Wothoutdedu = 0, Tot_Balamt = 0, Tot_Advamt = 0, Tot_Advusedamt = 0, T_TotalAmount = 0, F_TotalAmount = 0, balafteradvuseAmount = 0, BalancewdudcAmt = 0, AdminChares = 0, TotalNetPayAmt = 0;
                        double T_TotAmount = 0;


                        foreach (DataRow dr in dt.Rows)
                        {

                            i++; j++;

                            items.Append("<tr  style=\"font-family: 'Helvetica Neue','Helvetica', Helvetica, Arial, sans-serif;font-size:18px;\"><td style=\"border: 1px solid #000; text-align: center; padding: 6px;\">").Append(j).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["ClassName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Price"].ConvertToDouble()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: right; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\">").Append(dr["TotalAmt"].ConvertToDouble()).Append("</td></tr>");


                            TotalNetPayAmt = dr["NetPayableAmt"].ConvertToDouble();
                            Tot_Advamt = dr["AdvanceAmount"].ConvertToDouble();
                            if (Tot_Advamt.ConvertToDouble() > TotalNetPayAmt.ConvertToDouble())
                            {
                                balafteradvuseAmount = (Tot_Advamt - TotalNetPayAmt).ConvertToDouble();
                            }
                            if (Tot_Advamt.ConvertToDouble() < TotalNetPayAmt.ConvertToDouble())
                            {
                                BalancewdudcAmt = (TotalNetPayAmt - Tot_Advamt).ConvertToDouble();
                            }


                        }



                        html = html.Replace("{{Items}}", items.ToString());

                        html = html.Replace("{{BillNo}}", dt.GetColValue("BillNo"));


                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo").ToString());
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo").ToString());

                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName").ToString());
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear").ToString());

                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName").ToString());

                        html = html.Replace("{{AdmissionDate}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName").ToString());
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDoctorName"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("AdmittedDoctorName"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{DischargeDate}}", dt.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{BillDate}}", dt.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{PayMode}}", dt.GetColValue("PayMode").ToString());

                        html = html.Replace("{{TotalBillAmount}}", dt.GetColValue("TotalBillAmt").ConvertToDouble().ToString("0.00"));

                        html = html.Replace("{{AdvanceUsedAmount}}", dt.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{AdvanceBalAmount}}", dt.GetColValue("AdvanceBalAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{balafteradvuseAmount}}", balafteradvuseAmount.ConvertToDouble().ToString("0.00"));

                        html = html.Replace("{{BalancewdudcAmt}}", BalancewdudcAmt.ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{AdvanceAmount}}", dt.GetColValue("AdvanceAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{TaxAmount}}", dt.GetColValue("TaxAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{ConcessionAmount}}", dt.GetColValue("ConcessionAmt").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{T_NetAmount}}", dt.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{Qty}}", dt.GetColValue("Qty"));
                        html = html.Replace("{{Phone}}", dt.GetColValue("Phone"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));
                        string finalamt = conversion(dt.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());
                        html = html.Replace("{{balafteradvuseAmount}}", balafteradvuseAmount.ToString());
                        html = html.Replace("{{BalancewdudcAmt}}", BalancewdudcAmt.ToString());

                        html = html.Replace("{{AdvanceRefundAmount}}", dt.GetColValue("AdvanceRefundAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{CardPayAmount}}", dt.GetColValue("CardPayAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{CashPayAmount}}", dt.GetColValue("CashPayAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{ChequePayAmount}}", dt.GetColValue("ChequePayAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{NEFTPayAmount}}", dt.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("0.00"));

                        html = html.Replace("{{PayTMPayAmount}}", dt.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("0.00"));

                        html = html.Replace("{{AddedBy}}", dt.GetColValue("AddedBy"));


                        html = html.Replace("{{chkdiscflag}}", dt.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row" : "none");

                        html = html.Replace("{{chkpaidflag}}", dt.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkAdvflag}}", dt.GetColValue("AdvanceAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkdiscflag}}", dt.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkbalflag}}", balafteradvuseAmount.ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkbalafterdudcflag}}", BalancewdudcAmt.ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkadminflag}}", dt.GetColValue("TaxAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkadminchargeflag}}", AdminChares.ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkCompanyNameflag}}", dt.GetColValue("CompanyName").ConvertToString() != "" ? "visible" : "none");



                    }


                    break;

                case "IPFinalBillClassWise":
                    {

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

                            items.Append("<tr style=\"font-family: 'Helvetica Neue','Helvetica',, Arial, sans-serif;font-size:18px;\"><td style=\"border: 1px solid #000; text-align: Center; padding: 6px;\">").Append(j).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ClassName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: right; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");

                            if (dt.Rows.Count > 0 && dt.Rows.Count == i && i != length + 1)
                            {
                                items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\"><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                                 .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

                            }

                            if (i == length + 1)
                            {

                                T_TotalAmount = T_TotalAmount - Tot;
                                items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\"><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
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
                        //Added by Ashu 30 May 2025
                        string htmlHeader = "";
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

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
                        html = html.Replace("{{chkCompanyNameflag}}", dt.GetColValue("CompanyName").ConvertToString() != "" ? "visible" : "none");



                    }


                    break;

                case "IPFinalBillClassServiceWise":
                    {

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


                            if (i == 1)
                            {
                                String Label2;
                                Label2 = dr["ClassName"].ConvertToString();
                                items.Append("<tr style=\"font-size:18px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;border: 1px;font-weight:bold;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label2).Append("</td></tr>");

                            }

                            if (ClassLable != "" && ClassLable != dr["ClassName"].ConvertToString())
                            {

                                items.Append("<tr style=\"font-size:18px;border-bottom: 1px;font-weight:bold;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ClassName"].ConvertToString()).Append("</td></tr>");

                                if (dt.Rows.Count == i)
                                {
                                    items.Append("<tr style=\"font-size:18px;border-bottom: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");

                                }
                            }


                            T_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                            F_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                            ChargesTotalamt += dr["ChargesTotalAmt"].ConvertToDouble();

                            items.Append("<tr style=\"font-family: 'Helvetica Neue','Helvetica',, Arial, sans-serif;font-size:18px;\"><td style=\"border: 1px solid #000; text-align: center; padding: 6px;\">").Append(j).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: right; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");


                            if (dt.Rows.Count > 0 && dt.Rows.Count == i && i != length + 1)
                            {
                                items.Append("<tr style='font-size:18px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                                 .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

                            }

                            if (i == length + 1)
                            {

                                T_TotalAmount = T_TotalAmount - Tot;
                                items.Append("<tr style='font-size:18px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Class Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
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


                        string htmlHeader = "";
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));


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
                        html = html.Replace("{{ConcessionReason}}", dt.GetColValue("ConcessionReason").ConvertToString());

                    }


                    break;

                //case "IpFinalClasswiseBill":
                //    {


                //        int i = 0, j = 0;
                //        String[] GroupName;
                //        object GroupName1 = "";
                //        Boolean chkcommflag = false, chkpaidflag = false, chkbalflag = false, chkdiscflag = false, chkAdvflag = false, chkadminchargeflag = false;
                //        double T_NetAmount = 0, TotalNetPayAmt = 0, Tot_Advamt = 0, balafteradvuseAmount = 0, BalancewdudcAmt = 0;

                //        string GroupLable = "";
                //        string ClassLable = "";
                //        String FinalLabel = "";
                //        double T_TotAmount = 0, T_TotalAmt = 0, ChargesTotalamt = 0, T_TotalAmount = 0, F_TotalAmount = 0.0, AdminChares = 0, Tot_paidamt = 0;
                //        double Tot = 0;
                //        int length = dt.Rows.Count - 1;

                //        foreach (DataRow dr in dt.Rows)
                //        {

                //            i++; j++;

                //            items.Append("<tr style=\"font-family: 'Helvetica Neue','Helvetica',, Arial, sans-serif;font-size:20px;\"><td style=\"border: 1px solid #000; text-align: center; padding: 6px;\">").Append(j).Append("</td>");
                //            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                //            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                //            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ClassName"].ConvertToString()).Append("</td>");
                //            items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                //            items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                //            items.Append("<td style=\"border: 1px solid #000; text-align: right; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");


                //            ClassLable = dr["ClassName"].ConvertToString();
                //            GroupLable = dr["GroupName"].ConvertToString();
                //            T_TotalAmt = dr["ChargesTotalAmt"].ConvertToDouble();
                //            TotalNetPayAmt = dr["NetPayableAmt"].ConvertToDouble();
                //            Tot_Advamt = dr["AdvanceUsedAmount"].ConvertToDouble();
                //            Tot_paidamt = dr["PaidAmount"].ConvertToDouble();

                //            if (Tot_Advamt.ConvertToDouble() < TotalNetPayAmt.ConvertToDouble())
                //            {
                //                BalancewdudcAmt = (TotalNetPayAmt - Tot_Advamt - Tot_paidamt).ConvertToDouble();
                //            }

                //        }


                //        html = html.Replace("{{Items}}", items.ToString());

                //        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));

                //        string finalamt = conversion(dt.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace().ToString());
                //        html = html.Replace("{{T_TotalAmt}}", dt.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
                //        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


                //        html = html.Replace("{{BillNo}}", dt.GetColValue("PBillNo"));
                //        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo").ToString());

                //        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                //        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                //        html = html.Replace("{{Age}}", dt.GetColValue("Age"));
                //        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                //        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                //        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                //        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));


                //        html = html.Replace("{{AdmissionDate}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                //        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                //        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName"));

                //        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                //        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                //        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));

                //        html = html.Replace("{{DischargeDate}}", dt.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                //        html = html.Replace("{{BillDate}}", dt.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                //        html = html.Replace("{{PayMode}}", dt.GetColValue("PayMode"));
                //        html = html.Replace("{{TotalBillAmount}}", dt.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
                //        html = html.Replace("{{AdvanceUsedAmount}}", dt.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
                //        html = html.Replace("{{PaidAmount}}", dt.GetColValue("PaidAmount").ConvertToDouble().ToString("0.00"));
                //        html = html.Replace("{{TotalBillAmount}}", dt.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
                //        html = html.Replace("{{BalanceAmt}}", dt.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));
                //        html = html.Replace("{{TotalAmt}}", dt.GetColValue("TotalAmt").ConvertToDouble().ToString("0.00"));
                //        html = html.Replace("{{TaxAmount}}", dt.GetColValue("TaxAmount").ConvertToDouble().ToString("0.00"));

                //        html = html.Replace("{{CardPayAmount}}", dt.GetColValue("CardPayAmount").ConvertToDouble().ToString("0.00"));
                //        html = html.Replace("{{CashPayAmount}}", dt.GetColValue("CashPayAmount").ConvertToDouble().ToString("0.00"));
                //        html = html.Replace("{{ChequePayAmount}}", dt.GetColValue("ChequePayAmount").ConvertToDouble().ToString("0.00"));
                //        html = html.Replace("{{NEFTPayAmount}}", dt.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("0.00"));
                //        // html = html.Replace("{{PayTMPayAmount}}", Bills.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("0.00"));
                //        html = html.Replace("{{PayTMPayAmount}}", dt.GetColValue("OnlinePayAmount").ConvertToDouble().ToString("0.00"));

                //        html = html.Replace("{{TotalAdvanceAmount}}", dt.GetColValue("TotalAdvanceAmount").ConvertToDouble().ToString("0.00"));
                //        html = html.Replace("{{AdvanceUsedAmount}}", dt.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
                //        html = html.Replace("{{AdvanceBalAmount}}", dt.GetColValue("AdvanceBalAmount").ConvertToDouble().ToString("0.00"));
                //        html = html.Replace("{{AdvanceRefundAmount}}", dt.GetColValue("AdvanceRefundAmount").ConvertToDouble().ToString("0.00"));
                //        html = html.Replace("{{ConcessionAmount}}", dt.GetColValue("ConcessionAmt").ConvertToDouble().ToString("0.00"));
                //        html = html.Replace("{{BalanceAmt}}", dt.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));


                //        html = html.Replace("{{T_NetAmount}}", dt.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));

                //        html = html.Replace("{{ChargesTotalamt}}", ChargesTotalamt.ConvertToDouble().ToString("0.00"));
                //        html = html.Replace("{{BalancewdudcAmt}}", BalancewdudcAmt.ConvertToDouble().ToString("0.00"));

                //        html = html.Replace("{{Qty}}", dt.GetColValue("Qty"));
                //        html = html.Replace("{{Phone}}", dt.GetColValue("Phone"));
                //        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                //        html = html.Replace("{{F_TotalAmount}}", F_TotalAmount.ConvertToDouble().ToString("0.00"));
                //        html = html.Replace("{{balafteradvuseAmount}}", balafteradvuseAmount.ConvertToDouble().ToString("0.00"));

                //        html = html.Replace("{{UseName}}", dt.GetColValue("UseName"));
                //        html = html.Replace("{{TDSAmount}}", dt.GetColValue("TDSAmount"));
                //        html = html.Replace("{{AffilAmount}}", dt.GetColValue("AffilAmount"));

                //        html = html.Replace("{{chktotalflag}}", dt.GetColValue("ChargesTotalAmt").ConvertToDouble() > 0 ? "table-row " : "none");
                //        html = html.Replace("{{chkpaidflag}}", dt.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                //        html = html.Replace("{{chkAdvflag}}", dt.GetColValue("AdvanceUsedAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                //        html = html.Replace("{{chkdiscflag}}", dt.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");

                //        html = html.Replace("{{chkbalflag}}", dt.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "table-row " : "none");

                //        html = html.Replace("{{chkadminchargeflag}}", AdminChares.ConvertToDouble() > 0 ? "table-row " : "none");
                //        html = html.Replace("{{chkbalafterdudcflag}}", BalancewdudcAmt.ConvertToDouble() > 0 ? "table-row " : "none");
                //        html = html.Replace("{{chktdsflag}}", dt.GetColValue("TDSAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                //        html = html.Replace("{{chkWrfAmountflag}}", dt.GetColValue("AffilAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                //        html = html.Replace("{{chkadminflag}}", dt.GetColValue("TaxAmount").ConvertToDouble() > 0 ? "table-row " : "none");


                //    }


                //    break;
                case "IPFinalBillGroupwise":
                    {


                        int i = 0, j = 0;
                        String[] GroupName;
                        object GroupName1 = "";
                        Boolean chkcommflag = false, chkpaidflag = false, chkbalflag = false, chkdiscflag = false, chkAdvflag = false, chkadminchargeflag = false;
                        double T_NetAmount = 0, TotalNetPayAmt = 0, Tot_Advamt = 0, balafteradvuseAmount = 0, BalancewdudcAmt = 0;

                        string previousLabel = "";
                        string deptLabel = "";
                        String FinalLabel = "";
                        double T_TotAmount = 0, ChargesTotalamt = 0, T_TotalAmount = 0, F_TotalAmount = 0.0, AdminChares = 0, Tot_paidamt = 0;


                        foreach (DataRow dr in dt.Rows)
                        {

                            i++; j++;


                            if (i == 1)
                            {

                                String Label2;
                                Label2 = dr["GroupName"].ConvertToString();
                                items.Append("<tr style=\"font-size:18px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;border: 1px;font-weight:bold;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label2).Append("</td></tr>");

                            }


                            if (previousLabel != "" && previousLabel != dr["GroupName"].ConvertToString())
                            {
                                j = 1;
                                items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                              .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");
                                T_TotalAmount = 0;

                                items.Append("<tr style=\"font-size:20px;border-bottom: 1px;\"><td colspan=\"13\" style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:left;vertical-align:middle;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;font-weight:bold;\">").Append(dr["GroupName"].ConvertToString()).Append("</td></tr>");

                            }


                            T_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                            F_TotalAmount += dr["ChargesTotalAmt"].ConvertToDouble();
                            ChargesTotalamt += dr["ChargesTotalAmt"].ConvertToDouble();


                            previousLabel = dr["GroupName"].ConvertToString();


                            items.Append("<tr style=\"font-family: 'Helvetica Neue','Helvetica',, Arial, sans-serif;font-size:18px;\"><td style=\"border: 1px solid #000; text-align: center; padding: 6px;\">").Append(j).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: right; padding: 6px;font-size:18px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ChargesTotalAmt"].ConvertToDouble()).Append("</td></tr>");

                            if (dt.Rows.Count > 0 && dt.Rows.Count == i)
                            {

                                items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Group Wise Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                               .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");


                            }

                            TotalNetPayAmt = dr["NetPayableAmt"].ConvertToDouble();
                            Tot_Advamt = dr["AdvanceUsedAmount"].ConvertToDouble();
                            Tot_paidamt = dr["PaidAmount"].ConvertToDouble();

                            if (Tot_Advamt.ConvertToDouble() < TotalNetPayAmt.ConvertToDouble())
                            {
                                BalancewdudcAmt = (TotalNetPayAmt - Tot_Advamt - Tot_paidamt).ConvertToDouble();
                            }

                        }


                        string htmlHeader = "";
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

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
                        html = html.Replace("{{chkCompanyNameflag}}", dt.GetColValue("CompanyName").ConvertToString() != "" ? "visible" : "none");


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

                        html = html.Replace("{{ConcessionReason}}", dt.GetColValue("ConcessionReason").ConvertToString());
                        


                    }


                    break;
                case "IPCompanyFinalBillWithSR":
                    {

                        int i = 0, j = 0;
                        String[] GroupName;
                        object GroupName1 = "";
                        Boolean chkcommflag = false, chkpaidflag = false, chkbalflag = false, chkdiscflag = false, chkAdvflag = false, chkadminchargeflag = false;
                        double T_NetAmount = 0, TotalNetPayAmt = 0, Tot_Advamt = 0, balafteradvuseAmount = 0, BalancewdudcAmt = 0;

                        string GroupLable = "";
                        string ClassLable = "";
                        String FinalLabel = "";
                        double T_TotAmount = 0, ChargesTotalamt = 0, T_TotalAmount = 0, F_TotalAmount = 0.0, AdminChares = 0, NetPayableAmt = 0, Tot_paidamt = 0;
                        double Tot = 0;
                        int length = dt.Rows.Count - 1;
                        string previousLabel = "";
                        foreach (DataRow dr in dt.Rows)
                        {

                            i++; j++;


                            if (i == 1)
                            {
                                String Label;
                                Label = dr["DepartmentName"].ConvertToString();

                                items.Append("<tr style=\"font-size:20px;border: 1px;color:black;\"><td colspan=\"7\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                            }
                            if (previousLabel != "" && previousLabel != dr["DepartmentName"].ConvertToString())
                            {
                                j = 1;

                                items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='5' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                                   .Append(NetPayableAmt.ToString()).Append("</td></tr>");

                                NetPayableAmt = 0;
                                items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td colspan=\"7\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["DepartmentName"].ConvertToString()).Append("</td></tr>");

                            }


                            previousLabel = dr["DepartmentName"].ConvertToString();


                            T_TotalAmount += dr["NetPayableAmt"].ConvertToDouble();
                            F_TotalAmount += dr["NetPayableAmt"].ConvertToDouble();
                            NetPayableAmt += dr["NetPayableAmt"].ConvertToDouble();

                            items.Append("<tr style=\"font-family: 'Helvetica Neue','Helvetica',, Arial, sans-serif;font-size:22px;\"><td style=\"border: 1px solid #000; text-align: right; padding: 6px;\">").Append(j).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ChargesDoctorName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: left; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["ClassName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["Price"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: center; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #000; text-align: right; padding: 6px;font-size:15px;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\">").Append(dr["NetPayableAmt"].ConvertToDouble()).Append("</td></tr>");

                            if (dt.Rows.Count > 0 && dt.Rows.Count == i && i != length + 1)
                            {
                                items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\"><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                                 .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

                            }

                            if (i == length + 1)
                            {

                                T_TotalAmount = T_TotalAmount - Tot;
                                items.Append("<tr style='font-size:20px;border:1px solid black;font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='5' style=\"border:1px solid #000;border-collapse: collapse;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\"><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;font-weight:bold;\">")
                                .Append(T_TotalAmount.To2DecimalPlace()).Append("</td></tr>");

                            }

                            ClassLable = dr["ClassName"].ConvertToString();
                            GroupLable = dr["GroupName"].ConvertToString();

                            TotalNetPayAmt = dr["NetPayableAmt"].ConvertToDouble();
                        }


                        html = html.Replace("{{Items}}", items.ToString());

                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));

                        string finalamt = conversion(dt.GetColValue("TotalAmt").ConvertToDouble().To2DecimalPlace().ToString());
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

                        html = html.Replace("{{SubTPACompanyName}}", dt.GetColValue("SubTPACompanyName"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{AdmittedDoctorName}}", dt.GetColValue("AdmittedDoctorName"));

                        html = html.Replace("{{DischargeDate}}", dt.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{BillDate}}", dt.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{CompBillDate}}", dt.GetColValue("CompBillDate").ConvertToDateString("dd/MM/yyyy"));
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
                        // html = html.Replace("{{PayTMPayAmount}}", dt.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{PayTMPayAmount}}", dt.GetColValue("OnlinePayAmount").ConvertToDouble().ToString("0.00"));

                        html = html.Replace("{{TotalAdvanceAmount}}", dt.GetColValue("TotalAdvanceAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{AdvanceUsedAmount}}", dt.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{AdvanceBalAmount}}", dt.GetColValue("AdvanceBalAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{AdvanceRefundAmount}}", dt.GetColValue("AdvanceRefundAmount").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{ConcessionAmount}}", dt.GetColValue("ConcessionAmt").ConvertToDouble().ToString("0.00"));
                        html = html.Replace("{{BalanceAmt}}", dt.GetColValue("BalanceAmt").ConvertToDouble().ToString("0.00"));


                        html = html.Replace("{{T_NetAmount}}", dt.GetColValue("NetPayableAmt").ConvertToDouble().ToString("0.00"));

                        html = html.Replace("{{NetPayableAmt}}", NetPayableAmt.ConvertToDouble().ToString("0.00"));
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



                //case "AdmissionList":
                //    {
                //        HeaderItems.Append("<tr>");
                //        foreach (var hr in headerList)
                //        {
                //            HeaderItems.Append("<th style=\"border: 1px solid #000;font-size:20px;padding: 6px;\">");
                //            HeaderItems.Append(hr.ConvertToString());
                //            HeaderItems.Append("</th>");
                //        }
                //        HeaderItems.Append("</tr>");

                //        int k = 0, i = 0, D = 0, j = 0;
                //        string previousLabel = "";
                //        var groupedData = dt.AsEnumerable().GroupBy(row => row["AdmittedDoctorName"]).ToList();
                //        foreach (DataRow dr in dt.Rows)
                //        {
                //            i++; j++;
                //            if (i == 1)
                //            {
                //                String Label;
                //                Label = dr["AdmittedDoctorName"].ConvertToString();
                //                items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td colspan=\"4\" style=\"border:1px solid #000;font-weight:bold;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");

                //            }

                //            if (previousLabel != "" && previousLabel != dr["AdmittedDoctorName"].ConvertToString())
                //            {
                //                j = 1;

                //                items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='12' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                //                 .Append(D).Append("</td></tr>");


                //                D = 0;
                //                items.Append("<tr style=\"font-size:20px;border-bottom: 1px;border-right: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;font-weight:bold;vertical-align:middle\">").Append(dr["AdmittedDoctorName"].ConvertToString()).Append("</td></tr>");
                //            }
                //            D++;
                //            previousLabel = dr["AdmittedDoctorName"].ConvertToString();

                //            items.Append("<tr style=\"text-align: center; border: 1px solid #000; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #000; padding: 6px;\">").Append(j).Append("</td>");
                //            foreach (var colName in colList)
                //            {
                //                items.Append("<td style=\"text-align: center; border: 1px solid #000; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                //            }

                //            if (dt.Rows.Count > 0 && dt.Rows.Count == i)
                //            {

                //                items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='12' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                //                     .Append(D).Append("</td></tr>");

                //            }

                //        }

                //    }
                case "AdmissionList":
                    {
                        HeaderItems.Append("<tr>");
                        foreach (var hr in headerList)
                        {
                            HeaderItems.Append("<th style=\"border: 1px solid #000; font-size:20px; padding: 6px; font-family: Calibri,'Helvetica Neue','Helvetica',Helvetica,Arial,sans-serif;\">");
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
                                string Label = dr["AdmittedDoctorName"].ConvertToString();
                                items.Append("<tr style=\"font-size:20px; font-family: Calibri,'Helvetica Neue','Helvetica',Helvetica,Arial,sans-serif;\"><td colspan=\"4\" style=\"border:1px solid #000; font-weight:bold; padding:3px; height:10px; text-align:left; vertical-align:middle; font-family: Calibri,'Helvetica Neue','Helvetica',Helvetica,Arial,sans-serif;\">")
                                     .Append(Label)
                                     .Append("</td></tr>");
                            }

                            if (previousLabel != "" && previousLabel != dr["AdmittedDoctorName"].ConvertToString())
                            {
                                j = 1;
                                items.Append("<tr style='border:1px solid black; color:black; background-color:white; font-family: Calibri,\"Helvetica Neue\",\"Helvetica\",Helvetica,Arial,sans-serif;'><td colspan='12' style=\"border-right:1px solid #000; padding:3px; height:10px; text-align:right; vertical-align:middle; margin-right:20px; font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000; padding:3px; height:10px; text-align:center; vertical-align:middle\">")
                                     .Append(D)
                                     .Append("</td></tr>");

                                D = 0;
                                items.Append("<tr style=\"font-size:20px; border-bottom: 1px; border-right: 1px; font-family: Calibri,'Helvetica Neue','Helvetica',Helvetica,Arial,sans-serif;\"><td colspan=\"5\" style=\"border:1px solid #000; padding:3px; height:10px; text-align:left; font-weight:bold; vertical-align:middle; font-family: Calibri,'Helvetica Neue','Helvetica',Helvetica,Arial,sans-serif;\">")
                                     .Append(dr["AdmittedDoctorName"].ConvertToString())
                                     .Append("</td></tr>");
                            }

                            D++;
                            previousLabel = dr["AdmittedDoctorName"].ConvertToString();

                            items.Append("<tr style=\"text-align: center; border: 1px solid #000; padding: 6px; font-family: Calibri,'Helvetica Neue','Helvetica',Helvetica,Arial,sans-serif;\">")
                                 .Append("<td style=\"text-align: center; border: 1px solid #000; padding: 6px; font-family: Calibri,'Helvetica Neue','Helvetica',Helvetica,Arial,sans-serif;\">")
                                 .Append(j)
                                 .Append("</td>");

                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #000; padding: 6px; font-family: Calibri,'Helvetica Neue','Helvetica',Helvetica,Arial,sans-serif;\">")
                                     .Append(dr[colName].ConvertToString())
                                     .Append("</td>");
                            }

                            if (dt.Rows.Count > 0 && dt.Rows.Count == i)
                            {
                                items.Append("<tr style='border:1px solid black; color:black; background-color:white; font-family: Calibri,\"Helvetica Neue\",\"Helvetica\",Helvetica,Arial,sans-serif;'><td colspan='12' style=\"border-right:1px solid #000; padding:3px; height:10px; text-align:right; vertical-align:middle; margin-right:20px; font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000; padding:3px; height:10px; text-align:center; vertical-align:middle\">")
                                     .Append(D)
                                     .Append("</td></tr>");
                            }
                        }
                    }



                    break;



                case "IpPaymentReceipt":
                    {

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
                        html = html.Replace("{{ConcessionAmt}}", dt.GetColValue("ConcessionAmt"));


                        html = html.Replace("{{PayTMPayAmount}}", dt.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{CashPayAmount}}", dt.GetColValue("CashPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{CardPayAmount}}", dt.GetColValue("CardPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{ChequePayAmount}}", dt.GetColValue("ChequePayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{NEFTPayAmount}}", dt.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{OnlinePayAmount}}", dt.GetColValue("OnlinePayAmount").ConvertToDouble().ToString("F2"));
                        
                        html = html.Replace("{{chkConcessionflag}}", dt.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");
                        

                    }


                    break;

                case "IpAdvanceReceipt":
                    {

                        html = html.Replace("{{reason}}", dt.GetColValue("reason"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AdvanceNo}}", dt.GetColValue("AdvanceNo"));
                        html = html.Replace("{{Addedby}}", dt.GetColValue("Addedby"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{Age}}", dt.GetColValue("Age"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));

                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{PaymentTime}}", dt.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AdvanceAmount}}", dt.GetColValue("AdvanceAmount").ConvertToDouble().ToString("F2"));
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

                        html = html.Replace("{{CashPayAmount}}", dt.GetColValue("CashPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{ChequePayAmount}}", dt.GetColValue("ChequePayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{ChequeNo}}", dt.GetColValue("ChequeNo").ConvertToString());
                        html = html.Replace("{{CardPayAmount}}", dt.GetColValue("CardPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{CardBankName}}", dt.GetColValue("CardBankName").ConvertToString());
                        html = html.Replace("{{CardNo}}", dt.GetColValue("CardNo").ConvertToString());
                        html = html.Replace("{{BankName}}", dt.GetColValue("BankName").ConvertToString());
                        html = html.Replace("{{NEFTPayAmount}}", dt.GetColValue("PatientType").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{NEFTNo}}", dt.GetColValue("NEFTNo").ConvertToString());
                        html = html.Replace("{{NEFTBankMaster}}", dt.GetColValue("NEFTBankMaster").ConvertToString());
                        html = html.Replace("{{PayTMPayAmount}}", dt.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{PayTMTranNo}}", dt.GetColValue("PayTMTranNo").ConvertToString());
                        html = html.Replace("{{PayTMDate}}", dt.GetColValue("PayTMDate").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{chkCompanyNameflag}}", dt.GetColValue("CompanyName").ConvertToString() != "" ? "visible" : "none");


                        html = html.Replace("{{chkcashflag}}", dt.GetColValue("CashPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkchequeflag}}", dt.GetColValue("ChequePayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkcardflag}}", dt.GetColValue("CardPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkneftflag}}", dt.GetColValue("NEFTPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkpaytmflag}}", dt.GetColValue("PayTMAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        string finalamt = conversion(dt.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace().ToString());
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


                        html = html.Replace("{{chkresonflag}}", dt.GetColValue("reason").ConvertToString() != null ? "block" : "none");

                    }


                    break;

                case "IpAdvanceStatement":
                    {

                        int i = 0;

                        double T_AdvanceAmount = 0, T_UsedAmount = 0, T_BalanceAmount = 0, T_RefundAmount = 0, T_CashPayAmount = 0, T_ChequePayAmount = 0, T_CardPayAmount = 0, T_OnlineAmount = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;font-size: 18px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;font-size: 18px;text-align:center;\">").Append(dr["PaymentDate"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;font-size: 18px;text-align:center;\">").Append(dr["AdvanceNo"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;font-size: 18px;text-align:right;\">").Append(dr["AdvanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;font-size: 18px;text-align:right;\">").Append(dr["UsedAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;font-size: 18px;text-align:right;\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;font-size: 18px;text-align:right;\">").Append(dr["RefundAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;font-size: 18px;text-align:right;\">").Append(dr["CashPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;font-size: 18px;text-align:right;\">").Append(dr["ChequePayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;font-size: 18px;text-align:right;\">").Append(dr["CardPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; padding: 6px;font-size: 18px;text-align:right;\">").Append(dr["OnlineAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");
         


                            T_AdvanceAmount += dr["AdvanceAmount"].ConvertToDouble();
                            T_UsedAmount += dr["UsedAmount"].ConvertToDouble();
                            T_BalanceAmount += dr["BalanceAmount"].ConvertToDouble();
                            T_RefundAmount += dr["RefundAmount"].ConvertToDouble();
                            T_CashPayAmount += dr["CashPayAmount"].ConvertToDouble();
                            T_ChequePayAmount += dr["ChequePayAmount"].ConvertToDouble();
                            T_CardPayAmount += dr["CardPayAmount"].ConvertToDouble();
                            T_OnlineAmount += dr["OnlineAmount"].ConvertToDouble();



                        }


                        html = html.Replace("{{Items}}", items.ToString());
                        html = html.Replace("{{T_AdvanceAmount}}", T_AdvanceAmount.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_UsedAmount}}", T_UsedAmount.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_BalanceAmount}}", T_BalanceAmount.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_RefundAmount}}", T_RefundAmount.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_CashPayAmount}}", T_CashPayAmount.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_CardPayAmount}}", T_CardPayAmount.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_ChequePayAmount}}", T_ChequePayAmount.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_OnlineAmount}}", T_OnlineAmount.ConvertToDouble().ToString("F2"));

                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName").ConvertToString());
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo").ConvertToString());
                        html = html.Replace("{{DOA}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy hh:mm"));

                        //As per snajay sir req changes  by Ashu 27 May 2025
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName").ConvertToString());
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName").ConvertToString());
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName").ConvertToString());
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName").ConvertToString());
                        html = html.Replace("{{BillNo}}", dt.GetColValue("PBillNo").ConvertToString());
                        html = html.Replace("{{PaymentDate}}", dt.GetColValue("PaymentDate").ConvertToDateString("dd/MM/yyyy | H:mm tt"));
                        html = html.Replace("{{PayMode}}", dt.GetColValue("PayMode").ConvertToString());
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName").ConvertToString());
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName").ConvertToString());
                        html = html.Replace("{{ExtMobileNo}}", dt.GetColValue("ExtMobileNo"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo").ConvertToString());
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear").ConvertToString());
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth").ConvertToString());
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay").ConvertToString());
                        html = html.Replace("{{Date}}", dt.GetDateColValue("Date").ConvertToDateString());
                        html = html.Replace("{{VisitDate}}", dt.GetColValue("VisitTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{PhoneNo}}", dt.GetColValue("PhoneNo"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType").ConvertToString());
                        html = html.Replace("{{OPDNo}}", dt.GetColValue("OPDNo").ConvertToString());
                        html = html.Replace("{{chkCompanyNameflag}}", dt.GetColValue("CompanyName").ConvertToString() != "" ? "visible" : "none");


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
                        html = html.Replace("{{AdvanceAmount}}", dt.GetColValue("AdvanceAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{RefundAmount}}", dt.GetColValue("RefundAmount").ConvertToDouble().ToString("F2"));
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
                        html = html.Replace("{{RefundAmount}}", dt.GetColValue("RefundAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{AdvanceUsedAmount}}", dt.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("F2"));

                        html = html.Replace("{{BalanceAmount}}", dt.GetColValue("BalanceAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{CashPayAmount}}", dt.GetColValue("CashPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{CardPayAmount}}", dt.GetColValue("CardPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{ChequePayAmount}}", dt.GetColValue("ChequePayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{NEFTPayAmount}}", dt.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{PayTMPayAmount}}", dt.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("F2"));


                        html = html.Replace("{{DischargeDate}}", dt.GetColValue("DischargeDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        html = html.Replace("{{RefundTime}}", dt.GetColValue("RefundTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{PaymentTime}}", dt.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AddedBy}}", dt.GetColValue("AddedBy"));

                        string finalamt = conversion(dt.GetColValue("RefundAmount").ConvertToDouble().To2DecimalPlace().ToString());
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


                    }


                    break;


                case "IpBillRefundReceipt":
                    {

                        int i = 0;
                        string htmlHeader = "";
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                        html = html.Replace("{{Remark}}", dt.GetColValue("Remark"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{RefundNo}}", dt.GetColValue("RefundNo"));
                        html = html.Replace("{{Addedby}}", dt.GetColValue("AddedBy"));
                        html = html.Replace("{{Age}}", dt.GetColValue("Age"));
                        html = html.Replace("{{AdmissinDate}}", dt.GetColValue("AdmissinDate"));
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{NetPayableAmt}}", dt.GetColValue("NetPayableAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{RefundAmount}}", dt.GetColValue("RefundAmount").ConvertToDouble().ToString("F2"));
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

                        html = html.Replace("{{chkCompanyNameflag}}", dt.GetColValue("CompanyName").ConvertToString() != "" ? "visible" : "none");

                        html = html.Replace("{{DischargeDate}}", dt.GetColValue("DischargeDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{RefundTime}}", dt.GetColValue("RefundTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{PaymentTime}}", dt.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));

                        string finalamt = conversion(dt.GetColValue("RefundAmount").ConvertToDouble().To2DecimalPlace().ToString());
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());

                        return html;

                    }


                    break;
                case "IpDischargeReceipt":
                    {

                        string htmlHeader = "";
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AdvanceNo}}", dt.GetColValue("AdvanceNo"));
                        html = html.Replace("{{Addedby}}", dt.GetColValue("Addedby"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));

                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));

                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{PaymentTime}}", dt.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{DischargeTime}}", dt.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{BillTime}}", dt.GetColValue("BillTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AdvanceAmount}}", dt.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace());
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

                        html = html.Replace("{{chkCompanyNameflag}}", dt.GetColValue("CompanyName").ConvertToString() != "" ? "visible" : "none");

                        html = html.Replace("{{BillNo}}", dt.GetColValue("PBillNo"));
                        html = html.Replace("{{DischargeTime}}", dt.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));

                        html = html.Replace("{{OPD_IPD_ID}}", dt.GetColValue("OPD_IPD_ID"));
                        html = html.Replace("{{BillingUserName}}", dt.GetColValue("BillingUserName"));

                    }


                    break;
                case "IpDischargeSummaryReport":
                    {

                        int i = 0;
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{DischargeTime}}", dt.GetColValue("DischargeTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{Followupdate}}", dt.GetColValue("Followupdate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        html = html.Replace("{{History}}", dt.GetColValue("History"));
                        html = html.Replace("{{Diagnosis}}", dt.GetColValue("Diagnosis"));
                        html = html.Replace("{{ClinicalFinding}}", dt.GetColValue("ClinicalFinding"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{OpertiveNotes}}", dt.GetColValue("OpertiveNotes"));
                        html = html.Replace("{{TreatmentGiven}}", dt.GetColValue("TreatmentGiven"));
                        html = html.Replace("{{Investigation}}", dt.GetColValue("Investigation"));

                        html = html.Replace("{{p}}", dt.GetColValue("History"));
                        html = html.Replace("{{R}}", dt.GetColValue("Diagnosis"));
                        html = html.Replace("{{SPO2}}", dt.GetColValue("ClinicalFinding"));
                        html = html.Replace("{{RS}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{PA}}", dt.GetColValue("OpertiveNotes"));
                        html = html.Replace("{{CVS}}", dt.GetColValue("TreatmentGiven"));
                        html = html.Replace("{{CNS}}", dt.GetColValue("Investigation"));

                        html = html.Replace("{{DischargeTypeName}}", dt.GetColValue("DischargeTypeName"));
                        html = html.Replace("{{TreatmentAdvisedAfterDischarge}}", dt.GetColValue("TreatmentAdvisedAfterDischarge"));

                        html = html.Replace("{{ClinicalConditionOnAdmisssion}}", dt.GetColValue("ClinicalConditionOnAdmisssion"));

                        html = html.Replace("{{OtherConDrOpinions}}", dt.GetColValue("OtherConDrOpinions"));

                        html = html.Replace("{{PainManagementTechnique}}", dt.GetColValue("PainManagementTechnique"));
                        html = html.Replace("{{LifeStyle}}", dt.GetColValue("LifeStyle"));

                        html = html.Replace("{{WarningSymptoms}}", dt.GetColValue("WarningSymptoms"));
                        html = html.Replace("{{ConditionAtTheTimeOfDischarge}}", dt.GetColValue("ConditionAtTheTimeOfDischarge"));




                        html = html.Replace("{{DoctorAssistantName}}", dt.GetColValue("DoctorAssistantName"));
                        html = html.Replace("{{PreOthNumber}}", dt.GetColValue("PreOthNumber"));
                        html = html.Replace("{{SurgeryProcDone}}", dt.GetColValue("SurgeryProcDone"));
                        html = html.Replace("{{ICD10CODE}}", dt.GetColValue("ICD10CODE"));
                        html = html.Replace("{{Radiology}}", dt.GetColValue("Radiology"));
                        html = html.Replace("{{IsNormalOrDeath}}", dt.GetColValue("IsNormalOrDeath"));

                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{RefDoctorName}}", dt.GetColValue("RefDoctorName"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{AddedBy}}", dt.GetColValue("AddedBy"));
                        html = html.Replace("{{DischargeDoctor2}}", dt.GetColValue("DiscDoctor2"));

                        //border: 1px solid #d4c3c3;

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;
                            items.Append("<tr style=\" text-align: center; padding: 6px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, sans-serif;font-size: 24px;\"><td style=\"border-left: 1px solid #d4c3c3;text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                            //items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("<br>").Append(dr["ItemGenericName"].ConvertToString()).Append("</td>");
                            //items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
                            //items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3;text-align: center; padding: 6px;\">").Append(dr["DoseNameInEnglish"].ConvertToString()).Append("</td>");
                            //items.Append("<td style=\"border-right: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-bottom: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append("(").Append(dr["DoseNameInMarathi"].ConvertToString()).Append(")").Append("</td>");
                            //items.Append("<td style=\"border-left: 1px solid #d4c3c3;border-top: 1px solid #d4c3c3;border-right: 1px solid #d4c3c3;  text-align: center; padding: 6px;\">").Append(dr["Days"].ConvertToString()).Append("</td></tr>");


                        }

                        html = html.Replace("{{Items}}", items.ToString());

                        html = html.Replace("{{chkHistoryflag}}", dt.GetColValue("History").ConvertToString() != "" ? "table-row" : "none");

                        html = html.Replace("{{chkHistoryflag}}", dt.GetColValue("History").ConvertToString() != "" ? "block" : "table-row");

                        html = html.Replace("{{chkRadiologyflag}}", dt.GetColValue("Radiology").ConvertToString() != "" ? "table-row" : "none");

                        html = html.Replace("{{chkRadiologyflag}}", dt.GetColValue("Radiology").ConvertToString() != "" ? "block" : "table-row");


                        html = html.Replace("{{chkDignosflag}}", dt.GetColValue("Diagnosis").ConvertToString() != "" ? "table-row" : "none");

                        html = html.Replace("{{chkClfindingflag}}", dt.GetColValue("ClinicalFinding").ConvertToString() != "" ? "table-row" : "none");

                        html = html.Replace("{{chkOprativeflag}}", dt.GetColValue("OpertiveNotes").ConvertToString() != "" ? "table-row" : "none");

                        html = html.Replace("{{chkTreatmentflag}}", dt.GetColValue("TreatmentGiven").ConvertToString() != "" ? "table-row" : "none");


                        html = html.Replace("{{chkInvestigationflag}}", dt.GetColValue("Investigation").ConvertToString() != "" ? "table-row" : "none");



                        html = html.Replace("{{chktreatadviceflag}}", dt.GetColValue("TreatmentAdvisedAfterDischarge").ConvertToString() != "" ? "table-row" : "none");

                        html = html.Replace("{{chkTreatmentflag}}", dt.GetColValue("TreatmentGiven").ConvertToString() != " " ? "table-row" : "none");


                        html = html.Replace("{{chkInvestigationflag}}", dt.GetColValue("Investigation").ConvertToString() != "" ? "table-row" : "none");

                        html = html.Replace("{{chkClinicalconditionflag}}", dt.GetColValue("ClinicalConditionOnAdmisssion").ConvertToString() != "" ? "table-row" : "none");



                        html = html.Replace("{{chkotherconditionflag}}", dt.GetColValue("OtherConDrOpinions").ConvertToString() != "" ? "table-row" : "none");

                        html = html.Replace("{{chkpainmanageflag}}", dt.GetColValue("PainManagementTechnique").ConvertToString() != "" ? "table-row" : "none");


                        html = html.Replace("{{chkConondiscflag}}", dt.GetColValue("ConditionAtTheTimeOfDischarge").ConvertToString() != "" ? "table-row" : "none");


                        html = html.Replace("{{chkLifeStyleflag}}", dt.GetColValue("LifeStyle").ConvertToString() != "" ? "table-row" : "none");

                        html = html.Replace("{{chkWarningSymptomsflag}}", dt.GetColValue("WarningSymptoms").ConvertToString() != "" ? "table-row" : "none");


                        html = html.Replace("{{chkSurgeryProcDoneflag}}", dt.GetColValue("SurgeryProcDone").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkidischragetypeflag}}", dt.GetColValue("DischargeTypeName").ConvertToString() != "NULL" ? "table-row" : "none");


                        html = html.Replace("{{chkSurgeryProcDoneflag}}", dt.GetColValue("SurgeryProcDone").ConvertToString() != "" ? "table-row" : "none");
                        



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

                            items.Append("<tr style=\"font-size:15px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;border-bottom: 1px;\"><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center\">").Append(j).Append("</td>");

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
                                items.Append("<tr style='border:1px solid black; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='10' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle;margin-center:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
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
                                items.Append("<tr style=\"font-size:20px;border: 1;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td colspan=\"7\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");

                            }

                            if (previousLabel != "" && previousLabel != dr["RefDoctorName"].ConvertToString())
                            {
                                j = 1;

                                items.Append("<tr style='border:1px solid black;color:black;background-color:white'><td colspan='6' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")
                                 .Append(D).Append("</td></tr>");


                                D = 0;
                                items.Append("<tr style=\"font-size:20px;border-bottom: 1px;border-right: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td colspan=\"7\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(dr["RefDoctorName"].ConvertToString()).Append("</td></tr>");
                            }
                            D++;
                            previousLabel = dr["RefDoctorName"].ConvertToString();

                            items.Append("<tr style=\"text-align: center; border: 1px solid #000; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #000; padding: 6px;\">").Append(j).Append("</td>");
                            foreach (var colName in colList)
                            {
                                items.Append("<td style=\"text-align: center; border: 1px solid #000; padding: 6px;\">").Append(dr[colName].ConvertToString()).Append("</td>");
                            }

                            if (dt.Rows.Count > 0 && dt.Rows.Count == i)
                            {

                                items.Append("<tr style='border:1px solid black;color:black;background-color:white; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;'><td colspan='6' style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\"> Total Count</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:center;vertical-align:middle\">")

                                     .Append(D).Append("</td></tr>");

                            }

                        }


                    }
                    break;
                case "Purchaseorder":
                    {
                        string html1 = File.ReadAllText(htmlFilePath);
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);
                        StringBuilder item = new StringBuilder("");
                        int i = 0;
                        Boolean chkdiscflag = false;

                        double T_TotalAmount = 0, T_TotalVatAmount = 0, T_TotalDiscAmount = 0, T_TotalNETAmount = 0, T_TotalBalancepay = 0, T_TotalCGST = 0, T_TotalSGST = 0, T_TotalIGST = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr style=\" font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["UnitofMeasurementName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["MRP"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Rate"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ItemTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DiscPer"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CGSTPer"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SGSTPer"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["IGSTPer"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["GrandTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                            T_TotalAmount += dr["ItemTotalAmount"].ConvertToDouble();
                            T_TotalVatAmount += dr["VatAmount"].ConvertToDouble();
                            T_TotalDiscAmount += dr["ItemDiscAmount"].ConvertToDouble();
                            T_TotalNETAmount += dr["GrandTotalAmount"].ConvertToDouble();

                            //T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();
                            //T_TotalCGST += dr["CGSTAmt"].ConvertToDouble();
                            //T_TotalSGST += dr["SGSTAmt"].ConvertToDouble();
                            //T_TotalIGST += dr["IGSTAmt"].ConvertToDouble();


                        }
                        //| currency:'INR':'symbol-narrow':'0.2'

                        //T_TotalNETAmount = Math.Round(T_TotalNETAmount);
                        //T_TotalVatAmount = Math.Round(T_TotalVatAmount);
                        //T_TotalDiscAmount = Math.Round(T_TotalDiscAmount);
                        html = html.Replace("{{Items}}", items.ToString());
                        //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
                        //html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));

                        string htmlHeader = "";
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));



                        html = html.Replace("{{TotalAmount}}", T_TotalAmount.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{TotalVatAmount}}", T_TotalVatAmount.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{TotalDiscAmount}}", T_TotalDiscAmount.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_TotalDiscAmount}}", T_TotalDiscAmount.ConvertToDouble().ToString("F2"));


                        html = html.Replace("{{FreightAmount}}", dt.GetColValue("FreightAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{OctriAmount}}", dt.GetColValue("OctriAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{TotalAmount}}", dt.GetColValue("TotalAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{HandlingCharges}}", dt.GetColValue("HandlingCharges").ConvertToDouble().ToString("F2"));

                        html = html.Replace("{{TransportChanges}}", dt.GetColValue("TransportChanges").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{DiscAmount}}", dt.GetColValue("DiscAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{AddedByName}}", dt.GetColValue("AddedByName"));
                        html = html.Replace("{{VatAmount}}", dt.GetColValue("VatAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{DiscAmount}}", dt.GetColValue("DiscAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{GrandTotal}}", dt.GetColValue("GrandTotal").ConvertToDouble().ToString("F2"));

                        html = html.Replace("{{Remarks}}", dt.GetColValue("Remarks"));
                        html = html.Replace("{{PurchaseDate}}", dt.GetColValue("PurchaseDate").ConvertToDateString("dd/mm/yyyy hh:mm tt"));
                        html = html.Replace("{{SupplierName}}", dt.GetColValue("SupplierName").ConvertToString());
                        html = html.Replace("{{PurchaseNo}}", dt.GetColValue("PurchaseNo").ConvertToString());
                        html = html.Replace("{{Address}}", dt.GetColValue("Address"));
                        html = html.Replace("{{Email}}", dt.GetColValue("Email").ConvertToString());
                        html = html.Replace("{{GSTNo}}", dt.GetColValue("GSTNo").ConvertToString());
                        html = html.Replace("{{Mobile}}", dt.GetColValue("Mobile"));
                        html = html.Replace("{{Phone}}", dt.GetColValue("Phone"));
                        html = html.Replace("{{VatAmount}}", dt.GetColValue("VatAmount").ConvertToString());
                        html = html.Replace("{{Mobile}}", dt.GetColValue("Mobile"));
                        html = html.Replace("{{Phone}}", dt.GetColValue("Phone"));
                        html = html.Replace("{{PrintStoreName}}", dt.GetColValue("PrintStoreName"));
                        html = html.Replace("{{StoreAddress}}", dt.GetColValue("StoreAddress"));
                        html = html.Replace("{{VerifiedName}}", dt.GetColValue("VerifiedName"));

                        html = html.Replace("{{PurchaseTime}}", dt.GetColValue("PurchaseTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));

                        string finalamt = conversion(T_TotalNETAmount.ToString());
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


                        html = html.Replace("{{chkdiscflag}}", dt.GetColValue("T_TotalDiscAmount").ConvertToDouble() > 0 ? "block" : "none");

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
                            html = html.Replace("{{Remark}}", dt.GetColValue("Remark"));
                            html = html.Replace("{{DetailGiven}}", dt.GetColValue("DetailGiven"));



                            html = html.Replace("{{chkRemarkflag}}", dt.GetColValue("Remark") != null ? "table-row " : "none");

                            html = html.Replace("{{chkgivenflag}}", dt.GetColValue("DetailGiven").ConvertToString() != "" ? "table -row " : "none");

                            html = html.Replace("{{chkcashflag}}", dt.GetColValue("CashPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                            html = html.Replace("{{chkchequeflag}}", dt.GetColValue("ChequePayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                            html = html.Replace("{{chkcardflag}}", dt.GetColValue("CardPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                            html = html.Replace("{{chkneftflag}}", dt.GetColValue("NEFTPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                            html = html.Replace("{{chkpaytmflag}}", dt.GetColValue("PayTMAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                            html = html.Replace("{{chkCompanyNameflag}}", dt.GetColValue("CompanyName").ConvertToString() != "" ? "visible" : "none");



                            return html;

                        }

                    }
                    break;


                case "PathologyReportWithHeader":
                    {

                        Boolean chkresonflag = false;
                        string chkflag = "";
                        int Suggflag = 0;
                        int i = 0, j = 0, k = 0, testlength = 0, m;
                        String Label = "", Suggchk = "", Suggestion = "";
                        string previousLabel = "", previoussubLabel = "";
                        string signatureFileName = dt.Rows[0]["Signature"].ConvertToString();

                        var signature = string.IsNullOrWhiteSpace(signatureFileName) ? "" : _pdfUtility.GetBase64FromFolder("Doctors\\Signature", dt.Rows[0]["Signature"].ConvertToString());

                        html = html.Replace("{{Signature}}", signature);
                        html = html.Replace("{{chkSignature}}", !string.IsNullOrWhiteSpace(signatureFileName) ? "inline-block" : "none");
                        //html = html.Replace("{{ImgHeader}}", htmlHeader);

                        foreach (DataRow dr in dt.Rows)
                        {

                            i++;

                            if (i == 1)
                                items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;font-weight:bold;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:center;padding-left:30px;vertical-align:left\">").Append(dr["PrintTestName"].ConvertToString()).Append("</td></tr>");


                            if (previousLabel != "" && previousLabel != dr["PrintTestName"].ConvertToString())
                            {
                                if (i > 3)
                                    Suggestion = dt.Rows[i - 2]["SuggestionNote"].ConvertToString();
                                else if (i < 2)
                                    Suggestion = dt.Rows[i - 1]["SuggestionNote"].ConvertToString();
                                if (Suggestion != "")
                                {
                                    items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle;font-weight:bold;\">").Append("Interpretation Remark :").Append("</td></tr>");
                                    items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Suggestion).Append("</td></tr>");
                                }

                                items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;font-weight:bold;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:center;padding-left:30px;vertical-align:middle\">").Append(dr["PrintTestName"].ConvertToString()).Append("</td></tr>");


                            }
                            if (dr["ResultValue"].ConvertToString() != " ")
                            {

                                k++;

                                if (k == 1 && dr["SubTestName"].ConvertToString() != "")
                                {


                                    previoussubLabel = dr["SubTestName"].ConvertToString();

                                    items.Append("<tr style=\"font-size:18px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;font-weight:bold;padding-bottom:5px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle;\">").Append(dr["SubTestName"].ConvertToString()).Append("</td></tr>");

                                }
                                if (previoussubLabel != "" && previoussubLabel != dr["SubTestName"].ConvertToString())
                                {
                                    items.Append("<tr style=\"font-size:18px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;font-weight:bold;padding-bottom:5px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle;\">").Append(dr["SubTestName"].ConvertToString()).Append("</td></tr>");

                                }


                                previousLabel = dr["PrintTestName"].ConvertToString();


                                if (dr["IsBoldFlag"].ConvertToString() == "B")
                                    items.Append("<tr style=\"line-height: 20px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td style=\"vertical-align: top;padding-bottom:5px;height: 20px;text-align:left;font-size:18px;font-weight:bold;\">").Append(dr["PrintParameterName"].ConvertToString()).Append("</td>");
                                else
                                    items.Append("<tr  style=\"line-height: 20px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td style=\"vertical-align: top;padding: 0;height: 20px;text-align:left;font-size:18px;padding-right:10px;\">").Append(dr["PrintParameterName"].ConvertToString()).Append("</td>");



                                if (dr["NormalRange"].ConvertToString() != " -   ")
                                {
                                    if (dr["ParaBoldFlag"].ConvertToString() == "B")
                                        items.Append("<td style=\"vertical-align: top;padding-bottom: 5px;height: 15px;text-align:left;font-size:18px;font-weight:bold;padding-left:260px\">").Append((dr["ResultValue"].ConvertToString())).Append("</td>");
                                    else
                                        items.Append("<td style=\"vertical-align: top;padding-bottom:5px;height: 15px;text-align:left;font-size:18px;padding-left:260px\">").Append(dr["ResultValue"].ConvertToString()).Append("</td>");
                                    items.Append("<td style=\"vertical-align: top;padding-bottom: 5px;height: 15px;text-align:left;font-size:18px;\">").Append(dr["NormalRange"].ConvertToString()).Append("</td></tr>");
                                }
                                else if (dr["NormalRange"].ConvertToString() == " -   ")
                                {
                                    if (dr["ParaBoldFlag"].ConvertToString() == "B")
                                        items.Append("<td colspan=\"2\"   style=\"vertical-align: top;padding-bottom: 5px;height: 15px;text-align:left;font-size:18px;font-weight:bold;padding-left:260px\">").Append((dr["ResultValue"].ConvertToString())).Append("</td></tr>");
                                    else
                                        items.Append("<td colspan=\"2\" style=\"vertical-align: top;padding-bottom:5px;height: 15px;text-align:left;font-size:18px;padding-left:260px\">").Append(dr["ResultValue"].ConvertToString()).Append("</td></tr>");
                                }


                                previoussubLabel = dr["SubTestName"].ConvertToString();


                            }

                            if (i == dt.Rows.Count)
                            {
                                if (dt.Rows.Count == 1)
                                {
                                    Suggestion = dt.Rows[0]["SuggestionNote"].ConvertToString();
                                    if (Suggestion != "")
                                    {
                                        items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle;font-weight:bold;\">").Append("Interpretation Remark :").Append("</td></tr>");
                                        items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Suggestion).Append("</td></tr>");
                                    }
                                }
                                else
                                {
                                    Suggestion = dt.Rows[i - 2]["SuggestionNote"].ConvertToString();
                                    if (Suggestion != "")
                                    {
                                        items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle;font-weight:bold;\">").Append("Interpretation Remark :").Append("</td></tr>");
                                        items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Suggestion).Append("</td></tr>");
                                    }

                                }
                            }

                            previousLabel = dr["PrintTestName"].ConvertToString();


                        }


                        html = html.Replace("{{Items}}", items.ToString());



                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo").ConvertToString());


                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName").ConvertToString());
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear").ConvertToString());
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName").ConvertToString());

                        html = html.Replace("{{ConsultantDocName}}", dt.GetColValue("ConsultantDocName").ConvertToString());
                        html = html.Replace("{{PathTime}}", dt.GetColValue("PathTime").ConvertToDateString("dd/MM/yyyy"));
                        html = html.Replace("{{ReportTime}}", dt.GetColValue("ReportTime").ConvertToDateString("dd/MM/yyyy"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName").ConvertToString());
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName").ConvertToString());
                        html = html.Replace("{{PathResultDr1}}", dt.GetColValue("PathResultDr1").ConvertToString());
                        html = html.Replace("{{Adm_Visit_Time}}", dt.GetColValue("Adm_Visit_Time").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        //html = html.Replace("{{PathTemplateDetailsResult}}", dt.GetColValue("PathTemplateDetailsResult").ConvertToString());
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName").ConvertToString());


                        //html = html.Replace("{{AdmissionTime}}", dt.Rows[0]["AdmissionTime"].ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        //html = html.Replace("{{PaymentTime}}", dt.Rows[0]["PaymentTime"].ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        html = html.Replace("{{IPDNo}}", dt.GetColValue("OP_IP_Number").ConvertToString());

                        //html = html.Replace("{{AdvanceAmount}}", dt.Rows[0]["AdvanceAmount"].ConvertToDouble().To2DecimalPlace());
                        //html = html.Replace("{{Phone}}", dt.Rows[0]["Phone"].ConvertToString());


                        //html = html.Replace("{{AgeMonth}}", dt.Rows[0]["AgeMonth"].ConvertToString());
                        //html = html.Replace("{{AgeDay}}", dt.Rows[0]["AgeDay"].ConvertToString());
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("ConsultantDocName").ConvertToString());
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName").ConvertToString());
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName").ConvertToString());
                        //html = html.Replace("{{DepartmentName}}", dt.Rows[0]["DepartmentName"].ConvertToString());
                        //html = html.Replace("{{PatientType}}", dt.Rows[0]["PatientType"].ConvertToString());
                        //html = html.Replace("{{RefDocName}}", dt.Rows[0]["RefDocName"].ConvertToString());
                        //html = html.Replace("{{CompanyName}}", dt.Rows[0]["CompanyName"].ConvertToString());
                        //html = html.Replace("{{Path_DoctorName}}", dt.Rows[0]["Path_DoctorName"].ConvertToString());
                        html = html.Replace("{{Education}}", dt.GetColValue("Education").ConvertToString());
                        html = html.Replace("{{MahRegNo}}", dt.GetColValue("MahRegNo").ConvertToString());
                        html = html.Replace("{{SuggestionNote}}", dt.GetColValue("SuggestionNote"));

                        html = html.Replace("{{PathResultDr1}}", dt.GetColValue("PathResultDr1").ConvertToString());
                        html = html.Replace("{{chkSuggestionNote}}", dt.GetColValue("SuggestionNote").ConvertToString() != "" ? "table-row" : "none");
                        html = html.Replace("{{chkSignature}}", dt.GetColValue("Signature").ConvertToString() != "" ? "table-row" : "none");



                        return html;
                    }
                    break;
                case "PathologyReportWithOutHeader":
                    {

                        Boolean chkresonflag = false;
                        string chkflag = "";
                        int Suggflag = 0;
                        int i = 0, j = 0, k = 0, testlength = 0, m;
                        String Label = "", Suggchk = "", Suggestion = "";
                        string previousLabel = "", previoussubLabel = "";

                        string signatureFileName = dt.Rows[0]["Signature"].ConvertToString();

                        var signature = string.IsNullOrWhiteSpace(signatureFileName) ? "" : _pdfUtility.GetBase64FromFolder("Doctors\\Signature", dt.Rows[0]["Signature"].ConvertToString());

                        html = html.Replace("{{Signature}}", signature);
                        html = html.Replace("{{chkSignature}}", !string.IsNullOrWhiteSpace(signatureFileName) ? "inline-block" : "none");


                        foreach (DataRow dr in dt.Rows)
                        {
                                                     

                            i++;

                            if (i == 1)
                                items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;font-weight:bold;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:center;padding-left:30px;vertical-align:left\">").Append(dr["PrintTestName"].ConvertToString()).Append("</td></tr>");


                            if (previousLabel != "" && previousLabel != dr["PrintTestName"].ConvertToString())
                            {
                                if (i > 3)
                                    Suggestion = dt.Rows[i - 2]["SuggestionNote"].ConvertToString();
                                else if (i < 2)
                                    Suggestion = dt.Rows[i - 1]["SuggestionNote"].ConvertToString();
                                if (Suggestion != "")
                                {
                                    items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle;font-weight:bold;\">").Append("Interpretation Remark :").Append("</td></tr>");
                                    items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Suggestion).Append("</td></tr>");
                                }

                                items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;font-weight:bold;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:center;padding-left:30px;vertical-align:middle\">").Append(dr["PrintTestName"].ConvertToString()).Append("</td></tr>");


                            }
                            if (dr["ResultValue"].ConvertToString() != " ")
                            {

                                k++;

                                if (k == 1 && dr["SubTestName"].ConvertToString() != "")
                                {


                                    previoussubLabel = dr["SubTestName"].ConvertToString();

                                    items.Append("<tr style=\"font-size:18px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;font-weight:bold;padding-bottom:5px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle;\">").Append(dr["SubTestName"].ConvertToString()).Append("</td></tr>");

                                }
                                if (previoussubLabel != "" && previoussubLabel != dr["SubTestName"].ConvertToString())
                                {
                                    items.Append("<tr style=\"font-size:18px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;font-weight:bold;padding-bottom:5px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle;\">").Append(dr["SubTestName"].ConvertToString()).Append("</td></tr>");

                                }


                                previousLabel = dr["PrintTestName"].ConvertToString();


                                if (dr["IsBoldFlag"].ConvertToString() == "B")
                                    items.Append("<tr style=\"line-height: 20px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td style=\"vertical-align: top;padding-bottom:5px;height: 20px;text-align:left;font-size:18px;font-weight:bold;\">").Append(dr["PrintParameterName"].ConvertToString()).Append("</td>");
                                else
                                    items.Append("<tr  style=\"line-height: 20px;font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td style=\"vertical-align: top;padding: 0;height: 20px;text-align:left;font-size:18px;padding-right:10px;\">").Append(dr["PrintParameterName"].ConvertToString()).Append("</td>");



                                if (dr["NormalRange"].ConvertToString() != " -   ")
                                {
                                    if (dr["ParaBoldFlag"].ConvertToString() == "B")
                                        items.Append("<td style=\"vertical-align: top;padding-bottom: 5px;height: 15px;text-align:left;font-size:18px;font-weight:bold;padding-left:260px\">").Append((dr["ResultValue"].ConvertToString())).Append("</td>");
                                    else
                                        items.Append("<td style=\"vertical-align: top;padding-bottom:5px;height: 15px;text-align:left;font-size:18px;padding-left:260px\">").Append(dr["ResultValue"].ConvertToString()).Append("</td>");
                                    items.Append("<td style=\"vertical-align: top;padding-bottom: 5px;height: 15px;text-align:left;font-size:18px;\">").Append(dr["NormalRange"].ConvertToString()).Append("</td></tr>");
                                }
                                else if (dr["NormalRange"].ConvertToString() == " -   ")
                                {
                                    if (dr["ParaBoldFlag"].ConvertToString() == "B")
                                        items.Append("<td colspan=\"2\"   style=\"vertical-align: top;padding-bottom: 5px;height: 15px;text-align:left;font-size:18px;font-weight:bold;padding-left:260px\">").Append((dr["ResultValue"].ConvertToString())).Append("</td></tr>");
                                    else
                                        items.Append("<td colspan=\"2\" style=\"vertical-align: top;padding-bottom:5px;height: 15px;text-align:left;font-size:18px;padding-left:260px\">").Append(dr["ResultValue"].ConvertToString()).Append("</td></tr>");
                                }


                                previoussubLabel = dr["SubTestName"].ConvertToString();


                            }

                            if (i == dt.Rows.Count)
                            {
                                if (dt.Rows.Count == 1)
                                {
                                    Suggestion = dt.Rows[0]["SuggestionNote"].ConvertToString();
                                    if (Suggestion != "")
                                    {
                                        items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle;font-weight:bold;\">").Append("Interpretation Remark :").Append("</td></tr>");
                                        items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Suggestion).Append("</td></tr>");
                                    }
                                }
                                else
                                {
                                    Suggestion = dt.Rows[i - 2]["SuggestionNote"].ConvertToString();
                                    if (Suggestion != "")
                                    {
                                        items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle;font-weight:bold;\">").Append("Interpretation Remark :").Append("</td></tr>");
                                        items.Append("<tr style=\"font-size:20px;border: 1px; font-family: Calibri,'Helvetica Neue', 'Helvetica',, Arial, sans-serif;margin-bottom:10px;\"><td colspan=\"13\" style=\"padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Suggestion).Append("</td></tr>");
                                    }

                                }
                            }

                            previousLabel = dr["PrintTestName"].ConvertToString();


                        }


                        html = html.Replace("{{Items}}", items.ToString());



                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo").ConvertToString());


                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName").ConvertToString());
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear").ConvertToString());
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName").ConvertToString());

                        html = html.Replace("{{ConsultantDocName}}", dt.GetColValue("ConsultantDocName").ConvertToString());
                        html = html.Replace("{{PathTime}}", dt.GetColValue("PathTime").ConvertToDateString("dd/MM/yyyy"));
                        html = html.Replace("{{ReportTime}}", dt.GetColValue("ReportTime").ConvertToDateString("dd/MM/yyyy"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName").ConvertToString());
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName").ConvertToString());
                        html = html.Replace("{{PathResultDr1}}", dt.GetColValue("PathResultDr1").ConvertToString());
                        html = html.Replace("{{Adm_Visit_Time}}", dt.GetColValue("Adm_Visit_Time").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        //html = html.Replace("{{PathTemplateDetailsResult}}", dt.GetColValue("PathTemplateDetailsResult").ConvertToString());
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName").ConvertToString());


                        //html = html.Replace("{{AdmissionTime}}", dt.Rows[0]["AdmissionTime"].ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        //html = html.Replace("{{PaymentTime}}", dt.Rows[0]["PaymentTime"].ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        html = html.Replace("{{IPDNo}}", dt.GetColValue("OP_IP_Number").ConvertToString());

                        //html = html.Replace("{{AdvanceAmount}}", dt.Rows[0]["AdvanceAmount"].ConvertToDouble().To2DecimalPlace());
                        //html = html.Replace("{{Phone}}", dt.Rows[0]["Phone"].ConvertToString());


                        //html = html.Replace("{{AgeMonth}}", dt.Rows[0]["AgeMonth"].ConvertToString());
                        //html = html.Replace("{{AgeDay}}", dt.Rows[0]["AgeDay"].ConvertToString());
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("ConsultantDocName").ConvertToString());
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName").ConvertToString());
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName").ConvertToString());
                        //html = html.Replace("{{DepartmentName}}", dt.Rows[0]["DepartmentName"].ConvertToString());
                        //html = html.Replace("{{PatientType}}", dt.Rows[0]["PatientType"].ConvertToString());
                        //html = html.Replace("{{RefDocName}}", dt.Rows[0]["RefDocName"].ConvertToString());
                        //html = html.Replace("{{CompanyName}}", dt.Rows[0]["CompanyName"].ConvertToString());
                        //html = html.Replace("{{Path_DoctorName}}", dt.Rows[0]["Path_DoctorName"].ConvertToString());
                        html = html.Replace("{{Education}}", dt.GetColValue("Education").ConvertToString());
                        html = html.Replace("{{MahRegNo}}", dt.GetColValue("MahRegNo").ConvertToString());
                        html = html.Replace("{{SuggestionNote}}", dt.GetColValue("SuggestionNote"));

                        html = html.Replace("{{PathResultDr1}}", dt.GetColValue("PathResultDr1").ConvertToString());
                        html = html.Replace("{{chkSuggestionNote}}", dt.GetColValue("SuggestionNote").ConvertToString() != "" ? "table-row" : "none");

                        return html;

                    }
                    break;



                case "PathologyReportTemplateWithOutHeader":
                    {

                        int i = 0;
                        Boolean chkresonflag = false;

                        string signatureFileName = dt.Rows[0]["Signature"].ConvertToString();

                        var signature = string.IsNullOrWhiteSpace(signatureFileName) ? "" : _pdfUtility.GetBase64FromFolder("Doctors\\Signature", dt.Rows[0]["Signature"].ConvertToString());

                        html = html.Replace("{{Signature}}", signature);
                        html = html.Replace("{{chkSignature}}", !string.IsNullOrWhiteSpace(signatureFileName) ? "inline-block" : "none");

                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy|hh:mmtt"));
                        html = html.Replace("{{PathTime}}", dt.GetColValue("PathTime").ConvertToDateString("dd/MM/yyyy"));

                        html = html.Replace("{{IPDNo}}", dt.GetColValue("OP_IP_Number"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("ConsultantDocName"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("bedName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{ReportTime}}", dt.GetColValue("ReportTime").ConvertToDateString("dd/MM/yyyy"));

                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));

                        html = html.Replace("{{ConsultantDocName}}", dt.GetColValue("ConsultantDocName"));
                        html = html.Replace("{{PathTime}}", dt.GetColValue("PathTime").ConvertToDateString());
                        html = html.Replace("{{ReportTime}}", dt.GetColValue("ReportTime").ConvertToDateString());
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));

                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName").ConvertToString());
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName").ConvertToString());
                        html = html.Replace("{{ComapanyName}}", dt.GetColValue("ComapanyName"));

                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{Path_RefDoctorName}}", dt.GetColValue("Path_RefDoctorName"));
                        html = html.Replace("{{PathTemplateDetailsResult}}", dt.GetColValue("PathTemplateDetailsResult").ConvertToString());
                        //string s = dt.GetColValue("PathTemplateDetailsResult").ConvertToString();
                        //html = html.Replace("{{PathTemplateDetailsResult}}", s);

                        html = html.Replace("{{PrintTestName}}", dt.GetColValue("PrintTestName"));
                        html = html.Replace("{{Path_DoctorName}}", dt.GetColValue("Path_DoctorName"));
                        html = html.Replace("{{Education}}", dt.GetColValue("Education"));
                        html = html.Replace("{{MahRegNo}}", dt.GetColValue("MahRegNo"));
                        html = html.Replace("{{SampleCollection}}", dt.GetColValue("SampleCollection").ConvertToDateString("dd/MM/yyyy|hh:mmtt"));

                        html = html.Replace("{{PathResultDr1}}", dt.GetColValue("PathResultDr1"));
                        //html = html.Replace("{{chkresonflag}}", dt.GetColValue("reason").ConvertToString() != null ? "block" : "none");
                        return html;
                    }

                    break;
                case "PathologyReportTemplateWithHeader":
                    {

                        int i = 0;
                        Boolean chkresonflag = false;
                        string signatureFileName = dt.Rows[0]["Signature"].ConvertToString();

                        var signature = string.IsNullOrWhiteSpace(signatureFileName) ? "" : _pdfUtility.GetBase64FromFolder("Doctors\\Signature", dt.Rows[0]["Signature"].ConvertToString());

                        html = html.Replace("{{Signature}}", signature);
                        html = html.Replace("{{chkSignature}}", !string.IsNullOrWhiteSpace(signatureFileName) ? "inline-block" : "none");


                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy|hh:mmtt"));
                        html = html.Replace("{{PathTime}}", dt.GetColValue("PathTime").ConvertToDateString("dd/MM/yyyy"));

                        html = html.Replace("{{IPDNo}}", dt.GetColValue("OP_IP_Number"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("ConsultantDocName"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("bedName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{ReportTime}}", dt.GetColValue("ReportTime").ConvertToDateString("dd/MM/yyyy"));

                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));

                        html = html.Replace("{{ConsultantDocName}}", dt.GetColValue("ConsultantDocName"));
                        html = html.Replace("{{PathTime}}", dt.GetColValue("PathTime").ConvertToDateString());
                        html = html.Replace("{{ReportTime}}", dt.GetColValue("ReportTime").ConvertToDateString());
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));

                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName").ConvertToString());
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName").ConvertToString());
                        html = html.Replace("{{ComapanyName}}", dt.GetColValue("ComapanyName"));

                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{Path_RefDoctorName}}", dt.GetColValue("Path_RefDoctorName"));
                        html = html.Replace("{{PathTemplateDetailsResult}}", dt.GetColValue("PathTemplateDetailsResult").ConvertToString());
                        string s = dt.GetColValue("PathTemplateDetailsResult").ConvertToString();
                        html = html.Replace("{{PathTemplateDetailsResult}}", s);

                        html = html.Replace("{{PrintTestName}}", dt.GetColValue("PrintTestName"));
                        html = html.Replace("{{Path_DoctorName}}", dt.GetColValue("Path_DoctorName"));
                        html = html.Replace("{{Education}}", dt.GetColValue("Education"));
                        html = html.Replace("{{MahRegNo}}", dt.GetColValue("MahRegNo"));
                        html = html.Replace("{{SampleCollection}}", dt.GetColValue("SampleCollection").ConvertToDateString("dd/MM/yyyy|hh:mmtt"));

                        html = html.Replace("{{PathResultDr1}}", dt.GetColValue("PathResultDr1"));
                        //html = html.Replace("{{chkresonflag}}", dt.GetColValue("reason").ConvertToString() != null ? "block" : "none");
                        return html;

                    }

                    break;


                case "RadiologyTemplateReport":
                    {


                        
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("OPDNo"));

                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName").ConvertToString());
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));

                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));

                        html = html.Replace("{{ConsultantName}}", dt.GetColValue("ConsultantName"));
                        //  html = html.Replace("{{ReportTime}}", Bills.GetColValue("ReportTime").ConvertToDateString());
                        html = html.Replace("{{ReportTime}}", dt.GetColValue("ReportTime").ConvertToDateString("dd/MM/yy | hh:mm tt"));
                        html = html.Replace("{{RadTime}}", dt.GetColValue("RadTime").ConvertToDateString("dd/MM/yy | hh:mm tt"));

                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{RadiologyDocName}}", dt.GetColValue("RadiologyDocName"));
                        html = html.Replace("{{ResultEntry}}", dt.GetColValue("ResultEntry").ConvertToString());
                        html = html.Replace("{{PrintTestName}}", dt.GetColValue("PrintTestName"));
                        html = html.Replace("{{SuggestionNotes}}", dt.GetColValue("SuggestionNotes"));


                        html = html.Replace("{{PathResultDr1}}", dt.GetColValue("PathResultDr1"));
                        html = html.Replace("{{MahRegNo}}", dt.GetColValue("MahRegNo"));
                        html = html.Replace("{{Education}}", dt.GetColValue("Education"));


                        html = html.Replace("{{RadiologyDocName}}", dt.GetColValue("RadiologyDocName"));

                    }

                    break;
                case "NurMaterialConsumption":
                    {

                        int i = 0;
                        double T_TotalMrpAmount = 0, T_TotalLandAmount = 0, T_TotalNETAmount = 0, T_TotalPaidAmount = 0, T_TotalBalAmount = 0, T_TotalCashAmount = 0, T_TotalCardAmount = 0, T_TotalChequeAmount = 0, T_TotalNEFTAmount = 0, T_TotalOnlineAmount = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: center;padding: 3px;height:10px;text-align:center;font-size:20px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle;font-size:20px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;vertical-align:middle;text-align: center;font-size:20px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;text-align:center;vertical-align:middle;padding:3px;height:10px;font-size:20px;\">").Append(dr["PerUnitMRPRate"].ConvertToDouble()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;font-size:20px;\">").Append(dr["MRPTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;font-size:20px;\">").Append(dr["PerUnitLandedRate"].ConvertToDouble()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;font-size:20px;\">").Append(dr["LandedTotalAmount"].ConvertToString()).Append("</td></tr>");



                            T_TotalMrpAmount += dr["MRPTotalAmount"].ConvertToDouble();
                            T_TotalLandAmount += dr["LandedTotalAmount"].ConvertToDouble();
                        }


                        html = html.Replace("{{Items}}", items.ToString());

                        html = html.Replace("{{MaterialConsumptionId}}", dt.GetColValue("MaterialConsumptionId"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));

                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));


                        html = html.Replace("{{ConsumptionTime}}", dt.GetColValue("ConsumptionTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
                        html = html.Replace("{{Remark}}", dt.GetColValue("Remark"));
                        html = html.Replace("{{StoreName}}", dt.GetColValue("StoreName"));
                        html = html.Replace("{{Remark}}", dt.GetColValue("Remark"));

                        html = html.Replace("{{AddedBy}}", dt.GetColValue("AddedBy"));

                        html = html.Replace("{{T_TotalMrpAmount}}", T_TotalMrpAmount.To2DecimalPlace());
                        html = html.Replace("{{T_TotalLandAmount}}", T_TotalLandAmount.To2DecimalPlace());

                    }

                    break;

                case "NurLabRequestTest":
                    {

                        int i = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr style\"font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;font-size:20px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\" border: 1px solid #d4c3c3; text-align: left; padding: 6px;font-size:20px;\">").Append(dr["ServiceName"].ConvertToString()).Append("</td></tr>");

                        }

                        string htmlHeader = "";
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

                        html = html.Replace("{{Items}}", items.ToString());
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{Age}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AdmittedDocName}}", dt.GetColValue("AdmittedDocName"));


                        html = html.Replace("{{RequestId}}", dt.GetColValue("RequestId"));
                        html = html.Replace("{{OPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{ReqDate}}", dt.GetColValue("ReqDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));


                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));

                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{OP_IP_Type}}", dt.GetColValue("OP_IP_Type"));
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));


                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));


                    }

                    break;
                case "CanteenRequestprint":
                    {

                        int i = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;
                            items.Append("<tr style\"font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;\"><td style=\" border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"padding: 6px;height:10px;vertical-align:middle;text-align:left;font-size:20px;padding-left:10px;border: 1px solid #d4c3c3; \">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"padding: 6px;height:10px;vertical-align:middle;text-align:left;font-size:20px;padding-left:10px;border: 1px solid #d4c3c3; \">").Append(dr["Qty"].ConvertToString()).Append("</td>");



                        }

                        string htmlHeader = "";
                        html = html.Replace("{{RegId}}", dt.GetColValue("RegId"));
                        html = html.Replace("{{ReqDate}}", dt.GetColValue("ReqDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{ReqNo}}", dt.GetColValue("ReqNo"));
                        html = html.Replace("{{OP_IP_ID}}", dt.GetColValue("OP_IP_ID"));
                        html = html.Replace("{{ItemName}}", dt.GetColValue("ItemName"));
                        html = html.Replace("{{Qty}}", dt.GetColValue("Qty"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));
                        html = html.Replace("{{AddUserName}}", dt.GetColValue("AddUserName"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{AddedBy}}", dt.GetColValue("AddUserName"));

                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{Qty}}", dt.GetColValue("Qty"));

                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));

                        html = html.Replace("{{OPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AdmissionDate}}", dt.GetColValue("AdmissionDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AddUserName}}", dt.GetColValue("AddUserName"));


                    }

                    break;


                case "NurIPprescriptionReport":
                    {
                        int i = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr  style=\"font-family: 'Helvetica Neue','Helvetica',, Arial, sans-serif;font-size:15px;\"><td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;text-align:center;vertical-align:middle;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;text-align:center;vertical-align:middle;\">").Append(dr["DrugName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;vertical-align:middle;text-align: center;\">").Append(dr["DoseName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["TotalQty"].ConvertToString()).Append("</td></tr>");

                        }

                        string htmlHeader = "";
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));


                        html = html.Replace("{{Items}}", items.ToString());
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{ConsultantDocName}}", dt.GetColValue("ConsultantDocName"));
                        html = html.Replace("{{IPMedID}}", dt.GetColValue("IPMedID"));
                        html = html.Replace("{{OPDNo}}", dt.GetColValue("OPDNo"));
                        html = html.Replace("{{PDate}}", dt.GetColValue("PTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));


                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{WardName}}", dt.GetColValue("WardName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{PreparedBy}}", dt.GetColValue("PreparedBy"));
                        html = html.Replace("{{Address}}", dt.GetColValue("Address"));
                        html = html.Replace("{{BP}}", dt.GetColValue("BP"));
                        html = html.Replace("{{Pulse}}", dt.GetColValue("Pulse"));
                        html = html.Replace("{{PWeight}}", dt.GetColValue("PWeight"));
                        html = html.Replace("{{Diagnosis}}", dt.GetColValue("Diagnosis"));


                    }

                    break;
                //end lopp

                case "NurIPprescriptionReturnReport":
                    {
                        int i = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr><td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;text-align:center;vertical-align:middle;font-size:18px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;text-align:center;vertical-align:middle;font-size:18px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:3px;height:10px;border-bottom:1px solid #000;vertical-align:middle;text-align: center;font-size:18px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;border-bottom:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;font-size:18px;\">").Append(dr["Qty"].ConvertToString()).Append("</td></tr>");

                        }

                        html = html.Replace("{{Items}}", items.ToString());
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{Age}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{ConsultantDocName}}", dt.GetColValue("ConsultantDocName"));

                        html = html.Replace("{{PresReId}}", dt.GetColValue("PresReId"));
                        html = html.Replace("{{PresTime}}", dt.GetColValue("PresTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
                        html = html.Replace("{{Date}}", dt.GetColValue("Date").ConvertToDateString("dd/MM/yyyy hh:mm tt"));


                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
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
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString());
                        html = html.Replace("{{DischargeDate}}", dt.GetColValue("DischargeTime").ConvertToDateString());

                        html = html.Replace("{{PreparedBy}}", dt.GetColValue("PreparedBy"));
                        html = html.Replace("{{Address}}", dt.GetColValue("Address"));



                    }

                    break;


                case "IndentWiseReport":
                    {
                        int i = 0;
                        String Verify = " ";


                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;border-bottom: 1px solid black;text-align:center;font-size:18px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;border-bottom: 1px solid black;font-size:18px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0;border-right:1px solid #000;height:10px;text-align:center;border-bottom: 1px solid black;font-size:18px;\">").Append(dr["Qty"].ConvertToString()).Append("</td></tr>");

                        }



                        html = html.Replace("{{Items}}", items.ToString());
                        html = html.Replace("{{FromStoreName}}", dt.GetColValue("FromStoreName"));

                        html = html.Replace("{{VLabel}}", dt.GetColValue("VLabel"));
                        html = html.Replace("{{Remark}}", dt.GetColValue("Comments"));
                        html = html.Replace("{{ToStoreName}}", dt.GetColValue("ToStoreName"));
                        html = html.Replace("{{IndentNo}}", dt.GetColValue("IndentNo"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));
                        html = html.Replace("{{IndentTime}}", dt.GetColValue("IndentTime").ConvertToDateString("dd.MM.yyyy hhLmm tt"));
                        html = html.Replace("{{Isverify}}", dt.GetColValue("Isverify"));
                        html = html.Replace("{{Comments}}", dt.GetColValue("Comments"));

                        html = html.Replace("{{PrintStoreName}}", dt.GetColValue("PrintStoreName"));
                        html = html.Replace("{{StoreAddress}}", dt.GetColValue("StoreAddress"));
                        return html;

                    }

                    break;

                case "IndentverifyReport":
                    {
                        int i = 0;
                        String Verify = " ";


                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;border-bottom: 1px solid black;text-align:center;font-size:18px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;border-bottom: 1px solid black;font-size:18px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;vertical-align:middle;padding:0;border-right:1px solid #000;height:10px;text-align:center;border-bottom: 1px solid black;font-size:18px;\">").Append(dr["Qty"].ConvertToString()).Append("</td></tr>");

                        }



                        html = html.Replace("{{Items}}", items.ToString());
                        html = html.Replace("{{FromStoreName}}", dt.GetColValue("FromStoreName"));

                        html = html.Replace("{{VLabel}}", dt.GetColValue("VLabel"));
                        html = html.Replace("{{Remark}}", dt.GetColValue("Comments"));
                        html = html.Replace("{{ToStoreName}}", dt.GetColValue("ToStoreName"));
                        html = html.Replace("{{IndentNo}}", dt.GetColValue("IndentNo"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));
                        html = html.Replace("{{IndentTime}}", dt.GetColValue("IndentTime").ConvertToDateString("dd.MM.yyyy hhLmm tt"));
                        html = html.Replace("{{Isverify}}", dt.GetColValue("Isverify"));
                        html = html.Replace("{{Comments}}", dt.GetColValue("Comments"));

                        html = html.Replace("{{PrintStoreName}}", dt.GetColValue("PrintStoreName"));
                        html = html.Replace("{{StoreAddress}}", dt.GetColValue("StoreAddress"));


                    }
                    break;
                case "Issutodeptissuewise":
                    {
                        int i = 0;

                        double T_TotalNETAmount = 0, T_TotalVatAmount = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["UnitofMeasurementName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["UnitMRP"].ConvertToDouble().To2DecimalPlace()).Append("</td>");

                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["IssueQty"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/Mm/yyyy")).Append("</td>");

                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PerUnitLandedRate"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["VatPercentage"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["VatAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["LandedTotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                            T_TotalVatAmount += dr["VatAmount"].ConvertToDouble();
                            T_TotalNETAmount += dr["LandedTotalAmount"].ConvertToDouble();
                            //   exec RptPharmacyCreditReport '11-01-2022','11-26-2023',11052,24879,0,10016

                        }

                        T_TotalVatAmount = Math.Round(T_TotalVatAmount);
                        T_TotalNETAmount = Math.Round(T_TotalNETAmount);
                        html = html.Replace("{{Items}}", items.ToString());
                        //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
                        //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
                        html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
                        html = html.Replace("{{T_TotalVatAmount}}", T_TotalVatAmount.To2DecimalPlace());

                        html = html.Replace("{{Remark}}", dt.GetColValue("Remark"));
                        html = html.Replace("{{PrintStoreName}}", dt.GetColValue("PrintStoreName"));
                        html = html.Replace("{{StoreAddress}}", dt.GetColValue("StoreAddress"));

                        html = html.Replace("{{StoreName}}", dt.GetColValue("StoreName"));
                        html = html.Replace("{{AddedByName}}", dt.GetColValue("AddedByName").ToString());


                        html = html.Replace("{{IssueNo}}", dt.GetColValue("IssueNo"));

                        html = html.Replace("{{IssueTime}}", dt.GetColValue("IssueTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));

                        html = html.Replace("{{StoreName}}", dt.GetColValue("StoreName"));

                        html = html.Replace("{{ToStreName}}", dt.GetColValue("ToStreName"));
                        html = html.Replace("{{PrintStoreName}}", dt.GetColValue("PrintStoreName"));
                        html = html.Replace("{{StoreAddress}}", dt.GetColValue("StoreAddress"));

                        string finalamt = conversion(T_TotalNETAmount.ToString());
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());
                    }
                    break;
                case "Issutodeptsummarywise":
                    {
                        int i = 0;

                        double T_TotalLRateAmount = 0, T_TotalVatAmount = 0, T_TotalMRPAmount = 0, T_NetAmount = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["IssueNo"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["IssueTime"].ConvertToDateString("dd/MM/yyyy hh:mm tt")).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:left;\">").Append(dr["FromStoreName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:left;\">").Append(dr["ToStoreName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["TotalVatAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["UserName"].ConvertToString()).Append("</td></tr>");


                            T_TotalLRateAmount += dr["TotalAmount"].ConvertToDouble();
                            T_TotalVatAmount += dr["TotalVatAmount"].ConvertToDouble();

                            T_NetAmount += dr["NetAmount"].ConvertToDouble();
                            // T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();

                            //   exec RptPharmacyCreditReport '11-01-2022','11-26-2023',11052,24879,0,10016

                        }

                        html = html.Replace("{{Items}}", items.ToString());
                        html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
                        html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
                        html = html.Replace("{{T_TotalLRateAmount}}", T_TotalLRateAmount.To2DecimalPlace());
                        html = html.Replace("{{T_TotalVatAmount}}", T_TotalVatAmount.To2DecimalPlace());
                        html = html.Replace("{{T_NetAmount}}", T_NetAmount.To2DecimalPlace());

                        html = html.Replace("{{AddedByName}}", dt.GetColValue("AddedByName").ToString());

                        html = html.Replace("{{PrintStoreName}}", dt.GetColValue("PrintStoreName"));
                        html = html.Replace("{{StoreAddress}}", dt.GetColValue("StoreAddress"));
                    }
                    break;
                case "MaterialReceivedByDept":
                    {
                        int i = 0;

                        double T_TotalNETAmount = 0, T_TotalVatAmount = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["UnitofMeasurementName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["IssueQty"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/Mm/yyyy")).Append("</td>");

                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["PerUnitLandedRate"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["VatPercentage"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["VatAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                            T_TotalVatAmount += dr["VatAmount"].ConvertToDouble();
                            T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                            //   exec RptPharmacyCreditReport '11-01-2022','11-26-2023',11052,24879,0,10016

                        }
                        T_TotalNETAmount = Math.Round(T_TotalNETAmount);


                        html = html.Replace("{{Items}}", items.ToString());
                        //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
                        //html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));
                        html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace());
                        html = html.Replace("{{T_TotalVatAmount}}", T_TotalVatAmount.To2DecimalPlace());

                        html = html.Replace("{{Remark}}", dt.GetColValue("Remark"));
                        html = html.Replace("{{PrintStoreName}}", dt.GetColValue("PrintStoreName"));
                        html = html.Replace("{{StoreAddress}}", dt.GetColValue("StoreAddress"));

                        html = html.Replace("{{StoreName}}", dt.GetColValue("StoreName"));


                        html = html.Replace("{{IssueNo}}", dt.GetColValue("IssueNo"));

                        html = html.Replace("{{IssueTime}}", dt.GetColValue("IssueTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));

                        html = html.Replace("{{StoreName}}", dt.GetColValue("StoreName"));

                        html = html.Replace("{{ToStreName}}", dt.GetColValue("ToStreName"));


                        //string finalamt = NumberToWords(T_TotalNETAmount.ToInt());
                        //html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());

                        return html;
                    }
                    break;
                case "OpeningBalance":
                    {
                        int i = 0, j = 0;
                        double T_count = 0, T_PerUnitPurRate = 0, T_PerUnitMrp = 0, T_TotalQty = 0;


                        foreach (DataRow dr in dt.Rows)
                        {
                            i++; j++;

                            items.Append("<tr style=\"text-align: center;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\"><td style=\"text-align: center; border: 1px solid #d4c3c3; padding: 6px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"text-align: center;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["OpeningDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                            items.Append("<td style=\"text-align: center; font-size: 19px;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["OpeningHId"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"text-align: left; font-size: 19px;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"text-align: center; font-size: 19px;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"text-align: left;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                            items.Append("<td style=\"text-align: right;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PerUnitPurRate"].ConvertToDouble()).Append("</td>");
                            items.Append("<td style=\"text-align: right;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["PerUnitMrp"].ConvertToDouble()).Append("</td>");
                         //   items.Append("<td style=\"text-align: center; font-size: 19px;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["VatPer"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"text-align: center; font-size: 19px;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["TotalQty"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"text-align: center;font-size: 19px; border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["OpeningDocNo"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"text-align: left; font-size: 19px;border: 1px solid #d4c3c3; padding: 6px;\">").Append(dr["UserName"].ConvertToString()).Append("</td></tr>");


                            T_PerUnitPurRate += dr["PerUnitPurRate"].ConvertToDouble();
                            T_PerUnitMrp += dr["PerUnitMrp"].ConvertToDouble();
                            T_TotalQty += dr["TotalQty"].ConvertToDouble();

                        }


                        string htmlHeader = "";
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));



                        html = html.Replace("{{T_PerUnitPurRate}}", T_PerUnitPurRate.ToString());
                        html = html.Replace("{{T_PerUnitMrp}}", T_PerUnitMrp.ToString());
                        html = html.Replace("{{T_TotalQty}}", T_TotalQty.ToString());
                        html = html.Replace("{{Items}}", items.ToString());
                    }
                    break;


                case "WorkOrder":
                    {
                        int i = 0;
                        double T_TotalAmount = 0, T_TotalVatAmount = 0, T_TotalDiscAmount = 0, T_TotalNETAmount = 0, T_TotalBalancepay = 0, T_TotalCGST = 0, T_TotalSGST = 0, T_TotalIGST = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["Qty"].ConvertToString()).Append("</td>");

                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;;\">").Append(dr["Rate"].ConvertToDouble()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["DiscPer"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["VATPer"].ConvertToDouble().To2DecimalPlace()).Append("</td>");

                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");


                            T_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                            T_TotalVatAmount += dr["VATAmount"].ConvertToDouble();
                            T_TotalDiscAmount += dr["DiscAmount"].ConvertToDouble();
                            T_TotalNETAmount += dr["NetAmount"].ConvertToDouble();
                            //T_TotalBalancepay += dr["BalanceAmount"].ConvertToDouble();
                            //T_TotalCGST += dr["CGSTAmt"].ConvertToDouble();
                            //T_TotalSGST += dr["SGSTAmt"].ConvertToDouble();
                            //T_TotalIGST += dr["IGSTAmt"].ConvertToDouble();


                        }
                        //| currency:'INR':'symbol-narrow':'0.2'

                        T_TotalNETAmount = Math.Round(T_TotalNETAmount);
                        T_TotalVatAmount = Math.Round(T_TotalVatAmount);
                        T_TotalDiscAmount = Math.Round(T_TotalDiscAmount);
                        html = html.Replace("{{Items}}", items.ToString());
                        //html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
                        //html = html.Replace("{{Todate}}", ToDate.ToString("dd/MM/yy"));

                        html = html.Replace("{{TotalAmount}}", T_TotalAmount.To2DecimalPlace().ConvertToDouble().ToString("F2")); 
                        html = html.Replace("{{TotalVatAmount}}", T_TotalVatAmount.To2DecimalPlace().ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{TotalDiscAmount}}", T_TotalDiscAmount.To2DecimalPlace().ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{TotalNETAmount}}", T_TotalNETAmount.To2DecimalPlace().ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_TotalDiscAmount}}", T_TotalDiscAmount.To2DecimalPlace().ConvertToDouble().ToString("F2"));


                        html = html.Replace("{{AddedbyName}}", dt.GetColValue("AddedbyName"));
                        html = html.Replace("{{SupplierName}}", dt.GetColValue("SupplierName").ConvertToString());
                        html = html.Replace("{{WOId}}", dt.GetColValue("WOId").ConvertToString());
                        html = html.Replace("{{Address}}", dt.GetColValue("Address"));
                        html = html.Replace("{{Date}}", dt.GetColValue("Time").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
                        html = html.Replace("{{Mobile}}", dt.GetColValue("Mobile"));
                        html = html.Replace("{{Phone}}", dt.GetColValue("Phone"));
                        html = html.Replace("{{Remarks}}", dt.GetColValue("WORemark"));

                        string finalamt = conversion(T_TotalNETAmount.ToString());
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


                        html = html.Replace("{{chkdiscflag}}", dt.GetColValue("T_TotalDiscAmount").ConvertToDouble() > 0 ? "block" : "none");

                    }
                    break;


             


                case "SalesReturnBill":
                    {
                        int i = 0;

                        double TotalGST = 0, T_HTotalAmount = 0, T_TotalBalancepay = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr style=\"font-size:15px;\"><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["HSNcode"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["ConversionFactor"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BatchExpDate"].ConvertToDateString("dd/mm/yyyy")).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["Qty"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["UnitMRP"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");



                            T_HTotalAmount += dr["TotalAmount"].ConvertToDouble();
                        }

                        TotalGST = dt.GetColValue("CGSTAmt").ConvertToDouble() + dt.GetColValue("SGSTAmt").ConvertToDouble() + dt.GetColValue("IGSTAmt").ConvertToDouble();

                        html = html.Replace("{{Items}}", items.ToString());

                        html = html.Replace("{{TotalGST}}", TotalGST.To2DecimalPlace());
                        html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

                        html = html.Replace("{{StoreName}}", dt.GetColValue("StoreName"));
                        html = html.Replace("{{StoreAddress}}", dt.GetColValue("StoreAddress"));
                        html = html.Replace("{{HospitalMobileNo}}", dt.GetColValue("HospitalMobileNo"));
                        html = html.Replace("{{HospitalEmailId}}", dt.GetColValue("HospitalEmailId"));

                        html = html.Replace("{{OP_IP_Number}}", dt.GetColValue("OP_IP_Number"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));

                        html = html.Replace("{{ExtMobileNo}}", dt.GetColValue("ExtMobileNo"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));

                        html = html.Replace("{{HTotalAmount}}", T_HTotalAmount.To2DecimalPlace());
                        html = html.Replace("{{DiscAmount}}", dt.GetColValue("DiscAmount"));

                        html = html.Replace("{{NetAmount}}", dt.GetColValue("NetAmount"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));
                        html = html.Replace("{{GSTIN}}", dt.GetColValue("GSTIN"));
                        html = html.Replace("{{DL_NO}}", dt.GetColValue("DL_NO"));

                        html = html.Replace("{{SalesReturnNo}}", dt.GetColValue("SalesReturnNo"));
                        html = html.Replace("{{Date}}", dt.GetColValue("Date").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
                        html = html.Replace("{{PayMode}}", dt.GetColValue("PayMode"));
                        html = html.Replace("{{DL_NO}}", dt.GetColValue("DL_NO"));



                        return html;

                    }
                    break;


                case "ExpenseVoucharPrint":
                    {

                        int length = 0;
                        length = dt.Rows.Count;
                   
                        StringBuilder item = new StringBuilder("");
                        int i = 0;


                        html = html.Replace("{{ExpID}}", dt.GetColValue("ExpID"));
                        html = html.Replace("{{HeadName}}", dt.GetColValue("HeadName"));
                  
                        html = html.Replace("{{ExpType}}", dt.GetColValue("ExpType"));
                        html = html.Replace("{{ExpAmount}}", dt.GetColValue("ExpAmount"));

                        html = html.Replace("{{PersonName}}", dt.GetColValue("PersonName"));
                        html = html.Replace("{{Narration}}", dt.GetColValue("Narration"));
                        html = html.Replace("{{VoucharNo}}", dt.GetColValue("VoucharNo"));
                        html = html.Replace("{{AddedName}}", dt.GetColValue("AddedName"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));


                        html = html.Replace("{{ExpDate}}", dt.GetColValue("ExpTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));






                        html = html.Replace("{{Items}}", items.ToString());


                        return html;
                    }
                    break;



                case "OTRequest":
                    {
                        string html1 = File.ReadAllText(htmlFilePath);
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);
                        StringBuilder item = new StringBuilder("");
                        int i = 0;


                        //html = html.Replace("{{MaterialConsumptionId}}", Bills.GetColValue("MaterialConsumptionId"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));

                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{AdmissionID}}", dt.GetColValue("AdmissionID"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{SurgeonName}}", dt.GetColValue("SurgeonName"));

                        html = html.Replace("{{OTdepartment}}", dt.GetColValue("OTdepartment"));
                        html = html.Replace("{{Surgeryname}}", dt.GetColValue("Surgeryname"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));

                        html = html.Replace("{{SurgeryType}}", dt.GetColValue("SurgeryType"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{OPDIPDNO}}", dt.GetColValue("OPDIPDNO"));
                        html = html.Replace("{{OTdepartment}}", dt.GetColValue("OTdepartment"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{SurgeryCategoryName}}", dt.GetColValue("SurgeryCategoryName"));
                        html = html.Replace("{{SiteDescriptionName}}", dt.GetColValue("SiteDescriptionName"));

                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
                        html = html.Replace("{{OTRequestTime}}", dt.GetColValue("OTRequestTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));


                        html = html.Replace("{{AddedBy}}", dt.GetColValue("AddedBy"));


                        //html = html.Replace("{{chkInstructionflag}}", Bills.GetColValue("Instruction").ConvertToString() != "" ? "table-row" : "none");

                        //html = html.Replace("{{chkDigflag}}", Bills.GetColValue("OperativeDiagnosis").ConvertToString() != "" ? "block" : "table-row");



                        //html = html.Replace("{{chkOPFindingflag}}", Bills.GetColValue("OperativeFindings").ConvertToString() != "" ? "table-row" : "none");

                        //html = html.Replace("{{chkClfindingflag}}", Bills.GetColValue("ClinicalFinding").ConvertToString() != "" ? "table-row" : "none");

                        //html = html.Replace("{{chkOprativeflag}}", Bills.GetColValue("OperativeProcedure").ConvertToString() != "" ? "table-row" : "none");

                        //html = html.Replace("{{chkExtraflag}}", Bills.GetColValue("ExtraProPerformed").ConvertToString() != "" ? "table-row" : "none");


                        //html = html.Replace("{{chkClosureflag}}", Bills.GetColValue("ClosureTechnique").ConvertToString() != "" ? "table-row" : "none");



                        //html = html.Replace("{{chkpostflag}}", Bills.GetColValue("PostOpertiveInstru").ConvertToString() != "" ? "table-row" : "none");

                        //html = html.Replace("{{chkSpeciflag}}", Bills.GetColValue("DetSpecimenForLab").ConvertToString() != " " ? "table-row" : "none");


                        //html = html.Replace("{{chkSurgeryTypeflag}}", Bills.GetColValue("SurgeryType").ConvertToString() != "" ? "table-row" : "none");
                        //html = html.Replace("{{chkSurgeryProcDoneflag}}", Bills.GetColValue("SurgeryProcDone").ConvertToString() != "" ? "table-row" : "none");
                        //html = html.Replace("{{chkClinicalconditionflag}}", Bills.GetColValue("ClinicalConditionOnAdmisssion").ConvertToString() != "" ? "table-row" : "none");




                        return html;
                    }
                    break;


                case "OTReservationReport":
                    {
                        string html1 = File.ReadAllText(htmlFilePath);
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);
                        StringBuilder item = new StringBuilder("");
                        int i = 0;
                        //html = html.Replace("{{MaterialConsumptionId}}", Bills.GetColValue("MaterialConsumptionId"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));

                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{AdmissionID}}", dt.GetColValue("AdmissionID"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{SurgeonName}}", dt.GetColValue("SurgeonName"));

                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{Surgeryname}}", dt.GetColValue("Surgeryname"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{AnathesDrName1}}", dt.GetColValue("AnathesDrName1"));
                        html = html.Replace("{{AnathesDrName2}}", dt.GetColValue("AnathesDrName2"));
                        html = html.Replace("{{SurgeonName2}}", dt.GetColValue("SurgeonName2"));
                        html = html.Replace("{{SurgeonName1}}", dt.GetColValue("SurgeonName1"));
                        html = html.Replace("{{OTTableName}}", dt.GetColValue("OTTableName"));
                        html = html.Replace("{{AnaesthesiaType}}", dt.GetColValue("AnaesthesiaType"));
                        html = html.Replace("{{SurgeonName1}}", dt.GetColValue("SurgeonName1"));
                        html = html.Replace("{{SurgeonName1}}", dt.GetColValue("SurgeonName1"));

                        


                        html = html.Replace("{{SurgeryType}}", dt.GetColValue("SurgeryType"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{OPDIPDNO}}", dt.GetColValue("OPDIPDNO"));
                        html = html.Replace("{{OTdepartment}}", dt.GetColValue("OTdepartment"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{Instruction}}", dt.GetColValue("Instruction"));
                        html = html.Replace("{{SiteDescriptionName}}", dt.GetColValue("SiteDescriptionName"));

                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));
                        html = html.Replace("{{OTRequestTime}}", dt.GetColValue("OTRequestTime").ConvertToDateString("dd/MM/yyyy hh:mm tt"));


                        html = html.Replace("{{AddedBy}}", dt.GetColValue("AddedBy"));
                        html = html.Replace("{{chkInstructionflag}}", dt.GetColValue("Instruction").ConvertToString() != "" ? "table-row" : "none");


                        return html;
                    }
                    break;




             


                case "PharmacyPatientStatement":
                    {
                        var dynamicVariable = new Dictionary<string, double>();

                        string html1 = File.ReadAllText(htmlFilePath);
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                       
                        StringBuilder item = new StringBuilder("");
                        int i = 0, j = 0;
                        double salesTotal = 0, salesdisc = 0, salesnet = 0, salesbal = 0, salesrefund = 0, salesadv = 0, salesrefundTotal = 0, salesrefunddisc = 0, salesrefundnet = 0, salesrefundbal = 0, salesrefundrefund = 0, Total_Total = 0, Total_disc = 0, Total_net = 0, Total_bal = 0, Total_refund = 0, salespaid = 0, salesrefundpaid = 0;
                        double salescashTotal = 0, salescard = 0, salescheque = 0, salesneft = 0, salesonline = 0, salesreturncashTotal = 0, salesreturncard = 0, salesreturncheque = 0, salesreturnneft = 0, salesreturnonline = 0;
                        double G_NetPayableAmt = 0, G_TotalAmount = 0, G_discAmount = 0, G_balAmount = 0, G_RefundAmount = 0, G_PaidAmount = 0, G_cashAmount = 0, G_chequeAmount = 0, G_neftAmount = 0, G_cardAmount = 0, G_onlineAmount = 0, G_AdvAmount = 0;
                        double G_BalTotalamt = 0, T_Totalamt = 0, T_Discamt = 0, T_Netamt = 0, T_paidamt = 0, T_Balamt = 0, T_Refundamt = 0, T_Creditreturnamt = 0, T_Cashreturnamt = 0, T_Advamt = 0, T_Advrefundamt = 0, T_Advusedamt = 0, T_AdvBalamt = 0, T_Cashpayamt = 0, T_Cardpayamt = 0, T_Chequepayamt = 0, T_Onlinepayamt = 0, T_Neftpayamt = 0, Net_Billamount = 0, Total_PaidAmount = 0;
                        double Total_Cash = 0, Total_card = 0, Total_cheque = 0, Total_neft = 0, Total_online = 0;
                        string Lablecount = "";


                        string previousLabel = "";

                        foreach (DataRow dr in dt.Rows)
                        {

                            i++; j++;

                            if (i == 1)
                            {
                                String Label;
                                Label = dr["Label"].ConvertToString();
                                Lablecount = dr["Label"].ConvertToString();
                                items.Append("<tr style=\"font-size:20px;border: 1px;color:black;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-weight:bold;\"><td colspan=\"14\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle\">").Append(Label).Append("</td></tr>");
                            }
                            if (previousLabel != "" && previousLabel != dr["Label"].ConvertToString())
                            {
                                j = 1;
                                items.Append("<tr style=\"border:1px solid black;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size:22px;font-weight:bold;\"><td colspan='3' style=\"border-left:1px solid #ccc;border-right:1px solid #ccc;border-top:1px solid #ccc;padding:3px;height:15px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:15px;text-align:right;vertical-align:middle\">")
                                   .Append(Math.Round(G_TotalAmount.ConvertToDouble()).ToString("F2")).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                   .Append(Math.Round(G_discAmount.ConvertToDouble()).ToString("F2")).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                    .Append(Math.Round(G_NetPayableAmt.ConvertToDouble()).ToString("F2")).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                   .Append(Math.Round(G_PaidAmount.ConvertToDouble()).ToString("F2")).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                    .Append(Math.Round(G_balAmount.ConvertToDouble()).ToString("F2")).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                   .Append(Math.Round(G_RefundAmount.ConvertToDouble()).ToString("F2")).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                    .Append(Math.Round(G_cashAmount.ConvertToDouble()).ToString("F2")).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                    .Append(Math.Round(G_cardAmount.ConvertToDouble()).ToString("F2")).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                    .Append(Math.Round(G_chequeAmount.ConvertToDouble()).ToString("F2")).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                   //.AppendMath.Round(G_neftAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                   .Append(Math.Round(G_onlineAmount.ConvertToDouble()).ToString("F2")).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                   .Append(Math.Round(G_AdvAmount.ConvertToDouble()).ToString("F2")).Append("</td></tr>");

                                G_NetPayableAmt = 0; G_TotalAmount = 0; G_discAmount = 0; G_balAmount = 0; G_RefundAmount = 0; G_PaidAmount = 0; G_cashAmount = 0; G_chequeAmount = 0; G_neftAmount = 0; G_cardAmount = 0; G_onlineAmount = 0; G_AdvAmount = 0;


                                items.Append("<tr style=\"font-size:20px;border-bottom: 1px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;color:black\"><td colspan=\"15\" style=\"border:1px solid #000;padding:3px;height:10px;text-align:left;vertical-align:middle;font-weight:bold;\">").Append(dr["Label"].ConvertToString()).Append("</td></tr>");

                            }

                            G_TotalAmount += dr["TotalAmount"].ConvertToDouble();
                            G_discAmount += dr["DiscAmount"].ConvertToDouble();
                            G_NetPayableAmt += dr["NetAmount"].ConvertToDouble();
                            G_PaidAmount += dr["PaidAmount"].ConvertToDouble();
                            G_balAmount += dr["BalanceAmount"].ConvertToDouble();
                            G_RefundAmount += dr["RefundAmt"].ConvertToDouble();
                            G_AdvAmount += dr["AdvUsedAmt"].ConvertToDouble();

                            G_cashAmount += dr["CashPay"].ConvertToDouble();
                            G_cardAmount += dr["CardPay"].ConvertToDouble();
                            G_chequeAmount += dr["ChequePay"].ConvertToDouble();
                            G_onlineAmount += dr["PayTMAmount"].ConvertToDouble();
                            G_neftAmount += dr["NEFTPayAmount"].ConvertToDouble();



                            previousLabel = dr["Label"].ConvertToString();

                            items.Append("<tr style=\"font-size: 20px;line-height: 24px;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;\"><td style=\"border-left: 1px solid #ccc;border-bottom:1px solid #ccc;vertical-align: top;padding: 0;height: 20px;text-align:center\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;padding:0;height:10px;text-align:right;vertical-align:middle\">").Append(dr["Date"].ConvertToDateString("dd/MM/yyyy")).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;padding:0;height:10px;text-align:right;vertical-align:middle\">").Append(dr["Time"].ConvertToDateString("hh:mm")).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;padding:0;height:10px;vertical-align:middle;text-align: right;padding-left:10px;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;padding:0;height:10px;vertical-align:middle;text-align: right;padding-left:10px;\">").Append(dr["DiscAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["NetAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["PaidAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;padding:0;height:10px;vertical-align:middle;text-align: right;\">").Append(dr["BalanceAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["RefundAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["CashPay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["CardPay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["ChequePay"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            //  items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;padding:0;height:10px;vertical-align:middle;text-align: right;\">").Append(dr["NEFTPayAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;vertical-align:middle;padding:3px;height:10px;text-align:right;\">").Append(dr["PayTMAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #ccc;border-bottom:1px solid #ccc;border-right:1px solid #ccc;border-bottom:1px solid #ccc;padding:0;height:10px;vertical-align:middle;text-align: right;\">").Append(dr["AdvUsedAmt"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");





                            // salesTotal= salesTotal - 

                            //Total_Total = salesTotal.ConvertToDouble() - salesrefundTotal.ConvertToDouble();
                            //Total_disc = salesdisc.ConvertToDouble() - salesrefunddisc.ConvertToDouble();
                            //Total_net = salesnet.ConvertToDouble() - salesrefundnet.ConvertToDouble();
                            //Total_bal = salesbal.ConvertToDouble() - salesrefundbal.ConvertToDouble();
                            //Total_refund = salesrefund.ConvertToDouble() - salesrefundrefund.ConvertToDouble();
                            //Total_PaidAmount = salespaid.ConvertToDouble() - salesrefundpaid.ConvertToDouble();


                            //Total_Cash = salescashTotal.ConvertToDouble() - salesreturncashTotal.ConvertToDouble();
                            //Total_card = salescard.ConvertToDouble() - salesreturncard.ConvertToDouble();
                            //Total_cheque = salescheque.ConvertToDouble() - salesreturncheque.ConvertToDouble();
                            //Total_neft = salesneft.ConvertToDouble() - salesreturnneft.ConvertToDouble();
                            //Total_online = salesonline.ConvertToDouble() - salesreturnonline.ConvertToDouble();

                            if (dt.Rows.Count > 0 && dt.Rows.Count == i)
                            {
                                //items.Append("<tr style=\"border:1px solid black;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-weight:bold;font-size:22px;\"><td colspan='2' style=\"border-right:1px solid #ccc;border-left:1px solid #ccc;border-top:1px solid #ccc;padding:3px;height:15px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #ccc;padding:3px;height:15px;text-align:right;vertical-align:middle\">")
                                //                     .Append(G_TotalAmount.To2DecimalPlace()).Append(" </td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                //                     .Append(G_discAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                //                      .Append(G_NetPayableAmt.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                //                     .Append(G_PaidAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                //                        .Append(G_balAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                //                       .Append(G_RefundAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                //                        .Append(G_cashAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                //                    .Append(G_cardAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                //                    .Append(G_chequeAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                //                  //  .Append(G_neftAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                //                    .Append(G_onlineAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #ccc;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                //                    .Append("</td></tr>");

                                items.Append("<tr style=\"border:1px solid black;font-family: Calibri,'Helvetica Neue', 'Helvetica', Helvetica, Arial, sans-serif;font-size:22px;font-weight:bold;\"><td colspan='3' style=\"border-left:1px solid #ccc;border-right:1px solid #ccc;border-top:1px solid #ccc;padding:3px;height:15px;text-align:right;vertical-align:middle;margin-right:20px;font-weight:bold;\">Total</td><td style=\"border-right:1px solid #000;padding:3px;height:15px;text-align:right;vertical-align:middle\">")
                                 .Append(Math.Round(G_TotalAmount.ConvertToDouble()).ToString("F2")).Append(" </td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                 .Append(Math.Round(G_discAmount.ConvertToDouble()).ToString("F2")).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                  .Append(Math.Round(G_NetPayableAmt.ConvertToDouble()).ToString("F2")).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                 .Append(Math.Round(G_PaidAmount.ConvertToDouble()).ToString("F2")).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                  .Append(Math.Round(G_balAmount.ConvertToDouble()).ToString("F2")).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                 .Append(Math.Round(G_RefundAmount.ConvertToDouble()).ToString("F2")).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                  .Append(Math.Round(G_cashAmount.ConvertToDouble()).ToString("F2")).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                  .Append(Math.Round(G_cardAmount.ConvertToDouble()).ToString("F2")).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                  .Append(Math.Round(G_chequeAmount.ConvertToDouble()).ToString("F2")).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                 //.AppendMath.Round(G_neftAmount.To2DecimalPlace()).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                 .Append(Math.Round(G_onlineAmount.ConvertToDouble()).ToString("F2")).Append("</td><td style=\"border-right:1px solid #000;padding:3px;height:10px;text-align:right;vertical-align:middle\">")
                                 .Append(Math.Round(G_AdvAmount.ConvertToDouble()).ToString("F2")).Append("</td></tr>");


                            }
                        }


                        foreach (DataRow dr1 in dt.Rows)
                        {

                            //i++; j++;
                            if (dr1["Label"].ConvertToString() == "Sales")
                            {

                                salesTotal += dr1["TotalAmount"].ConvertToDouble();
                                salesdisc += dr1["DiscAmount"].ConvertToDouble();
                                salesnet += dr1["NetAmount"].ConvertToDouble();
                                salesbal += dr1["BalanceAmount"].ConvertToDouble();
                                salesrefund += dr1["RefundAmt"].ConvertToDouble();
                                salespaid += dr1["PaidAmount"].ConvertToDouble();

                                salescashTotal += dr1["CashPay"].ConvertToDouble();
                                salescard += dr1["CardPay"].ConvertToDouble();
                                salescheque += dr1["ChequePay"].ConvertToDouble();
                                salesneft += dr1["NEFTPayAmount"].ConvertToDouble();
                                salesonline += dr1["PayTMAmount"].ConvertToDouble();
                                salesadv += dr1["AdvUsedAmt"].ConvertToDouble();

                            }

                            if (dr1["Label"].ConvertToString() == "Sales Return")
                            {
                                salesrefundTotal += dr1["TotalAmount"].ConvertToDouble();
                                salesrefunddisc += dr1["DiscAmount"].ConvertToDouble();
                                salesrefundnet += dr1["NetAmount"].ConvertToDouble();
                                salesrefundbal += dr1["BalanceAmount"].ConvertToDouble();
                                salesrefundrefund += dr1["RefundAmt"].ConvertToDouble();
                                salesrefundpaid += dr1["PaidAmount"].ConvertToDouble();


                                salesreturncashTotal += dr1["CashPay"].ConvertToDouble();
                                salesreturncard += dr1["CardPay"].ConvertToDouble();
                                salesreturncheque += dr1["ChequePay"].ConvertToDouble();
                                salesreturnneft += dr1["NEFTPayAmount"].ConvertToDouble();
                                salesreturnonline += dr1["PayTMAmount"].ConvertToDouble();
                            }


                        }

                        salesnet = Math.Round(salesTotal.ConvertToDouble() - salesdisc.ConvertToDouble());
                        salesbal = Math.Round(salesbal.ConvertToDouble());
                        salesadv = Math.Round(salesadv.ConvertToDouble());
                        salesdisc = Math.Round(salesdisc.ConvertToDouble());
                        salesTotal = Math.Round(salesTotal.ConvertToDouble());
                        salesreturncashTotal = Math.Round(salesreturncashTotal.ConvertToDouble());

                        G_BalTotalamt = salesnet.ConvertToDouble() - (T_Creditreturnamt.ConvertToDouble() + salesadv.ConvertToDouble() + salescashTotal.ConvertToDouble() + salesonline.ConvertToDouble());


                        //TotalCollection = T_CashPayAmount.ConvertToDouble() + T_CardPayAmount.ConvertToDouble() + T_ChequePayAmount.ConvertToDouble() + T_TotalNEFT.ConvertToDouble() + T_TotalPAYTM.ConvertToDouble();


                        html = html.Replace("{{Items}}", items.ToString());


                       


                        html = html.Replace("{{salesreturncashTotal}}", salesreturncashTotal.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{salesonline}}", salesonline.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{salescashTotal}}", salescashTotal.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{salesnet}}", salesnet.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{salesdisc}}", salesdisc.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{salesrefund}}", salesrefund.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{salesTotal}}", salesTotal.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{salesbal}}", salesbal.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{salesadv}}", salesadv.ConvertToDouble().ToString("F2"));

                       


                        html = html.Replace("{{T_Totalamt}}", T_Totalamt.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_Discamt}}", T_Discamt.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_Netamt}}", T_Netamt.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_paidamt}}", T_paidamt.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_Creditreturnamt}}", T_Creditreturnamt.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_Cashreturnamt}}", T_Cashreturnamt.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_Advamt}}", T_Advamt.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_Advrefundamt}}", T_Advrefundamt.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_Advusedamt}}", T_Advusedamt.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_AdvBalamt}}", T_AdvBalamt.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_Cashpayamt}}", T_Cashpayamt.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{T_Onlinepayamt}}", T_Onlinepayamt.ConvertToDouble().ToString("F2"));

                        html = html.Replace("{{G_BalTotalamt}}", G_BalTotalamt.To2DecimalPlace());


                        html = html.Replace("{{StoreName}}", dt.GetColValue("StoreName"));
                        html = html.Replace("{{StoreAddress}}", dt.GetColValue("StoreAddress"));
                        html = html.Replace("{{HospitalMobileNo}}", dt.GetColValue("HospitalMobileNo"));
                        html = html.Replace("{{HospitalEmailId}}", dt.GetColValue("HospitalEmailId"));

                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));

                        html = html.Replace("{{ExtMobileNo}}", dt.GetColValue("ExtMobileNo"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));


                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));

                        html = html.Replace("{{Date}}", dt.GetColValue("Time").ConvertToDateString("dd/M/yyyy hh:mm tt"));

                        html = html.Replace("{{PayMode}}", dt.GetColValue("PayMode"));
                        html = html.Replace("{{DL_NO}}", dt.GetColValue("DL_NO"));

                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AdvanceNo}}", dt.GetColValue("AdvanceNo"));
                        html = html.Replace("{{Addedby}}", dt.GetColValue("Addedby"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{Age}}", dt.GetColValue("Age"));

                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{PaymentTime}}", dt.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AdvanceAmount}}", dt.GetColValue("AdvUsedAmt").ConvertToDouble().To2DecimalPlace());
                        html = html.Replace("{{Phone}}", dt.GetColValue("Phone"));

                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));

                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{RoomName}}", dt.GetColValue("RoomName"));
                        html = html.Replace("{{BedName}}", dt.GetColValue("BedName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{RefDocName}}", dt.GetColValue("RefDocName"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));

                     


                        html = html.Replace("{{AdvUsedAmt}}", dt.GetColValue("AdvUsedAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{BalAdvAmount}}", dt.GetColValue("BalAdvAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{AdvAmount}}", dt.GetColValue("AdvAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{CashPay}}", dt.GetColValue("CashPay").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{PayTMAmount}}", dt.GetColValue("PayTMAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{AdvanceAmount}}", dt.GetColValue("AdvUsedAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{creditreturnamt}}", dt.GetColValue("creditreturnamt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{AdvRefundAmt}}", dt.GetColValue("AdvRefundAmt").ConvertToDouble().ToString("F2"));


                        return html;
                    }
                    break;

                case "PharmacySalesDetails":
                    {

                      

                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                        StringBuilder item = new StringBuilder("");
                        int i = 0, j = 0;
                        double totalSales = 0, totalSalesReturn = 0;
                        double netPayableAmt = 0;

                        string previousSalesType = "";
                        string previousSalesDate = "";
                        string previousSalesNo = "";

                        var sortedBills = dt.AsEnumerable()
                            .OrderBy(dr => dr["SalesType"].ToString() == "Sales" ? 0 : 1)
                            .ThenBy(dr => dr["Date"])
                            .ThenBy(dr => dr["SalesNo"])
                            .ToList();

                        foreach (DataRow dr in sortedBills)
                        {
                            i++; j++;

                            string currentSalesType = dr["SalesType"].ToString();
                            string currentSalesDate = dr["Date"].ConvertToDateString("dd-MM-yyyy");
                            string currentSalesNo = dr["SalesNo"].ToString();

                            // Print group header if SalesType changes
                            if (previousSalesType != currentSalesType)
                            {
                                if (!string.IsNullOrEmpty(previousSalesNo) && netPayableAmt > 0)
                                {
                                    items.Append("<tr style='border:1px solid black;font-weight:bold;'>")
                                         .Append("<td colspan='6' style='text-align:right;padding:3px;font-size:18px;'>Total Amt</td>")
                                         .Append("<td style='text-align:right;padding:3px;font-size:18px;'>")
                                         .Append(netPayableAmt.ToString("0.00")).Append("</td><td></td></tr>");

                                    if (previousSalesType == "Sales") totalSales += netPayableAmt;
                                    else totalSalesReturn += netPayableAmt;

                                    netPayableAmt = 0;
                                }

                                items.Append("<tr style='font-size:22px;font-weight:bold;'>")
                                     .Append("<td colspan='8' style='text-align:left;color:black;'>")
                                     .Append(currentSalesType).Append("</td></tr>");
                            }

                            // Print Sales Date & No if different
                            if (previousSalesDate != currentSalesDate || previousSalesNo != currentSalesNo || previousSalesType != currentSalesType)
                            {
                                if (i != 1 && netPayableAmt > 0 && previousSalesNo != "")
                                {
                                    items.Append("<tr style='border:1px solid black;font-weight:bold;'>")
                                         .Append("<td colspan='6' style='text-align:right;padding:3px;font-size:18px;'>Total Amt</td>")
                                         .Append("<td style='text-align:right;padding:3px;font-size:18px;'>")
                                         .Append(netPayableAmt.ToString("0.00")).Append("</td><td></td></tr>");

                                    if (previousSalesType == "Sales") totalSales += netPayableAmt;
                                    else totalSalesReturn += netPayableAmt;

                                    netPayableAmt = 0;
                                }

                                items.Append("<tr style='font-size:20px;font-family: Calibri, Helvetica, Arial, sans-serif;'>")
                                     .Append("<td colspan='8' style='border:1px solid #000;padding:3px;text-align:left;'>")
                                     .Append("Sales Date: ").Append(currentSalesDate)
                                     .Append(" | Sales No: ").Append(currentSalesNo)
                                     .Append("</td></tr>");
                            }

                            // Item row
                            items.Append("<tr style='font-family: Helvetica, Arial, sans-serif;'>")
                                 .Append("<td style='border: 1px solid #d4c3c3; text-align: right; padding: 6px;'>").Append(j).Append("</td>")
                                 .Append("<td style='text-align: left; border: 1px solid #d4c3c3; padding: 6px;'>").Append(dr["ItemName"].ToString()).Append("</td>")
                                 .Append("<td style='text-align: left; border: 1px solid #d4c3c3; padding: 6px;'>").Append(dr["BatchNo"].ToString()).Append("</td>")
                                 .Append("<td style='text-align: center; border: 1px solid #d4c3c3; padding: 6px;'>").Append(dr["BatchExpDate"].ConvertToDateString("dd-MM-yyyy")).Append("</td>")
                                 .Append("<td style='text-align: center; border: 1px solid #d4c3c3; padding: 6px;'>").Append(Convert.ToDouble(dr["UnitMRP"]).ToString("0.00")).Append("</td>")
                                 .Append("<td style='text-align: center; border: 1px solid #d4c3c3; padding: 6px;'>").Append(dr["Qty"].ToString()).Append("</td>")
                                 .Append("<td style='text-align: right; border: 1px solid #d4c3c3; padding: 6px;'>").Append(Convert.ToDouble(dr["TotalAmount"]).ToString("0.00")).Append("</td>")
                                 .Append("<td style='text-align: right; border: 1px solid #d4c3c3; padding: 6px;'>").Append(Convert.ToDouble(dr["GrossAmount"]).ToString("0.00")).Append("</td>")
                                 .Append("</tr>");

                            netPayableAmt += Convert.ToDouble(dr["TotalAmount"]);

                            previousSalesType = currentSalesType;
                            previousSalesDate = currentSalesDate;
                            previousSalesNo = currentSalesNo;

                            // Final item
                            if (i == sortedBills.Count && netPayableAmt > 0)
                            {
                                items.Append("<tr style='border:1px solid black;font-weight:bold;'>")
                                     .Append("<td colspan='6' style='text-align:right;padding:3px;font-size:18px;'>Total Amt</td>")
                                     .Append("<td style='text-align:right;padding:3px;font-size:18px;'>")
                                     .Append(netPayableAmt.ToString("0.00")).Append("</td><td></td></tr>");

                                if (currentSalesType == "Sales") totalSales += netPayableAmt;
                                else totalSalesReturn += netPayableAmt;
                            }
                        }

                        // Grand totals
                        items.Append("<tr style='font-weight:bold;font-size:23px;background-color:#f0f0f0;'>")
                             .Append("<td colspan='6' style='text-align:right;'>Total Sales</td>")
                             .Append("<td colspan='6' style='text-align:right;'>").Append(totalSales.ToString("0.00")).Append("</td></tr>");

                        items.Append("<tr style='font-weight:bold;font-size:23px;background-color:#f0f0f0;'>")
                             .Append("<td colspan='6' style='text-align:right;'>Total Sales Return</td>")
                             .Append("<td colspan='6' style='text-align:right;'>").Append(totalSalesReturn.ToString("0.00")).Append("</td></tr>");



                        // Update the total amounts for all doctors and patients
                        //T_NetAmount += dr["TotalAmount"].ConvertToDouble();
                        //    T_Amount += dr["NetAmount"].ConvertToDouble();
                        //NetAmount += dr["NetAmount"].ConvertToDouble();


                        html = html.Replace("{{SalesType}}", dt.GetColValue("SalesType"));
                        html = html.Replace("{{OP_IP_Type}}", dt.GetColValue("OP_IP_Type"));
                        html = html.Replace("{{IP_OP_Number}}", dt.GetColValue("IP_OP_Number").ToString());

                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        // Replace the placeholders with actual totals and the generated items
                        //html = html.Replace("{{T_NetAmount}}", T_NetAmount.ToString("0.00"));
                        //html = html.Replace("{{T_Amount}}", T_Amount.ToString("0.00"));
                        html = html.Replace("{{PrintStoreName}}", dt.GetColValue("PrintStoreName"));
                        html = html.Replace("{{NetAmount}}", dt.GetColValue("NetAmount"));


                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{AgeMonth}}", dt.GetColValue("AgeMonth"));
                        html = html.Replace("{{AgeDay}}", dt.GetColValue("AgeDay"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{CompanyName}}", dt.GetColValue("CompanyName"));
                        html = html.Replace("{{DepartmentName}}", dt.GetColValue("DepartmentName"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));




                        html = html.Replace("{{Items}}", items.ToString());
                        html = html.Replace("{{FromDate}}", FromDate.ToString("dd/MM/yy"));
                        html = html.Replace("{{ToDate}}", ToDate.ToString("dd/MM/yy"));

                        return html;
                    }
                    break;


                case "PharamcySalesBill":
                    {


                        int i = 0;
                        var dynamicVariable = new Dictionary<string, double>();

                        double TotalGST = 0, T_HTotalAmount = 0, T_TotalBalancepay = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr style=\"font-size:15px;\"><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["HSNcode"].ConvertToString()).Append("</td>");
                         //   items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["ConversionFactor"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BatchExpDate"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["Qty"].ConvertToDouble()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["UnitMRP"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");



                            T_HTotalAmount += dr["TotalAmount"].ConvertToDouble();
                        }

                        TotalGST = dt.GetColValue("CGSTAmt").ConvertToDouble() + dt.GetColValue("SGSTAmt").ConvertToDouble() + dt.GetColValue("IGSTAmt").ConvertToDouble();

                        html = html.Replace("{{Items}}", items.ToString());

                        html = html.Replace("{{TotalGST}}", TotalGST.To2DecimalPlace());
                        html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

                        html = html.Replace("{{StoreName}}", dt.GetColValue("StoreName"));
                        html = html.Replace("{{StoreAddress}}", dt.GetColValue("StoreAddress"));
                        html = html.Replace("{{HospitalMobileNo}}", dt.GetColValue("HospitalMobileNo"));
                        html = html.Replace("{{HospitalEmailId}}", dt.GetColValue("HospitalEmailId"));

                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{Address}}", dt.GetColValue("Address"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));



                        html = html.Replace("{{ExtMobileNo}}", dt.GetColValue("ExtMobileNo"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));

                        html = html.Replace("{{HTotalAmount}}", T_HTotalAmount.To2DecimalPlace());
                        html = html.Replace("{{NetAmount}}", dt.GetColValue("NetAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{PaidAmount}}", dt.GetColValue("PaidAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{RoundOff}}", dt.GetColValue("RoundOff").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{DiscAmount}}", dt.GetColValue("DiscAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{HTotalAmount}}", dt.GetColValue("T_HTotalAmount").ConvertToDouble().ToString("F2"));

                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));
                        html = html.Replace("{{GSTIN}}", dt.GetColValue("GSTIN"));
                        html = html.Replace("{{DL_NO}}", dt.GetColValue("DL_NO"));

                        html = html.Replace("{{SalesNo}}", dt.GetColValue("SalesNo"));
                        // html = html.Replace("{{Date}}", Bills.GetColValue("Date"));
                        html = html.Replace("{{Date}}", dt.GetColValue("Time").ConvertToDateString("dd/M/yyyy hh:mm tt"));

                        html = html.Replace("{{PayMode}}", dt.GetColValue("PayMode"));
                        html = html.Replace("{{DL_NO}}", dt.GetColValue("DL_NO"));



                        return html;

                    }
                    break;


                case "PharamcySalesReturn":
                    {


                        int i = 0;

                        var dynamicVariable = new Dictionary<string, double>();

                        double TotalGST = 0, T_HTotalAmount = 0, T_TotalBalancepay = 0;

                        foreach (DataRow dr in dt.Rows)
                        {
                            i++;

                            items.Append("<tr style=\"font-size:15px;\"><td style=\"border-left: 1px solid black;vertical-align: top;padding: 0;height: 20px;text-align:center\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append(dr["HSNcode"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;text-align:center;vertical-align:middle\">").Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["ItemName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: left;padding-left:10px;\">").Append(dr["ConversionFactor"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BatchNo"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["BatchExpDate"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["Qty"].ConvertToDouble()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;vertical-align:middle;padding:3px;height:10px;text-align:center;\">").Append(dr["UnitMRP"].ConvertToDouble().To2DecimalPlace()).Append("</td>");
                            items.Append("<td style=\"border-left:1px solid #000;border-right:1px solid #000;padding:0;height:10px;vertical-align:middle;text-align: center;\">").Append(dr["TotalAmount"].ConvertToDouble().To2DecimalPlace()).Append("</td></tr>");



                            T_HTotalAmount += dr["TotalAmount"].ConvertToDouble();
                        }

                        TotalGST = dt.GetColValue("CGSTAmt").ConvertToDouble() + dt.GetColValue("SGSTAmt").ConvertToDouble() + dt.GetColValue("IGSTAmt").ConvertToDouble();

                        html = html.Replace("{{Items}}", items.ToString());

                        html = html.Replace("{{TotalGST}}", TotalGST.To2DecimalPlace());
                        html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

                        html = html.Replace("{{StoreName}}", dt.GetColValue("StoreName"));
                        html = html.Replace("{{StoreAddress}}", dt.GetColValue("StoreAddress"));
                        html = html.Replace("{{HospitalMobileNo}}", dt.GetColValue("HospitalMobileNo"));
                        html = html.Replace("{{HospitalEmailId}}", dt.GetColValue("HospitalEmailId"));

                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{Address}}", dt.GetColValue("Address"));
                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));

                        html = html.Replace("{{ExtMobileNo}}", dt.GetColValue("ExtMobileNo"));
                        html = html.Replace("{{DoctorName}}", dt.GetColValue("DoctorName"));

                        html = html.Replace("{{HTotalAmount}}", T_HTotalAmount.To2DecimalPlace());

                        html = html.Replace("{{DiscAmount}}", dt.GetColValue("DiscAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{NetAmount}}", dt.GetColValue("NetAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{RoundOff}}", dt.GetColValue("RoundOff").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{PaidAmount}}", dt.GetColValue("PaidAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{HTotalAmount}}", dt.GetColValue("HTotalAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{GrossAmount}}", dt.GetColValue("GrossAmount").ConvertToDouble().ToString("F2"));



                        html = html.Replace("{{UserName}}", dt.GetColValue("UserName"));
                        html = html.Replace("{{GSTIN}}", dt.GetColValue("GSTIN"));
                        html = html.Replace("{{DL_NO}}", dt.GetColValue("DL_NO"));


                        html = html.Replace("{{SalesNo}}", dt.GetColValue("SalesNo"));
                        // html = html.Replace("{{Date}}", Bills.GetColValue("Date"));
                        html = html.Replace("{{Date}}", dt.GetColValue("Time").ConvertToDateString("dd/M/yyyy hh:mm tt"));

                        html = html.Replace("{{PayMode}}", dt.GetColValue("PayMode"));
                        html = html.Replace("{{DL_NO}}", dt.GetColValue("DL_NO"));



                        return html;

                    }
                    break;



                case "PharamcyAdvanceReceipt":
                    {
                      
                        StringBuilder item = new StringBuilder("");
                        int i = 0;
                        Boolean chkresonflag = false;


                        //html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());
                        //html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

                        html = html.Replace("{{reason}}", dt.GetColValue("reason"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AdvanceNo}}", dt.GetColValue("AdvanceNo"));
                        html = html.Replace("{{Addedby}}", dt.GetColValue("Addedby"));
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{Age}}", dt.GetColValue("Age"));

                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{PaymentTime}}", dt.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{AdvanceAmount}}", dt.GetColValue("AdvanceAmount").ConvertToDouble().ToString("F2"));
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

                        


                        html = html.Replace("{{CashPayAmount}}", dt.GetColValue("CashPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{ChequePayAmount}}", dt.GetColValue("ChequePayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{ChequeNo}}", dt.GetColValue("ChequeNo").ConvertToString());
                        html = html.Replace("{{CardPayAmount}}", dt.GetColValue("CardPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{CardBankName}}", dt.GetColValue("CardBankName").ConvertToString());
                        html = html.Replace("{{CardNo}}", dt.GetColValue("CardNo").ConvertToString());
                        html = html.Replace("{{BankName}}", dt.GetColValue("BankName").ConvertToString());
                        html = html.Replace("{{NEFTPayAmount}}", dt.GetColValue("PatientType").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{NEFTNo}}", dt.GetColValue("NEFTNo").ConvertToString());
                        html = html.Replace("{{NEFTBankMaster}}", dt.GetColValue("NEFTBankMaster").ConvertToString());
                        html = html.Replace("{{PayTMPayAmount}}", dt.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{OnlineAmount}}", dt.GetColValue("OnlineAmount").ConvertToDouble().ToString("F2"));

                        html = html.Replace("{{PayTMTranNo}}", dt.GetColValue("PayTMTranNo").ConvertToString());
                        html = html.Replace("{{PayTMDate}}", dt.GetColValue("PayTMDate").ConvertToDouble().To2DecimalPlace());




                        html = html.Replace("{{chkcashflag}}", dt.GetColValue("CashPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkchequeflag}}", dt.GetColValue("ChequePayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkcardflag}}", dt.GetColValue("CardPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkneftflag}}", dt.GetColValue("NEFTPayAmount").ConvertToDouble() > 0 ? "table-row " : "none");

                        html = html.Replace("{{chkpaytmflag}}", dt.GetColValue("PayTMAmount").ConvertToDouble() > 0 ? "table-row " : "none");


                        string finalamt = conversion(dt.GetColValue("AdvanceAmount").ConvertToDouble().To2DecimalPlace().ToString());
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


                        html = html.Replace("{{chkresonflag}}", dt.GetColValue("reason").ConvertToString() != null ? "block" : "none");
                        return html;
                    }
                    break;


                case "PharamcyAdvanceReturnReceipt":
                    {
                        string html1 = File.ReadAllText(htmlFilePath);
                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                        html = html.Replace("{{NewHeader}}", htmlHeaderFilePath);
                        StringBuilder item = new StringBuilder("");
                        int i = 0;



                        //html = html.Replace("{{TotalIGST}}", T_TotalIGST.To2DecimalPlace());
                        //html = html.Replace("{{TotalBalancepay}}", T_TotalBalancepay.To2DecimalPlace());

                        html = html.Replace("{{Remark}}", dt.GetColValue("Remark"));
                        html = html.Replace("{{PatientName}}", dt.GetColValue("PatientName"));
                        html = html.Replace("{{RefundNo}}", dt.GetColValue("RefundNo"));
                        html = html.Replace("{{Addedby}}", dt.GetColValue("Addedby"));

                        html = html.Replace("{{AgeYear}}", dt.GetColValue("AgeYear"));
                        html = html.Replace("{{GenderName}}", dt.GetColValue("GenderName"));
                        html = html.Replace("{{AdmissinDate}}", dt.GetColValue("AdmissinDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{IPDNo}}", dt.GetColValue("IPDNo"));
                        html = html.Replace("{{AdvanceAmount}}", dt.GetColValue("AdvanceAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{RefundAmount}}", dt.GetColValue("RefundAmount").ConvertToDouble().ToString("F2"));
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
                        html = html.Replace("{{RefundAmount}}", dt.GetColValue("RefundAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{AdvanceUsedAmount}}", dt.GetColValue("AdvanceUsedAmount").ConvertToDouble().ToString("F2"));


                        html = html.Replace("{{BalanceAmount}}", dt.GetColValue("BalanceAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{CashPayAmount}}", dt.GetColValue("CashPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{CardPayAmount}}", dt.GetColValue("CardPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{ChequePayAmount}}", dt.GetColValue("ChequePayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{NEFTPayAmount}}", dt.GetColValue("NEFTPayAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{PayTMPayAmount}}", dt.GetColValue("PayTMPayAmount").ConvertToDouble().ToString("F2"));

                        html = html.Replace("{{chkCompanyNameflag}}", dt.GetColValue("CompanyName").ConvertToString() != "" ? "visible" : "none");

                        html = html.Replace("{{DischargeDate}}", dt.GetColValue("DischargeDate").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AdmissionTime}}", dt.GetColValue("AdmissionTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));

                        html = html.Replace("{{RefundTime}}", dt.GetColValue("RefundTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{PaymentTime}}", dt.GetColValue("PaymentTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{AddedBy}}", dt.GetColValue("AddedBy"));

                        string finalamt = conversion(dt.GetColValue("RefundAmount").ConvertToDouble().To2DecimalPlace().ToString());
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());
                        return html;
                    }
                    break;



                case "SupplierPaymentReciept":
                    {


                        int i = 0, j = 0;
                        double Dcount = 0;
                        string previousLabel = "";
                        int k = 0;
                        var dynamicVariable = new Dictionary<string, double>();


                        html = html.Replace("{{CurrentDate}}", DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"));
                        //html = html.Replace("{{NewHeader}}", htmlHeader);
                        html = html.Replace("{{RegNo}}", dt.GetColValue("RegNo"));
                        html = html.Replace("{{TotalBillAmount}}", dt.GetColValue("TotalBillAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{ConcessionAmt}}", dt.GetColValue("ConcessionAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{NetPayableAmt}}", dt.GetColValue("NetPayableAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{SupplierName}}", dt.GetColValue("SupplierName"));
                        html = html.Replace("{{Address}}", dt.GetColValue("Address"));
                        html = html.Replace("{{Email}}", dt.GetColValue("Email"));
                        html = html.Replace("{{SupPayDate}}", dt.GetColValue("SupPayTime").ConvertToDateString("dd/MM/yyyy | hh:mm tt"));
                        html = html.Replace("{{Mobile}}", dt.GetColValue("Mobile"));
                        html = html.Replace("{{PatientType}}", dt.GetColValue("PatientType"));
                        html = html.Replace("{{OPDNo}}", dt.GetColValue("OPDNo"));

                        double T_NetAmount = 0;
                        foreach (DataRow dr in dt.Rows)
                        {
                            i++; j++;
                            items.Append("<tr style=\"font-family: 'Helvetica Neue', 'Helvetica',, Arial, sans-serif;font-size:15;\"><td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(i).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: left; padding: 6px;\">").Append(dr["SupplierName"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["SupPayDate"].ConvertToString()).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["CashPayAmt"].ConvertToDouble().ToString("F2")).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["ChequePayAmt"].ConvertToDouble().ToString("F2")).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["OnlineAmount"].ConvertToDouble().ToString("F2")).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["PaidAmount"].ConvertToDouble().ToString("F2")).Append("</td>");
                            items.Append("<td style=\"border: 1px solid #d4c3c3; text-align: center; padding: 6px;\">").Append(dr["NetAmount"].ConvertToDouble().ToString("F2")).Append("</td></tr>");

                            T_NetAmount += dr["NetAmount"].ConvertToDouble();
                        }
                        T_NetAmount = Math.Round(T_NetAmount);

                        html = html.Replace("{{Items}}", items.ToString());


                        html = html.Replace("{{T_NetAmount}}", T_NetAmount.ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{TotalBillAmount}}", dt.GetColValue("TotalBillAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{BalanceAmt}}", dt.GetColValue("BalanceAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{PaidAmount}}", dt.GetColValue("PaidAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{OnlineAmount}}", dt.GetColValue("OnlineAmount").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{TotalGst}}", dt.GetColValue("TotalGst").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{NetPayableAmt}}", dt.GetColValue("NetPayableAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{UserName}}", dt.GetColValue("AddedByName").ConvertToString());
                        html = html.Replace("{{HospitalName}}", dt.GetColValue("HospitalName").ConvertToString());
                        html = html.Replace("{{DiscComments}}", dt.GetColValue("DiscComments").ConvertToString());
                        html = html.Replace("{{CashPayAmt}}", dt.GetColValue("CashPayAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{CashPayAmt}}", dt.GetColValue("CashPayAmt").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{CardPay}}", dt.GetColValue("CardPay").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{NeftPay}}", dt.GetColValue("NeftPay").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{PayTMPay}}", dt.GetColValue("PayTMPay").ConvertToDouble().ToString("F2"));
                        html = html.Replace("{{PaymentMode}}", dt.GetColValue("PaymentMode").ConvertToString());
                        html = html.Replace("{{RefundInfo}}", dt.GetColValue("RefundInfo"));
                        html = html.Replace("{{chkpaidflag}}", dt.GetColValue("PaidAmount").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkbalflag}}", dt.GetColValue("BalanceAmt").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkdiscflag}}", dt.GetColValue("ConcessionAmt").ConvertToDouble() > 0 ? "table-row " : "none");
                        html = html.Replace("{{chkRefundflag}}", dt.GetColValue("RefundAmt").ConvertToDouble() > 0 ? "table-row " : "none");

                        string finalamt = conversion(T_NetAmount.ToString());
                        html = html.Replace("{{finalamt}}", finalamt.ToString().ToUpper());


                        return html;

                    }

              

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

        public static string conversion(string amount)
        {
            double m = Convert.ToInt64(Math.Floor(Convert.ToDouble(amount)));
            double l = Convert.ToDouble(amount);

            double j = (l - m) * 100;
            //string Word = " ";

            var beforefloating = ConvertNumbertoWords(Convert.ToInt64(m));
            ConvertNumbertoWords(Convert.ToInt64(j));

            // Word = beforefloating + '.' + afterfloating;

            var Content = beforefloating + ' ' + " RUPEES" + ' ' + " only";

            return Content;
        }

        public static string ConvertNumbertoWords(long number)
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
        public DataTable GetReportDataBySp(ReportConfigDto model)
        {
            Dictionary<string, string> fields = Data.Extensions.SearchFieldExtension.GetSearchFields(model.SearchFields).ToDictionary(e => e.FieldName, e => e.FieldValueString);
            DatabaseHelper odal = new();
            int sp_Para = 0;
            SqlParameter[] para = new SqlParameter[fields.Count];
            foreach (var property in fields)
            {
                var param = new SqlParameter
                {
                    ParameterName = "@" + property.Key,
                    Value = ((property.Key == "FromDate" || property.Key == "ToDate") ? DateTime.ParseExact(property.Value, "yyyy-MM-dd", CultureInfo.InvariantCulture) : property.Value.ToString())
                };

                para[sp_Para] = param;
                sp_Para++;
            }
            return odal.FetchDataTableBySP(model.SPName, para, true);
        }
    }
}


