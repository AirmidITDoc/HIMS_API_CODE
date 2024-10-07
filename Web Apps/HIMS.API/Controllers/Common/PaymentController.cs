using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Common;
using HIMS.API.Models.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Common;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Common
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



        [HttpPost("PaymentInsert")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(NewPaymentModel obj)
        {
            Payment model = obj.MapTo<Payment>();
                     
            if (obj.PaymentId == 0)
            {
                 model.PaymentTime = Convert.ToDateTime(obj.PaymentTime);
              
                await _IPaymentService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Payment added successfully.");

        }
    }
}
