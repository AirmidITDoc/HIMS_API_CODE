using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Models.Masters;
using Asp.Versioning;
using HIMS.API.Models.Administration;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class TemplateDescriptionConfigController : BaseController
    {
        private readonly IGenericService<MReportTemplateConfig> _repository;
        public TemplateDescriptionConfigController(IGenericService<MReportTemplateConfig> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "TemplateDescription", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MReportTemplateConfig> MReportTemplateConfigList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MReportTemplateConfigList.ToGridResponse(objGrid, "MReport Template Config List"));
        }

        //Add API
        [HttpPost("Insert")]
        //[Permission(PageCode = "TemplateDescription", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ReportTemplateConfigModel obj)
        {
            MReportTemplateConfig model = obj.MapTo<MReportTemplateConfig>();
            model.IsActive = true;

            if (obj.TemplateId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }

        //Edit API
        [HttpPut("Update{id:int}")]
        //[Permission(PageCode = "TemplateDescription", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ReportTemplateConfigModel obj)
        {
            MReportTemplateConfig model = obj.MapTo<MReportTemplateConfig>();
            model.IsActive = true;
            if (obj.TemplateId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "TemplateDescription", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MReportTemplateConfig? model = await _repository.GetById(x => x.TemplateId == Id);
            if ((model?.TemplateId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
