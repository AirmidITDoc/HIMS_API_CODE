using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Inventory;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Services.Inventory;
using Asp.Versioning;
using HIMS.Services.OutPatient;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ItemMasterController : BaseController
    {
        private readonly IItemMasterService _ItemMasterServices;
        public ItemMasterController(IItemMasterService repository)
        {
            _ItemMasterServices = repository;
        }

        [HttpPost("InsertSP")]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(ItemMasterModel obj)
        {
            MItemMaster model = obj.MapTo<MItemMaster>();
            if (obj.ItemId == 0)
            {
                model.ItemTime = Convert.ToDateTime(obj.ItemTime);
                model.Addedby = CurrentUserId;
                model.IsActive = true;
                await _ItemMasterServices.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Item Name  added successfully.");
        }

        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(ItemMasterModel obj)
        {
            MItemMaster model = obj.MapTo<MItemMaster>();
            if (obj.ItemId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.IsActive = true;
                model.Addedby = CurrentUserId;
                model.UpDatedBy = CurrentUserId;
                await _ItemMasterServices.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Item Name  added successfully.");
        }

         [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ItemMasterModel obj)
        {
            MItemMaster model = obj.MapTo<MItemMaster>();
            if (obj.ItemId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ItemTime = Convert.ToDateTime(obj.ItemTime);
                await _ItemMasterServices.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Item Name updated successfully.");
        }
        [HttpPost("ItemCanceled")]
        //[Permission(PageCode = "TestMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(DeleteAssignItemToStore obj)
        {
            MItemMaster model = new();
            if (obj.ItemId != 0)
            {
                model.ItemId = obj.ItemId;
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _ItemMasterServices.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Item Name Canceled successfully.");
        }
    }
}

