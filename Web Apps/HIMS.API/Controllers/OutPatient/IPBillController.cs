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
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services.Common;
using HIMS.Services.OutPatient;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security;
using static HIMS.API.Models.OutPatient.PaymentModel11;

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
        private readonly IIPBillService _IIPBillService;
        public IPBillController(IIPBIllwithpaymentService repository, IIPBillwithCreditService repository1, IIPAdvanceService repository2, IIPBillService iIPBillService)
        {
            _IPBillService = repository;
            _IPCreditBillService = repository1;
            _IIPAdvanceService = repository2;
            _IIPBillService = iIPBillService;
        }

        //[HttpPost("IPPreviousBillList")]
        ////[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        //public async Task<IActionResult> GetIPPreviousBillAsync(GridRequestModel objGrid)
        //{
        //    IPagedList<IPPreviousBillListDto> IPPreviousBillList = await _IIPAdvanceService.GetIPPreviousBillAsync(objGrid);
        //    return Ok(IPPreviousBillList.ToGridResponse(objGrid, "IPPreviousBill List"));
        //}
        //[HttpPost("IPAddchargesList")]
        //public async Task<IActionResult> GetIPAddchargesAsync(GridRequestModel objGrid)
        //{
        //    IPagedList<IPAddchargesListDto> IPAddchargesList = await _IIPAdvanceService.GetIPAddchargesAsync(objGrid);
        //    return Ok(IPAddchargesList.ToGridResponse(objGrid, "IPAddcharges List"));
        //}

        //[HttpPost("IPBillList")]
        //public async Task<IActionResult> GetIPBillListAsync(GridRequestModel objGrid)
        //{
        //    IPagedList<IPBillList> IPBill = await _IIPAdvanceService.GetIPBillListAsync(objGrid);
        //    return Ok(IPBill.ToGridResponse(objGrid, "IPBill List"));
        //}

        [HttpPost("PaymentSettelmentInsert")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(ModelPayment obj)
        {
            Payment model = obj.Payment.MapTo<Payment>();
            Bill objBillModel = obj.Billupdate.MapTo<Bill>();
            List<AdvanceDetail> objAdvanceDetail = obj.AdvanceDetailupdate.MapTo<List<AdvanceDetail>>();
            AdvanceHeader objAdvanceHeader = obj.AdvanceHeaderupdate.MapTo<AdvanceHeader>();
            if (obj.Payment.PaymentId == 0)
            {
                model.PaymentDate = Convert.ToDateTime(obj.Payment.PaymentDate);
                model.PaymentTime = Convert.ToDateTime(obj.Payment.PaymentTime);
                model.AddBy = CurrentUserId;
<<<<<<< HEAD
              
=======



>>>>>>> 49a3ebdf8c039b51c08e9bb77661cc4650f1765a
                await _IIPAdvanceService.paymentAsyncSP(model, objBillModel, objAdvanceDetail, objAdvanceHeader, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Payment added successfully.");
        }

        [HttpPost("IPBillListSettlementList")]
        ////[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> List1(GridRequestModel objGrid)
        {
            IPagedList<IPBillListSettlementListDto> OPBillListSettlementList = await _IIPBillService.IPBillListSettlementList(objGrid);
            return Ok(OPBillListSettlementList.ToGridResponse(objGrid, "IP Patient Bill List "));
        }


    }
}