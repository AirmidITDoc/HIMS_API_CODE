using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Extensions;
using HIMS.Data.Models;
using HIMS.Services.Common;
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

namespace HIMS.API.Controllers.Report
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ReportController : BaseController
    {
        private readonly IGenericService<MReportConfiguration> _reportlistRepository;
        private readonly IGenericService<LoginManager> _userRepository;
        private readonly IGenericService<DoctorMaster> _doctorRepository;
        private readonly IReportService _reportService;
        public readonly IConfiguration _configuration;
        public readonly IPdfUtility _pdfUtility;
        public ReportController(IGenericService<MReportConfiguration> reportlistRepository, IGenericService<LoginManager> userRepository, IGenericService<DoctorMaster> doctorRepository,
            IReportService reportService, IConfiguration configuration, IPdfUtility pdfUtility)
        {
            _reportlistRepository = reportlistRepository;
            _userRepository = userRepository;
            _doctorRepository = doctorRepository;
            _reportService = reportService;
            _configuration = configuration;
            _pdfUtility = pdfUtility;
        }

        [HttpPost("ReportList")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<IActionResult> ReportList(GridRequestModel objGrid)
        {
            IPagedList<MReportConfiguration> ReportList = await _reportlistRepository.GetAllPagedAsync(objGrid);
            return Ok(ReportList.ToGridResponse(objGrid, "Report List"));
        }
        [HttpPost("UserList")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<IActionResult> UserList(GridRequestModel objGrid)
        {
            IPagedList<LoginManager> UserList = await _userRepository.GetAllPagedAsync(objGrid);
            return Ok(UserList.ToGridResponse(objGrid, "User List"));
        }
        [HttpPost("DoctorList")]
        [Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<IActionResult> DoctorList(GridRequestModel objGrid)
        {
            IPagedList<DoctorMaster> DoctorList = await _doctorRepository.GetAllPagedAsync(objGrid);
            return Ok(DoctorList.ToGridResponse(objGrid, "Doctor List"));
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



                    {
                        if (!CommonExtensions.CheckPermission("OPReports", PagePermission.View))
                            return Unauthorized("You don't have permission to access this report.");
                        break;
                    }
                #endregion


                #region"OPBilling Reports"

                case "BillReportSummary":
                case "BillReportSummarySummary":
                case "OPDBillBalanceReport":
                case "OPDRefundOfBill":
                case "OPDailyCollectionReport":
                case "OPCollectionSummary":

                    {
                        if (!CommonExtensions.CheckPermission("OPBilling Reports", PagePermission.View))
                            return Unauthorized("You don't have permission to access this report.");
                        break;
                    }
                #endregion

                #region"Nursing Reports"

                case "DoctorNotesReceipt":
                case "NursingNotesReceipt":
                case "DoctorPatientHandoverReceipt":




                    {
                        if (!CommonExtensions.CheckPermission("OPBilling Reports", PagePermission.View))
                            return Unauthorized("You don't have permission to access this report.");
                        break;
                    }
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

                    {
                        if (!CommonExtensions.CheckPermission("OP MIS Reports", PagePermission.View))
                            return Unauthorized("You don't have permission to access this report.");
                        break;
                    }
                #endregion




                #region"IP Reports"

                case "IpCasepaperReport":
                case "IptemplateCasepaperReport":
                case "AdmissionList":
                case "IpFinalBill":
                case "IpPaymentReceipt":
                case "IpAdvanceRefundReceipt":
                case "IpBillRefundReceipt":

                //{
                //    if (!CommonExtensions.CheckPermission("OPReports", PagePermission.View))
                //        return Unauthorized("You don't have permission to access this report.");
                //    break;
                //}
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
