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
using HIMS.API.Models.OutPatient;

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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Report Update successfully.");
        }

        [HttpPost("BatchUpdate")]
        [Permission(PageCode = "StockAdjustment", Permission = PagePermission.Add)]
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Update successfully.");
        }
        [HttpPost("GSTUpdate")]
        [Permission(PageCode = "StockAdjustment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> GSTUpdate(GSTUpdateModel obj)
        {
            TGstadjustment model = obj.MapTo<TGstadjustment>();
            if (obj.StkId != 0)
            {
                model.AddedBy = CurrentUserId;
                await _IStockAdjustmentService.GSTUpdateSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " Record Update successfully.");
        }
      
        //Shilpa//22/05/2025 updated

        [HttpPost("MrpAdjustmentUpdate")]
        //[Permission(PageCode = "StockAdjustment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> GSTUpdate(MRPAdjModel obj)
        {
            TMrpAdjustment model = obj.MRPAdjustmentMod.MapTo<TMrpAdjustment>();
            TCurrentStock CurruntStock = obj.CurruntStockModel.MapTo<TCurrentStock>();
            if (model.StoreId == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
            // 👇 Manually assign fields 
            var CurruntStockModel = new TCurrentStock
            {
                StoreId = CurruntStock.StoreId,
                ItemId = CurruntStock.ItemId,
                BatchNo = CurruntStock.BatchNo,

            };
            await _IStockAdjustmentService.MrpAdjustmentUpdate(model, CurruntStock, CurrentUserId, CurrentUserName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Update  successfully.");
        }

    }
}