using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Administration;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.Administration;
using HIMS.Services.Masters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class BarcodeConfigController : BaseController
    {
        private readonly IGenericService<BarcodeConfigMaster> _repository;
        private readonly IBarcodeConfigService _IBarcodeConfigService;
        public BarcodeConfigController(IGenericService<BarcodeConfigMaster> repository, IBarcodeConfigService barcodeConfigService)
        {
            _repository = repository;
            _IBarcodeConfigService = barcodeConfigService;
        }
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "BarcodeConfig", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<BarcodeConfigMaster> DocList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(DocList.ToGridResponse(objGrid, "Barcode Config List"));
        }
        [HttpGet("{id?}")]
        [Permission(PageCode = "BarcodeConfig", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.Id == id);
            return data.ToSingleResponse<BarcodeConfigMaster, BarcodeConfigModel>("Barcode Config");
        }

        [HttpPost]
        [Permission(PageCode = "BarcodeConfig", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(BarcodeConfigModel obj)
        {
            BarcodeConfigMaster model = obj.MapTo<BarcodeConfigMaster>();
            model.IsActive = true;

            if (obj.Id == 0)
            {
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        [HttpPut("{id:int}")]
        [Permission(PageCode = "BarcodeConfig", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(BarcodeConfigModel obj)
        {
            BarcodeConfigMaster model = obj.MapTo<BarcodeConfigMaster>();
            model.IsActive = true;

            if (obj.Id == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                await _repository.Update(model, CurrentUserId, CurrentUserName, null);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
        [HttpDelete]
        [Permission(PageCode = "BarcodeConfig", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            BarcodeConfigMaster model = await _repository.GetById(x => x.Id == Id);
            if ((model?.Id ?? 0) > 0)
            {
                model.IsActive = model.IsActive != true;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
        [HttpGet]
        [Route("get-barcodeconfigs")]
        [Permission(PageCode = "BarcodeConfig", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown()
        {
            var MDepartmentMasterList = await _repository.GetAll(x => x.IsActive);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Barcode Config dropdown", MDepartmentMasterList.Select(x => new { x.Id, x.TemplateCode }));
        }
    }
}
