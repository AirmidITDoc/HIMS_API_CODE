using Asp.Versioning;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Controllers;
using HIMS.Core.Domain.Grid;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Inventory.Masters;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class RoleMasterController : BaseController
    {
        private readonly IGenericService<RoleMaster> _repository;
        public RoleMasterController(IGenericService<RoleMaster> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<RoleMaster> RoleMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(RoleMasterList.ToGridResponse(objGrid, "RoleMaster  List"));
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "RoleMaster  added successfully.");
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "RoleMaster  updated successfully.");
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
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "RoleMaster  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
