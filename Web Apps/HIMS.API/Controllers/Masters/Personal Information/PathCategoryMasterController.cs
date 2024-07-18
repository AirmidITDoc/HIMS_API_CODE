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
    public class PathCategoryMasterController : BaseController
    {
        private readonly IGenericService<MPathCategoryMaster> _repository;
        public PathCategoryMasterController(IGenericService<MPathCategoryMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "PathCategoryMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MPathCategoryMaster> PathCategoryMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(PathCategoryMasterList.ToGridResponse(objGrid, "PathCategory List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "PathCategoryMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.CategoryId == id);
            return data.ToSingleResponse<MPathCategoryMaster, PathCategoryMasterModel>("PathCategory Master");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "PathCategoryMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(PathCategoryMasterModel obj)
        {
            MPathCategoryMaster model = obj.MapTo<MPathCategoryMaster>();
            model.IsActive = true;
            if (obj.CategoryId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Category name added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "PathCategoryMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PathCategoryMasterModel obj)
        {
            MPathCategoryMaster model = obj.MapTo<MPathCategoryMaster>();
            model.IsActive = true;
            if (obj.CategoryId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Category Name updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "PathCategoryMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> delete(int Id)
        {
            MPathCategoryMaster model = await _repository.GetById(x => x.CategoryId == Id);
            if ((model?.CategoryId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Category Name deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
