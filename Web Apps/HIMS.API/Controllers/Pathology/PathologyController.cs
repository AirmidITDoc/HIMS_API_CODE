﻿using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.Common;
using HIMS.Services.Inventory;
using HIMS.Services.IPPatient;
using HIMS.Services.Nursing;
using HIMS.Services.OPPatient;
using HIMS.Services.OutPatient;
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
        public PathologyController(IPathlogySampleCollectionService repository, ILabRequestService repository1, IPathlogyService repository2)
        {
            _IPathlogySampleCollectionService = repository;
            _ILabRequestService = repository1;
            _IPathlogyService = repository2;
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


        [HttpPost("Path_TestList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PathTestList(GridRequestModel objGrid)
        {
            IPagedList<TestListDTo> TestList = await _IPathlogyService.GetPathTestListAsync(objGrid);
            return Ok(TestList.ToGridResponse(objGrid, "Path Test List "));
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
        //[Permission(PageCode = "Advance", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PathologyResultModel obj)
        {
            TPathologyReportDetail model = obj.PathologyResult.MapTo<TPathologyReportDetail>();
            TPathologyReportHeader objTPathology = obj.PathologyReport.MapTo<TPathologyReportHeader>();
            if (model.PathReportDetId == 0)
            {
                objTPathology.ReportDate = Convert.ToDateTime(objTPathology.ReportDate);
                objTPathology.ReportTime = Convert.ToDateTime(objTPathology.ReportTime);

                await _IPathlogyService.InsertAsyncResultEntry(model, objTPathology, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PathologyResult Entry  added successfully.");
        }
       
    }
}
