using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.OPPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;

using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PathlogySampleCollectionController : BaseController
    {
        private readonly IPathlogySampleCollectionService _IPathlogySampleCollectionService;
        public PathlogySampleCollectionController(IPathlogySampleCollectionService repository)
        {
            _IPathlogySampleCollectionService = repository;
        }
        [HttpPost("SampleCollectionPatientList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> SampleCollectionList(GridRequestModel objGrid)
        {
            IPagedList<SampleCollectionPatientListDto> SampleCollectionList = await _IPathlogySampleCollectionService.GetListAsync(objGrid);
            return Ok(SampleCollectionList.ToGridResponse(objGrid, "SampleCollectionList "));
        }
        [HttpPost("SampleCollectionTestList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> SampleCollectionTestList(GridRequestModel objGrid)
        {
            IPagedList<SampleCollectionTestListDto> SampleCollectionTestList = await _IPathlogySampleCollectionService.GetListAsyn(objGrid);
            return Ok(SampleCollectionTestList.ToGridResponse(objGrid, "SampleCollectionTestList "));
        }
        [HttpPost("LabOrRadRequestPatientList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> LabOrRadRequestList(GridRequestModel objGrid)
        {
            IPagedList<LabOrRadRequestListDto> LabOrRadRequestList = await _IPathlogySampleCollectionService.LGetListAsync(objGrid);
            return Ok(LabOrRadRequestList.ToGridResponse(objGrid, "LabOrRadRequestList "));
        }
        [HttpPost("LabOrRadRequestDetailList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> LabOrRadRequestDetailList(GridRequestModel objGrid)
        {
            IPagedList<LabOrRadRequestDetailListDto> LabOrRadRequestList = await _IPathlogySampleCollectionService.LGetListAsync1(objGrid);
            return Ok(LabOrRadRequestList.ToGridResponse(objGrid, "LabOrRadRequestDetail List "));
        }
        [HttpPost("PathRadServiceList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PathRadServiceList(GridRequestModel objGrid)
        {
            IPagedList<PathRadServiceListDto> PathRadServiceList = await _IPathlogySampleCollectionService.GetListAsync1(objGrid);
            return Ok(PathRadServiceList.ToGridResponse(objGrid, "PathRadService List "));
        }
        [HttpPut("Update")]
        //[Permission(PageCode = "Pathology", Permission = PagePermission.Edit)]
        [Permission]
        public async Task<ApiResponse> Update(PathlogySampleCollectionsModel obj)
        {
            List<TPathologyReportHeader> model = obj.PathlogySampleCollection.MapTo<List<TPathologyReportHeader>>();
            if (model.Count > 0)
            {
                await _IPathlogySampleCollectionService.UpdateAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Pathology Sample Collection updated successfully.");
        }



        [HttpPost("UpdateSamplecollectionDatetime")]
        //[Permission(PageCode = "Pathology", Permission = PagePermission.Edit)]
        [Permission]
        public async Task<ApiResponse> Verify(PathologyUpdateDateTimeModel obj)
        {
            TPathologyReportHeader model = obj.MapTo<TPathologyReportHeader>();
            if (obj.SampleNo != 0)
            {
                await _IPathlogySampleCollectionService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Updated successfully.");
        }

    }
}
