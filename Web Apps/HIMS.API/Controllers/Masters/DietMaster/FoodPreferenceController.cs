using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Diet;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.DietMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class FoodPreferenceController : BaseController
    {
        private readonly IGenericService<MFoodPreferenceMaster> _repository;

        public FoodPreferenceController(IGenericService<MFoodPreferenceMaster> repository)
        {
            _repository = repository;
        }

        // List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "DietMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MFoodPreferenceMaster> list = await _repository.GetAllPagedAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "Food Preference List"));
        }

        // Get By Id API
        [HttpGet("{id?}")]
        //[Permission(PageCode = "DietMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }

            var data = await _repository.GetById(x => x.FoodPreferenceId == id);
            return data.ToSingleResponse<MFoodPreferenceMaster, FoodPreferenceModel>("FoodPreferenceMaster");
        }

        // Post / Insert API
        [HttpPost]
        //[Permission(PageCode = "DietMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(FoodPreferenceModel obj)
        {
            MFoodPreferenceMaster model = obj.MapTo<MFoodPreferenceMaster>();
            model.Active = true;
            if (obj.FoodPreferenceId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        // Edit / Update API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "DietMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(FoodPreferenceModel obj)
        {
            MFoodPreferenceMaster model = obj.MapTo<MFoodPreferenceMaster>();
            model.Active = true;
            if (obj.FoodPreferenceId == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        // Delete API (Soft Delete / Toggle Status)
        [HttpDelete]
        //[Permission(PageCode = "DietMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(long Id)
        {
            MFoodPreferenceMaster? model = await _repository.GetById(x => x.FoodPreferenceId == Id);
            if ((model?.FoodPreferenceId ?? 0) > 0)
            {
                model!.Active = model.Active == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
        }
    }
}