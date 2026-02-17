using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.OPPatient;
using Microsoft.AspNetCore.Mvc;


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

        //OP//

        [HttpPost("OPBilllistforrefundList")]
        //[Permission(PageCode = "Refund", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<OpBilllistforRefundDto> BillList = await _IRefundOfBillService.GeOpbilllistforrefundAsync(objGrid);
            return Ok(BillList.ToGridResponse(objGrid, "OP Bill list for refund List"));
        }
        [HttpPost("OPBillservicedetailList")]
        //[Permission(PageCode = "Refund", Permission = PagePermission.View)]
        public async Task<IActionResult> OPbillservicedetail(GridRequestModel objGrid)
        {
            IPagedList<OPBillservicedetailListDto> Servicelist = await _IRefundOfBillService.GetBillservicedetailListAsync(objGrid);
            return Ok(Servicelist.ToGridResponse(objGrid, " OP Bill service detail List"));
        }

        [HttpPost("RefundAgainstBillList")]
        //[Permission(PageCode = "Refund", Permission = PagePermission.View)]
        public async Task<IActionResult> GetListAsync(GridRequestModel objGrid)
        {
            IPagedList<OPBillservicedetailListDto> Servicelist = await _IRefundOfBillService.GetBillservicedetailListAsync(objGrid);
            return Ok(Servicelist.ToGridResponse(objGrid, "Refund Against Bill List "));
        }


        //IP// 


        [HttpPost("IPBillListforRefundList")]
        //[Permission(PageCode = "Refund", Permission = PagePermission.View)]
        public async Task<IActionResult> IPBillGetListAsync(GridRequestModel objGrid)
        {
            IPagedList<IPBillListforRefundListDto> IPBillListforRefundList = await _IRefundOfBillService.IPBillGetListAsync(objGrid);
            return Ok(IPBillListforRefundList.ToGridResponse(objGrid, "IPBillListforRefund List "));
        }

        [HttpPost("IPBillForRefundList")]
        //[Permission(PageCode = "Refund", Permission = PagePermission.View)]
        public async Task<IActionResult> IPBillForRefundListAsync(GridRequestModel objGrid)
        {
            IPagedList<IPBillForRefundListDto> RequestList = await _IRefundOfBillService.IPBillForRefundListAsync(objGrid);
            return Ok(RequestList.ToGridResponse(objGrid, "IP Bill For Refund List"));
        }

        [HttpPost("IPRefundOfBILLInsert")]
        [Permission(PageCode = "Refund", Permission = PagePermission.Add)]
        public ApiResponse InsertSP(RefundBillModel obj)
        {
            Refund model = obj.Refund.MapTo<Refund>();
            List<TRefundDetail> objTRefundDetail = obj.TRefundDetails.MapTo<List<TRefundDetail>>();
            List<AddCharge> objAddCharge = obj.AddCharges.MapTo<List<AddCharge>>();
            Payment objPayment = obj.Payment.MapTo<Payment>();
            List<TPayment> ObjTPayment = obj.TPayments.MapTo<List<TPayment>>();

            if (obj.Refund.RefundId == 0)
            {
                model.RefundTime = Convert.ToDateTime(obj.Refund.RefundTime);
                model.AddedBy = CurrentUserId;

                objTRefundDetail.ForEach(x => { x.RefundId = obj.Refund.RefundId; x.AddBy = CurrentUserId; x.UpdatedBy = CurrentUserId; });
                obj.AddCharges.ForEach(x => { x.ChargesId = obj.Refund.RefundId; });

                obj.Payment.RefundId = obj.Refund.RefundId;
                objPayment.PaymentTime = Convert.ToDateTime(objPayment.PaymentTime);
                objPayment.AddBy = CurrentUserId;
                objPayment.IsCancelledBy = CurrentUserId;

                _IRefundOfBillService.InsertIP(model, objTRefundDetail, objAddCharge, objPayment, ObjTPayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.RefundId);
        }


        [HttpPost("InsertOPRefundOfBill")]
        //[Permission(PageCode = "Refund", Permission = PagePermission.Add)]
        [Permission]
        public ApiResponse Insert(RefundBillModel obj)
        {
            Refund model = obj.Refund.MapTo<Refund>();
            List<TRefundDetail> objTRefundDetail = obj.TRefundDetails.MapTo<List<TRefundDetail>>();
            List<AddCharge> objAddCharge = obj.AddCharges.MapTo<List<AddCharge>>();
            Payment objPayment = obj.Payment.MapTo<Payment>();
            List<TPayment> ObjTPayment = obj.TPayments.MapTo<List<TPayment>>();

            if (obj.Refund.RefundId == 0)
            {
                model.RefundTime = Convert.ToDateTime(obj.Refund.RefundTime);
                model.AddedBy = CurrentUserId;

                objTRefundDetail.ForEach(x => { x.RefundId = obj.Refund.RefundId; x.AddBy = CurrentUserId; x.UpdatedBy = CurrentUserId; });
                obj.AddCharges.ForEach(x => { x.ChargesId = obj.Refund.RefundId; });

                obj.Payment.RefundId = obj.Refund.RefundId;
                objPayment.PaymentTime = Convert.ToDateTime(objPayment.PaymentTime);
                objPayment.AddBy = CurrentUserId;
                objPayment.IsCancelledBy = CurrentUserId;

                _IRefundOfBillService.InsertOP(model, objTRefundDetail, objAddCharge, objPayment, ObjTPayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.RefundId);
        }
    }
}

