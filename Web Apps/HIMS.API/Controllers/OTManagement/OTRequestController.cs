using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OTManagement;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.OTManagement;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;
using static HIMS.API.Models.OTManagement.OTBookingRequestModel;

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
        private readonly IGenericService<MOttableMaster> _repository4;
        public OTRequestController(IOTBookingRequestService repository, IGenericService<TOtRequestHeader> repository1, IGenericService<TOtRequestAttendingDetail> repository2, IGenericService<TOtRequestDiagnosis> repository3, IGenericService<TOtRequestSurgeryDetail> repository4, IGenericService<MOttableMaster> repository5)
        {
            _OTBookingRequestService = repository;
            _repository = repository1;
            _repository1 = repository2;
            _repository2 = repository3;
            _repository3 = repository4;
            _repository4 = repository5;

        }
        [HttpGet("{id?}")]
        //[Permission(PageCode = "OTRequest", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {

            var data1 = await _repository.GetById(x => x.OtrequestId == id);
            return data1.ToSingleResponse<TOtRequestHeader, GetTOtRequestHeaderModel>("TOtRequestHeader");
        }
        [HttpGet]
        [Route("getlocationByOttable")]
        //[Permission(PageCode = "Prefix", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetDropdown()
        {
            var MOttableMasterList = await _repository4.GetAll(x => x.IsActive.Value);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "getlocationByOttable dropdown", MOttableMasterList.Select(x => new { x.OttableId, x.LocationId, x.OttableName }));
        }


        //[HttpGet("getlocationByOttable/{id?}")]
        ////[Permission(PageCode = "Prefix", Permission = PagePermission.View)]
        //public async Task<ApiResponse> GetOt(int id)
        //{
        //    if (id == 0)
        //    {
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
        //    }
        //    var data = await _repository4.GetById(x => x.OttableId == id);
        //    return data.ToSingleResponse<MOttableMaster, OtTableModel>("MOttableMaster");
        //}


        [HttpPost("OTRequestList")]
        //[Permission(PageCode = "OTRequest", Permission = PagePermission.View)]
        public async Task<IActionResult> List7(GridRequestModel objGrid)
        {
            IPagedList<OtRequestListDto> OTRequestList = await _OTBookingRequestService.GetListAsyncot(objGrid);
            return Ok(OTRequestList.ToGridResponse(objGrid, "OT Request List "));
        }


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
                foreach (var q in model.TOtRequestAttendingDetails)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }
                foreach (var q in model.TOtRequestDiagnoses)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }
                foreach (var q in model.TOtRequestSurgeryDetails)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _OTBookingRequestService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.",model.OtrequestId);
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
                foreach (var q in model.TOtRequestAttendingDetails)
                {
                    if (q.OtrequestAttendingDetId == 0)
                    {
                        q.CreatedBy = CurrentUserId;
                        q.CreatedDate = DateTime.Now;
                    }
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = DateTime.Now;
                    q.OtrequestAttendingDetId = 0;
                }

                foreach (var v in model.TOtRequestDiagnoses)
                {
                    if (v.OtrequestDiagnosisDetId == 0)
                    {
                        v.Createdby = CurrentUserId;
                        v.CreatedDate = DateTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = DateTime.Now;
                    v.OtrequestDiagnosisDetId = 0;
                }

                foreach (var p in model.TOtRequestSurgeryDetails)
                {
                    if (p.OtrequestSurgeryDetId == 0)
                    {
                        p.Createdby = CurrentUserId;
                        p.CreatedDate = DateTime.Now;
                    }
                    p.ModifiedBy = CurrentUserId;
                    p.ModifiedDate = DateTime.Now;
                    p.OtrequestSurgeryDetId = 0;
                }
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _OTBookingRequestService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.OtrequestId);
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
