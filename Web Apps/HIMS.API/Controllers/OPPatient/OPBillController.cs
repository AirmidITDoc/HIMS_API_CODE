using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.API.Models.Pathology;
using HIMS.API.Models.PaymentGateway;
using HIMS.API.PaymentGateway;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data.DTO.Administration;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Administration;
using HIMS.Services.Common;
using HIMS.Services.OPPatient;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static HIMS.API.Models.IPPatient.OtbookingModelValidator;
using static HIMS.API.Models.OutPatient.AppointmentBillModel;
//using OPBillListDto = HIMS.Data.DTO.Administration.OPDRBillListDto;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OPBillController : BaseController
    {
        private readonly IOPBillingService _oPBillingService;
        private readonly IOPCreditBillService _IOPCreditBillService;
        private readonly IOPSettlementService _IOPSettlementService;
        private readonly IAdministrationService _IAdministrationService;
        private readonly IVisitDetailsService _IVisitDetailsService;
        public OPBillController(IOPBillingService repository, IOPCreditBillService repository1, IOPSettlementService repository2, IAdministrationService repository3, IVisitDetailsService repository4)
        {
            _oPBillingService = repository;
            _IOPCreditBillService = repository1;
            _IOPSettlementService = repository2;
            _IAdministrationService = repository3;
            _IVisitDetailsService = repository4;
        }
        [HttpPost("BrowseOPRefundList")]
        //   [Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> OPRefundList(GridRequestModel objGrid)
        {
            IPagedList<OPRefundListDto> OpRefundlist = await _IVisitDetailsService.GeOpRefundListAsync(objGrid);
            return Ok(OpRefundlist.ToGridResponse(objGrid, "OP Refund List"));
        }
        [HttpPost("BrowseOPDBillPagiList")]
        [Permission]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> BrowseOPDBillPagList(GridRequestModel objGrid)
        {
            IPagedList<BrowseOPDBillPagiListDto> BrowseOPDBillPagList = await _IAdministrationService.BrowseOPDBillPagiList(objGrid);
            return Ok(BrowseOPDBillPagList.ToGridResponse(objGrid, "Browse OPD Bill Pagi App List"));
        }
        [HttpPost("BrowseOPPaymentList")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> OPPaymentList(GridRequestModel objGrid)
        {
            IPagedList<OPPaymentListDto> OpPaymentlist = await _IVisitDetailsService.GeOpPaymentListAsync(objGrid);
            return Ok(OpPaymentlist.ToGridResponse(objGrid, "OP Payment List"));
        }

        [HttpPost("PatientWisePaymentList")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> PatientWisePaymentList(GridRequestModel objGrid)
        {
            IPagedList<OPPaymentListDto> OpPaymentlist = await _IVisitDetailsService.GetPatientWisePaymentList(objGrid);
            return Ok(OpPaymentlist.ToGridResponse(objGrid, "OP Payment List"));
        }

        [HttpPost("OPBillListSettlementList")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<OPBillListSettlementListDto> OPBillListSettlementList = await _IOPSettlementService.OPBillListSettlementList(objGrid);
            return Ok(OPBillListSettlementList.ToGridResponse(objGrid, "OP Patient Bill List "));
        }

        [HttpPost("OPDraftBillList")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> OPBillList(GridRequestModel objGrid)
        {
            IPagedList<OPDRBillListDto> OPBillList = await _oPBillingService.GeOPBillListAsync(objGrid);
            return Ok(OPBillList.ToGridResponse(objGrid, "OP Draft Bill List"));
        }


        [HttpPost("OPDraftAddChargeslList")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> OPDRChargeslList(GridRequestModel objGrid)
        {
            IPagedList<OPDRChargesDto> OPDRChargeslList = await _oPBillingService.OPDRChargeslListAsync(objGrid);
            return Ok(OPDRChargeslList.ToGridResponse(objGrid, "OP Draft AddCharges lList"));
        }

        [HttpPost("OPBillingInsert")]
        [Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(OPBillIngModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            Payment objPayment = obj.Payments.MapTo<Payment>();
            List<AddCharge> ObjPackagecharge = obj.Packcagecharges.MapTo<List<AddCharge>>();
            List<TPayment> ObjTPayment = obj.TPayments.MapTo<List<TPayment>>();
            TDrbill ObjTdrBill = obj.TdrBill.MapTo<TDrbill>();



            if (obj.BillNo == 0)
            {
                model.BillDate = Convert.ToDateTime(obj.BillDate);
                model.BillTime = Convert.ToDateTime(obj.BillTime);
                model.AddedBy = CurrentUserId;
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _oPBillingService.InsertAsyncSP(model, objPayment, ObjPackagecharge, ObjTPayment, ObjTdrBill, CurrentUserId, CurrentUserName);
                /// set condition based on payment type=mpesa.
                //string phone = "254723939232";
                //var result = await _stkService.StkPushAsync(phone, obj.NetPayableAmt.Value.ToDecimal(), _config["MPesa:ConfirmationUrl"], model.BillNo.ToString());
                //var Data = JsonConvert.DeserializeObject<MPesaResponseDto>(result);
                //return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", new { model.BillNo, MPesaResponse = Data });
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.BillNo);
        }

        [HttpPost("OPCreditBillingInsert")]
        [Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse> OPCreditBillingInsert(OPBillIngModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            List<AddCharge> ObjPackagecharge = obj.Packcagecharges.MapTo<List<AddCharge>>();
            TDrbill ObjTdrBill = obj.TdrBill.MapTo<TDrbill>();


            if (obj.BillNo == 0)
            {
                model.BillDate = Convert.ToDateTime(obj.BillDate);
                model.BillTime = Convert.ToDateTime(obj.BillTime);
                model.AddedBy = CurrentUserId;
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _IOPCreditBillService.InsertAsyncSP(model, ObjPackagecharge, ObjTdrBill, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.", model.BillNo);
        }

        [HttpPost("AppointmentBillingInsert")]
        [Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse> AppBillInsert(AppBillingMainModels obj)
        {
            Registration Regmodel = obj.AppRegistrationBills.MapTo<Registration>();
            VisitDetail objVisitDetail = obj.Visit.MapTo<VisitDetail>();
            Bill billmodel = obj.AppOPBillIngModels.MapTo<Bill>();
            Payment objPayment = obj.AppOPBillIngModels.Payments.MapTo<Payment>();
            List<AddCharge> ObjPackagecharge = obj.AppOPBillIngModels.Packcagecharges.MapTo<List<AddCharge>>();

            if (obj.AppRegistrationBills.RegId == 0)
            {
                Regmodel.CreatedDate = AppTime.Now;
                Regmodel.CreatedBy = CurrentUserId;
                Regmodel.ModifiedDate = AppTime.Now;
                Regmodel.ModifiedBy = CurrentUserId;
                await _oPBillingService.AppBillInsert(Regmodel, objVisitDetail, billmodel, objPayment, ObjPackagecharge, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", new { billmodel.BillNo, billmodel.OpdIpdId });
        }

        [HttpPost("AppointmentBillingRegisteredInsert")]
        [Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse> AppBillRegisteredInsert(RegistredAppBillingMainModels obj)
        {
            //Registration Regmodel = obj.AppRegistrationBills.MapTo<Registration>();
            VisitDetail objVisitDetail = obj.Visit.MapTo<VisitDetail>();


            Bill billmodel = obj.AppOPBillIngModels.MapTo<Bill>();
            Payment objPayment = obj.AppOPBillIngModels.Payments.MapTo<Payment>();
            List<AddCharge> ObjPackagecharge = obj.AppOPBillIngModels.Packcagecharges.MapTo<List<AddCharge>>();

            if (obj.Visit.RegId != 0)
            {
                objVisitDetail.AddedBy = CurrentUserId;

                if (obj.Visit.VisitId == 0)
                {
                    objVisitDetail.VisitTime = Convert.ToDateTime(obj.Visit.VisitTime);
                    objVisitDetail.AddedBy = CurrentUserId;
                    objVisitDetail.UpdatedBy = CurrentUserId;
                }
                await _oPBillingService.RegisteredAppBillInsert(objVisitDetail, billmodel, objPayment, ObjPackagecharge, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", new { billmodel.BillNo, billmodel.OpdIpdId });
        }

        [HttpPost("AppointmentCreditBillingInsert")]
        [Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse> OPAppointCreditBillingInsert(AppBillingMainModels obj)
        {
            Registration Regmodel = obj.AppRegistrationBills.MapTo<Registration>();

            Bill model = obj.AppOPBillIngModels.MapTo<Bill>();
            Payment objPayment = obj.AppOPBillIngModels.Payments.MapTo<Payment>();

            List<AddCharge> ObjPackagecharge = obj.AppOPBillIngModels.Packcagecharges.MapTo<List<AddCharge>>();
            VisitDetail objVisitDetail = obj.Visit.MapTo<VisitDetail>();

            if (obj.AppRegistrationBills.RegId == 0)
            {
                Regmodel.RegTime = Convert.ToDateTime(obj.AppRegistrationBills.RegTime);
                Regmodel.AddedBy = CurrentUserId;

                if (obj.Visit.VisitId == 0)
                {
                    objVisitDetail.VisitTime = Convert.ToDateTime(obj.Visit.VisitTime);
                    objVisitDetail.AddedBy = CurrentUserId;
                    objVisitDetail.UpdatedBy = CurrentUserId;
                }

            }

            if (obj.AppOPBillIngModels.BillNo == 0)
            {
                model.BillDate = Convert.ToDateTime(obj.AppOPBillIngModels.BillDate);
                model.BillTime = Convert.ToDateTime(obj.AppOPBillIngModels.BillTime);
                model.AddedBy = CurrentUserId;
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _oPBillingService.InsertAppointmentCreditBillAsyncSP(Regmodel, objVisitDetail, model, objPayment, ObjPackagecharge, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.", new { model.BillNo, model.OpdIpdId });
        }


        [HttpPost("OPDraftBillInsert")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse> OPDraftBillInsert(DraftBillModel obj)
        {
            TDrbill model = obj.DRBill.MapTo<TDrbill>();
            List<TDrbillDet> model1 = obj.TDrbillDet.MapTo<List<TDrbillDet>>();
            List<TDraddCharge> model2 = obj.TDraddCharge.MapTo<List<TDraddCharge>>();

            if (obj.DRBill.Drbno == 0)
            {
                model.BillDate = Convert.ToDateTime(model.BillDate);
                model.BillTime = Convert.ToDateTime(model.BillTime);
                model.AddedBy = CurrentUserId;

                await _oPBillingService.InsertAsyncTDrbill(model, model1, model2, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.", model.Drbno);
        }
        [HttpPost("OPDraftBillUpdate")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.Add)]
        public async Task<ApiResponse> OPDraftBillUpdate(DraftBillModel obj)
        {
            TDrbill model = obj.DRBill.MapTo<TDrbill>();
            List<TDrbillDet> model1 = obj.TDrbillDet.MapTo<List<TDrbillDet>>();
            List<TDraddCharge> model2 = obj.TDraddCharge.MapTo<List<TDraddCharge>>();

            if (obj.DRBill.Drbno != 0)
            {
                model.BillDate = Convert.ToDateTime(model.BillDate);
                model.BillTime = Convert.ToDateTime(model.BillTime);
                model.AddedBy = CurrentUserId;

                await _oPBillingService.UpdateAsyncTDrbill(model, model1, model2, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  Update successfully.", model.Drbno);
        }

        [HttpPost("Cancel")]
        //[Permission(PageCode = "Bill", Permission = PagePermission.Delete)]
        public ApiResponse Cancel(DRBillCancel obj)
        {
            TDrbill model = obj.MapTo<TDrbill>();
            if (obj.Drbno != 0)
            {
                model.Drbno = obj.Drbno;
                _oPBillingService.Cancel(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.");
        }
    }
}
