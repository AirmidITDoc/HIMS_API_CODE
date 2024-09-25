using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OutPatient;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;

namespace HIMS.API.Controllers.OutPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class IPBillController : BaseController
    {
        private readonly IIPBIllwithpaymentService _IPBillService;
        private readonly IIPBillwithCreditService _IPCreditBillService;
        private readonly IIPAdvanceService _IIPAdvanceService;
        public IPBillController(IIPBIllwithpaymentService repository, IIPBillwithCreditService repository1, IIPAdvanceService repository2)
        {
            _IPBillService = repository;
            _IPCreditBillService = repository1;
            _IIPAdvanceService = repository2;
        }



        [HttpPost("IPBIllInsertSP")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> IPBIllInsertSP(IPBillingModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            //BillDetail objBillDetail = obj.IPBillDetailsModel.MapTo<BillDetail>();
            //Payment objpayment = obj.IpPayment.MapTo<Payment>();

            if (obj.BillNo == 0)
            {
                model.BillTime = Convert.ToDateTime(obj.BillTime);
                model.AddedBy = CurrentUserId;
                await _IPBillService.InsertAsyncSP(model,CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IP Bill added successfully.");
        }




        //[HttpPost("IPBIllCreditInsertSP")]
        ////[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> IPBIllCreditInsertSP(IPBillModel obj)
        //{
        //    Bill model = obj.MapTo<Bill>();

        //    if (obj.BillNo == 0)
        //    {
        //        model.BillTime = Convert.ToDateTime(obj.BillTime);
        //        model.AddedBy = CurrentUserId;
        //        await _IPCreditBillService.InsertAsyncSP(model,CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Ip Bill added successfully.");
        //}

        [HttpPost("IPAdvanceInsert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(NewIPAdvance obj)
        {
            AdvanceHeader model = obj.IPAdvanceHeader.MapTo<AdvanceHeader>();
            AdvanceDetail ObjAdvanceDetail = obj.IPAdvanceDetail.MapTo<AdvanceDetail>();
            Payment Objpayment = obj.IPPayments.MapTo<Payment>();

            if (obj.IPAdvanceHeader.AdvanceId == 0)
            {
                ObjAdvanceDetail.Time = Convert.ToDateTime(ObjAdvanceDetail.Time);
                Objpayment.PaymentTime = Convert.ToDateTime(Objpayment.PaymentTime);
                model.AddedBy = CurrentUserId;
                await _IIPAdvanceService.InsertAsyncSP(model,ObjAdvanceDetail,Objpayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Advance added successfully.");
        }



    }
}
