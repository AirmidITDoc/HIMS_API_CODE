using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pharmacy
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PharmacyRefundController : BaseController 
    {
        private readonly IPharmacyRefundService _IPharmacyRefundService;
        public PharmacyRefundController(IPharmacyRefundService repository)
        {
            _IPharmacyRefundService = repository;
        }

        [HttpPost("PharmacyRefundInsert")]
        //[Permission]
        [Permission(PageCode = "Pharmacy", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(PharRefundModel obj)

        {
            TPhRefund model = obj.PharmacyRefund.MapTo<TPhRefund>();
            TPhadvanceHeader model1 = obj.PhAdvanceHeader.MapTo<TPhadvanceHeader>();
            List<TPhadvRefundDetail> model3 = obj.PHAdvRefundDetail.MapTo<List<TPhadvRefundDetail>>();
            List<TPhadvanceDetail> model4 = obj.PHAdvanceDetailBalAmount.MapTo<List<TPhadvanceDetail>>();
            PaymentPharmacy model5 = obj.PharPayment.MapTo<PaymentPharmacy>();
            List<TPaymentPharmacy> TPayments = obj.TPayments.MapTo<List<TPaymentPharmacy>>();

            if (obj.PharmacyRefund.RefundId == 0)
            {
                model.AddBy = CurrentUserId;
                await _IPharmacyRefundService.Insert(model, model1, model3, model4, model5, TPayments, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Added Successfully.", model.RefundId);
        }
    }
}
