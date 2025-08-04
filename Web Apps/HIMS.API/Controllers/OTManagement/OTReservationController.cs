using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.DTO.Inventory;
using HIMS.Services;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;
using HIMS.Data.DTO.IPPatient;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Inventory.Masters;
using HIMS.Data.Models;
using HIMS.API.Models.IPPatient;
using static HIMS.API.Models.IPPatient.OtbookingModelValidator;
using DocumentFormat.OpenXml.Office2010.Excel;
using HIMS.API.Models.OTManagement;
using Asp.Versioning;

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
        [HttpPost("OTBookinglist")]
        [Permission(PageCode = "OTReservation", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<OTBookinglistDto> OTBookinglist = await _OTService.GetListAsync(objGrid);
            return Ok(OTBookinglist.ToGridResponse(objGrid, "OTBookinglist "));
        }
        
        [HttpPost("InsertEDMX")]
        [Permission(PageCode = "OTReservation", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(OTReservationModel obj)
        {
            TOtReservation model = obj.MapTo<TOtReservation>();
            if (obj.OtreservationId == 0)
            {
                model.ReservationDate = Convert.ToDateTime(obj.ReservationDate);
                model.CreatedBy = CurrentUserId;

                //model.IsActive = true;
                await _OTService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "OTReservation", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(OTReservationModel obj)
        {
            TOtReservation model = obj.MapTo<TOtReservation>();
            if (obj.OtreservationId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ReservationTime = Convert.ToDateTime(obj.ReservationTime);
                model.ModifiedBy = CurrentUserId;

                await _OTService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

       
        [HttpPost("Cancel")]
        [Permission(PageCode = "OTReservation", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(OTReservationCancel obj)
        {
            TOtReservation model = new();
            if (obj.OtreservationId != 0)
            {
                model.OtreservationId = obj.OtreservationId;
                await _OTService.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.");
        }


    }
}
