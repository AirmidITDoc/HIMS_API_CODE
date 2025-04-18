using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Utility;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Common;
using HIMS.Services.Masters;
using HIMS.Services.OPPatient;
using HIMS.Services.Report;
using HIMS.Services.Utilities;
using LinqToDB.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Data;
using System.Security;
using WkHtmlToPdfDotNet;
using HIMS.API.Utility;

namespace HIMS.API.Controllers.Report
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ReportController : BaseController
    {
        private readonly IRegistrationService _IRegistrationService;
        private readonly IDoctorMasterService _IDoctorMasterService;
        private readonly IGenericService<MReportConfig> _reportlistRepository;
        private readonly IReportService _reportService;
        public readonly IConfiguration _configuration;
        public readonly IPdfUtility _pdfUtility;
        public readonly IFileUtility _FileUtility;
        public ReportController(IRegistrationService repository, IDoctorMasterService doctorRepository,
            IGenericService<MReportConfig> reportlistRepository, IFileUtility fileUtility,
            IReportService reportService, IConfiguration configuration, IPdfUtility pdfUtility)
        {
            _IRegistrationService = repository;
            _IDoctorMasterService = doctorRepository;
            _reportlistRepository = reportlistRepository;
            _reportService = reportService;
            _configuration = configuration;
            _pdfUtility = pdfUtility;
            _FileUtility = fileUtility;
        }

        [HttpPost("ReportList")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<IActionResult> ReportList(GridRequestModel objGrid)
        {
            IPagedList<MReportConfig> ReportList = await _reportlistRepository.GetAllPagedAsync(objGrid);
            return Ok(ReportList.ToGridResponse(objGrid, "Report List"));
        }
        [HttpGet("UserList/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetUserListAutoComplete(string Keyword)
        {
            var data = await _IRegistrationService.SearchRegistration(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "User Data.", data.Select(x => new { Text = x.FirstName + " " + x.LastName, Value = x.Id }));
        }
        [HttpGet("DoctorList/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDoctorListAutoComplete(string Keyword)
        {
            var data = await _IDoctorMasterService.SearchDoctor(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor Data.", data.Select(x => new { Text = x.FirstName + " " + x.LastName, Value = x.DoctorId }));
        }
        [HttpGet("ServiceList/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetServiceListAutoComplete(string Keyword)
        {
            var data = await _reportService.SearchService(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service Data.", data.Select(x => new { Text = x.ServiceName, Value = x.ServiceId }));
        }
        [HttpGet("DepartmentList/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDepartmentListAutoComplete(string Keyword)
        {
            var data = await _reportService.SearchDepartment(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Department Data.", data.Select(x => new { Text = x.DepartmentName, Value = x.DepartmentId }));
        }
        [HttpGet("CashCounterList/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetCashCounterListAutoComplete(string Keyword)
        {
            var data = await _reportService.SearchCashCounter(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "CashCounter Data.", data.Select(x => new { Text = x.CashCounterName, Value = x.CashCounterId }));
        }

        [HttpGet("{mode?}")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(string mode)
        {
            if (string.IsNullOrEmpty(mode))
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _reportlistRepository.GetAll(x => x.IsActive.Value);
            var sdata = data.Where(x => x.ReportMode == mode).FirstOrDefault();
            return sdata.ToSingleResponse<MReportConfig, MReportConfig>("Report");
        }
        [HttpPost("ViewReport")]
        public IActionResult ViewReport(ReportRequestModel model)
        {
            switch (model.Mode)
            {
                #region"OP Reports"

                case "RegistrationReport":
                case "AppointmentListReport":
                case "DoctorWiseVisitReport":
                case "RefDoctorWiseReport":
                case "DepartmentWisecountSummury":
                case "OPDoctorWiseVisitCountSummary":
                case "OPAppoinmentListWithServiseAvailed":
                case "CrossConsultationReport":
                case "OPDoctorWiseNewOldPatientReport":
                case "OPRefundReceipt":
                case "OPPaymentReceipt":
                case "AppointmentReceipt":
                case "OpBillReceipt":
                case "OPBillWithPackagePrint":
                case "GRNReport":




                //{
                //    if (!CommonExtensions.CheckPermission("OPReports", PagePermission.View))
                //        return Unauthorized("You don't have permission to access this report.");
                //    break;
                //}
                #endregion


                #region"OPBilling Reports"

                case "BillReportSummary":
                case "BillReportSummarySummary":
                case "OPDBillBalanceReport":
                case "OPDRefundOfBill":
                case "OPDailyCollectionReport":
                case "OPCollectionSummary":

                    //{
                    //    if (!CommonExtensions.CheckPermission("OPBilling Reports", PagePermission.View))
                    //        return Unauthorized("You don't have permission to access this report.");
                    //    break;
                    //}
                #endregion

                #region"Nursing Reports"

                case "DoctorNotesReceipt":
                case "NursingNotesReceipt":
                case "DoctorPatientHandoverReceipt":
                case "NurMaterialConsumption":
                case "NurLabRequestTest":




                //{
                //    if (!CommonExtensions.CheckPermission("OPBilling Reports", PagePermission.View))
                //        return Unauthorized("You don't have permission to access this report.");
                //    break;
                //}
                #endregion




                #region"OP MIS Reports"

                case "DayWiseOpdCountDetails":
                case "DayWiseOpdCountSummry":
                case "DepartmentWiseOPDCount":
                case "DepartmentWiseOpdCountSummary":
                case "DrWiseOPDCountDetail":
                case "DoctorWiseOpdCountSummary":
                case "DrWiseOPDCollectionDetails":
                case "DoctorWiseOpdCollectionSummary":
                case "DepartmentWiseOPDCollectionDetails":
                case "DepartmentWiseOpdCollectionSummary":
                case "DepartmentServiceGroupWiseCollectionDetails":
                case "DepartmentServiceGroupWiseCollectionSummary":


                //{
                //    if (!CommonExtensions.CheckPermission("OP MIS Reports", PagePermission.View))
                //        return Unauthorized("You don't have permission to access this report.");
                //    break;
                //}
                #endregion




                #region"IP Reports"

                case "IpCasepaperReport":
                //case "IpCasepaperReport1":

                case "IptemplateCasepaperReport":
                case "AdmissionList":
                case "IpDraftBill":
                case "IpDraftBillNew":
                case "IpFinalBillNew":
                case "IpFinalBill":
                case "IpFinalBillClassservicewise":
                case "IpFinalClasswiseBill":
                case "IpFinalGroupwiseBill":
                case "IpCreditBill":
                case "IpInterimBill":
                case "IpPaymentReceipt":
                case "IpAdvanceRefundReceipt":
                case "IpBillRefundReceipt":
                case "IpDischargeReceipt":
                case "IpDischargeSummaryReport":
                case "IPDRefrancefDoctorwise":
                case "IPDCurrentrefDoctorAdmissionList":
                case "IPDDoctorWiseCountSummaryList":
                case "IPDCurrentwardwisecharges":
                case "Dischargetypewise":
                case "Dischargetypecompanywise":
                case "DepartmentwiseCount":
                case "IPDDischargewithmarkstatus":
                case "IpMLCCasePaperPrint":
                case "PathresultEntry":
                case "PathresultEntryWithHeader":
                case "PathTemplateReport":
                case "RadiologyTemplateReport":
                case "IpAdvanceReceipt":
                case "IpAdvanceStatement":

                case "NurIPprescriptionReport":
                case "NurIPprescriptionReturnReport":
                case "DischargSummary":
                case "IpDischargeSummaryReportTesting":

                    
                case "Purchaseorder":
                case "PathTemplateHeaderReport":





                #endregion


                default:
                    break;
            }
            model.BaseUrl = Convert.ToString(_configuration["BaseUrl"]);
            model.StorageBaseUrl = Convert.ToString(_configuration["StorageBaseUrl"]);
            string byteFile = _reportService.GetReportSetByProc(model);
            return Ok(ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Report.", new { base64 = byteFile }));



        }

        [HttpPost("NewViewReport")]
        public IActionResult NewViewReport(ReportNewRequestModel model)
        {
            //if (!CommonExtensions.CheckPermission("OPReports", PagePermission.View))
            //    return Unauthorized("You don't have permission to access this report.");
            model.BaseUrl = Convert.ToString(_configuration["BaseUrl"]);
            model.StorageBaseUrl = Convert.ToString(_configuration["StorageBaseUrl"]);
            string byteFile = _reportService.GetNewReportSetByProc(model);
            return Ok(ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Report.", new { base64 = byteFile }));
        }

        [HttpGet("view-AdmissionTemplate")]
        public IActionResult viewAdmissionTemplate(int AdmissionId)
        {
            // string htmlFilePath = Path.Combine(_hostingEnvironment.WebRootPath, "PdfTemplates", "PrimeAdmissionPaper.html");


            // Hospital Header 
            //string Hospitalheader = _pdfUtility.GetHeader(1, 1);// hospital header
            //Hospitalheader = Hospitalheader.Replace("{{BaseUrl}}", _configuration.GetValue<string>("BaseUrl").Trim('/'));

            ////Report content
            //string Admissiontemplate = _pdfUtility.GetTemplateHeader(2);// Admission header
            //Admissiontemplate = Admissiontemplate.Replace("{{BaseUrl}}", _configuration.GetValue<string>("BaseUrl").Trim('/'));

            //DataTable dt = _Admission.GetDataForReport(AdmissionId);
            //var html = _Admission.ViewAdmissiontemplatePaper(dt, Admissiontemplate, Hospitalheader);
            //html = html.Replace("{{NewHeader}}", Hospitalheader);

            //var tuple = _pdfUtility.GeneratePdfFromHtml(html, "IPAdmission", "IPAdmission" + AdmissionId, Wkhtmltopdf.NetCore.Options.Orientation.Portrait);

            //return Ok(new { base64 = Convert.ToBase64String(tuple.Item1) });
            return Ok();

        }
    }
}
