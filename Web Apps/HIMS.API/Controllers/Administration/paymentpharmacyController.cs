using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Inventory;
using HIMS.Core;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using HIMS.Services.Inventory;
using HIMS.API.Models.Inventory.Masters;
using HIMS.Services.Administration;
using HIMS.API.Models.Masters;
using Asp.Versioning;
using HIMS.Api.Controllers;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class paymentpharmacyController : BaseController
    {
        private readonly IPaymentpharmacyService _paymentpharmacyService;
        public paymentpharmacyController(IPaymentpharmacyService repository)
        {
            _paymentpharmacyService = repository;
        }
        [HttpPost("InsertEDMX")]
        [Permission(PageCode = "PaymentPharmacy", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(paymentpharmacyModel obj)
        {
            PaymentPharmacy model = obj.MapTo<PaymentPharmacy>();
            if (obj.PaymentId == 0)
            {
                model.PaymentTime = Convert.ToDateTime(obj.PaymentTime);
                model.PaymentDate = Convert.ToDateTime(obj.PaymentDate);

                model.AddBy = CurrentUserId;
                //model.IsActive = true;
                await _paymentpharmacyService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "paymentpharmacy   added successfully.");
        }
        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "PaymentPharmacy", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(paymentpharmacyModel obj)
        {
            PaymentPharmacy model = obj.MapTo<PaymentPharmacy>();
            if (obj.PaymentId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.PaymentTime = Convert.ToDateTime(obj.PaymentTime);
                await _paymentpharmacyService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "paymentpharmacy  updated successfully.");
        }


    }
}
