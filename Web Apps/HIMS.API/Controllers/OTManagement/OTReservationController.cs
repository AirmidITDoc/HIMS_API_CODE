using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.IPPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using HIMS.Services;
using Microsoft.AspNetCore.Mvc;
using static HIMS.API.Models.IPPatient.OtbookingModelValidator;

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class OTReservationController : BaseController
    {

        private readonly IOTService _OTService;
        public OTReservationController(IOTService repository)
        {
            _OTService = repository;
        }
        [HttpPost("OTReservationlist")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<OTBookinglistDto> OTBookinglist = await _OTService.GetListAsync(objGrid);
            return Ok(OTBookinglist.ToGridResponse(objGrid, "OT Reservation list "));
        }

        [HttpGet("search-patient-OTRequest")]
        public ApiResponse SearchPatientNew(string Keyword)
        {
            var data = _OTService.SearchPatient(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OT Request List data", data);
        }

        [HttpPost("Insert")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(OTReservationModel obj)
        {
            TOtReservation model = obj.MapTo<TOtReservation>();
            if (obj.OtreservationId == 0)
            {
                model.ReservationDate = Convert.ToDateTime(obj.ReservationDate);
                model.ReservationTime = Convert.ToDateTime(obj.ReservationTime);

                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;

                await _OTService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(OTReservationModel obj)
        {
            TOtReservation model = obj.MapTo<TOtReservation>();
            if (obj.OtreservationId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ReservationDate = Convert.ToDateTime(obj.ReservationDate);
                model.ReservationTime = Convert.ToDateTime(obj.ReservationTime);

                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;


                await _OTService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }




        [HttpPost("Cancel")]
        [Permission(PageCode = "OTReservation", Permission = PagePermission.Delete)]
        public ApiResponse Cancel(OTReservationCancel obj)
        {
            TOtReservation model = obj.MapTo<TOtReservation>();
            if (obj.OtreservationId != 0)
            {
                model.OtreservationId = obj.OtreservationId;
                _OTService.Cancel(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.");
        }

        [HttpPost("OTBookingPostPone")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.Add)]
        public ApiResponse Insert(OTBookingPostPoneModel obj)
        {
            TOtReservation model = obj.MapTo<TOtReservation>();
            if (obj.NewOTReservationId == 0)
            {
                model.Opdate = Convert.ToDateTime(obj.Opdate);
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                _OTService.InsertSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

    }
}
