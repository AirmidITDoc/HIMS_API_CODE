using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.Common;
using HIMS.Services.Inventory;
using HIMS.Services.IPPatient;
using HIMS.Services.Nursing;
using HIMS.Services.OPPatient;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PathologyController : BaseController
    {
        private readonly IPathlogySampleCollectionService _IPathlogySampleCollectionService;
        private readonly ILabRequestService _ILabRequestService;
        private readonly IPathlogyService _IPathlogyService;
        private readonly IPathologyResultEntryService _IPathologyResultEntryService;
        public PathologyController(IPathlogySampleCollectionService repository, ILabRequestService repository1, IPathlogyService repository2, IPathologyResultEntryService repository3)
        {
            _IPathlogySampleCollectionService = repository;
            _ILabRequestService = repository1;
            _IPathlogyService = repository2;
            _IPathologyResultEntryService = repository3;
        }
        [HttpPost("PathTemplateForUpdateList")]
        //[Permission(PageCode = "PathTemplateForUpdateList", Permission = PagePermission.View)]
        public async Task<IActionResult> PathTemplateForUpdateList(GridRequestModel objGrid)
        {
            IPagedList<PathTemplateForUpdateListDto> PathTestForUpdateList = await _IPathlogyService.PathTemplateForUpdateList(objGrid);
            return Ok(PathTestForUpdateList.ToGridResponse(objGrid, "PathTemplateForUpdate List"));
        }


        [HttpPost("PathTestForUpdateList")]
        //[Permission(PageCode = "PathTemplateForUpdateList", Permission = PagePermission.View)]
        public async Task<IActionResult> PathTestForUpdateList(GridRequestModel objGrid)
        {
            IPagedList<PathTestForUpdateListdto> PathTestForUpdateList= await _IPathlogyService.PathTestForUpdateList(objGrid);
            return Ok(PathTestForUpdateList.ToGridResponse(objGrid, "PathTestForUpdate List"));
        }

        [HttpPost("PathParaFillList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PathParaFillListList(GridRequestModel objGrid)
        {
            IPagedList<PathParaFillListDto> PathParaFillList = await _IPathlogyService.PathParaFillList(objGrid);
            return Ok(PathParaFillList.ToGridResponse(objGrid, "PathParaFillList App List"));
        }
        [HttpPost("PathSubtestFillList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PathSubtestFillList(GridRequestModel objGrid)
        {
            IPagedList<PathSubtestFillListDto> PathSubtestFillList = await _IPathlogyService.PathSubtestFillList(objGrid);
            return Ok(PathSubtestFillList.ToGridResponse(objGrid, "PathSubtestFill App List"));
        }
        [HttpPost("PathologyTestList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PathologyTestList(GridRequestModel objGrid)
        {
            IPagedList<PathologyTestListDto> PathologyTestList = await _IPathlogyService.PathologyTestList(objGrid);
            return Ok(PathologyTestList.ToGridResponse(objGrid, "PathologyTest App List"));
        }
      
        [HttpPost("LabOrRadRequestList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> LabOrRadRequestList(GridRequestModel objGrid)
        {
            IPagedList<LabOrRadRequestListDto> LabOrRadRequestList = await _IPathlogyService.LGetListAsync(objGrid);
            return Ok(LabOrRadRequestList.ToGridResponse(objGrid, "LabOrRadRequestList "));
        }

        [HttpPost("PathologyPatientTestList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PatientList(GridRequestModel objGrid)
        {
            IPagedList<PatientTestListDto> PatientList = await _IPathlogyService.PGetListAsync(objGrid);
            return Ok(PatientList.ToGridResponse(objGrid, "PathologyPatientTestList "));
        }


        [HttpPost("PathResultEntryList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PathResultEntryList(GridRequestModel objGrid)
        {
            IPagedList<PathResultEntryListDto> PathResultEntryList = await _IPathlogyService.PathResultEntry(objGrid);
            return Ok(PathResultEntryList.ToGridResponse(objGrid, "PathResultEntryList"));
        }
        [HttpPost("InsertResultEntry")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(PathologyResultEntryModel obj)
        {
            TPathologyReportDetail model = obj.MapTo<TPathologyReportDetail>();
            if (obj.PathReportDetId == 0)
            {
                //model.PathDate = Convert.ToDateTime(obj.PathDate);
                //model.AddedBy = CurrentUserId;

                await _IPathologyResultEntryService.InsertAsyncResultEntry(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Pathology Result Entry added successfully.");
        }

        [HttpPost("InsertTemplateResult")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PathTemplateResultModel obj)
        {
            TPathologyReportTemplateDetail model = obj.MapTo<TPathologyReportTemplateDetail>();
            if (obj.PathReportTemplateDetId == 0)
            {
                //model.PathDate = Convert.ToDateTime(obj.PathDate);
                //model.AddedBy = CurrentUserId;

                await _IPathologyResultEntryService.InsertAsyncTemplateResult(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Pathology Template Result  added successfully.");
        }


        [HttpPost("Cancel")]
        //[Permission(PageCode = "VisitDetail", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(PathologyResultEntryModel obj)
        {
            TPathologyReportDetail model = new();
            if (obj.PathReportDetId != 0)
            {
                //model.IndentId = obj.IndentId;
                //model.Isclosed = true;
                //model.IsCancelledDate = DateTime.Now;
                await _IPathologyResultEntryService.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Pathology Result Entry Canceled successfully.");
        }

    }
}
