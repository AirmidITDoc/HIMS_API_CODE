using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using HIMS.Services.Inventory;
using HIMS.API.Models.Inventory;
using static HIMS.API.Models.OutPatient.RefundAdvanceModelValidator;
using HIMS.Services.IPPatient;
using HIMS.API.Models.IPPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Core;
using HIMS.Services.OutPatient;
using HIMS.Data.DTO.Administration;

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AdvanceController : BaseController
    {
        private readonly IAdvanceService _IAdvanceService;
        public AdvanceController(IAdvanceService repository)
        {
            _IAdvanceService = repository;
        }
        [HttpPost("IPRefundAdvanceReceiptList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> IPRefundAdvanceReceiptList(GridRequestModel objGrid)
        {
            IPagedList<IPRefundAdvanceReceiptListDto> IPRefundAdvanceReceiptList = await _IAdvanceService.IPRefundAdvanceReceiptList(objGrid);
            return Ok(IPRefundAdvanceReceiptList.ToGridResponse(objGrid, "IPRefundAdvanceReceipt App List"));
        }
        [HttpPost("IPAdvanceList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> IPAdvanceList(GridRequestModel objGrid)
        {
            IPagedList<IPAdvanceListDto> IPAdvanceList = await _IAdvanceService.IPAdvanceList(objGrid);
            return Ok(IPAdvanceList.ToGridResponse(objGrid, "IPAdvance App List"));
        }

        [HttpPost("AdvanceDetailList")]
        [Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> AdvanceDetailList(GridRequestModel objGrid)
        {
            IPagedList<AdvanceDetailListDto> AdvanceDetailList = await _IAdvanceService.AdvanceDetailListAsync(objGrid);
            return Ok(AdvanceDetailList.ToGridResponse(objGrid, "AdvanceDetail List"));
        }


        [HttpPost("AdvanceList")]
        [Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> AdvanceList(GridRequestModel objGrid)
        {
            IPagedList<AdvanceListDto> AdvanceList = await _IAdvanceService.GetAdvanceListAsync(objGrid);
            return Ok(AdvanceList.ToGridResponse(objGrid, "Advance List"));
        }


        [HttpPost("RefundOfAdvanceList")]
        [Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> RefundAdvanceList(GridRequestModel objGrid)
        {
            IPagedList<RefundOfAdvanceListDto> RefundAdvanceList = await _IAdvanceService.GetRefundOfAdvanceListAsync(objGrid);
            return Ok(RefundAdvanceList.ToGridResponse(objGrid, "Refund Of Advance List"));
        }
        [HttpPost("InsertSP")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(ModelAdvance1 obj)
        {
            AdvanceHeader model = obj.Advance.MapTo<AdvanceHeader>();
            AdvanceDetail objAdvanceDetail = obj.AdvanceDetail.MapTo<AdvanceDetail>();
            Payment objpayment = obj.AdvancePayment.MapTo<Payment>();
            if (obj.Advance.AdvanceId == 0)
            {
                model.Date = Convert.ToDateTime(obj.Advance.Date);
                model.AddedBy = CurrentUserId;

                objAdvanceDetail.Date = Convert.ToDateTime(obj.AdvanceDetail.Date);
                objAdvanceDetail.AddedBy = CurrentUserId;
                objpayment.PaymentTime = Convert.ToDateTime(objpayment.PaymentTime);
                objpayment.AddBy = CurrentUserId;

               await _IAdvanceService.InsertAdvanceAsyncSP1(model, objAdvanceDetail, objpayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Advance added successfully.");
        }



        //[HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.Edit)]
        //public async Task<ApiResponse> Update(IPAdvance obj)
        //{
        //    AdvanceDetail model = obj.AdvanceDetailModel.MapTo<AdvanceDetail>();
        //    if (obj.AdvanceDetailModel.AdvanceId != 0)
        //    {
        //        model.AddedBy = CurrentUserId;

        //        await _IAdvanceService.UpdateAdvanceAsyncSP(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Advance Updated successfully.");
        //}


























        //[HttpPost("RefundAdvanceInsertSP")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> InsertSP(RefundAdvance obj)
        //{
        //    Refund model = obj.RefundAdvanceModel.MapTo<Refund>();
        //    AdvRefundDetail objAdvRefundDetail = obj.AdvRefundDetailModel.MapTo<AdvRefundDetail>();
        //    AdvanceHeader objAdvanceHeader = obj.AdvanceHeaderModel.MapTo<AdvanceHeader>();
        //    AdvanceDetail objAdvanceDetail = obj.AdvanceDetailModel1.MapTo<AdvanceDetail>();
        //    Payment objPayment = obj.PaymentModel2.MapTo<Payment>();
        //    if (obj.RefundAdvanceModel.RefundId == 0)
        //    {
        //        model.RefundDate = Convert.ToDateTime(obj.RefundAdvanceModel.RefundDate);
        //        model.AddedBy = CurrentUserId;

        //        if (obj.AdvanceHeaderModel.AdvanceId == 0)
        //        {
        //            //objAdvanceHeader.Date = Convert.ToDateTime(obj.AdvanceHeaderModel.Date);
        //            objAdvanceHeader.AddedBy = CurrentUserId;
        //            // objVisitDetail.UpdatedBy = CurrentUserId;
        //        }

        //        if (obj.AdvRefundDetailModel.AdvRefId == 0)
        //        {
        //            objAdvRefundDetail.RefundDate = Convert.ToDateTime(obj.AdvRefundDetailModel.RefundDate);

        //            // objVisitDetail.UpdatedBy = CurrentUserId;
        //        }
        //        if (obj.AdvanceDetailModel1.AdvanceDetailId == 0)
        //        {

        //            objAdvanceDetail.AddedBy = CurrentUserId;
        //            // objVisitDetail.UpdatedBy = CurrentUserId;
        //        }

        //        if (obj.PaymentModel2.PaymentId == 0)
        //        {
        //            objPayment.PaymentDate = Convert.ToDateTime(obj.PaymentModel2.PaymentDate);


        //        }
        //        await _IAdvanceService.InsertAsyncSP(model, objAdvanceHeader, objAdvRefundDetail, objAdvanceDetail, objPayment, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Refund of Advance added successfully.");
        //}



    }
}
