using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.OPPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PhoneAppointmet1Controller : BaseController
    {
        private readonly IPhoneAppointment1Service _IPhoneAppointment1Service;
        public PhoneAppointmet1Controller(IPhoneAppointment1Service repository)
        {
            _IPhoneAppointment1Service = repository;
        }
        [HttpPost("InsertSP")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertAsyncSP(phoneAppointmentModel1 obj)
        {
            TPhoneAppointment model = obj.MapTo<TPhoneAppointment>();
            if (obj.PhoneAppId == 0)
            {
                model.AppDate = Convert.ToDateTime(obj.AppDate);
                model.AppTime = Convert.ToDateTime(obj.AppTime);
                model.UpdatedBy = CurrentUserId;
                await _IPhoneAppointment1Service.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PhoneApp added  added successfully.");
        }
    }
}
