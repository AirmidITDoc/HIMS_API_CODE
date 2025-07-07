using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Inventory.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Services.Administration;
using HIMS.Data.DTO.Administration;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class ReportConfigController : BaseController
    {
        private readonly IReportConfigService _ReportConfigService;
        public ReportConfigController(IReportConfigService  repository)
        {
            _ReportConfigService = repository;
        }
        [HttpPost("MReportConfigDetailsList")]
        // [Permission(PageCode = "Report", Permission = PagePermission.View)]
        public async Task<IActionResult> MReportConfigList(GridRequestModel objGrid)
        {
            IPagedList<MReportConfigListDto> MReportConfigList = await _ReportConfigService.MReportConfigList(objGrid);
            return Ok(MReportConfigList.ToGridResponse(objGrid, "MReportConfigDetails  List"));
        }

        [HttpPost("ReportConfigsave")]
        //[Permission(PageCode = "Report", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(ReportConfigModel obj)
        {
            MReportConfig model = obj.MapTo<MReportConfig>();
            if (obj.ReportId == 0)
            {
                model.CreatedOn = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.IsActive = true;
                await _ReportConfigService.InsertAsyncm(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        [HttpPut("ReportConfig/{id:int}")]
        //[Permission(PageCode = "Report", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ReportConfigModel obj)
        {
            MReportConfig model = obj.MapTo<MReportConfig>();
            if (obj.ReportId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.UpdatedOn = DateTime.Now;
                model.UpdateBy = CurrentUserId;
                model.IsActive = true;
                await _ReportConfigService.UpdateAsyncm(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

    }
}
