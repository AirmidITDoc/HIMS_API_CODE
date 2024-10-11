using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

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
        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(StockAdjustmentModel obj)
        {
            TIssueToDepartmentDetail model = obj.MapTo<TIssueToDepartmentDetail>();
            if (obj.IssueDepId == 0)
            {
               
                model.IssueId = CurrentUserId;
              
                await _IStockAdjustmentService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Stock to main store added successfully.");
        }

        [HttpPut("UpdateEDMX")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(StockAdjustmentModel obj)
        {
            TIssueToDepartmentDetail model = obj.MapTo<TIssueToDepartmentDetail>();
            if (obj.IssueDepId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {

                model.IssueId = CurrentUserId;

                await _IStockAdjustmentService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Stock  updated successfully.");
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
