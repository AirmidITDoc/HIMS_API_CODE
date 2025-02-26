using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Common;
using HIMS.Services.OPPatient;
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
    public class OPBillController : BaseController
    {
        private readonly IOPBillingService _oPBillingService;
        private readonly IOPCreditBillService _IOPCreditBillService;
        private readonly IOPSettlementService _IOPSettlementService;
        public OPBillController(IOPBillingService repository, IOPCreditBillService repository1, IOPSettlementService repository2)
        {
            _oPBillingService = repository;
            _IOPCreditBillService = repository1;
            _IOPSettlementService= repository2;
        }
        [HttpPost("OPBillListSettlementList")]
        ////[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<OPBillListSettlementListDto> OPBillListSettlementList = await _IOPSettlementService.OPBillListSettlementList(objGrid);
            return Ok(OPBillListSettlementList.ToGridResponse(objGrid, "OP Patient Bill List "));
        }

        [HttpPost("OPBillingInsert")]
        [Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(OPBillIngModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            Payment objPayment = obj.Payments.MapTo<Payment>();
            if (obj.BillNo == 0)
            {
                model.BillTime = Convert.ToDateTime(obj.BillTime);
                model.AddedBy = CurrentUserId;
                await _oPBillingService.InsertAsyncSP(model, objPayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Bill added successfully.");
        }

        [HttpPost("OPCreditBillingInsert")]
        [Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse> OPCreditBillingInsert(OPBillIngModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            if (obj.BillNo == 0)
            {
                model.BillTime = Convert.ToDateTime(obj.BillTime);
                model.AddedBy = CurrentUserId;
                await _IOPCreditBillService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Credit Bill added successfully.");
        }


        //[HttpPost("OPSettlementCreditPayment")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> OPSettlementCreditPayment(OPSettlementpayment obj)
        //{
        //    Bill model = obj.MapTo<Bill>();
        //    if (obj.OPSettlementCredit.BillNo != 0)
        //    {
        //      //  model.BillTime = Convert.ToDateTime(obj.BillTime);
        //        model.AddedBy = CurrentUserId;
        //        await _IOPCreditBillService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Credit Bill Payment added successfully.");
        //}


    }
}
