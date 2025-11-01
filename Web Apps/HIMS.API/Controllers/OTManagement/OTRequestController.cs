using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;
using static HIMS.API.Models.Inventory.Masters.OTBookingRequestModel;

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class OTRequestController : BaseController
    {
        private readonly IOTBookingRequestService _OTBookingRequestService;
        private readonly IGenericService<TOtRequestHeader> _repository;
        private readonly IGenericService<TOtRequestAttendingDetail> _repository1;
        private readonly IGenericService<TOtRequestDiagnosis> _repository2;
        private readonly IGenericService<TOtRequestSurgeryDetail> _repository3;



        public OTRequestController(IOTBookingRequestService repository, IGenericService<TOtRequestHeader> repository1, IGenericService<TOtRequestAttendingDetail> repository2, IGenericService<TOtRequestDiagnosis> repository3, IGenericService<TOtRequestSurgeryDetail> repository4)
        {
            _OTBookingRequestService = repository;
            _repository = repository1;
            _repository1 = repository2;
            _repository2 = repository3;
            _repository3 = repository4;
        }
       
        //[HttpPost("OTBookingRequestEmergencyList")]
        //[Permission(PageCode = "OTRequest", Permission = PagePermission.View)]
        //public async Task<IActionResult> List1(GridRequestModel objGrid)
        //{
        //    IPagedList<OTBookingRequestEmergencyListDto> OTBookingRequestEmergencyList = await _OTBookingRequestService.GetListAsynco(objGrid);
        //    return Ok(OTBookingRequestEmergencyList.ToGridResponse(objGrid, "OTBookingRequestEmergencyList "));
        //}
       
     
        [HttpPost("OtRequestDiagnosisList")]

        //[Permission(PageCode = "OTRequest", Permission = PagePermission.View)]
        public async Task<IActionResult> List4(GridRequestModel objGrid)
        {
            IPagedList<TOtRequestDiagnosis> OtRequestDiagnosisList = await _repository2.GetAllPagedAsync(objGrid);
            return Ok(OtRequestDiagnosisList.ToGridResponse(objGrid, "OT Request Diagnosis List "));
        }
       

        [HttpPost("OtRequestSurgeryDetailList")]
        //[Permission(PageCode = "OTRequest", Permission = PagePermission.View)]
        public async Task<IActionResult> List6(GridRequestModel objGrid)
        {
            IPagedList<OtRequestSurgeryDetailListDto> OtRequestSurgeryDetailList = await _OTBookingRequestService.GetListAsyncs(objGrid);
            return Ok(OtRequestSurgeryDetailList.ToGridResponse(objGrid, "OT Request SurgeryDetail List "));
        }

        [HttpPost("OTRequestList")]
        //[Permission(PageCode = "OTRequest", Permission = PagePermission.View)]
        public async Task<IActionResult> List7(GridRequestModel objGrid)
        {
            IPagedList<OtRequestListDto> OTRequestList = await _OTBookingRequestService.GetListAsyncot(objGrid);
            return Ok(OTRequestList.ToGridResponse(objGrid, "OT Request List "));
        }

        [HttpPost("OtRequestAttendingDetailList")]
        //[Permission(PageCode = "OTRequest", Permission = PagePermission.View)]
        public async Task<IActionResult> Listr(GridRequestModel objGrid)
        {
            IPagedList<OtRequestAttendingDetailListDto> OtRequestAttendingDetailList = await _OTBookingRequestService.GetListAsyncor(objGrid);
            return Ok(OtRequestAttendingDetailList.ToGridResponse(objGrid, "OT Request AttendingDetail List "));
        }

        [HttpGet("GetRequestDiagnosisList")]
        //[Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDiagnosisList(string DescriptionType)
        {
            var result = await _OTBookingRequestService.GetDiagnosisListAsync(DescriptionType);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get Diagnosis List", result);
        }



        [HttpPost("Insert")]
        //[Permission(PageCode = "OTRequest", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(TOtRequestHeaderModel obj)
        {
            TOtRequestHeader model = obj.MapTo<TOtRequestHeader>();
            if (obj.OtrequestId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _OTBookingRequestService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }



        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "OTRequest", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(TOtRequestHeaderModel obj)
        {
            TOtRequestHeader model = obj.MapTo<TOtRequestHeader>();
            if (obj.OtrequestId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _OTBookingRequestService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
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
