using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Diet;
using HIMS.API.Models.DietKitchen;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.DietKitchen;
using HIMS.Services.Transaction;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.DietKitchen
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class FoodCategoryMasterController : BaseController
    {
        private readonly IGenericService<MFoodCategoryMaster> _repository;

        public FoodCategoryMasterController(IGenericService<MFoodCategoryMaster> repository)
        {
            _repository = repository;
        }
        // List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "DietMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MFoodCategoryMaster> list = await _repository.GetAllPagedAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "Food Item List"));
        }

        // Get By Id API
        [HttpGet("{id?}")]
        //[Permission(PageCode = "MFoodCategoryMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }

            var data = await _repository.GetById(x => x.FoodCategoryId == id);
            return data.ToSingleResponse<MFoodCategoryMaster, FoodCategorymasterModel>("FoodItemMaster");
        }

        [HttpPost]
        //[Permission(PageCode = "MFoodCategoryMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(FoodCategorymasterModel obj)
        {
            MFoodCategoryMaster model = obj.MapTo<MFoodCategoryMaster>();
            model.Active = true;
            if (obj.FoodCategoryId == 0)
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


        [HttpPut("{id:int}")]
        //[Permission(PageCode = "MFoodCategoryMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(FoodCategorymasterModel obj)
        {
            MFoodCategoryMaster model = obj.MapTo<MFoodCategoryMaster>();
            model.Active = true;
            if (obj.FoodCategoryId == 0)
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


        [HttpDelete]
        //[Permission(PageCode = "MFoodCategoryMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(long Id)
        {
            MFoodCategoryMaster? model = await _repository.GetById(x => x.FoodCategoryId == Id);
            if ((model?.FoodCategoryId ?? 0) > 0)
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



