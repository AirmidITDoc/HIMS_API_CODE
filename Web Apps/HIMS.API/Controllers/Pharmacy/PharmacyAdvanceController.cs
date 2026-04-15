using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Services.Pharmacy;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pharmacy
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PharmacyAdvanceController : BaseController
    {
        private readonly IPharmacyAdvanceService _IPharmacyAdvanceService;
        public PharmacyAdvanceController(IPharmacyAdvanceService repository)
        {
            _IPharmacyAdvanceService = repository;
        }
        [HttpPost("PharmacyAdvanceInsert")]
        [Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public ApiResponse Insert(PharAdvanceModel obj)
        {
            TPhadvanceHeader model = obj.PharmacyAdvance.MapTo<TPhadvanceHeader>();
            TPhadvanceDetail model1 = obj.PharmacyAdvanceDetails.MapTo<TPhadvanceDetail>();
            PaymentPharmacy model3 = obj.PaymentPharmacy.MapTo<PaymentPharmacy>();
            List<TPaymentPharmacy> TPayments = obj.TPayments.MapTo<List<TPaymentPharmacy>>();


            if (obj.PharmacyAdvance.AdvanceId == 0)
            {
                model.Date = Convert.ToDateTime(obj.PharmacyAdvance.Date);
                model.AddedBy = CurrentUserId;
                _IPharmacyAdvanceService.Insert(model, model1, model3, TPayments, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model1.AdvanceDetailId);
        }
        [HttpPut("PharmacyAdvanceUpdate")]
        [Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public ApiResponse Update(PharmacyHeaderUpdate obj)
        {
            TPhadvanceHeader model = obj.PharmacyHeader.MapTo<TPhadvanceHeader>();
            TPhadvanceDetail model1 = obj.PharmacyAdvanceDetails.MapTo<TPhadvanceDetail>();
            PaymentPharmacy model3 = obj.PaymentPharmacy.MapTo<PaymentPharmacy>();
            List<TPaymentPharmacy> TPayments = obj.TPayments.MapTo<List<TPaymentPharmacy>>();
            if (obj.PharmacyHeader.AdvanceId != 0)
            {
                model.AddedBy = CurrentUserId;
                _IPharmacyAdvanceService.Update(model, model1, model3, TPayments, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Update  successfully.", model1.AdvanceDetailId);
        }

    }
}
