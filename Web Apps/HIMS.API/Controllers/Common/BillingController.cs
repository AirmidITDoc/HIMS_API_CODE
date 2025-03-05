using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Common;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.OPPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services.Common;
using HIMS.Services.Inventory;
using HIMS.Services.IPPatient;
using HIMS.Services.OPPatient;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Common
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class BillingController : BaseController
    {
        private readonly IOPAddchargesService _IOPAddchargesService;
        private readonly IOPBillingService _oPBillingService;
        private readonly IOPSettlementCreditService _IOPCreditBillService;
        private readonly IIPBillService _IPBillService;
       // private readonly IIPBILLCreditService _iPBILLCreditService;
        private readonly IIPDraftBillSerive _iPDraftBillSerive;
        private readonly IIPInterimBillSerive _iPInterimBillSerive;

        public BillingController(IOPAddchargesService repository, IOPBillingService repository1, IOPSettlementCreditService repository2, IIPBillService iPBIllwithpay, IIPBILLCreditService iPBILLCreditService,
            IIPInterimBillSerive iPInterimBill,IIPDraftBillSerive iPDraftBill)
            {
            _IOPAddchargesService = repository;
            _oPBillingService = repository1;
            _IOPCreditBillService = repository2;
            _IPBillService = iPBIllwithpay;
            //_iPBILLCreditService = iPBILLCreditService;
            _iPDraftBillSerive = iPDraftBill;
            _iPInterimBillSerive = iPInterimBill;
        }

        [HttpPost("BrowseIPBillList")]
        public async Task<IActionResult> GetIPBillListAsync(GridRequestModel objGrid)
        {
            IPagedList<BrowseIPDBillListDto> IPBill = await _IPBillService.GetIPBillListAsync(objGrid);
            return Ok(IPBill.ToGridResponse(objGrid, "BrowseIPDBill List"));
        }
        [HttpPost("BrowseIPPaymentList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> PaymentList(GridRequestModel objGrid)
        {
            IPagedList<BrowseIPPaymentListDto> IPPaymentList = await _IPBillService.GetIPPaymentListAsync(objGrid);
            return Ok(IPPaymentList.ToGridResponse(objGrid, "BrowseIPPaymentList"));
        }
        [HttpPost("BrowseIPRefundlist")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> RefundBillList(GridRequestModel objGrid)
        {
            IPagedList<BrowseIPRefundListDto> BrowseIPRefundlist = await _IPBillService.GetIPRefundBillListListAsync(objGrid);
            return Ok(BrowseIPRefundlist.ToGridResponse(objGrid, " BrowseIPRefundlist "));
        }
        [HttpPost("ServiceClassdetaillList")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.View)]
        public async Task<IActionResult> ServiceDetaillList(GridRequestModel objGrid)
        {
            IPagedList<ServiceClassdetailListDto> ServiceClassdetailList = await _IPBillService.ServiceClassdetailList(objGrid);
            return Ok(ServiceClassdetailList.ToGridResponse(objGrid, "ServiceClassdetail App List"));
        }


        [HttpPost("OPAddchargesInsert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> OPAddchargesInsert(OPAddchargesModel obj)
        {
            AddCharge model = obj.MapTo<AddCharge>();
            if (obj.ChargesId == 0)
            {
                // model.ChargesTime = Convert.ToDateTime(obj.ChargesTime);
                model.AddedBy = CurrentUserId;
                model.ChargesDate = Convert.ToDateTime(model.ChargesDate);
                model.IsCancelledDate = Convert.ToDateTime(model.IsCancelledDate);
                model.ChargesTime = Convert.ToDateTime(model.ChargesTime);
                await _IOPAddchargesService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Addc Charges added successfully.");
        }

        [HttpPost("OPAddchargesDelete")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> OPAddchargesDelete(OPAddchargesModel obj)
        {
            AddCharge model = obj.MapTo<AddCharge>();
            if (obj.ChargesId != 0)
            {

                await _IOPAddchargesService.DeleteAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Addc Charges Deleted successfully.");
        }

        //OPBilling

        [HttpPost("OPBillingInsert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(OPBillIngModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            Payment objPayment = obj.Payments.MapTo<Payment>();
            if (obj.BillNo == 0)
            {
                model.BillTime = Convert.ToDateTime(obj.BillTime);
                model.AddedBy = CurrentUserId;
                await _oPBillingService.InsertAsyncSP1(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Bill added successfully.");
        }


        [HttpPost("OPCreditBillingInsert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> OPCreditBillingInsert(OPBillIngModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            if (obj.BillNo == 0)
            {
                model.BillTime = Convert.ToDateTime(obj.BillTime);
                model.AddedBy = CurrentUserId;
                await _oPBillingService.InsertCreditBillAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Credit Bill added successfully.");
        }

       
        [HttpPost("OPSettlementCreditPayment")]

        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> OPSettlementCreditPayment(OPSettlementpayment obj)
        {
                       
            Bill model = obj.OPSettlementCredit.MapTo<Bill>();
            Payment objpayment = obj.OPSettlementPayment.MapTo<Payment>();
            if (obj.OPSettlementCredit.BillNo != 0)
            {
                //  model.BillTime = Convert.ToDateTime(obj.BillTime);
               // model.AddedBy = CurrentUserId;
                await _IOPCreditBillService.InsertAsyncSP(model, objpayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Credit Bill Payment added successfully.");
        }

        //Ip Bill

        [HttpPost("IPBIllInsertSP")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> IPBIllInsertSP(IPBILLModel obj)
        {
            Bill model = obj.MapTo<Bill>();
             if (obj.BillNo == 0)
            {
                model.BillTime = Convert.ToDateTime(obj.BillTime);
                model.AddedBy = CurrentUserId;
                await _IPBillService.InsertBillAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IP Bill added successfully.",model);
        }




        [HttpPost("IPBIllCreditInsertSP")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> IPBIllCreditInsertSP(IPBILLModel obj)
        {
            Bill model = obj.MapTo<Bill>();

            if (obj.BillNo == 0)
            {
                model.BillTime = Convert.ToDateTime(obj.BillTime);
                model.AddedBy = CurrentUserId;
                await _IPBillService.InsertCreditBillAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Ip Bill Credited successfully.",model);
        }

        [HttpPost("IPInterimInsert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> IPInterimInsert(IPBILLModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            if (obj.BillNo == 0)
            {
                model.BillTime = Convert.ToDateTime(obj.BillTime);
                model.AddedBy = CurrentUserId;
                await _iPInterimBillSerive.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IP Interim Bill added successfully.",model);
        }



        [HttpPost("IPDraftBillInsert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> IPDraftBillInsert(IPDraftBillModel obj)
        {
            TDrbill model = obj.MapTo<TDrbill>();
            if (obj.Drbno == 0)
            {
                model.BillTime = Convert.ToDateTime(obj.BillTime);
                model.AddedBy = CurrentUserId;
                await _iPDraftBillSerive.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IP Draft Bill added successfully.",model);
        }

    }
}

