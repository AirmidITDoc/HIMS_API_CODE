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
    public class ItemTypeController : BaseController
    {
        private readonly IGenericService<MItemTypeMaster> _repository;
        public ItemTypeController(IGenericService<MItemTypeMaster> repository)
        {
            _repository = repository;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "ItemTypeMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MItemTypeMaster> ItemTypeMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(ItemTypeMasterList.ToGridResponse(objGrid, "Item Type List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "ItemTypeMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ItemTypeId == id);
            return data.ToSingleResponse<MItemTypeMaster, ItemTypeModel>("ItemType List");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "ItemTypeMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ItemTypeModel obj)
        {
            MItemTypeMaster model = obj.MapTo<MItemTypeMaster>();
            model.IsActive = true;
            if (obj.ItemTypeId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Item  Type Name added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "ItemTypeMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ItemTypeModel obj)
        {
            MItemTypeMaster model = obj.MapTo<MItemTypeMaster>();
            model.IsActive = true;
            if (obj.ItemTypeId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Item Type Name updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "ItemTypeMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MItemTypeMaster model = await _repository.GetById(x => x.ItemTypeId == Id);
            if ((model?.ItemTypeId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Item  Type Name deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
    }
}
