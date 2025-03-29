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
using HIMS.Core;


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
        [Permission(PageCode = "Refund", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<OpBilllistforRefundDto> BillList = await _IRefundOfBillService.GeOpbilllistforrefundAsync(objGrid);
            return Ok(BillList.ToGridResponse(objGrid, "OP Bill list for refund List"));
        }
        [HttpPost("OPBillservicedetailList")]
        [Permission(PageCode = "Refund", Permission = PagePermission.View)]
        public async Task<IActionResult> OPbillservicedetail(GridRequestModel objGrid)
        {
            IPagedList<OPBillservicedetailListDto> Servicelist = await _IRefundOfBillService.GetBillservicedetailListAsync(objGrid);
            return Ok(Servicelist.ToGridResponse(objGrid, " OP Bill service detail List"));
        }

        [HttpPost("RefundAgainstBillList")]
        [Permission(PageCode = "Refund", Permission = PagePermission.View)]
        public async Task<IActionResult> GetListAsync(GridRequestModel objGrid)
        {
            IPagedList<OPBillservicedetailListDto> Servicelist = await _IRefundOfBillService.GetBillservicedetailListAsync(objGrid);
            return Ok(Servicelist.ToGridResponse(objGrid, "Refund Against Bill List "));
        }

        [HttpPost("IPBillListforRefundList")]
        [Permission(PageCode = "Refund", Permission = PagePermission.View)]
        public async Task<IActionResult> IPBillGetListAsync(GridRequestModel objGrid)
        {
            IPagedList<IPBillListforRefundListDto> IPBillListforRefundList = await _IRefundOfBillService.IPBillGetListAsync(objGrid);
            return Ok(IPBillListforRefundList.ToGridResponse(objGrid, "IPBillListforRefund List "));
        }

        [HttpPost("OPRefundOfBILLInsert")]
        [Permission(PageCode = "Refund", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(RefundBillModel obj)
        {
            Refund model = obj.Refund.MapTo<Refund>();
            List<TRefundDetail> objTRefundDetail = obj.TRefundDetails.MapTo<List<TRefundDetail>>();
            List<AddCharge> objAddCharge = obj.AddCharges.MapTo<List<AddCharge>>();
            Payment objPayment = obj.Payment.MapTo<Payment>();
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

                await _IRefundOfBillService.InsertAsyncOP(model, objTRefundDetail, objAddCharge, objPayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Refund added successfully.", model.RefundId);
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

        //[HttpPost("InsertEDMX")]
        ////[Permission(PageCode = "ItemMaster", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> InsertEDMX(OPRefundOfBillModel obj)
        //{
        //    Refund model = obj.MapTo<Refund>();
        //    object returnId = 0;

        //    if (obj.RefundId == 0)
        //    {
        //        model.RefundTime = Convert.ToDateTime(obj.RefundTime);
        //        model.CreatedDate = DateTime.Now;
        //        model.CreatedBy = CurrentUserId;
        //        model.AddedBy = CurrentUserId;
        //        returnId = await _IRefundOfBillService.InsertAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Refund added successfully.", returnId);
        //}

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

