using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.InventoryMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ItemCategoryMasterController : BaseController
    {
        private readonly IGenericService<MItemCategoryMaster> _repository;
        public ItemCategoryMasterController(IGenericService<MItemCategoryMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "ItemCategoryMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MItemCategoryMaster> ItemCategoryList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(ItemCategoryList.ToGridResponse(objGrid, "Item Category List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "ItemCategoryMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ItemCategoryId == id);
            return data.ToSingleResponse<MItemCategoryMaster, ItemCategoryModel>("Item CategoryMaster");
        }
        //Add API
        [HttpPost]
        [Permission(PageCode = "ItemCategoryMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ItemCategoryModel obj)
        {
            MItemCategoryMaster model = obj.MapTo<MItemCategoryMaster>();
            model.IsActive = true;
            if (obj.ItemCategoryId == 0)
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
        [Permission(PageCode = "ItemCategoryMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ItemCategoryModel obj)
        {
            MItemCategoryMaster model = obj.MapTo<MItemCategoryMaster>();
            model.IsActive = true;
            if (obj.ItemCategoryId == 0)
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
        [Permission(PageCode = "ItemCategoryMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MItemCategoryMaster model = await _repository.GetById(x => x.ItemCategoryId == Id);
            if ((model?.ItemCategoryId ?? 0) > 0)
            {
                model.IsActive = false;
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
