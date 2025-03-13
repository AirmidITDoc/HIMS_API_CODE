using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
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



namespace HIMS.API.Controllers.OutPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class IPBillController : BaseController
    {
        private readonly IIPBillService _IPBillService;
        private readonly IIPBillwithCreditService _IPCreditBillService;
        private readonly IIPAdvanceService _IIPAdvanceService;
        public IPBillController(IIPBillService repository, IIPBillwithCreditService repository1, IIPAdvanceService repository2)
        {
            _IPBillService = repository;
            _IPCreditBillService = repository1;
            _IIPAdvanceService = repository2;
        }

        [HttpPost("IPPreviousBillList")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> GetIPPreviousBillAsync(GridRequestModel objGrid)
        {
            IPagedList<IPPreviousBillListDto> IPPreviousBillList = await _IPBillService.GetIPPreviousBillAsync(objGrid);
            return Ok(IPPreviousBillList.ToGridResponse(objGrid, "IPPreviousBill List"));
        }
        [HttpPost("IPAddchargesList")]
        public async Task<IActionResult> GetIPAddchargesAsync(GridRequestModel objGrid)
        {
            IPagedList<IPAddchargesListDto> IPAddchargesList = await _IPBillService.GetIPAddchargesAsync(objGrid);
            return Ok(IPAddchargesList.ToGridResponse(objGrid, "IPAddcharges List"));
        }

        [HttpPost("IPBillList")]
        public async Task<IActionResult> GetIPBillListAsync(GridRequestModel objGrid)
        {
            IPagedList<BrowseIPDBillListDto> IPBill = await _IPBillService.GetIPBillListAsync(objGrid);
            return Ok(IPBill.ToGridResponse(objGrid, "IPBill List"));
        }
        [HttpPost("AddChargeInsert")]
        //   [Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(AddChargesModel obj)
        {
            AddCharge model = obj.MapTo<AddCharge>();
            if (obj.ChargesId == 0)
            {
                
                model.AddedBy = CurrentUserId;
                await _IPBillService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "AddCharge  added successfully.");
        }

        //[HttpDelete("IPAddchargesdelete")]
        //public async Task<ApiResponse> IPAddchargesdelete(AddChargesDeleteModel obj)
        //{
        //    var RPAP = _IPBillService.DeleteAsync(obj);
        //    return Ok(RPAP);
        //}



        [HttpPost("PaymentSettelment")]
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
                await _IPBillService.paymentAsyncSP(model, objBillModel, objAdvanceDetail, objAdvanceHeader, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Payment added successfully.",model);
        }
        [HttpPost("IPBilllwithCashCounterInsert")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insertsp(BillingModel obj)
        {

            Bill Model = obj.Bill.MapTo<Bill>();
            BillDetail BillDetailModel = obj.BillDetail.MapTo<BillDetail>();
            AddCharge AddChargeModel = obj.AddCharge.MapTo<AddCharge>();
            Admission AddmissionModel = obj.Addmission.MapTo<Admission>();
            Payment paymentModel = obj.payment.MapTo<Payment>();
            Bill BillModel = obj.Bills.MapTo<Bill>();
            List<AdvanceDetail> objAdvanceDetail = obj.Advancesupdate.MapTo<List<AdvanceDetail>>();
            AdvanceHeader objAdvanceHeader = obj.advancesHeaderupdate.MapTo<AdvanceHeader>();
            if (obj.Bill.BillNo == 0)
            {
                Model.BillDate = Convert.ToDateTime(obj.Bill.BillDate);
                Model.BillTime = Convert.ToDateTime(obj.Bill.BillTime);
                paymentModel.PaymentDate = Convert.ToDateTime(obj.payment.PaymentDate);
                paymentModel.PaymentTime = Convert.ToDateTime(obj.payment.PaymentTime);
                Model.AddedBy = CurrentUserId;
                await _IPBillService.IPbillAsyncSp(Model, BillDetailModel, AddChargeModel, AddmissionModel, paymentModel, BillModel, objAdvanceDetail, objAdvanceHeader,CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Bill added successfully.", Model);
        }

        [HttpPost("IPBilllCreditInsert")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertCreditSP(BillingModel obj)
        {

            Bill Model = obj.Bill.MapTo<Bill>();
            BillDetail BillDetailModel = obj.BillDetail.MapTo<BillDetail>();
            AddCharge AddChargeModel = obj.AddCharge.MapTo<AddCharge>();
            Admission AddmissionModel = obj.Addmission.MapTo<Admission>();
         //   Payment paymentModel = obj.payment.MapTo<Payment>();
            Bill BillModel = obj.Bills.MapTo<Bill>();
            List<AdvanceDetail> objAdvanceDetail = obj.Advancesupdate.MapTo<List<AdvanceDetail>>();
            AdvanceHeader objAdvanceHeader = obj.advancesHeaderupdate.MapTo<AdvanceHeader>();
            if (obj.Bill.BillNo == 0)
            {
                Model.BillDate = Convert.ToDateTime(obj.Bill.BillDate);
                Model.BillTime = Convert.ToDateTime(obj.Bill.BillTime);
              //  paymentModel.PaymentDate = Convert.ToDateTime(obj.payment.PaymentDate);
             //   paymentModel.PaymentTime = Convert.ToDateTime(obj.payment.PaymentTime);
                Model.AddedBy = CurrentUserId;
                await _IPBillService.IPbillCreditAsyncSp(Model, BillDetailModel, AddChargeModel, AddmissionModel,  BillModel, objAdvanceDetail, objAdvanceHeader, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Credit Bill added successfully.", Model);
        }


        [HttpPost("IPInterimBillInsertWithCashCounter")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.Add)]
        public async Task<ApiResponse> IPInterimBillCashCounterAsyncSp(IPBillModel obj)
        {
            AddCharge AddChargeModel = obj.AddChargeM.MapTo<AddCharge>();
            Bill Model = obj.IPBillling.MapTo<Bill>();
            BillDetail BillDetailModel = obj.BillingDetails.MapTo<BillDetail>();
          //  Admission AddmissionModel = obj.Addmission.MapTo<Admission>();
               Payment paymentModel = obj.payments.MapTo<Payment>();
          //  Bill BillModel = obj.Bills.MapTo<Bill>();
           // List<AdvanceDetail> objAdvanceDetail = obj.Advancesupdate.MapTo<List<AdvanceDetail>>();
          //  AdvanceHeader objAdvanceHeader = obj.advancesHeaderupdate.MapTo<AdvanceHeader>();
            if (obj.IPBillling.BillNo == 0)
            {
                Model.BillDate = Convert.ToDateTime(obj.IPBillling.BillDate);
                Model.BillTime = Convert.ToDateTime(obj.IPBillling.BillTime);
                 paymentModel.PaymentDate = Convert.ToDateTime(obj.payments.PaymentDate);
                   paymentModel.PaymentTime = Convert.ToDateTime(obj.payments.PaymentTime);
                Model.AddedBy = CurrentUserId;
                await _IPBillService.IPInterimBillCashCounterAsyncSp(AddChargeModel,Model, BillDetailModel,  paymentModel, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Interim Bill added successfully.", Model);
        }
        [HttpPost("InsertIPDraftBill")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertIP(TDrbillingModel obj)
        {

            TDrbill Model = obj.TDrbill.MapTo<TDrbill>();
          List<TDrbillDet> DetModel = obj.TDRBillDet.MapTo<List<TDrbillDet>>();

            if (obj.TDrbill.Drbno == 0)
            {
                Model.BillDate = Convert.ToDateTime(obj.TDrbill.BillDate);
                Model.BillTime = Convert.ToDateTime(obj.TDrbill.BillTime);

                Model.AddedBy = CurrentUserId;
                await _IPBillService.IPDraftBillAsync(Model, DetModel, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Draft Bill added successfully.", Model);
        }

        [HttpDelete("IPAddchargesdelete")]
        public async Task<ApiResponse> IPAddchargesdelete(AddChargeDModel obj)
        {

            AddCharge Model = obj.DeleteCharges.MapTo<AddCharge>();


            if (obj.DeleteCharges.ChargesId != 0)
            {
               


                Model.AddedBy = CurrentUserId;
                await _IPBillService.IPAddchargesdelete(Model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IPAddcharges delete successfully.");
        }

    }
}
