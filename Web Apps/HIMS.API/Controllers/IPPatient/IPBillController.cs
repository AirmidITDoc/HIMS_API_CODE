using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;
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



namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class IPBillController : BaseController
    {
        private readonly IIPBillService _IPBillService;
        private readonly IIPBillwithCreditService _IPCreditBillService;
        private readonly IIPAdvanceService _IIPAdvanceService;
        private readonly IGenericService<AddCharge> _repository;
        private readonly IGenericService<Admission> _repository2;

        public IPBillController(IIPBillService repository, IIPBillwithCreditService repository1, IIPAdvanceService repository2,IGenericService<AddCharge> repository3, IGenericService<Admission> _repository4)
        {
            _IPBillService = repository;
            _IPCreditBillService = repository1;
            _IIPAdvanceService = repository2;
            _repository = repository3;
            _repository2 = _repository4;


        }

        //List API Get By Id
        [HttpGet("RegID/{id?}")]
        //[Permission(PageCode = "PatientType", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetByRegId(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository2.GetById(x => x.RegId == id);
            return data.ToSingleResponse<Admission, ADMISSIONModel>("Admission");
        }

        [HttpPost("IPPreviousBillList")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> GetIPPreviousBillAsync(GridRequestModel objGrid)
        {
            IPagedList<IPPreviousBillListDto> IPPreviousBillList = await _IPBillService.GetIPPreviousBillAsync(objGrid);
            return Ok(IPPreviousBillList.ToGridResponse(objGrid, "IPPreviousBill List"));
        }
        [HttpPost("IPAddchargesList")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> GetIPAddchargesAsync(GridRequestModel objGrid)
        {
            IPagedList<IPAddchargesListDto> IPAddchargesList = await _IPBillService.GetIPAddchargesAsync(objGrid);
            return Ok(IPAddchargesList.ToGridResponse(objGrid, "IPAddcharges List"));
        }

        [HttpPost("IPBillList")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> GetIPBillListAsync(GridRequestModel objGrid)
        {
            IPagedList<BrowseIPDBillListDto> IPBill = await _IPBillService.GetIPBillListAsync(objGrid);
            return Ok(IPBill.ToGridResponse(objGrid, "IPBill List"));
        }
        [HttpPost("PreviousBillDetailList")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> GetPreviousBillListAsync(GridRequestModel objGrid)
        {
            IPagedList<PreviousBillListDto> IPBill = await _IPBillService.GetPreviousBillListAsync(objGrid);
            return Ok(IPBill.ToGridResponse(objGrid, "PreviousBill List"));
        }
        [HttpPost("PathRadRequestList")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> PathRadRequestListAsync(GridRequestModel objGrid)
        {
            IPagedList<PathRadRequestListDto> RequestList = await _IPBillService.PathRadRequestListAsync(objGrid);
            return Ok(RequestList.ToGridResponse(objGrid, "Path Rad Request List"));
        }
        //[HttpPost("IPBillForRefundList")]
        ////[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        //public async Task<IActionResult> IPBillForRefundListAsync(GridRequestModel objGrid)
        //{
        //    IPagedList<IPBillForRefundListDto> RequestList = await _IPBillService.IPBillForRefundListAsync(objGrid);
        //    return Ok(RequestList.ToGridResponse(objGrid, "Path Rad Request List"));
        //}

        [HttpPost("AddChargeInsert")]
     //   [Permission(PageCode = "Charges", Permission = PagePermission.Add)]
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

        [HttpPost("PaymentSettelment")]
        //[Permission(PageCode = "Payment", Permission = PagePermission.Add)]
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Payment added successfully.", model.PaymentId);
        }
        [HttpPost("IPBilllwithCashCounterInsert")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insertsp(BillingModel obj)
        {

            Bill Model = obj.Bill.MapTo<Bill>();
            List<BillDetail> BillDetailModel = obj.BillDetail.MapTo<List<BillDetail>>();
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
                await _IPBillService.IPbillAsyncSp(Model, BillDetailModel, AddChargeModel, AddmissionModel, paymentModel, BillModel, objAdvanceDetail, objAdvanceHeader, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Bill added successfully.", paymentModel.BillNo);
        }

        [HttpPost("IPBilllCreditInsert")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertCreditSP(BillingModel obj)
        {

            Bill Model = obj.Bill.MapTo<Bill>();
          List<BillDetail> BillDetailModel = obj.BillDetail.MapTo<List<BillDetail>>();
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
                await _IPBillService.IPbillCreditAsyncSp(Model, BillDetailModel, AddChargeModel, AddmissionModel, BillModel, objAdvanceDetail, objAdvanceHeader, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Credit Bill added successfully.", Model.BillNo);
        }


        [HttpPost("IPInterimBillInsertWithCashCounter")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse> IPInterimBillCashCounterAsyncSp(IPBillModel obj)
        {
            AddCharge AddChargeModel = obj.AddChargeM.MapTo<AddCharge>();
            Bill Model = obj.IPBillling.MapTo<Bill>();
            List<BillDetail> BillDetailModel = obj.BillingDetails.MapTo<List<BillDetail>>();
            Payment paymentModel = obj.payments.MapTo<Payment>();
          
            if (obj.IPBillling.BillNo == 0)
            {
                Model.BillDate = Convert.ToDateTime(obj.IPBillling.BillDate);
                Model.BillTime = Convert.ToDateTime(obj.IPBillling.BillTime);
                paymentModel.PaymentDate = Convert.ToDateTime(obj.payments.PaymentDate);
                paymentModel.PaymentTime = Convert.ToDateTime(obj.payments.PaymentTime);
                Model.AddedBy = CurrentUserId;
                await _IPBillService.IPInterimBillCashCounterAsyncSp(AddChargeModel, Model, BillDetailModel, paymentModel, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Interim Bill added successfully.", paymentModel.BillNo);
        }
        [HttpPost("InsertIPDraftBill")]
        [Permission(PageCode = "Bill", Permission = PagePermission.Add)]
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

        [HttpPost("IPAddchargesdelete")]
        //[Permission(PageCode = "Charges", Permission = PagePermission.Add)]
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



        //[HttpDelete("Addchargesdelete")]
        //public async Task<ApiResponse> Delete(int Id)
        //{
        //    AddCharge model = await _repository.GetById(x => x.ChargesId == Id);

        //    if ((model?.ChargesId ?? 0) > 0)
        //    {
        //        model.IsCancelledBy = CurrentUserId;
        //        model.IsCancelledDate = DateTime.Now; 

        //        await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IPAddcharges deleted  successfully.");
        //    }
        //    else
        //    {
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    }

        //}

    }
}
