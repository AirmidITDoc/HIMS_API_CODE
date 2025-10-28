using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class RoleTemplateController : BaseController
    {
        private readonly IGenericService<RoleTemplateMaster> _repository;
        public RoleTemplateController(IGenericService<RoleTemplateMaster> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "RoleTemplateMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<RoleTemplateMaster> RoleTemplateList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(RoleTemplateList.ToGridResponse(objGrid, "RoleTemplateList"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "RoleTemplateMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.RoleId == id);
            return data.ToSingleResponse<RoleTemplateMaster, RoleTemplateModel>("RoleTemplate");
        }
        //Add API
        [HttpPost]
        [Permission(PageCode = "RoleTemplateMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(RoleTemplateModel obj)
        {
            RoleTemplateMaster model = obj.MapTo<RoleTemplateMaster>();
            model.IsActive = true;
            if (obj.RoleId == 0)
            {
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "RoleTemplateMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(RoleTemplateModel obj)
        {
            RoleTemplateMaster model = obj.MapTo<RoleTemplateMaster>();
            model.IsActive = true;
            if (obj.RoleId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "RoleTemplateMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            RoleTemplateMaster model = await _repository.GetById(x => x.RoleId == Id);
            if ((model?.RoleId ?? 0) > 0)
            {
                model.IsActive = false;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
