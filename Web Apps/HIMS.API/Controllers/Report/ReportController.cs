using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Utility;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.Services.Masters;
using HIMS.Services.OPPatient;
using HIMS.Services.Report;
using HIMS.Services.Utilities;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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

        [HttpPost("NewList")]
        //   [Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<IActionResult> MReportListDto(GridRequestModel objGrid)
        {
            objGrid.Filters.Add(new SearchGrid() { FieldName = "Id", FieldValue = "0" });
            IPagedList<MReportListDto> MReportConfigList = await _reportService.MReportListDto(objGrid);
            return Ok(MReportConfigList.ToGridResponse(objGrid, "MReportConfig  List"));
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
        [HttpGet("RefDoctorList/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDoctorListAutoCompletes(string Keyword)
        {
            var data = await _reportService.SearchDoctor(Keyword);
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

        [HttpGet("WardList/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetWardListAutoComplete(string Keyword)
        {
            var data = await _reportService.SearchWard(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SearchWard Data.", data.Select(x => new { Text = x.RoomName, Value = x.RoomId }));
        }
        //[HttpGet("AdmissionList/auto-complete")]
        ////[Permission(PageCode = "Report", Permission = PagePermission.View)]
        //public async Task<ApiResponse> GetAdmissionListAutoComplete(string Keyword)
        //{
        //    var data = await _reportService.SearchAdmission(Keyword);
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SearchAdmission Data.", data.Select(x => new { Text = x.Admissions, Value = x.AdmissionId }));
        //}
        [HttpGet("CompanyList/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetCompanyListAutoComplete(string Keyword)
        {
            var data = await _reportService.SearchCompany(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SearchCompany Data.", data.Select(x => new { Text = x.CompanyName, Value = x.CompanyId }));
        }

        [HttpGet("DischargeTypeList/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDischargeTypeListAutoComplete(string Keyword)
        {
            var data = await _reportService.SearchDischargeType(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SearchDischargeType Data.", data.Select(x => new { Text = x.DischargeTypeName, Value = x.DischargeTypeId }));
        }

        [HttpGet("GroupMasterList/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetGroupMasterListAutoComplete(string Keyword)
        {
            var data = await _reportService.SearchGroupMaster(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SearchGroupMaster Data.", data.Select(x => new { Text = x.GroupName, Value = x.GroupId }));
        }

        [HttpGet("ClassMasterList/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetClassMasterListAutoComplete(string Keyword)
        {
            var data = await _reportService.SearchClassMaster(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SearchClassMaster Data.", data.Select(x => new { Text = x.ClassName, Value = x.ClassId }));
        }

        [HttpGet("MStoreMasterList/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetMStoreMasterListAutoComplete(string Keyword)
        {
            var data = await _reportService.SearchMStoreMaster(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SearchMStoreMaster Data.", data.Select(x => new { Text = x.StoreName, Value = x.StoreId }));
        }

        [HttpGet("MSupplierMasterList/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetMSupplierMasterListAutoComplete(string Keyword)
        {
            var data = await _reportService.SearchMSupplierMaster(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SearchMSupplierMaster Data.", data.Select(x => new { Text = x.SupplierName, Value = x.SupplierId }));
        }


        [HttpGet("MItemDrugTypeMasterList/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetMItemDrugTypeMasterListAutoComplete(string Keyword)
        {
            var data = await _reportService.SearchMItemDrugTypeMaster(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SearchMItemDrugTypeMaster Data.", data.Select(x => new { Text = x.DrugTypeName, Value = x.ItemDrugTypeId }));
        }

        [HttpGet("MCreditReasonMasterList/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetMCreditReasonMasterListAutoComplete(string Keyword)
        {
            var data = await _reportService.SearchMCreditReasonMaster(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SearchMCreditReasonMaster Data.", data.Select(x => new { Text = x.CreditReason, Value = x.CreditId }));
        }


        [HttpGet("MItemMasterList/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetMItemMasterListAutoComplete(string Keyword)
        {
            var data = await _reportService.SearchMItemMaster(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SearchMItemMaster Data.", data.Select(x => new { Text = x.ItemName, Value = x.ItemId }));
        }


        [HttpGet("Expensesheadmaster/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetExpensesheadmasterAutoComplete(string Keyword)
        {
            var data = await _reportService.SearchMExpensesHeadMaster(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Searchmexpensesheadmaster Data.", data.Select(x => new { Text = x.HeadName, Value = x.ExpHedId }));
        }

        [HttpGet("MExpensesCategoryMaster/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetMExpensesCategoryMasterAutoComplete(string Keyword)
        {
            var data = await _reportService.SearchMExpensesCategoryMaster(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SearchMExpensesCategoryMaster Data.", data.Select(x => new { Text = x.CategoryName, Value = x.ExpCatId }));
        }

        [HttpGet("MModeOfPaymentList/auto-complete")]
        //[Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetMModeofpaymentListAutoComplete(string Keyword)
        {
            var data = await _reportService.SearchMModeOfPayment(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "SearchMModeOfPayment Data.", data.Select(x => new { Text = x.ModeOfPayment, Value = x.Id }));
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
        public async Task<IActionResult> ViewReport(ReportRequestModel model)
        {
            switch (model.Mode)
            {
                #region"OP Reports"
                case "RegistrationForm":
                case "OPStickerPrint":

                case "OPRefundReceipt":
                case "OPPaymentReceipt":
                case "LabPaymentReceipt":
                case "AppointmentReceipt":
                case "AppointmentReceiptWithoutHeader":
                case "OpBillReceipt":
                case "OpBillReceiptT":
                case "OPBillWithPackagePrint":
                case "GRNReport":
                case "GRNReturnReport":
                case "KenyaGRNReport":

                //case "IndentwiseReport":
                case "OPCasePaper":
                //case "OPDSpineCasePaper":
                case "OPPrescription":
                case "OPPrescriptionA5":
                case "OPGastrologyPrescription":

                case "OPPrescriptionwithoutHeader":
                case "OPPrescriptionwithoutHeaderA5":

                case "CertificateInformationReport":
                case "Certificate":
                case "EmergencyPrint":
                case "EmergencyPrescription":
                case "ConsentInformation":
                case "ConsentInformationWithoutHeader":
                case "LabregisterBillReceipt":

                #endregion



                #region"Nursing Reports"

                case "DoctorNotesReceipt":
                case "NursingNotesReceipt":
                case "DoctorPatientHandoverReceipt":
                case "NursingPatientHandoverReceipt":

                case "NurMaterialConsumption":
                case "NurLabRequestTest":
                case "CanteenRequestprint":
                case "DoctorNoteandNursingNoteReport":

                //OT
                //case "OTOprativereport":

                #endregion

                #region"IP Reports"

                case "IpCasepaperReport":
                case "IPStickerPrint":

                case "IptemplateCasepaperReport":
                case "AdmissionList":
                case "IpDraftBillGroupWise"://Namechange
                case "IpDraftBillGroupWiseA5":
                case "IpDraftBillClassWise"://Namechange
                case "IpDraftBillClassWiseA5"://
                case "IpDraftBillDateWise"://


                case "IpFinalBillNew":
                case "IPFinalBillClassWise"://Namechanges
                case "IPFinalBillClassServiceWise"://Namechange
                case "IPFinalBillChargesDateWise":
                case "IPFinalBillChargesDateWisegroupwise":
                case "IPFinalBillChargesDateWisegroupwisewithoutadvance":


                case "IPFinalBillGroupwise"://Namechanges
                case "IpCreditBill":
                case "IPDInterimBill"://change
                case "IPDInterimBillA5"://change


                case "IpPaymentReceipt":
                case "IpAdvanceRefundReceipt":
                case "IpBillRefundReceipt":
                case "IpDischargeReceipt":
                case "BedTransferReceipt":


                //DISCHARGE Summary Reports
                case "IpDischargeSummaryReport":
                case "IpDischargeSummaryReportWithoutHeader":
                case "IpDischargeSummaryTemplate":
                case "IpDischargeSummaryTemplateWithoutHeader":
                case "IpDischargeSummaryTemplatewithPatientHeader":
                case "IpDischargeSummaryTemplatepatientWithoutHeader":

                case "IPDRefrancefDoctorwise":
                case "IPDCurrentrefDoctorAdmissionList":
                case "IPDDoctorWiseCountSummaryList":
                case "IPDCurrentwardwisecharges":
                case "Dischargetypewise":
                case "Dischargetypecompanywise":
                case "DepartmentwiseCount":
                case "IPDDischargewithmarkstatus":
                case "IpMLCCasePaperPrint":


                // OT
                case "OTAnaesthesiaRecord":
                case "OTCheckInOutPatientWise":
                case "OTInOperationReport":
                case "OTPreOperationReport":
                case "OTRequestReport":
                case "OTOperativeNotesReport":
                case "OTReservation":


                //PATHOLOGY   

                case "PathologyReportWithHeader":
                case "PathologyReportWithOutHeader":
                case "PathologyReportTemplateWithHeader":
                case "PathologyReportTemplateWithOutHeader":

                case "PathologyReportTemplateWithImgHeader":
                case "PathologyReportWithImgHeader":

                case "PathologySampleBarcode":



                //Radiology 
                case "RadiologyTemplateReportWithHeader":
                case "RadiologyTemplateReportWithoutHeader":
                case "RadiologyTemplateReportWithImgHeader":

                //Advance Page Printouts
                case "IpAdvanceReceipt":
                case "IpAdvanceStatement":
                case "NurIPprescriptionReport":
                case "NurIPprescriptionReturnReport":
                case "DischargSummary":
                //case "IpDischargeSummaryReportTesting":

                //Inventory
                case "Purchaseorder":
                case "PathTemplateHeaderReport":
                case "IndentWiseReport":
                case "IndentverifyReport":
                case "Issutodeptissuewise":
                case "Issutodeptsummarywise":
                case "OpeningBalance":
                case "WorkOrder":
                case "MaterialReceivedByDept":

                //Pharmacy
                case "PharmacyPatientStatement":
                case "PharmacySalesDetails":
                case "PharamcySalesBill":
                case "PharamcySalesReturn":
                case "PharamcyAdvanceReceipt":
                case "PharamcyAdvanceReturnReceipt":

                case "SalesReturnBill":
                case "ExpenseVoucharPrint":

                case "PatientBillStatement":


                //PharmacyKenya
                case "PharamcySalesReturnKenya":
                case "PharamcyInPatientSalesBillKenya":
                case "PharamcyInPatientSalesReturnKenya":
                case "PharmacyInPatientSalesDetails":
                case "PharmacyInPatientStatement":

                //Clinical Care
                case "NursingVitalsReport":

                case "SupplierPaymentReciept":


                //Labrequest Detail
                case "LabRegistrationListReport":
                case "CommonTemplateReport":

                #endregion


                default:
                    break;
            }
            model.BaseUrl = Convert.ToString(_configuration["BaseUrl"]);
            model.StorageBaseUrl = Convert.ToString(_configuration["StorageBaseUrl"]);
            var byteFile = await _reportService.GetReportSetByProc(model, _configuration["PdfFontPath"]);
            return Ok(ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Report.", new { base64 = Convert.ToBase64String(byteFile.Item1) }));
        }

        [HttpPost("get-report-html")]
        public async Task<ApiResponse> GetBarcodeHtml(ReportRequestModel model)
        {
            var data = await _reportService.GetPatientBarcode(model);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Barcode Data.", new { html = data.Item1, title = data.Item2 });
        }

        [HttpPost("NewViewReport")]
        public async Task<IActionResult> NewViewReport(NewReportRequestDto request)
        {
            //if (!CommonExtensions.CheckPermission("OPReports", PagePermission.View))
            //    return Unauthorized("You don't have permission to access this report.");
            //model.BaseUrl = Convert.ToString(_configuration["BaseUrl"]);
            ReportConfigDto model = new();
            GridRequestModel objGrid = new() { Filters = new List<SearchGrid>() };
            objGrid.Filters.Add(new SearchGrid() { FieldName = "Id", FieldValue = request.ReportId.ToString() });
            objGrid.Filters.Add(new SearchGrid() { FieldName = "MenuId", FieldValue = "0" });
            objGrid.First = 0;
            objGrid.Rows = 0;
            IPagedList<MReportListDto> MReportConfigList = await _reportService.MReportListDto(objGrid);
            if (MReportConfigList.Count > 0)
            {
                model.colList = MReportConfigList[0].ReportColumn.Split(',');
                model.SearchFields = request.SearchFields;
                model.Mode = MReportConfigList[0].ReportMode;
                model.RepoertName = MReportConfigList[0].ReportName;
                model.headerList = MReportConfigList[0].ReportHeader.Split(',');
                model.totalFieldList = MReportConfigList[0].ReportTotalField.Split(',');
                model.groupByLabel = MReportConfigList[0].ReportGroupByLabel;
                model.summaryLabel = MReportConfigList[0].SummaryLabel;
                model.columnWidths = MReportConfigList[0].ReportColumnWidth.Split(',');
                model.htmlFilePath = MReportConfigList[0].ReportBodyFile;
                model.htmlHeaderFilePath = MReportConfigList[0].ReportHeaderFile;
                model.SPName = MReportConfigList[0].ReportSpname;
                model.FolderName = MReportConfigList[0].ReportFolderName;
                model.FileName = MReportConfigList[0].ReportFileName;
                model.vPageOrientation = MReportConfigList[0].ReportPageOrientation;
            }
            model.StorageBaseUrl = Convert.ToString(_configuration["StorageBaseUrl"]);
            string byteFile = _reportService.GetNewReportSetByProc(model);
            return Ok(ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Report.", new { base64 = byteFile }));
        }
        [HttpPost("NewExportExcelReport")]
        public async Task<IActionResult> NewExcelReport(NewReportRequestDto request)
        {
            ReportConfigDto model = new();
            GridRequestModel objGrid = new() { Filters = new List<SearchGrid>() };
            objGrid.Filters.Add(new SearchGrid() { FieldName = "Id", FieldValue = request.ReportId.ToString() });
            objGrid.Filters.Add(new SearchGrid() { FieldName = "MenuId", FieldValue = "0" });
            objGrid.First = 0;
            objGrid.Rows = 0;
            IPagedList<MReportListDto> MReportConfigList = await _reportService.MReportListDto(objGrid);
            if (MReportConfigList.Count > 0)
            {
                model.colList = MReportConfigList[0].ReportColumn.Split(',');
                model.SearchFields = request.SearchFields;
                model.Mode = MReportConfigList[0].ReportMode;
                model.RepoertName = MReportConfigList[0].ReportName;
                model.headerList = MReportConfigList[0].ReportHeader.Split(',');
                model.totalFieldList = MReportConfigList[0].ReportTotalField.Split(',');
                model.groupByLabel = MReportConfigList[0].ReportGroupByLabel;
                model.summaryLabel = MReportConfigList[0].SummaryLabel;
                model.columnWidths = MReportConfigList[0].ReportColumnWidth.Split(',');
                model.htmlFilePath = MReportConfigList[0].ReportBodyFile;
                model.htmlHeaderFilePath = MReportConfigList[0].ReportHeaderFile;
                model.SPName = MReportConfigList[0].ReportSpname;
                model.FolderName = MReportConfigList[0].ReportFolderName;
                model.FileName = MReportConfigList[0].ReportFileName;
                model.vPageOrientation = MReportConfigList[0].ReportPageOrientation;
            }
            DataTable dt = _reportService.GetReportDataBySp(model);
            return Ok(ReportExcelHelper.GetExcel(model, dt));
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

        [HttpPost("new-vimal-html-pdf")]
        public async Task<IActionResult> NewVimalHtmlPdf()
        {
            string StorageBaseUrl = Convert.ToString(_configuration["StorageBaseUrl"]);
            string byteFile = _reportService.GeneratePdfFromSp("ps_getMultipleTabForReport", StorageBaseUrl);
            return Ok(ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Report.", new { base64 = byteFile }));
        }
    }
}
