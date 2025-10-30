using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;
using static HIMS.API.Models.Inventory.Masters.OTBookingRequestModel;

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class OTBookingController : BaseController
            //public class OTRequestController : BaseController

    {
        private readonly IOTBookingRequestService _OTBookingRequestService;
        private readonly IGenericService<TOtbookingRequest> _repository;

        public OTBookingController(IOTBookingRequestService repository, IGenericService<TOtbookingRequest> repository1)
        {
            _OTBookingRequestService = repository;
            _repository = repository1;

        }
        [HttpPost("OtbookingRequestList")]
        [Permission(PageCode = "OTRequest", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<OTBookingRequestListDto> OTBookinglist = await _OTBookingRequestService.GetListAsync(objGrid);
            return Ok(OTBookinglist.ToGridResponse(objGrid, "OTBookinglist "));
        }
        [HttpPost("OTBookingRequestEmergencyList")]
        [Permission(PageCode = "OTRequest", Permission = PagePermission.View)]
        public async Task<IActionResult> List1(GridRequestModel objGrid)
        {
            IPagedList<OTBookingRequestEmergencyListDto> OTBookingRequestEmergencyList = await _OTBookingRequestService.GetListAsynco(objGrid);
            return Ok(OTBookingRequestEmergencyList.ToGridResponse(objGrid, "OTBookingRequestEmergencyList "));
        }

      

        [HttpPost("Cancel")]
        [Permission(PageCode = "OTRequest", Permission = PagePermission.Delete)]
        public ApiResponse Cancel(OTBookingRequestCancel obj)
        {
            TOtbookingRequest model = obj.MapTo<TOtbookingRequest>();

            if (obj.OtbookingId != 0)
            {
                model.OtbookingId = obj.OtbookingId;
                _OTBookingRequestService.Cancel(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.");
        }
    }
}
