using Asp.Versioning;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Controllers;

namespace HIMS.API.Controllers.Masters.InventoryMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ItemManufactureMasterController : BaseController
    {
        private readonly IGenericService<MItemManufactureMaster> _repository;
        public ItemManufactureMasterController(IGenericService<MItemManufactureMaster> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        [Permission(PageCode = "ItemManufactureMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MItemManufactureMaster> ItemManufactureMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(ItemManufactureMasterList.ToGridResponse(objGrid, "ItemManufacture List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "ItemManufactureMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ItemManufactureId == id);
            return data.ToSingleResponse<MItemManufactureMaster, ItemManufactureModel>("ItemManufacture Master");
        }
        //Add API
        [HttpPost]
        [Permission(PageCode = "ItemManufactureMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ItemManufactureModel obj)
        {
            MItemManufactureMaster model = obj.MapTo<MItemManufactureMaster>();
            model.IsActive = true;
            if (obj.ItemManufactureId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);


            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record   added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        [Permission(PageCode = "ItemManufactureMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ItemManufactureModel obj)
        {
            MItemManufactureMaster model = obj.MapTo<MItemManufactureMaster>();
            model.IsActive = true;
            if (obj.ItemManufactureId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Name updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "ItemManufactureMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MItemManufactureMaster model = await _repository.GetById(x => x.ItemManufactureId == Id);
            if ((model?.ItemManufactureId ?? 0) > 0)
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
