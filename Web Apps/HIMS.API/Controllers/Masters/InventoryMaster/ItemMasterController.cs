﻿using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Inventory;
using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Extensions;
using HIMS.Services.Inventory;
using Asp.Versioning;
using HIMS.Services.OutPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Inventory;
using HIMS.Core;
using HIMS.Services.OPPatient;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class ItemMasterController : BaseController
    {
        private readonly IItemMasterService _ItemMasterServices;
        private readonly IGenericService<MItemMaster> _repository;
        public ItemMasterController(IItemMasterService repository, IGenericService<MItemMaster> repository1)
        {
            _ItemMasterServices = repository;
            _repository = repository1;
        }
        [HttpPost("ItemMasterList")]
        [Permission(PageCode = "ItemMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ItemMasterListDto> ItemMasterList = await _ItemMasterServices.GetItemMasterListAsync(objGrid);
            return Ok(ItemMasterList.ToGridResponse(objGrid, "ItemMaster App  List "));
        }

        [HttpGet("{id?}")]
        [Permission(PageCode = "ItemMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _ItemMasterServices.GetById(id);
            return data.ToSingleResponse<MItemMaster, ItemMasterModel>("Item Master");
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        [HttpPost("InsertEDMX")]
        [Permission(PageCode = "ItemMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(ItemMasterModel obj)
        {
            MItemMaster model = obj.MapTo<MItemMaster>();
            if (obj.ItemId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.IsActive = true;
                model.IsCreatedBy = DateTime.Now;
                await _ItemMasterServices.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }

         [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "ItemMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ItemMasterModel obj)
        {
            MItemMaster model = obj.MapTo<MItemMaster>();
            if (obj.ItemId == 0)

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {

                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.IsUpdatedBy = DateTime.Now;
                model.ItemTime = Convert.ToDateTime(obj.ItemTime);
                await _ItemMasterServices.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
        //Delete API
        [HttpDelete]
        [Permission(PageCode = "ItemMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            MItemMaster model = await _repository.GetById(x => x.ItemId == Id);
            if ((model?.ItemId ?? 0) > 0)
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


        [HttpGet("GetItemListForPrescription")]
        public async Task<ApiResponse> GetItemListForPrescription(int StoreId, string ItemName)
        {
            var resultList = await _ItemMasterServices.GetItemListForPrescription(StoreId, ItemName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get Item List For Prescription List.", resultList);
        }

        [HttpGet("GetItemListForGRNOrPO")]
        public async Task<ApiResponse> GetItemListForGRNOrPO(int StoreId, string ItemName)
        {
            var resultList = await _ItemMasterServices.GetItemListForGRNOrPO(StoreId, ItemName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get Item List For GRN or PO List.", resultList);
        }

        [HttpGet("GetItemListForSalesBatchPop")]
        public async Task<ApiResponse> GetItemListForSalesBatchPop(int StoreId, int ItemId)
        {
            var resultList = await _ItemMasterServices.GetItemListForSalesBatchPop(StoreId, ItemId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get Item List Sales Page Batch Pop.", resultList);
        }

        [HttpGet("GetItemListforSalesPage")]
        public async Task<ApiResponse> GetItemListforSalesPage(int StoreId, String ItemName)
        {
            var resultList = await _ItemMasterServices.GetItemListForSalesPage(StoreId, ItemName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get Item List Sales Page", resultList);
        }
    }
}

