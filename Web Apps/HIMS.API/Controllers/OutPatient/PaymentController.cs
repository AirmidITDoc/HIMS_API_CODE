using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Models.OutPatient;
using HIMS.API.Extensions;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OutPatient;
using HIMS.API.Models.Pharmacy;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using HIMS.Services.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OutPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PaymentController : BaseController
    {
        private readonly IPaymentService _IPaymentService;
        public PaymentController(IPaymentService repository)
        {
            _IPaymentService = repository;
        }

        ////Add API
        //[HttpPost("InsertEDMX")]
        ////[Permission(PageCode = "PhoneAppointment", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> Add(OPPaymentModel obj)
        //{
        //    Payment model = obj.MapTo<Payment>();
        //    if (obj.PaymentId == 0)
        //    {
        //        model.PaymentDate = Convert.ToDateTime(obj.PaymentDate);
        //        model.PaymentTime = Convert.ToDateTime(obj.PaymentTime);
        //        await _IPaymentService.InsertAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Payment added successfully.");
        //}


        //Add API
        [HttpPost("PaymentInsertSP")]
        //[Permission(PageCode = "PhoneAppointment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PaymentModel1 obj)
        {
            Payment model = obj.MapTo<Payment>();
            if (obj.PaymentId == 0)
            {
                model.PaymentDate = Convert.ToDateTime(obj.PaymentDate);
                model.PaymentTime = Convert.ToDateTime(obj.PaymentTime);
                await _IPaymentService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Payment added successfully.");
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
