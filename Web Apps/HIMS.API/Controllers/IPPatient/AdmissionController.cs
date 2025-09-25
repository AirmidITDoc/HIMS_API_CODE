using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Administration;
using HIMS.API.Models.IPPatient;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class AdmissionController : BaseController
    {

        private readonly IAdmissionService _IAdmissionService;
        private readonly IGenericService<Admission> _repository1;
        public AdmissionController(IAdmissionService repository, IGenericService<Admission> repository1)
        {
            _IAdmissionService = repository;
            _repository1 = repository1;
        }


        [HttpPost("AdmissionList")]
        [Permission(PageCode = "Admission", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<AdmissionListDto> AdmissionListList = await _IAdmissionService.GetAdmissionListAsync(objGrid);
            return Ok(AdmissionListList.ToGridResponse(objGrid, "Admission List"));
        }

        [HttpPost("OPRequestListForIPAdmission")]
        [Permission(PageCode = "Admission", Permission = PagePermission.View)]
        public async Task<IActionResult> RList(GridRequestModel objGrid)
        {
            IPagedList<RequestForIPListDto> OPRequestListForIPAdmission = await _IAdmissionService.GetAsync(objGrid);
            return Ok(OPRequestListForIPAdmission.ToGridResponse(objGrid, "RequestForIP List"));
        }

        [HttpPost("AdmissionDischargeList")]
        [Permission(PageCode = "Admission", Permission = PagePermission.View)]
        public async Task<IActionResult> AdmDiscList(GridRequestModel objGrid)
        {
            IPagedList<AdmissionListDto> AdmissionDischargeList = await _IAdmissionService.GetAdmissionDischargeListAsync(objGrid);
            return Ok(AdmissionDischargeList.ToGridResponse(objGrid, "Admission Discharge List"));
        }

        [HttpGet("{id?}")]
        [Permission(PageCode = "Admission", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {

            var data = await _repository1.GetById(x => x.AdmissionId == id);
            return data.ToSingleResponse<Admission, ADMISSIONModel>("Admission");
        }
        [HttpGet("BedList")]
        public async Task<ApiResponse> GetBedmaster(int RoomId)
        {
            var resultList = await _IAdmissionService.GetBedmaster(RoomId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "BedList.", resultList.Select(x => new { x.BedId, x.BedName, }));
        }

        [HttpPost("AdmissionInsertSP")]
        [Permission(PageCode = "Admission", Permission = PagePermission.Add)]
        public ApiResponse InsertSP(AdmissionRegistered obj)
        {
            //Registration model = obj.AdmissionReg.MapTo<Registration>();
            Admission model = obj.Admission.MapTo<Admission>();
            if (model.RegId != 0)
            {
                model.AdmissionTime = Convert.ToDateTime(model.AdmissionTime);
                model.AddedBy = CurrentUserId;
                _IAdmissionService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.AdmissionId);
        }

        //UPDATE SHILPA 09-08-2025//
         [HttpPost("AdmissionRegInsertSP")]
        [Permission(PageCode = "Admission", Permission = PagePermission.Add)]
        public ApiResponse Insert(NewAdmission obj)
        {
            Registration model = obj.AdmissionReg.MapTo<Registration>();
            Admission objAdmission = obj.Admission.MapTo<Admission>();
            //Bedmaster Bmodel = obj.BedMaster.MapTo<Bedmaster>();

            if (objAdmission.AdmissionId == 0)
            {
                objAdmission.AdmissionTime = Convert.ToDateTime(objAdmission.AdmissionTime);
                objAdmission.AddedBy = CurrentUserId;
                _IAdmissionService.InsertRegAsyncSP(model ,objAdmission, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", objAdmission.AdmissionId);
        }


        

        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "Admission", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> UpdateSP(NewAdmission obj)
        {

            Admission objAdmission = obj.Admission.MapTo<Admission>();
            if (obj.Admission.AdmissionId != 0)
            {

                objAdmission.IsUpdatedBy = CurrentUserId;

                await _IAdmissionService.UpdateAdmissionAsyncSP(objAdmission, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  Updated successfully.", objAdmission.AdmissionId);
        }

        [HttpGet("search-patient")]
        [Permission(PageCode = "Admission", Permission = PagePermission.View)]
        public async Task<ApiResponse> SearchPatient(string Keyword)
        {
            var data = await _IAdmissionService.PatientAdmittedListSearch(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Patient Admission data", data);
        }


        [HttpGet("Dischargesearch-patient")]
        [Permission(PageCode = "Admission", Permission = PagePermission.View)]
        public async Task<ApiResponse> DischargeSearchPatient(string Keyword)
        {
            var data = await _IAdmissionService.PatientDischargeListSearch(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Patient Admission data", data);
        }

        [HttpPut("Companyinformation/{id:int}")]
        [Permission(PageCode = "Administration", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Update(CompanyinformationModel obj)
        {
            Admission model = obj.MapTo<Admission>();
            if (obj.AdmissionId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {

                await _IAdmissionService.UpdateAsyncInfo(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
    }
}
