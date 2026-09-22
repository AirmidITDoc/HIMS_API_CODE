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
    public class FoodItemMasterController : BaseController
    {
        private readonly IGenericService<MFoodItemMaster> _repository;

        public FoodItemMasterController(IGenericService<MFoodItemMaster> repository)
        {
            _repository = repository;
        }

        // List API
        [HttpPost]
        [Route("[action]")]
        [Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MFoodItemMaster> list = await _repository.GetAllPagedAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "Food Item List"));
        }

        // Get By Id API
        [HttpGet("{id?}")]
        [Permission]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }

            var data = await _repository.GetById(x => x.FoodItemId == id);
            return data.ToSingleResponse<MFoodItemMaster, FoodItemMasterModel>("FoodItemMaster");
        }

        // Post / Insert API
        [HttpPost]
        [Permission]
        public async Task<ApiResponse> Post(FoodItemMasterModel obj)
        {
            MFoodItemMaster model = obj.MapTo<MFoodItemMaster>();
            model.Active = true; 
            if (obj.FoodItemId == 0)
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
        [Permission]
        public async Task<ApiResponse> Edit(FoodItemMasterModel obj)
        {
            MFoodItemMaster model = obj.MapTo<MFoodItemMaster>();
            model.Active = true; 
            if (obj.FoodItemId == 0)
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
        [Permission]
        public async Task<ApiResponse> Delete(long Id)
        {
            MFoodItemMaster? model = await _repository.GetById(x => x.FoodItemId == Id);
            if ((model?.FoodItemId ?? 0) > 0)
            {
                model!.Active = model.Active == true ? false : true; // Status toggle kela
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