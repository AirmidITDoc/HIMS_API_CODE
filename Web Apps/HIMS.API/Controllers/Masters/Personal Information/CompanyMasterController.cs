using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Controllers;

namespace HIMS.API.Controllers
{
    public class CompanyMasterController : BaseController
    {
        private readonly IGenericService<CompanyMaster> _repository;
        public CompanyMasterController(IGenericService<CompanyMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "CompanyMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<CompanyMaster> CompanyMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(CompanyMasterList.ToGridResponse(objGrid, "Company List"));
        }
        [HttpGet("{id?}")]
        [Permission(PageCode = "CompanyMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.CompanyId == id);
            return data.ToSingleResponse<CompanyMaster, CompanyMasterModel>("CompanyMaster");
        }


        [HttpPost]
        [Permission(PageCode = "CompanyMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(CompanyMasterModel obj)
        {
            CompanyMaster model = obj.MapTo<CompanyMaster>();
            model.IsActive = true;
            if (obj.CompanyId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Company added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "CompanyMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(CompanyMasterModel obj)
        {
            CompanyMaster model = obj.MapTo<CompanyMaster>();
            model.IsActive = true;
            if (obj.CompanyId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Company updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "CompanyMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            CompanyMaster model = await _repository.GetById(x => x.CompanyId == Id);
            if ((model?.CompanyId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Company deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }



    }
}
