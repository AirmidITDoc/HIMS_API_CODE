using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;
using static HIMS.API.Models.OutPatient.RefundAdvanceModelValidator;

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AdvanceController : BaseController
    {
        private readonly IAdvanceService _IAdvanceService;
        private readonly IGenericService<AdvanceHeader> _repository1;
        public AdvanceController(IAdvanceService repository, IGenericService<AdvanceHeader> AdvanceHeaderrepository)
        {
            _IAdvanceService = repository;
            _repository1 = AdvanceHeaderrepository;
        }

        [HttpPost("PatientWiseAdvanceList")]
        //[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> PatientWiseAdvanceList(GridRequestModel objGrid)
        {
            IPagedList<PatientWiseAdvanceListDto> PatientWiseAdvanceList = await _IAdvanceService.PatientWiseAdvanceList(objGrid);
            return Ok(PatientWiseAdvanceList.ToGridResponse(objGrid, "Patient Wise Advance List"));

        }

        [HttpPost("BrowseAdvanceList")]
        [Permission]
        //[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> AdvanceList(GridRequestModel objGrid)
        {
            IPagedList<AdvanceListDto> AdvanceList = await _IAdvanceService.GetAdvanceListAsync(objGrid);
            return Ok(AdvanceList.ToGridResponse(objGrid, "Advance List"));
        }
        [HttpPost("PatientRefundOfAdvancesList")]
        [Permission]
        //[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> RefundOfAdvancesList(GridRequestModel objGrid)
        {
            IPagedList<RefundOfAdvancesListDto> RefundOfAdvancesList = await _IAdvanceService.GetAdvancesListAsync(objGrid);
            return Ok(RefundOfAdvancesList.ToGridResponse(objGrid, "Patient Refund Of Advances List"));
        }

        //List API Get By Id
        [HttpGet("{id?}")]
        [Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data1 = await _repository1.GetById(x => x.OpdIpdId == id);
            return data1.ToSingleResponse<AdvanceHeader, AdvanceHeaderModel>("Advance Details ");
        }

        [HttpPost("BrowseRefundOfAdvanceList")]
        [Permission]
        //[Permission(PageCode = "Advance", Permission = PagePermission.View)]
        public async Task<IActionResult> RefundAdvanceList(GridRequestModel objGrid)
        {
            IPagedList<RefundOfAdvanceListDto> RefundAdvanceList = await _IAdvanceService.GetRefundOfAdvanceListAsync(objGrid);
            return Ok(RefundAdvanceList.ToGridResponse(objGrid, "Refund Of Advance List"));
        }

        [HttpPost("InsertSP")]
        [Permission(PageCode = "Advance", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(ModelAdvance1 obj)
        {
            AdvanceHeader model = obj.Advance.MapTo<AdvanceHeader>();
            AdvanceDetail objAdvanceDetail = obj.AdvanceDetail.MapTo<AdvanceDetail>();
            Payment objpayment = obj.AdvancePayment.MapTo<Payment>();
            List<TPayment> ObjTPayment = obj.TPayments.MapTo<List<TPayment>>();

            if (obj.Advance.AdvanceId == 0)
            {
                model.Date = Convert.ToDateTime(obj.Advance.Date);
                model.AddedBy = CurrentUserId;

                objAdvanceDetail.Date = Convert.ToDateTime(obj.AdvanceDetail.Date);
                objAdvanceDetail.AddedBy = CurrentUserId;
                objpayment.PaymentTime = Convert.ToDateTime(objpayment.PaymentTime);
                objpayment.AddBy = CurrentUserId;

                await _IAdvanceService.InsertAdvanceAsyncSP(model, objAdvanceDetail, objpayment, ObjTPayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", objpayment.AdvanceId);
        }
        [HttpPut("Edit")]
        [Permission(PageCode = "Advance", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(UpdateAdvance obj)
        {
            AdvanceHeader model = obj.Advance.MapTo<AdvanceHeader>();
            AdvanceDetail objAdvanceDetail = obj.AdvanceDetail.MapTo<AdvanceDetail>();
            Payment objpayment = obj.AdvancePayment.MapTo<Payment>();
            List<TPayment> ObjTPayment = obj.TPayments.MapTo<List<TPayment>>();


            if (obj.Advance.AdvanceId != 0)
            {
                model.AddedBy = CurrentUserId;
                objAdvanceDetail.Date = Convert.ToDateTime(obj.AdvanceDetail.Date);
                objAdvanceDetail.AddedBy = CurrentUserId;
                objpayment.PaymentTime = Convert.ToDateTime(objpayment.PaymentTime);
                objpayment.AddBy = CurrentUserId;

                await _IAdvanceService.UpdateAdvanceSP(model, objAdvanceDetail, objpayment, ObjTPayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Update successfully.", objpayment.AdvanceId);
        }



        [HttpPost("IPRefundofAdvanceInsert")]
        [Permission(PageCode = "Advance", Permission = PagePermission.Add)]
        public ApiResponse IPInsertAsyncSP(RefundsModel obj)
        {
            Refund model = obj.Refund.MapTo<Refund>();
            AdvanceHeader AdvanceHeadermodel = obj.advanceHeaderupdate.MapTo<AdvanceHeader>();
            List<AdvRefundDetail> AdvDetailmodel = obj.AdvDetailRefund.MapTo<List<AdvRefundDetail>>();
            List<AdvanceDetail> objAdvanceDetail = obj.AdveDetailupdate.MapTo<List<AdvanceDetail>>();
            Payment objpayment = obj.payment.MapTo<Payment>();
            List<TPayment> ObjTPayment = obj.TPayments.MapTo<List<TPayment>>();



            if (obj.Refund.RefundId == 0)
            {
                model.RefundDate = Convert.ToDateTime(obj.Refund.RefundDate);
                model.RefundTime = Convert.ToDateTime(obj.Refund.RefundTime);
                model.AddedBy = CurrentUserId;

                _IAdvanceService.IPInsertSP(model, AdvanceHeadermodel, AdvDetailmodel, objAdvanceDetail, objpayment, ObjTPayment ,CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.RefundId);
        }



        [HttpPut("UpdateAdvanceDate")]
        [Permission(PageCode = "Advance", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(UpdateAdvanceCancel obj)
        {
            AdvanceDetail model = obj.MapTo<AdvanceDetail>();


            if (obj.AdvanceDetailId != 0)
            {
                model.AddedBy = CurrentUserId;
                model.IsCancelledDate = Convert.ToDateTime(model.IsCancelledDate);
                model.IsCancelledby = CurrentUserId;


                await _IAdvanceService.UpdateAdvance(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Update successfully .");
        }

        [HttpPost("Cancel")]
        [Permission(PageCode = "Advance", Permission = PagePermission.Add)]
        public ApiResponse Update(UpdateCancel obj)
        {
            if (obj.AdvanceId == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
            // 👇 Manually assign fields from LabRequestsModel to AddCharge
            var model = new AdvanceHeader
            {
                AdvanceAmount = obj.AdvanceAmount,
                AdvanceId = obj.AdvanceId,
                AddedBy = obj.AddedBy,


            };
            _IAdvanceService.Cancel(model, obj.AdvanceDetailId);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Cancel  successfully.");

        }


    }
}
