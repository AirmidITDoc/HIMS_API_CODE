using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DataProviders;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HIMS.API.Controllers.Pharmacy
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class GRNController : BaseController
    {
        private readonly IGRNService _IGRNService;
        public GRNController(IGRNService repository)
        {
            _IGRNService = repository;
        }

        [HttpPost("GRNUpdateList")]
        [Permission(PageCode = "GRN", Permission = PagePermission.View)]
        public async Task<IActionResult> GRNUpdateList(GridRequestModel objGrid)
        {
            IPagedList<ItemDetailsForGRNUpdateListDto> GRNUpdateList = await _IGRNService.GRNUpdateList(objGrid);
            return Ok(GRNUpdateList.ToGridResponse(objGrid, "GRNUpdateList"));
        }

        [HttpPost("GRNHeaderList")]
  //      [Permission(PageCode = "GRN", Permission = PagePermission.View)]
        public async Task<IActionResult> GRNHeaderList(GridRequestModel objGrid)
        {
            IPagedList<GRNListDto> GRNUpdateList = await _IGRNService.GRNHeaderList(objGrid);
            return Ok(GRNUpdateList.ToGridResponse(objGrid, "GRNList"));
        }

        [HttpPost("GRNDetailsList")]
        [Permission(PageCode = "GRN", Permission = PagePermission.View)]
        public async Task<IActionResult> GRNDetailsList(GridRequestModel objGrid)
        {
            IPagedList<GRNDetailsListDto> GRNUpdateList = await _IGRNService.GRNDetailsList(objGrid);
            return Ok(GRNUpdateList.ToGridResponse(objGrid, "GRNList"));
        }
        [HttpPost("GrnInvoiceNocheck")]
        //  [Permission(PageCode = "oPDPaymentReceiptList", Permission = PagePermission.View)]
        public async Task<IActionResult> List1(GridRequestModel objGrid)
        {
            IPagedList<InvoiceNoChecListDto> InvoiceNoChecList = await _IGRNService.InvoiceNoChecList(objGrid);
            return Ok(InvoiceNoChecList.ToGridResponse(objGrid, "PoDetailList "));
        }

        [HttpPost("Poheaderlist")]
        //  [Permission(PageCode = "oPDPaymentReceiptList", Permission = PagePermission.View)]
        public async Task<IActionResult> List4(GridRequestModel objGrid)
        {
            IPagedList<DirectPOListDto> DirectPOList = await _IGRNService.GetListAsync(objGrid);
            return Ok(DirectPOList.ToGridResponse(objGrid, "DirectPOList "));
        }
        [HttpPost("Podetaillist")]
        //  [Permission(PageCode = "oPDPaymentReceiptList", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<PoDetailListDto> PoDetailList = await _IGRNService.GetListAsync1(objGrid);
            return Ok(PoDetailList.ToGridResponse(objGrid, "PoDetailList "));
        }

        [HttpGet("{id?}")]
        [Permission(PageCode = "GRN", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _IGRNService.GetById(id);
            return data.ToSingleResponse<TGrnheader, GRNModel>("TGrnheader ");
        }

        [HttpPost("Insert")]
      //  [Permission(PageCode = "GRN", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(GRNReqDto obj)
        {
            TGrnheader model = obj.Grn.MapTo<TGrnheader>();
            List<MItemMaster> objItems = obj.GrnItems.MapTo<List<MItemMaster>>();
            if (obj.Grn.Grnid == 0)
            {
                model.Grndate = DateTime.Now.Date;
                model.Grntime = DateTime.Now;
                model.AddedBy = CurrentUserId;
                model.UpdatedBy = 0;
                await _IGRNService.InsertAsync(model, objItems, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.",model.Grnid);
        }

        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "GRN", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(GRNReqDto obj)
        {
            TGrnheader model = obj.Grn.MapTo<TGrnheader>();
            List<MItemMaster> objItems = obj.GrnItems.MapTo<List<MItemMaster>>();
            if (obj.Grn.Grnid == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.Grndate = DateTime.Now.Date;
                model.Grntime = DateTime.Now;
                model.UpdatedBy = CurrentUserId;
                await _IGRNService.UpdateAsync(model, objItems, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.",model.Grnid);
        }

        [HttpPost("InsertPO")]
     //   [Permission(PageCode = "GRN", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertPO(GRNPOReqDto obj)
        {
            TGrnheader model = obj.Grn.MapTo<TGrnheader>();
            List<MItemMaster> objItems = obj.GrnItems?.MapTo<List<MItemMaster>>();
            List<TPurchaseDetail> objPurDetails = obj.GrnPODetails?.MapTo<List<TPurchaseDetail>>();
            List<TPurchaseHeader> objPurHeaders = obj.GrnPOHeaders?.MapTo<List<TPurchaseHeader>>();
            if (obj.Grn.Grnid == 0)
            {
                model.Grndate = DateTime.Now.Date;
                model.Grntime = DateTime.Now;
                model.AddedBy = CurrentUserId;
                model.UpdatedBy = 0;
                await _IGRNService.InsertWithPOAsync(model, objItems, objPurDetails, objPurHeaders, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        [HttpPut("EditPO/{id:int}")]
        [Permission(PageCode = "GRN", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> EditPO(GRNPOReqDto obj)
        {
            TGrnheader model = obj.Grn.MapTo<TGrnheader>();
            List<MItemMaster> objItems = obj.GrnItems.MapTo<List<MItemMaster>>();
            List<TPurchaseDetail> objPurDetails = obj.GrnPODetails.MapTo<List<TPurchaseDetail>>();
            List<TPurchaseHeader> objPurHeaders = obj.GrnPOHeaders.MapTo<List<TPurchaseHeader>>();
            if (obj.Grn.Grnid == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.Grndate = DateTime.Now.Date;
                model.Grntime = DateTime.Now;
                model.UpdatedBy = CurrentUserId;
                await _IGRNService.UpdateWithPOAsync(model, objItems, objPurDetails, objPurHeaders, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        [HttpPost("Verify")]
        [Permission(PageCode = "GRN", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Verify(GRNVerifyModel obj)
        {
            TGrnheader model = obj.MapTo<TGrnheader>();
            if (obj.Grnid != 0)
            {
                model.IsVerified = true;
                //model.IsVerifiedDatetime = DateTime.Now.Date;
                await _IGRNService.VerifyAsyncSp(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record verify successfully.");
        }


        //[HttpPost("GrnInvoiceNocheck")]
        ////    [Permission(PageCode = "GRN", Permission = PagePermission.Edit)]
        //public async Task<ApiResponse> Edit(grnInvoicenocheckModel obj)
        //{
        //    TGrnheader model = obj.MapTo<TGrnheader>();
        //    if (obj.SupplierId != 0)
        //    {

        //        //model.IsVerifiedDatetime = DateTime.Now.Date;
        //        await _IGRNService.AsyncSp(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Added successfully.",model.Grnid);
        //}



        //[HttpPost("InsertGRNPurchase")]
        ////    [Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> InsertSp(GRNDModel obj)
        //{
        //    TGrnheader Model = obj.GRNDetails.MapTo<TGrnheader>();
        //    //Bill Model = obj.IPBillling.MapTo<Bill>();
        //    //List<BillDetail> BillDetailModel = obj.BillingDetails.MapTo<List<BillDetail>>();
        //    //Payment paymentModel = obj.payments.MapTo<Payment>();

        //    if (obj.GRNDetails.Grnid == 0)
        //    {
        //        //Model.BillDate = Convert.ToDateTime(obj.IPBillling.BillDate);
        //        //Model.BillTime = Convert.ToDateTime(obj.IPBillling.BillTime);
        //        //paymentModel.PaymentDate = Convert.ToDateTime(obj.payments.PaymentDate);
        //        //paymentModel.PaymentTime = Convert.ToDateTime(obj.payments.PaymentTime);
        //        //Model.AddedBy = CurrentUserId;
        //        //Model.CreatedBy = CurrentUserId;
        //        //Model.CreatedDate = DateTime.Now;
        //        //Model.ModifiedBy = CurrentUserId;
        //        //Model.ModifiedDate = DateTime.Now;
        //        await _IGRNService.InsertSp(Model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " Record added successfully.");
        //}
    }
}
