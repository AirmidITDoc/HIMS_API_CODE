using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.OPPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PhoneAppointment2Controller : BaseController
    {
        private readonly IPhoneAppointment2Service _IPhoneAppointment2Service;
        public PhoneAppointment2Controller(IPhoneAppointment2Service repository)
        {
            _IPhoneAppointment2Service = repository;
        }
        [HttpPost("PhoneAppList")]
        [Permission(PageCode = "PhoneAppointment", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<PhoneAppointment2ListDto> PhoneAppList = await _IPhoneAppointment2Service.GetListAsync(objGrid);
            return Ok(PhoneAppList.ToGridResponse(objGrid, "PhoneApp List"));
        }

        [HttpPost("InsertSP")]
        //[Permission(PageCode = "PhoneAppointment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PhoneAppointment2Model obj)
        {
            TPhoneAppointment model = obj.MapTo<TPhoneAppointment>();
            if (obj.PhoneAppId == 0)
            {
                model.AppDate = Convert.ToDateTime(obj.AppDate);
                model.AppTime = Convert.ToDateTime(obj.AppTime);

                model.UpdatedBy = CurrentUserId;
                await _IPhoneAppointment2Service.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PhoneAppointment added successfully.", model);
        }


        [HttpPost("Cancel")]
        [Permission(PageCode = "PhoneAppointment", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(PhoneAppointmentCancel obj)
        {
            TPhoneAppointment model = new();
            if (obj.PhoneAppId != 0)
            {
                model.PhoneAppId = obj.PhoneAppId;
                model.IsCancelled = true;
                model.IsCancelledBy = CurrentUserId;
                model.IsCancelledDate = DateTime.Now;
                await _IPhoneAppointment2Service.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "PhoneAppointment Canceled successfully.");
        }

    }
}

