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

namespace HIMS.API.Controllers.IPPatient
{
    public class OTReservationController : BaseController
    {

        private readonly IOTService _OTService;
        public OTReservationController(IOTService repository)
        {
            _OTService = repository;
        }
        [HttpPost("OTBookinglist")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<OTBookinglistDto> OTBookinglist = await _OTService.GetListAsync(objGrid);
            return Ok(OTBookinglist.ToGridResponse(objGrid, "OTBookinglist "));
        }
        
        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(OtbookingModel obj)
        {
            TOtbooking model = obj.MapTo<TOtbooking>();
            if (obj.OtbookingId == 0)
            {
                model.TranDate = Convert.ToDateTime(obj.TranDate);
                model.CreatedBy = CurrentUserId;
                //model.IsActive = true;
                await _OTService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(OtbookingModel obj)
        {
            TOtbooking model = obj.MapTo<TOtbooking>();
            if (obj.OtbookingId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.TranTime = Convert.ToDateTime(obj.TranTime);
                await _OTService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }


    }
}
