using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

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
        
        // List API Get By HSNcode
        [HttpGet("ItemInformationByHSNcode")]
        [Permission(PageCode = "ItemMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> IPGet(string HSNcode)
        {
            if (string.IsNullOrWhiteSpace(HSNcode))
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }

            var data = _ItemMasterServices.GetInformationHSNCode(HSNcode);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Data loaded", data);
        }




        [HttpPost("InsertSP")]
        [Permission(PageCode = "ItemMaster", Permission = PagePermission.Add)]
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
                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.UpDatedBy = CurrentUserId;
                model.IsActive = true;
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
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ItemTime = Convert.ToDateTime(obj.ItemTime);
                await _ItemMasterServices.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
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
                model.IsActive = model.IsActive != true;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        }

        [HttpGet("GetItemListForPrescription")]
        [Permission(PageCode = "ItemMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetItemListForPrescription(int StoreId, string ItemName)
        {
            var resultList = await _ItemMasterServices.GetItemListForPrescription(StoreId, ItemName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get Item List For Prescription List.", resultList);
        }

        [HttpGet("GetItemListForPrescriptionSearch")]
        [AnyPermission(Permission = PagePermission.View)]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.View)]
        public  ApiResponse GetItemListForPrescriptionSearch(string ItemName, int StoreId)
        {
            var resultList =  _ItemMasterServices.GetItemListForPrescriptionSearch( ItemName, StoreId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get Item List For Prescription List.", resultList);
        }


        [HttpGet("GetItemListForPrescriptionReturn")]
        [AnyPermission(Permission = PagePermission.View)]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.View)]
        public ApiResponse GetItemListForPrescriptionReturn(int StoreId,  int IPAdmID, string ItemName)
        {
            var resultList = _ItemMasterServices.GetItemListForPrescriptionretrun(StoreId,  IPAdmID, ItemName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get Item List For Prescription Return List.", resultList);
        }


        [HttpGet("GetItemListForGRNOrPO")]
        [AnyPermission(Permission = PagePermission.View)]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetItemListForGRNOrPO(int StoreId, string ItemName)
        {
            var resultList = await _ItemMasterServices.GetItemListForGRNOrPO(StoreId, ItemName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get Item List For GRN or PO List.", resultList);
        }

        [HttpGet("NewGetItemListForGRNOrPO")]
        [AnyPermission(Permission = PagePermission.View)]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetItemListGRN(int StoreId, string ItemName)
        {
           var resultList = await _ItemMasterServices.GetItemListGRN(StoreId, ItemName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "NewGetItemListForGRNOrPO", resultList);
        }


        [HttpGet("search-GetItemListForIndent")]
        [AnyPermission(Permission = PagePermission.View)]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.View)]
        public ApiResponse ItemListForIndent(int StoreId, string ItemName)
        {
            var data = _ItemMasterServices.ItemListForIndent(StoreId, ItemName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Item List For Indent", data);
        }

        [HttpGet("GetItemListForSalesBatchPop")]
        [AnyPermission(Permission = PagePermission.View)]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetItemListForSalesBatchPop(int StoreId, int ItemId)
        {
            var resultList = await _ItemMasterServices.GetItemListForSalesBatchPop(StoreId, ItemId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get Item List Sales Page Batch Pop.", resultList);
        }

        [HttpGet("NewGetItemListForSalesBatchPop")]
        [AnyPermission(Permission = PagePermission.View)]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> NewGetItemListForSalesBatchPop(int StoreId, int ItemId)
        {
            var resultList = await _ItemMasterServices.NewGetItemListForSalesBatchPop(StoreId, ItemId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get Item List Sales Page Batch Pop.", resultList);
        }

        [HttpGet("search-GetItemListForSalesBatchPop")]
        [AnyPermission(Permission = PagePermission.View)]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.View)]
        public ApiResponse ItemListForBatch(int StoreId, int ItemId, int PatientTypeId)
        {
            var data = _ItemMasterServices.ItemListForBatch(StoreId, ItemId, PatientTypeId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Pharmacy Item Wise Batch List.", data);
        }
        [HttpGet("GetProsearch-GetItemListForSalesBatchPop")]
        [AnyPermission(Permission = PagePermission.View)]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.View)]
        public ApiResponse GetItemListForBatch(int StoreId, int ItemId)
        {
            var data = _ItemMasterServices.GetItemListForBatch(StoreId, ItemId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "GetProsearch Get ItemList ForSalesBatchPop.", data);
        }

        [HttpGet("GetItemListforSalesPage")]
        [AnyPermission(Permission = PagePermission.View)]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetItemListforSalesPage(int StoreId, string ItemName)
        {
            var resultList = await _ItemMasterServices.GetItemListForSalesPage(StoreId, ItemName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get Item List Sales Page", resultList);
        }

        [HttpGet("NewGetItemListforSalesPage")]
        [AnyPermission(Permission = PagePermission.View)]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> NewGetItemListforSalesPage(int StoreId, string ItemName)
        {
            var resultList = await _ItemMasterServices.NewGetItemListForSalesPage(StoreId, ItemName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " NEW Get ItemList Sales Page", resultList);
        }

        [HttpGet("GetItemListByGenericName")]
        [AnyPermission(Permission = PagePermission.View)]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetItemListByGenericName(string ItemGenericNameId,string ItemName, int StoreId)
        {
            var resultList = _ItemMasterServices.GetItemListByGenericName(ItemGenericNameId, ItemName, StoreId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get Item List By Generic Name.", resultList);
        }
    }
}

