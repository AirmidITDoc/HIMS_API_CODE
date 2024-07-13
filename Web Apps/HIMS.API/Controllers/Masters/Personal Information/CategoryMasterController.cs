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

namespace HIMS.API.Controllers.Masters.Personal_Information
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class CategoryMasterController : BaseController
    {
        private readonly IGenericService<MItemCategoryMaster> _repository;
        public CategoryMasterController(IGenericService<MItemCategoryMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "RadiologyCategoryMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MItemCategoryMaster> PatientTypeList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(PatientTypeList.ToGridResponse(objGrid, "Categoty List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "RadiologyCategoryMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ItemCategoryId == id);
            return data.ToSingleResponse<MItemCategoryMaster, CategoryMasterModel>("CategoryMaster");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "RadiologyCategoryMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(CategoryMasterModel obj)
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "ItemCategory added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "RadiologyCategoryMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(CategoryMasterModel obj)
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " Item Category updated successfully.");
        }

        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "RadiologyCategoryMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> delete(int Id)
        {
            MItemCategoryMaster model = await _repository.GetById(x => x.ItemCategoryId == Id);
            if ((model?.ItemCategoryId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Item Category deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
