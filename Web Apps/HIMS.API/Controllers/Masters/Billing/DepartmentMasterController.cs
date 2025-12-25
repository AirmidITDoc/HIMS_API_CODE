using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.Billing
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DepartmentMasterController : BaseController
    {
        private readonly IGenericService<MDepartmentMaster> _repository;
        public DepartmentMasterController(IGenericService<MDepartmentMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "DepartmentMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MDepartmentMaster> MDepartmentMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MDepartmentMasterList.ToGridResponse(objGrid, "Department List"));
        }
        //List API
        [HttpGet]
        [Route("get-departments")]
        [Permission(PageCode = "DepartmentMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown()
        {
            var MDepartmentMasterList = await _repository.GetAll(x => x.IsActive.Value);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Department dropdown", MDepartmentMasterList.Select(x => new { x.DepartmentName, x.DepartmentId }));
        }

        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "DepartmentMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.DepartmentId == id);
            return data.ToSingleResponse<MDepartmentMaster, DepartmentMasterModel>("DepartmentMaster ");
        }
        [HttpPost]
        [Permission(PageCode = "DepartmentMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(DepartmentMasterModel obj)
        {
            MDepartmentMaster model = obj.MapTo<MDepartmentMaster>();
            model.IsActive = true;
            if (obj.DepartmentId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "DepartmentMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(DepartmentMasterModel obj)
        {
            MDepartmentMaster model = obj.MapTo<MDepartmentMaster>();
            model.IsActive = true;
            if (obj.DepartmentId == 0)
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
        [Permission(PageCode = "DepartmentMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MDepartmentMaster model = await _repository.GetById(x => x.DepartmentId == Id);
            if ((model?.DepartmentId ?? 0) > 0)
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


    }
}


