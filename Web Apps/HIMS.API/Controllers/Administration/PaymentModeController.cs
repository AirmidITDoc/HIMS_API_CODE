using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Administration;
using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using HIMS.Services.Administration;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PaymentModeController : BaseController
    {
        private readonly IPaymentModeService _IPaymentModeService;

        public PaymentModeController(IPaymentModeService repository)
        {
            _IPaymentModeService = repository;

        }

        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "Payment", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(PaymentModeModel obj)
        {
            Payment model = obj.MapTo<Payment>();
            if (obj.PaymentId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.PaymentTime = Convert.ToDateTime(obj.PaymentTime);
                model.PaymentDate = Convert.ToDateTime(obj.PaymentDate);
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IPaymentModeService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }

    }
}
