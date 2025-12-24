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

namespace HIMS.API.Controllers.Masters.SurgeryCategoryMasterController
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class SurgeryCategoryMasterController : BaseController
    {
        private readonly IGenericService<MOtSurgeryCategoryMaster> _repository;
        public SurgeryCategoryMasterController(IGenericService<MOtSurgeryCategoryMaster> repository)
        {
            _repository = repository;
        }

        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "OTManagement", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MOtSurgeryCategoryMaster> MSurgeryCategoryMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MSurgeryCategoryMasterList.ToGridResponse(objGrid, "SurgeryCategory Master List"));
        }

        [HttpGet("{id?}")]
        [Permission(PageCode = "OTManagement", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.SurgeryCategoryId == id);
            return data.ToSingleResponse<MOtSurgeryCategoryMaster, SurgeryCategoryMasterModel>("MOtSurgeryCategoryMaster");
        }
        //Insert API
        [HttpPost]
        [Permission(PageCode = "OTManagement", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(SurgeryCategoryMasterModel obj)
        {
            MOtSurgeryCategoryMaster model = obj.MapTo<MOtSurgeryCategoryMaster>();
            model.IsActive = true;
            if (obj.SurgeryCategoryId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record   added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "OTManagement", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(SurgeryCategoryMasterModel obj)
        {
            MOtSurgeryCategoryMaster model = obj.MapTo<MOtSurgeryCategoryMaster>();
            model.IsActive = true;
            if (obj.SurgeryCategoryId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }


        //Delete API
        [HttpDelete]
        [Permission(PageCode = "OTManagement", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MOtSurgeryCategoryMaster? model = await _repository.GetById(x => x.SurgeryCategoryId == Id);
            if ((model?.SurgeryCategoryId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}