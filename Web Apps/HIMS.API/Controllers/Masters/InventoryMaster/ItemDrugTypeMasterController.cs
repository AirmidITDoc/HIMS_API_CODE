using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Core;
using Asp.Versioning;

namespace HIMS.API.Controllers.Masters.InventoryMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class ItemDrugTypeMasterController : BaseController
    {
        private readonly IGenericService<MItemDrugTypeMaster> _repository;
        public ItemDrugTypeMasterController( IGenericService<MItemDrugTypeMaster> repository)
        {
            _repository = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "MItemDrugTypeMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<MItemDrugTypeMaster> ItemDrugTypeMasterList = await _repository.GetAllPagedAsync(objGrid);
            return Ok(ItemDrugTypeMasterList.ToGridResponse(objGrid, "ItemDrugTypeMaster List"));
        }
        //List API Get By Id
        [HttpGet("{id?}")]
        //[Permission(PageCode = "MItemDrugTypeMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.ItemDrugTypeId == id);
            return data.ToSingleResponse<MItemDrugTypeMaster, ItemDrugTypeMasterModel>("MItemDrugTypeMaster");
        }
        //Add API
        [HttpPost]
        //[Permission(PageCode = "MItemDrugTypeMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(ItemDrugTypeMasterModel obj)
        {
            MItemDrugTypeMaster model = obj.MapTo<MItemDrugTypeMaster>();
            model.IsActive = true;
            if (obj.ItemDrugTypeId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        //Edit API
        [HttpPut("{id:int}")]
        //[Permission(PageCode = "MItemDrugTypeMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ItemDrugTypeMasterModel obj)
        {
            MItemDrugTypeMaster model = obj.MapTo<MItemDrugTypeMaster>();
            model.IsActive = true;
            if (obj.ItemDrugTypeId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;

                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }
        //Delete API
        [HttpDelete]
        //[Permission(PageCode = "MItemDrugTypeMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MItemDrugTypeMaster model = await _repository.GetById(x => x.ItemDrugTypeId == Id);
            if ((model?.ItemDrugTypeId ?? 0) > 0)
            {
                model.IsActive = model.IsActive == true ? false : true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

    }
}
