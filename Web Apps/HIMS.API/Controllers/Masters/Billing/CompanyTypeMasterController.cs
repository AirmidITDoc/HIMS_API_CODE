using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.Billing
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CompanyTypeMasterController : BaseController
    {
        private readonly IGenericService<CompanyTypeMaster> _repository;
        public CompanyTypeMasterController(IGenericService<CompanyTypeMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "CompanyTypeMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<CompanyTypeMaster> CompanyTypeMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(CompanyTypeMasterList.ToGridResponse(objGrid, "CompanyType List"));
        }
        [HttpGet("{id?}")]
        [Permission(PageCode = "CompanyTypeMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.CompanyTypeId == id);
            return data.ToSingleResponse<CompanyTypeMaster, CompanyTypeMasterModel>("CompanyTypeMaster");
        }


        [HttpPost]
        [Permission(PageCode = "CompanyTypeMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(CompanyTypeMasterModel obj)
        {
            CompanyTypeMaster model = obj.MapTo<CompanyTypeMaster>();
            model.IsActive = true;
            if (obj.CompanyTypeId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "CompanyTypeMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(CompanyTypeMasterModel obj)
        {
            CompanyTypeMaster model = obj.MapTo<CompanyTypeMaster>();
            model.IsActive = true;
            if (obj.CompanyTypeId == 0)
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
        [Permission(PageCode = "CompanyTypeMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            CompanyTypeMaster model = await _repository.GetById(x => x.CompanyTypeId == Id);
            if ((model?.CompanyTypeId ?? 0) > 0)
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
