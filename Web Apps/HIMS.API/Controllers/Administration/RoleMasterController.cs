using Asp.Versioning;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Controllers;
using HIMS.Core.Domain.Grid;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Inventory.Masters;
using HIMS.Services.Administration;
using HIMS.Data.DTO.User;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class RoleMasterController : BaseController
    {
        private readonly IGenericService<RoleMaster> _repository;
        private readonly IRoleService _IRoleService;
        public RoleMasterController(IGenericService<RoleMaster> repository,IRoleService roleService)
        {
            _repository = repository;
            _IRoleService = roleService;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<RoleMaster> RoleMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(RoleMasterList.ToGridResponse(objGrid, "Role Master List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.RoleId == id);
            return data.ToSingleResponse<RoleMaster, RoleMasterModel>("RoleMaster");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(RoleMasterModel obj)
        {
            RoleMaster model = obj.MapTo<RoleMaster>();
            model.IsActive = true;
            if (obj.RoleId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Role added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(RoleMasterModel obj)
        {
            RoleMaster model = obj.MapTo<RoleMaster>();
            model.IsActive = true;
            if (obj.RoleId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Role updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            RoleMaster model = await _repository.GetById(x => x.RoleId == Id);
            if ((model?.RoleId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Role deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
        [HttpGet]
        [Route("get-permissions")]
        public ApiResponse GetPermission(int RoleId)
        {
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Fetch Permission successfully.", _IRoleService.GetPermisison(RoleId));
        }
        [HttpPost]
        [Route("save-permission")]
        public ApiResponse PostPermission(List<PermissionModel> obj)
        {
            _IRoleService.SavePermission(obj);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Permission updated successfully.");
        }
    }
}
