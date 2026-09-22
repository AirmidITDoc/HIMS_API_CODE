using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Diet;
using HIMS.API.Models.DietKitchen;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.DietKitchen;
using HIMS.Services.DietkitchenMaster;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.DietMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class FoodItemMasterController : BaseController
    {
        private readonly IGenericService<MFoodItemMaster> _repository;
        private readonly IFoodItemmasterService _FoodItemmasterService;

        public FoodItemMasterController(IGenericService<MFoodItemMaster> repository, IFoodItemmasterService repository1)
        {
            _repository = repository;
            _FoodItemmasterService = repository1;

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
        [HttpPost("Insert")]
        [Permission]
        public async Task<ApiResponse> Post(FoodItemMasterModel obj)
        {
            MFoodItemMaster model = obj.MapTo<MFoodItemMaster>();
            model.Active = true; 
            if (obj.FoodItemId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                await _FoodItemmasterService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        //// Edit / Update API
        //[HttpPut("{id:int}")]
        ////[Permission(PageCode = "DietMaster", Permission = PagePermission.Edit)]
        //public async Task<ApiResponse> Edit(FoodItemMasterModel obj)
        //{
        //    MFoodItemMaster model = obj.MapTo<MFoodItemMaster>();
        //    model.Active = true; 
        //    if (obj.FoodItemId == 0)
        //    {
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    }
        //    else
        //    {
        //        model.ModifiedBy = CurrentUserId;
        //        model.ModifiedDate = AppTime.Now;
        //        await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        //}


        [HttpPut("Edit/{id:int}")]
        [Permission]
        public async Task<ApiResponse> UpdateAsync(FoodItemMasterModel obj)
        {
            if (obj.FoodItemId == 0)

                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

            MFoodItemMaster model = obj.MapTo<MFoodItemMaster>();


            model.Active = true;
            model.ModifiedDate = AppTime.Now;
            model.ModifiedBy = CurrentUserId;

            await _FoodItemmasterService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });


            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.FoodItemId);
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

        [HttpGet]
        [Route("get-FoodItemMaster")]
        [Permission]
        public async Task<ApiResponse> GetDropdown2()
        {
            var MMasterList = await _repository.GetAll();
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Food Item Master  dropdown", MMasterList.Select(x => new { x.FoodItemId, x.FoodCode, x.FoodName ,x.FoodCategoryId ,x.LocalName ,x.Unit,x.IsVegetarian}));
        }

    }
}
