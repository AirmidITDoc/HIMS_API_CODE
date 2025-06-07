using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.API.Models.Inventory.Masters;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ConfigurationController : BaseController
    {
        private readonly IGenericService<ConfigSetting> _repository;
        public ConfigurationController(IGenericService<ConfigSetting> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "Configuration", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ConfigSetting> ConfigSettingList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(ConfigSettingList.ToGridResponse(objGrid, "ConfigSettingList "));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "Configuration", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ConfigId == id);
            return data.ToSingleResponse<ConfigSetting, ConfigurationModel>("ConfigSetting");
        }
        //Add API
        [HttpPost]
        [Permission(PageCode = "Configuration", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ConfigurationModel obj)
        {
            ConfigSetting model = obj.MapTo<ConfigSetting>();
            //model.IsActive = true;
            if (obj.ConfigId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "Configuration", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ConfigurationModel obj)
        {
            ConfigSetting model = obj.MapTo<ConfigSetting>();
            //model.IsActive = true;
            if (obj.ConfigId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "Configuration", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            ConfigSetting model = await _repository.GetById(x => x.ConfigId == Id);
            if ((model?.ConfigId ?? 0) > 0)
            {
                //model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
