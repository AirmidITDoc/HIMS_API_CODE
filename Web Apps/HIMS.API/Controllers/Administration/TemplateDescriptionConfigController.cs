using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Administration;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Administration;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class TemplateDescriptionConfigController : BaseController
    {
        private readonly IGenericService<MReportTemplateConfig> _repository;
        private readonly ITemplateDescriptionConfigService _ITemplateDescriptionConfigService;

        public TemplateDescriptionConfigController(IGenericService<MReportTemplateConfig> repository, ITemplateDescriptionConfigService repository1)
        {
            _repository = repository;
            _ITemplateDescriptionConfigService = repository1;

        }
        [HttpPost("TemlateByCategoryList")]
        [Permission(PageCode = "TemplateDescription", Permission = PagePermission.View)]
        public async Task<IActionResult> ListTemplate(GridRequestModel objGrid)
        {
            IPagedList<TemplateByCategoryListDto> TemlateByCategoryList = await _ITemplateDescriptionConfigService.GetListAsync(objGrid);
            return Ok(TemlateByCategoryList.ToGridResponse(objGrid, "TemlateByCategory List"));
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "TemplateDescription", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MReportTemplateConfig> MReportTemplateConfigList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MReportTemplateConfigList.ToGridResponse(objGrid, "MReport Template Config List"));
        }

        //Add API
        [HttpPost("Insert")]
        [Permission(PageCode = "TemplateDescription", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ReportTemplateConfigModel obj)
        {
            MReportTemplateConfig model = obj.MapTo<MReportTemplateConfig>();
            model.IsActive = true;

            if (obj.TemplateId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }

        //Edit API
        [HttpPut("Update{id:int}")]
        [Permission(PageCode = "TemplateDescription", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ReportTemplateConfigModel obj)
        {
            MReportTemplateConfig model = obj.MapTo<MReportTemplateConfig>();
            model.IsActive = true;
            if (obj.TemplateId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        //Delete API
        [HttpDelete]
        [Permission(PageCode = "TemplateDescription", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MReportTemplateConfig? model = await _repository.GetById(x => x.TemplateId == Id);
            if ((model?.TemplateId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
        [HttpGet("GetDischargeTemplateList")]
        //[Permission(PageCode = "TemplateDescription", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDischargeTemplateList(string CategoryName)
        {
            var result = await _ITemplateDescriptionConfigService.GetDischargeTemplateListAsync(CategoryName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GetDischargeTemplate List", result);
        }

    }
}
