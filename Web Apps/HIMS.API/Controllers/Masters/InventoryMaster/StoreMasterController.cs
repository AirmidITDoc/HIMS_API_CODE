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
using HIMS.Services.Inventory;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Master;

namespace HIMS.API.Controllers.Masters.InventoryMaster
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class StoreMasterController : BaseController
    {
        private readonly IGenericService<MStoreMaster> _repository;
        private readonly IStoreMasterService _StoreMasterService;
        public StoreMasterController(IStoreMasterService repository1, IGenericService<MStoreMaster> repository)
        {
            _repository = repository;
            _StoreMasterService = repository1;
        }

        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "StoreMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MStoreMaster> MStoreMastereList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(MStoreMastereList.ToGridResponse(objGrid, "StoreMastere List"));
        }

        //[HttpPost("List")]
        //[Permission(PageCode = "StoreMaster", Permission = PagePermission.View)]
        //public async Task<IActionResult> GetListAsync(GridRequestModel objGrid)
        //{
        //    IPagedList<StoreMasterListDto> StoreMasterList = await _StoreMasterService.GetListAsync(objGrid);
        //    return Ok(StoreMasterList.ToGridResponse(objGrid, "StoreMaster List"));
        //}

        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "StoreMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.StoreId == id);
            return data.ToSingleResponse<MStoreMaster, StoreMasterModel>("storeMaster");
        }

        //Add API
        [HttpPost]
        [Permission(PageCode = "StoreMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(StoreMasterModel obj)
        {
            MStoreMaster model = obj.MapTo<MStoreMaster>();
            model.IsActive = true;
            if (obj.StoreId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }

        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "StoreMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(StoreMasterModel obj)
        {
            MStoreMaster model = obj.MapTo<MStoreMaster>();
            model.IsActive = true;
            if (obj.StoreId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "StoreMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MStoreMaster model = await _repository.GetById(x => x.StoreId == Id);
            if ((model?.StoreId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }
        //List API
        [HttpGet]
        [Route("get-store")]
        [Permission(PageCode = "StoreMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown()
        {
            var MStoreMasterList = await _repository.GetAll(x => x.IsActive.Value);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Store dropdown", MStoreMasterList.Select(x => new { x.StoreName, x.StoreId }));
        }
    }
}
