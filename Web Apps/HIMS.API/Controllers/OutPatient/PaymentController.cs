using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Models.OutPatient;
using HIMS.API.Extensions;
using HIMS.Services.OutPatient;

namespace HIMS.API.Controllers.OutPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PaymentController : BaseController
    {
        private readonly IOPPayment _oPPayment;
        public PaymentController(IOPPayment repository)
        {
            _oPPayment = repository;
        }



        [HttpPost("OPPaymentInsert")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> OPPaymentInsert(OPPaymentdetailModel obj)
        {
          
            Payment objPayment = obj.MapTo<Payment>();
            if (obj.PaymentId == 0)
            {
                                              
                await _oPPayment.InsertAsyncSP(objPayment, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OP Payment added successfully.");
        }

        [HttpPost("PaymentUpdate")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(PaymentModel1 obj)
        {
            Payment model = obj.MapTo<Payment>();
            if (obj.PaymentId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.PaymentDate = Convert.ToDateTime(obj.PaymentDate);
                model.PaymentTime = Convert.ToDateTime(obj.PaymentTime);
                await _IPaymentService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Payment updated successfully.");
        }

    }
}
