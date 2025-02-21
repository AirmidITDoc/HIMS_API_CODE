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

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class ReportConfigController : BaseController
    {
        private readonly IGenericService<MReportConfig> _repository;
        public ReportConfigController(IGenericService<MReportConfig> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "ReportConfig", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MReportConfig> MReportConfigList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MReportConfigList.ToGridResponse(objGrid, "Report List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "ReportConfig", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ReportId == id);
            return data.ToSingleResponse<MReportConfig, ReportConfigModel>("MReportConfig");
        }
        //Add API
        [HttpPost]
        [Permission(PageCode = "ReportConfig", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ReportConfigModel obj)
        {
            MReportConfig model = obj.MapTo<MReportConfig>();
            model.IsActive = true;
            if (obj.ReportId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedOn = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "ReportConfig added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "ReportConfig", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ReportConfigModel obj)
        {
            MReportConfig model = obj.MapTo<MReportConfig>();
            model.IsActive = true;
            if (obj.ReportId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.UpdateBy = CurrentUserId;
                model.UpdatedOn = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "UpdateBy", "UpdatedOn" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "ReportConfig updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "ReportConfig", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MReportConfig model = await _repository.GetById(x => x.ReportId == Id);
            if ((model?.ReportId ?? 0) > 0)
            {
                model.IsActive = false;
                model.UpdateBy = CurrentUserId;
                model.UpdatedOn = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "ReportConfig deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

    }
}
