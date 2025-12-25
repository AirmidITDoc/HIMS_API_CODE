using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OutPatient;
using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class OPSettlementController : BaseController
    {
        private readonly IOPSettlementService _OPSettlementService;
        public OPSettlementController(IOPSettlementService repository)
        {
            _OPSettlementService = repository;
        }

        [HttpPost("InsertSettlement")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(OPSettlementModel obj)
        {
            Payment model = obj.OPCreditPayment.MapTo<Payment>();
            Bill BillUpdateModel = obj.BillUpdate.MapTo<Bill>();
            List<TPayment> ObjTPayment = obj.TPayments.MapTo<List<TPayment>>();

            if (obj.OPCreditPayment.PaymentId == 0)
            {
                model.PaymentDate = Convert.ToDateTime(obj.OPCreditPayment.PaymentDate);
                model.PaymentTime = Convert.ToDateTime(obj.OPCreditPayment.PaymentTime);

                model.AddBy = CurrentUserId;
                BillUpdateModel.CreatedBy = CurrentUserId;
                BillUpdateModel.CreatedDate = AppTime.Now;
                BillUpdateModel.ModifiedBy = CurrentUserId;
                BillUpdateModel.ModifiedDate = AppTime.Now;
                await _OPSettlementService.InsertAsyncSP(model, BillUpdateModel, ObjTPayment, CurrentUserId, CurrentUserName);

            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Op Settlement Save successfully.", model.PaymentId);
        }

        [HttpPost("InsertSettlementMultiple")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(OPSettlementMultipleModel obj)

        {
            List<Payment> model = obj.OPCreditPayment.MapTo<List<Payment>>();
            List<Bill> BillUpdateModel = obj.BillUpdate.MapTo<List<Bill>>();
            List<TPayment> ObjTPayment = obj.TPayments.MapTo<List<TPayment>>();

            if (model.Count > 0)

            {

                await _OPSettlementService.InsertSettlementMultiple(model, BillUpdateModel, ObjTPayment ,CurrentUserId, CurrentUserName);

            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, " Record Added successfully.", model.FirstOrDefault()?.PaymentId);
        }

    }
}
