using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Inventory.Masters;
using HIMS.API.Models.IPPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services;
using HIMS.Services.Inventory;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;
using static HIMS.API.Models.Inventory.Masters.OTBookingRequestModel;
using static HIMS.API.Models.IPPatient.OtbookingModelValidator;

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class OTBookingController : BaseController
    {
        private readonly IOTBookingRequestService _OTBookingRequestService;
        public OTBookingController(IOTBookingRequestService repository)
        {
            _OTBookingRequestService = repository;
        }
        [HttpPost("OtbookingRequestList")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<OTBookingRequestListDto> OTBookinglist = await _OTBookingRequestService.GetListAsync(objGrid);
            return Ok(OTBookinglist.ToGridResponse(objGrid, "OTBookinglist "));
        }

        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(OTBookingRequestModel obj)
        {
            TOtbookingRequest model = obj.MapTo<TOtbookingRequest>();
            if (obj.OtbookingId == 0)
            {
                model.OtbookingDate = Convert.ToDateTime(obj.OtbookingDate);
                model.OtbookingTime = Convert.ToDateTime(obj.OtbookingTime);
                model.CreatedDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedBy = CurrentUserId;
                await _OTBookingRequestService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }
        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(OTBookingRequestModel obj)
        {
            TOtbookingRequest model = obj.MapTo<TOtbookingRequest>();
            if (obj.OtbookingId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.OtbookingDate = Convert.ToDateTime(obj.OtbookingDate);
                model.OtbookingTime = Convert.ToDateTime(obj.OtbookingTime);
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _OTBookingRequestService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }


        [HttpPost("Cancel")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(OTBookingRequestCancel obj)
        {
            TOtbookingRequest model = new();
            if (obj.OtbookingId != 0)
            {
                model.OtbookingId = obj.OtbookingId;
                await _OTBookingRequestService.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.");
        }
    }
}
