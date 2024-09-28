using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Common;
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
        public BillingController(IOPAddchargesService repository, IOPBillingService repository1, IOPSettlementCreditService repository2)
            {
                _IOPAddchargesService = repository;
            _oPBillingService = repository1;
            _IOPCreditBillService = repository2;
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
            if (obj.BillNo == 0)
            {
                model.BillTime = Convert.ToDateTime(obj.BillTime);
                model.AddedBy = CurrentUserId;
                await _oPBillingService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
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
    }
    }

