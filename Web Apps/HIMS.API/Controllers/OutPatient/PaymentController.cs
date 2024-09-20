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
    }
}
