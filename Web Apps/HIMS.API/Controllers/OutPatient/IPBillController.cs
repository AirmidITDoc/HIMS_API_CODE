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
        private readonly IOPCreditBillService _IOPCreditBillService;
        private readonly IIPAdvanceService _IIPAdvanceService;
        public IPBillController(IIPBIllwithpaymentService repository, IOPCreditBillService repository1, IIPAdvanceService repository2)
        {
            _IPBillService = repository;
            _IOPCreditBillService = repository1;
            _IIPAdvanceService = repository2;
        }

        [HttpPost("IPBIllInsertSP")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> IPBIllInsertSP(IPBillModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            Admission ObjAdmission = obj.MapTo<Admission>();
            Payment Objpayment = obj.MapTo<Payment>();
            if (obj.BillNo == 0)
            {
                model.BillTime = Convert.ToDateTime(obj.BillTime);
                model.AddedBy = CurrentUserId;
                await _IPBillService.InsertAsyncSP(model, ObjAdmission, Objpayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Ip Bill added successfully.");
        }

        [HttpPost("IPBIllCreditInsertSP")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> IPBIllCreditInsertSP(IPBillModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            Admission ObjAdmission = obj.MapTo<Admission>();
            Payment Objpayment = obj.MapTo<Payment>();
            if (obj.BillNo == 0)
            {
                model.BillTime = Convert.ToDateTime(obj.BillTime);
                model.AddedBy = CurrentUserId;
                await _IPBillService.InsertAsyncSP(model, ObjAdmission, Objpayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Ip Bill added successfully.");
        }

        [HttpPost("IPAdvanceInsert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(NewIPAdvance obj)
        {
            AdvanceHeader model = obj.AdvanceHeaderModel.MapTo<AdvanceHeader>();
            AdvanceDetail ObjAdvanceDetail = obj.AdvanceDetail.MapTo<AdvanceDetail>();
            Payment Objpayment = obj.AdvPayment.MapTo<Payment>();

            if (obj.AdvanceHeaderModel.AdvanceId == 0)
            {
                ObjAdvanceDetail.Time = Convert.ToDateTime(new DateTime());
                model.AddedBy = CurrentUserId;
                await _IIPAdvanceService.InsertAsyncSP(model,ObjAdvanceDetail,Objpayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Advance added successfully.");
        }



    }
}
