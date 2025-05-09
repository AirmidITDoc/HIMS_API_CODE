using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.GRN;
using HIMS.Data.DTO.Purchase;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pharmacy
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PurchaseController : BaseController
    {
        private readonly IPurchaseService _IPurchaseService;
        public PurchaseController(IPurchaseService repository)
        {
            _IPurchaseService = repository;
        }
        [HttpPost("PurchaseOrderList")]
        //[Permission(PageCode = "PurchaseOrder", Permission = PagePermission.View)]
        public async Task<IActionResult> GetPurchaseorderListAsync(GridRequestModel objGrid)
        {
            IPagedList<PurchaseListDto> List = await _IPurchaseService.GetPurchaseListAsync(objGrid);
            return Ok(List.ToGridResponse(objGrid, "Purchase Order List"));
        }

        [HttpPost("PurchaseItemList")]
        //[Permission(PageCode = "PurchaseOrder", Permission = PagePermission.View)]
        public async Task<IActionResult> GetPurchaseItemListAsync(GridRequestModel objGrid)
        {
            IPagedList<PurchaseDetailListDto> List1 = await _IPurchaseService.GetPurchaseDetailListAsync(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "Purchase Item List")); 
        }

        [HttpPost("OldPurchaseOrderList")]
        [Permission(PageCode = "PurchaseOrder", Permission = PagePermission.View)]
        public async Task<IActionResult> GetOldPurchaseItemListAsync(GridRequestModel objGrid)
        {
            IPagedList<PurchaseDetailListDto> List1 = await _IPurchaseService.GetOldPurchaseorderAsync(objGrid);
            return Ok(List1.ToGridResponse(objGrid, "Purchase Order Item List"));  
        }

        [HttpPost("LastThreeItemList")]
        [Permission(PageCode = "PurchaseOrder", Permission = PagePermission.View)]
        public async Task<IActionResult> GetLastThreeItemListAsync(GridRequestModel objGrid)
        {
            IPagedList<LastthreeItemListDto> List1 = await _IPurchaseService.GetLastthreeItemListAsync(objGrid);
            return Ok(List1.ToGridResponse(objGrid, " Item List"));
        }

        [HttpPost("SupplierrateList")]
        [Permission(PageCode = "PurchaseOrder", Permission = PagePermission.View)]
        public async Task<IActionResult> GeSupplierrateListAsync(GridRequestModel objGrid)
        {
            IPagedList<SupplierRatelistDto> List1 = await _IPurchaseService.GetSupplierRatetAsync(objGrid);
            return Ok(List1.ToGridResponse(objGrid, " Supplier Rate List"));
        }


        [HttpPost("Insert")]
        //[Permission(PageCode = "PurchaseOrder", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PurchaseModel obj)
        {
            TPurchaseHeader model = obj.MapTo<TPurchaseHeader>();
            if (obj.PurchaseId == 0)
            {
                
                model.PurchaseDate = Convert.ToDateTime(obj.PurchaseDate);
                //model.PurchaseTime = Convert.ToDateTime(obj.PurchaseTime);
                model.PurchaseTime = DateTime.Now;
                model.AddedBy = CurrentUserId;
                model.UpdatedBy = 0;
                await _IPurchaseService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Purchase added successfully.",model.PurchaseId);
        }

        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "PurchaseOrder", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PurchaseModel obj)
        {
            TPurchaseHeader model = obj.MapTo<TPurchaseHeader>();
            if (obj.PurchaseId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.PurchaseDate = Convert.ToDateTime(obj.PurchaseDate);
                model.PurchaseTime = DateTime.Now;
                model.UpdatedBy = CurrentUserId;
                model.AddedBy = CurrentUserId;


                await _IPurchaseService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Purchase updated successfully.", model.PurchaseId);
        }

        [HttpPost("Verify")]
        [Permission(PageCode = "PurchaseOrder", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Verify(PurchaseVerifyModel obj)
        {
            TPurchaseHeader model = obj.MapTo<TPurchaseHeader>();
            if (obj.PurchaseId != 0)
            {
                model.IsVerified = true;
                model.VerifiedDateTime = DateTime.Now.Date;

                await _IPurchaseService.VerifyAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Purchase verify successfully.");
        }

    }
}
