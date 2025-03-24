using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Inventory;
using HIMS.Core;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class StockAdjustmentController : BaseController
    {
        private readonly IStockAdjustmentService _IStockAdjustmentService;
        public StockAdjustmentController(IStockAdjustmentService repository)
        {
            _IStockAdjustmentService = repository;
        }
        [HttpPost("ItemWiseStockList")]
        [Permission(PageCode = "StockAdjustment", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<ItemWiseStockListDto> ItemWiseStockList = await _IStockAdjustmentService.StockAdjustmentList(objGrid);
            return Ok(ItemWiseStockList.ToGridResponse(objGrid, "ItemWiseStockList"));

        }


        [HttpPost("StockUpdate")]
        [Permission(PageCode = "StockAdjustment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PharStockAdjustmentModel obj)
        {
            TStockAdjustment model = obj.MapTo<TStockAdjustment>();
            if (obj.StockAdgId == 0)
            {

                model.AddedBy = CurrentUserId;

                await _IStockAdjustmentService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Stock Update  successfully.");
        }

        [HttpPost("BatchUpdate")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> BatchUpdate(BatchAdjustmentModel obj)
        {
            TBatchAdjustment model = obj.MapTo<TBatchAdjustment>();
            if (obj.BatchAdjId == 0)
            {

                model.AddedBy = CurrentUserId;

                await _IStockAdjustmentService.BatchUpdateSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Batch Update  successfully.");
        }
        [HttpPost("GSTUpdate")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> GSTUpdate(GSTUpdateModel obj)
        {
            TGstadjustment model = obj.MapTo<TGstadjustment>();
            if (obj.StoreId == 0)
            {

                model.AddedBy = CurrentUserId;

                await _IStockAdjustmentService.GSTUpdateSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " GSTUpdate  successfully.");
        }





        [HttpPost("InsertStockAdjustmentEDMX")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertAsync(PharStockAdjustmentModel obj)
        {
            TStockAdjustment model = obj.MapTo<TStockAdjustment>();
            if (obj.StockAdgId == 0)
            {

                model.StoreId = CurrentUserId;

                await _IStockAdjustmentService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Stock added successfully.");
        }


        [HttpPost("InsertBatchAdjustmentEDMX")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertAsync(BatchAdjustmentModel obj)
        {
            TBatchAdjustment model = obj.MapTo<TBatchAdjustment>();
            if (obj.BatchAdjId == 0)
            {

                model.StoreId = CurrentUserId;

                await _IStockAdjustmentService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Batch Adjustment Stock added successfully.");
        }



        [HttpPost("InsertMRPAdjustmentEDMX")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertAsync(MRPAdjustmentModel obj)
        {
            TMrpAdjustment model = obj.MapTo<TMrpAdjustment>();
            if (obj.MrpAdjId == 0)
            {

                model.StoreId = CurrentUserId;

                await _IStockAdjustmentService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "MRP Adjustment Stock added successfully.");
        }

    }
}
