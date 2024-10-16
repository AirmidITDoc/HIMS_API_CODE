using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.Data.Models;
using HIMS.Services.OPPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class PhoneAppointmentController : BaseController
    {
        private readonly IPhoneAppointmentService _IPhoneAppointmentService;
        public PhoneAppointmentController(IPhoneAppointmentService repository)
        {
            _IPhoneAppointmentService = repository;
        }

        [HttpPost("InsertSP")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(phoneAppointmentModel1 obj)
        {
            TPhoneAppointment model = obj.MapTo<TPhoneAppointment>();
            if (obj.PhoneAppId == 0)
            {
                model.AppDate = Convert.ToDateTime(obj.AppDate);
                model.AppTime = Convert.ToDateTime(obj.AppTime);

                model.UpdatedBy = CurrentUserId;
                model = await _IPhoneAppointmentService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PhoneApp added successfully.");
        }

        //[HttpPost("Cancel")]
        //[Permission(PageCode = "VisitDetail", Permission = PagePermission.Delete)]
        //public async Task<ApiResponse> Cancel(PhoneAppointmentCancel obj)
        //{
        //    TPhoneAppointment model = new();
        //    if (obj.PhoneAppId != 0)
        //    {
        //        model.PhoneAppId = obj.PhoneAppId;
        //        model.IsCancelled = true;
        //        model.IsCancelledBy = CurrentUserId;
        //        model.IsCancelledDate = DateTime.Now;
        //        await _IPhoneAppointmentService.CancelAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PhoneApp Canceled successfully.");
        //}


    }
}

