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
using HIMS.Services.DietkitchenMaster;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.DietKitchenMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class FoodPreferenceController : BaseController
    {
        private readonly IGenericService<MFoodPreferenceMaster> _repository;
        private readonly IFoodPreferenceMasterService _FoodPreferenceMasterService;

        public FoodPreferenceController(IGenericService<MFoodPreferenceMaster> repository, IFoodPreferenceMasterService foodPreferenceMasterService)
        {
            _repository = repository;
            _FoodPreferenceMasterService = foodPreferenceMasterService;
        }

        // List API
        [HttpPost]
        [Route("[action]")]
        [Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MFoodPreferenceMaster> list = await _repository.GetAllPagedAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "Food Preference List"));
        }

        // Get By Id API
        [HttpGet("{id?}")]
        [Permission]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0) return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            var data = await _repository.GetById(x => x.FoodPreferenceId == id);
            return data.ToSingleResponse<MFoodPreferenceMaster, FoodPreferenceModel>("FoodPreferenceMaster");
        }

        // Post / Insert API
        [HttpPost("Insert")]
        [Permission]
        public async Task<ApiResponse> Post(FoodPreferenceModel obj)
        {
            MFoodPreferenceMaster model = obj.MapTo<MFoodPreferenceMaster>();
            model.Active = true;
            if (obj.FoodPreferenceId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _FoodPreferenceMasterService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        // Edit / Update API
        [HttpPut("Edit/{id:int}")]
        [Permission]
        public async Task<ApiResponse> UpdateAsync(FoodPreferenceModel obj)
        {
            if (obj.FoodPreferenceId == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            MFoodPreferenceMaster model = obj.MapTo<MFoodPreferenceMaster>();

            model.Active = true;
            model.ModifiedBy = CurrentUserId;
            model.ModifiedDate = AppTime.Now;

            await _FoodPreferenceMasterService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.FoodPreferenceId);
        }

        // Delete API (Soft Delete / Toggle Status)
        [HttpDelete]
        [Permission]
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