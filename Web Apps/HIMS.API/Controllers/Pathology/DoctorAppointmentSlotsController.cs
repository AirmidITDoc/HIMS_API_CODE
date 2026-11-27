

using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OutPatient;
using HIMS.API.Models.Pathology;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.Pathlogy;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Pathology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DoctorAppointmentSlotsController : BaseController
    {
        private readonly IDoctorAppointmentSlotsService _doctorAppointmentSlotsService; 
        private readonly IGenericService<TDoctorAppointmentSlot> _repository;
        public DoctorAppointmentSlotsController(IDoctorAppointmentSlotsService doctorAppointmentSlotsService, IGenericService<TDoctorAppointmentSlot> repository)
        {
            _doctorAppointmentSlotsService = doctorAppointmentSlotsService;
            _repository = repository;
        }


        [HttpPost("DoctorAppointmentSlotsList")]
        [Permission(PageCode = "ExternalInvestigation", Permission = PagePermission.View)]
       // [AllowAnonymous]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<DoctorAppointmentSlotsListDto> list = await _doctorAppointmentSlotsService.GetDoctorAppointmentSlotsListAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "Doctor Appointment Slots List"));
        }
        [HttpPost("Insert")]
        [Permission(PageCode = "ExternalInvestigation", Permission = PagePermission.Add)]
        //[AllowAnonymous]
        public async Task<ApiResponse> Insert(DoctorAppointmentSlotsModel obj)
        {
            TDoctorAppointmentSlot model = obj.MapTo<TDoctorAppointmentSlot>();
            if (obj.DoctorId != 0)
            {
                model.CreatedBy = CurrentUserId;
                await _doctorAppointmentSlotsService.Insert(model, obj.SlotDuration, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Added successfully.");
        }

        [HttpPost("Cancel")]
        [Permission(PageCode = "ExternalInvestigation", Permission = PagePermission.Delete)]
        //[AllowAnonymous]
        public async Task<ApiResponse> Cancel(DoctorAppointmentSlotsCancelModel obj)
        {
            TDoctorAppointmentSlot model = obj.MapTo<TDoctorAppointmentSlot>();
            if (obj.SlotId != 0)
            {
                model.IsCancelledBy = CurrentUserId;
                await _doctorAppointmentSlotsService.Cancel(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Cancelled successfully.");
        }


    }
}
