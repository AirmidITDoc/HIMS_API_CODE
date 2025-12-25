using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.Administration;
using HIMS.Data.Models;
using HIMS.Services.Administration;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class ReportConfigController : BaseController
    {
        private readonly IReportConfigService _ReportConfigService;
        private readonly IGenericService<MReportConfig> _repository1;

        public ReportConfigController(IGenericService<MReportConfig> repository1, IReportConfigService repository)
        {
            _ReportConfigService = repository;
            _repository1 = repository1;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "ReportConfig", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MReportConfig> MReportConfigList = await _repository1.GetAllPagedAsync(objGrid);
            return Ok(MReportConfigList.ToGridResponse(objGrid, "MReportConfig List"));
        }

        [HttpPost("NewList")]
        [Permission(PageCode = "ReportConfig", Permission = PagePermission.View)]
        public async Task<IActionResult> MReportConfigList(GridRequestModel objGrid)
        {
            IPagedList<MReportConfigListDto> MReportConfigList = await _ReportConfigService.MReportConfigList(objGrid);
            return Ok(MReportConfigList.ToGridResponse(objGrid, "MReportConfigDetails  List"));
        }

        //Add API
        [HttpPost]
        [Permission(PageCode = "ReportConfig", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(MReportConfigModel obj)
        {
            MReportConfig model = obj.MapTo<MReportConfig>();
            model.IsActive = true;
            if (obj.ReportId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedOn = AppTime.Now;
                await _repository1.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "ReportConfig", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edits(MReportConfigModel obj)
        {
            MReportConfig model = obj.MapTo<MReportConfig>();
            model.IsActive = true;
            if (obj.ReportId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.UpdateBy = CurrentUserId;
                model.UpdatedOn = AppTime.Now;
                await _repository1.Update(model, CurrentUserId, CurrentUserName, new string[2] { "UpdateBy", "UpdatedOn" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }


        [HttpDelete]
        [Permission(PageCode = "ReportConfig", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MReportConfig model = await _repository1.GetById(x => x.ReportId == Id);
            if ((model?.ReportId ?? 0) > 0)
            {
                model.IsActive = false;
                model.UpdateBy = CurrentUserId;
                model.UpdatedOn = AppTime.Now;
                await _repository1.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }



        [HttpPost("ReportConfigsave")]
        [Permission(PageCode = "ReportConfig", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(ReportConfigModel obj)
        {
            MReportConfig model = obj.MapTo<MReportConfig>();
            if (obj.ReportId == 0)
            {
                model.CreatedOn = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.IsActive = true;
                await _ReportConfigService.InsertAsyncm(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        [HttpPut("ReportConfig/{id:int}")]
        [Permission(PageCode = "ReportConfig", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ReportConfigModel obj)
        {
            MReportConfig model = obj.MapTo<MReportConfig>();
            if (obj.ReportId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.UpdatedOn = AppTime.Now;
                model.UpdateBy = CurrentUserId;
                model.IsActive = true;
                await _ReportConfigService.UpdateAsyncm(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }






    }
}
