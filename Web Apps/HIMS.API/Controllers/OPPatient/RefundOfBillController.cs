using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.OPPatient;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Common;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Common;
using HIMS.Services.IPPatient;
using HIMS.Services.OPPatient;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;


namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class RefundOfBillController : BaseController
    {
        private readonly IOPRefundOfBillService _IRefundOfBillService;
        public RefundOfBillController(IOPRefundOfBillService repository)
        {
            _IRefundOfBillService = repository;
        }


        [HttpPost("OPBilllistforrefundList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<OpBilllistforRefundDto> BillList = await _IRefundOfBillService.GeOpbilllistforrefundAsync(objGrid);
            return Ok(BillList.ToGridResponse(objGrid, "OP Bill list for refund List"));
        }
        [HttpPost("OPBillservicedetailList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> OPbillservicedetail(GridRequestModel objGrid)
        {
            IPagedList<OPBillservicedetailListDto> Servicelist = await _IRefundOfBillService.GetBillservicedetailListAsync(objGrid);
            return Ok(Servicelist.ToGridResponse(objGrid, " OP Bill service detail List"));
        }

        [HttpPost("RefundAgainstBillList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> GetListAsync(GridRequestModel objGrid)
        {
            IPagedList<OPBillservicedetailListDto> Servicelist = await _IRefundOfBillService.GetBillservicedetailListAsync(objGrid);
            return Ok(Servicelist.ToGridResponse(objGrid, "Refund Against Bill List "));
        }

        [HttpPost("OPInsert")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(RefundBillModel obj)
        {
            Refund model = obj.Refund.MapTo<Refund>();
            TRefundDetail objTRefundDetail = obj.TRefundDetails.MapTo<TRefundDetail>();
            //AddCharge objAddCharge = obj.AddCharges.MapTo<AddCharge>();
            //Payment objPayment = obj.Payment.MapTo<Payment>();
            if (obj.Refund.RefundId == 0)
            {
                model.RefundTime = Convert.ToDateTime(obj.Refund.RefundTime);
                model.AddedBy = CurrentUserId;

                obj.TRefundDetails.RefundId = obj.Refund.RefundId;
                objTRefundDetail.AddBy = CurrentUserId;
                objTRefundDetail.UpdatedBy = CurrentUserId;

                //obj.AddCharges.ChargesId = obj.Refund.RefundId;

                //obj.Payment.RefundId = obj.Refund.RefundId;
                //objPayment.AddBy = CurrentUserId;
                //objPayment.IsCancelledBy = CurrentUserId;

                await _IRefundOfBillService.InsertAsyncOP(model, objTRefundDetail,  CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Refund added successfully.");
        }

        //[HttpPost("IPInsert")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Insert(RefundBillModel obj)
        //{
        //    Refund model = obj.Refund.MapTo<Refund>();
        //    TRefundDetail objTRefundDetail = obj.TRefundDetails.MapTo<TRefundDetail>();
        //    AddCharge objAddCharge = obj.AddCharges.MapTo<AddCharge>();
        //    Payment objPayment = obj.Payment.MapTo<Payment>();
        //    if (obj.Refund.RefundId == 0)
        //    {
        //        model.RefundTime = Convert.ToDateTime(obj.Refund.RefundTime);
        //        model.AddedBy = CurrentUserId;

        //        obj.TRefundDetails.RefundId = obj.Refund.RefundId;
        //        objTRefundDetail.AddBy = CurrentUserId;
        //        objTRefundDetail.UpdatedBy = CurrentUserId;

        //        obj.AddCharges.ChargesId = obj.Refund.RefundId;


        //        obj.Payment.RefundId = obj.Refund.RefundId;
        //        objPayment.AddBy = CurrentUserId;
        //        objPayment.IsCancelledBy = CurrentUserId;

        //        await _IRefundOfBillService.InsertAsyncIP(model, objTRefundDetail, objAddCharge, objPayment, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Refund added successfully.");
        //}
        //[HttpPost("OPInsert")]
        ////[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> InsertSP(OPRefundOfBillModel obj)
        //{
        //    Refund model = obj.MapTo<Refund>();
        //    if (obj.RefundId == 0)
        //    {
        //        model.RefundDate = Convert.ToDateTime(obj.RefundDate);
        //        model.AddedBy = CurrentUserId;
        //        await _IRefundOfBillService.InsertAsyncOP(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Refund added successfully.");
        //}

        //[HttpPost("InsertSP")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Insert(RefundBillModel obj)
        //{
        //    if (obj == null || obj.Refund == null)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Invalid input parameters.");

        //    // Map the refund model
        //    Refund model = obj.Refund.MapTo<Refund>();

        //    if (model.RefundId != 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "RefundId must be 0 for new refunds.");

        //    try
        //    {
        //        // Set properties for Refund
        //        model.RefundTime = Convert.ToDateTime(obj.Refund.RefundTime);
        //        model.AddedBy = CurrentUserId;


        //        // Prepare TRefundDetail
        //        TRefundDetail objTRefundDetail = obj.TRefundDetails.MapTo<TRefundDetail>();
        //        objTRefundDetail.RefundId = model.RefundId; // This should be set after Refund is inserted
        //        objTRefundDetail.AddBy = CurrentUserId;
        //        objTRefundDetail.UpdatedBy = CurrentUserId;

        //        // Prepare AddCharge
        //        AddCharge objAddCharge = obj.AddCharges.MapTo<AddCharge>();
        //        objAddCharge.ChargesId = model.RefundId; // This should be set after Refund is inserted

        //        // Prepare Payment
        //        Payment objPayment = obj.Payment.MapTo<Payment>();
        //        objPayment.RefundId = model.RefundId; // This should be set after Refund is inserted
        //        objPayment.AddBy = CurrentUserId;
        //        objPayment.IsCancelledBy = CurrentUserId;

        //        // Start a transaction for inserting all related data
        //        await _IRefundOfBillService.BeginTransactionAsync();

        //        // Insert the refund
        //        var insertedRefundId = await _IRefundOfBillService.InsertRefundAsync(model, CurrentUserId);
        //        model.RefundId = insertedRefundId; // Update the model with the new RefundId

        //        // Set the correct RefundId for related records
        //        objTRefundDetail.RefundId = model.RefundId;
        //        objAddCharge.ChargesId = model.RefundId;
        //        objPayment.RefundId = model.RefundId;

        //        // Insert related data
        //        await _IRefundOfBillService.InsertTRefundDetailAsync(objTRefundDetail);
        //        await _IRefundOfBillService.InsertAddChargeAsync(objAddCharge);
        //        await _IRefundOfBillService.InsertPaymentAsync(objPayment);

        //        // Commit transaction
        //        await _IRefundOfBillService.CommitTransactionAsync();

        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status201Created, "Refund added successfully.");
        //    }
        //    catch (Exception ex)
        //    {
        //        // Rollback transaction in case of error
        //        await _IRefundOfBillService.RollbackTransactionAsync();

        //        // Log the exception (using a logging framework)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "An error occurred while processing your request.");
        //    }
        //}

        //[HttpPost("InsertIpSP")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Insert(RefundBillModel obj)
        //{
        //    Refund model = obj.Refund.MapTo<Refund>();
        //    TRefundDetail objTRefundDetail = obj.TRefundDetails.MapTo<TRefundDetail>();
        //    if (obj.Refund.RefundId == 0)
        //    {
        //        model.RefundTime = Convert.ToDateTime(obj.Refund.RefundTime);
        //        model.AddedBy = CurrentUserId;

        //        obj.TRefundDetails.RefundId = obj.Refund.RefundId;
        //        objTRefundDetail.AddBy = CurrentUserId;
        //        objTRefundDetail.UpdatedBy = CurrentUserId;

        //        await _IRefundOfBillService.InsertAsyncSP(model, objTRefundDetail, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Refund added successfully.");
        //}

        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(OPRefundOfBillModel obj)
        {
            Refund model = obj.MapTo<Refund>();
            object returnId = 0;

            if (obj.RefundId == 0)
            {
                model.RefundTime = Convert.ToDateTime(obj.RefundTime);
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.AddedBy = CurrentUserId;
                returnId = await _IRefundOfBillService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Refund added successfully.", returnId);
        }
        //[HttpPut("Edit/{id:int}")]
        ////[Permission(PageCode = "ItemMaster", Permission = PagePermission.Edit)]
        //public async Task<ApiResponse> Edit(OPRefundOfBillModel obj)
        //{
        //    Refund model = obj.MapTo<Refund>();
        //    if (obj.RefundId == 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    else
        //    {
        //        model.RefundTime = Convert.ToDateTime(obj.RefundTime);
        //        await _IRefundOfBillService.UpdateAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Refund updated successfully.");
        //}

    }
}

