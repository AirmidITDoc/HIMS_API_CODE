using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory.Masters;
using HIMS.API.Models.IPPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
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
        private readonly IGenericService<TOtReservationHeader> _repository;
        private readonly IGenericService<TOtReservationDiagnosis> _repository1;


        public OTReservationController(IOTService repository, IGenericService<TOtReservationHeader> repository1, IGenericService<TOtReservationDiagnosis> repository2)
        {
            _OTService = repository;
            _repository = repository1;
            _repository1 = repository2;


        }
        [HttpGet("{id?}")]
        //[Permission(PageCode = "OTRequest", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {

            var data1 = await _repository.GetById(x => x.OtreservationId == id);
            return data1.ToSingleResponse<TOtReservationHeader, ReservationGetModel>("TOtReservationHeader");
        }

        [HttpGet("GetReservationDiagnosisList")]
        //[Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> GetReservationDiagnosisList(string DescriptionType)
        {
            var result = await _OTService.GetDiagnosisListAsync(DescriptionType);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get Diagnosis List", result);
        }

        [HttpPost("OTReservationlist")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.View)]
        public async Task<IActionResult> Reservationlist(GridRequestModel objGrid)
        {
            IPagedList<OTReservationListDto> ReservationAttendingDetailList = await _OTService.GetListOtReservationAsync(objGrid);
            return Ok(ReservationAttendingDetailList.ToGridResponse(objGrid, "OTReservation List"));
        }

        [HttpPost("OtReservationDiagnosisList")]

        //[Permission(PageCode = "OTRequest", Permission = PagePermission.View)]
        public async Task<IActionResult> List4(GridRequestModel objGrid)
        {
            IPagedList<TOtReservationDiagnosis> OtReservationDiagnosisList = await _repository1.GetAllPagedAsync(objGrid);
            return Ok(OtReservationDiagnosisList.ToGridResponse(objGrid, "OT Request Diagnosis List "));
        }


        [HttpPost("OtReservationAttendingDetailList")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<requestAttendentListDto> ReservationAttendingDetailList = await _OTService.OTGetListAsync(objGrid);
            return Ok(ReservationAttendingDetailList.ToGridResponse(objGrid, "ReservationAttendingDetail List "));
        }
        [HttpPost("OtReservationSurgeryDetailList")]
        //[Permission(PageCode = "OTReservation", Permission = PagePermission.View)]
        public async Task<IActionResult> Listot(GridRequestModel objGrid)
        {
            IPagedList<ReservationSurgeryDetailListDto> OtReservationSurgeryDetailList = await _OTService.OTreservationGetListAsync(objGrid);
            return Ok(OtReservationSurgeryDetailList.ToGridResponse(objGrid, "OtReservationSurgeryDetail List "));
        }

        [HttpGet("search-patient-OTRequest")]
        public ApiResponse SearchPatientNew(string Keyword)
        {
            var data = _OTService.SearchPatient(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OT Request List data", data);
        }
        [HttpPost("Insert")]
        //[Permission(PageCode = "OTRequest", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(ReservationModel obj)
        {
            TOtReservationHeader model = obj.MapTo<TOtReservationHeader>();
            if (obj.OtreservationId == 0)
            {
                foreach (var q in model.TOtReservationAttendingDetails)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }
                foreach (var q in model.TOtReservationSurgeryDetails)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }
                foreach (var q in model.TOtReservationDiagnoses)
                {
                    q.Createdby = CurrentUserId;
                    q.CreatedDate = DateTime.Now;

                }

                model.CreatedDate = DateTime.Now;
                model.Createdby = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _OTService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "OTRequest", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ReservationModel obj)
        {
            TOtReservationHeader model = obj.MapTo<TOtReservationHeader>();
            if (obj.OtreservationId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                foreach (var q in model.TOtReservationAttendingDetails)
                {
                    if (q.OtreservationAttendingDetId == 0)
                    {
                        q.Createdby = CurrentUserId;
                        q.CreatedDate = DateTime.Now;
                    }
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = DateTime.Now;
                    q.OtreservationAttendingDetId = 0;
                }

                foreach (var v in model.TOtReservationSurgeryDetails)
                {
                    if (v.OtreservationSurgeryDetId == 0)
                    {
                        v.Createdby = CurrentUserId;
                        v.CreatedDate = DateTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = DateTime.Now;
                    v.OtreservationSurgeryDetId = 0;
                }
                foreach (var v in model.TOtReservationDiagnoses)
                {
                    if (v.OtreservationDiagnosisDetId == 0)
                    {
                        v.Createdby = CurrentUserId;
                        v.CreatedDate = DateTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = DateTime.Now;
                    v.OtreservationDiagnosisDetId = 0;
                }



                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _OTService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "Createdby", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }




        //[HttpPost("Insert")]
        ////[Permission(PageCode = "OTReservation", Permission = PagePermission.Add)]
        //public async Task<ApiResponse> InsertEDMX(OTReservationModel obj)
        //{
        //    TOtReservation model = obj.MapTo<TOtReservation>();
        //    if (obj.OtreservationId == 0)
        //    {
        //        model.ReservationDate = Convert.ToDateTime(obj.ReservationDate);
        //        model.ReservationTime = Convert.ToDateTime(obj.ReservationTime);

        //        model.CreatedBy = CurrentUserId;
        //        model.CreatedDate = DateTime.Now;
        //        model.ModifiedBy = CurrentUserId;
        //        model.ModifiedDate = DateTime.Now;

        //        await _OTService.InsertAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    else
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        //}

        //[HttpPut("Edit/{id:int}")]
        ////[Permission(PageCode = "OTReservation", Permission = PagePermission.Edit)]
        //public async Task<ApiResponse> Edit(OTReservationModel obj)
        //{
        //    TOtReservation model = obj.MapTo<TOtReservation>();
        //    if (obj.OtreservationId == 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    else
        //    {
        //        model.ReservationDate = Convert.ToDateTime(obj.ReservationDate);
        //        model.ReservationTime = Convert.ToDateTime(obj.ReservationTime);

        //        model.ModifiedBy = CurrentUserId;
        //        model.ModifiedDate = DateTime.Now;


        //        await _OTService.UpdateAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        //}




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
