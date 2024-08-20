using HIMS.Data.Models;
using HIMS.Data;
using Microsoft.AspNetCore.Mvc;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Models.OutPatient;
using HIMS.API.Extensions;

namespace HIMS.API.Controllers.OutPatient
{
    public class PaymentController : BaseController
    {
        private readonly IGenericService<Payment> _repository;
        public PaymentController(IGenericService<Payment> repository)
        {
            _repository = repository;
        }

        //Add API
        [HttpPost]
        //[Permission(PageCode = "PhoneAppointment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Post(OPPaymentModel obj)
        {
            Payment model = obj.MapTo<Payment>();
            if (obj.PaymentId == 0)
            {
               // model.AddedBy = CurrentUserId;
                //model.CreatedDate = DateTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Payment added successfully.");
        }
    }
}
