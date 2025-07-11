﻿using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Services.OutPatient;
using Microsoft.AspNetCore.Mvc;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data;
using HIMS.Services.Common;
using HIMS.Services.Masters;
using HIMS.Services.OPPatient;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class VisitDetailController : BaseController
    {
        private readonly IVisitDetailsService _visitDetailsService;
        private readonly IGenericService<VisitDetail> _repository;
        private readonly IDoctorMasterService _IDoctorMasterService;
        private readonly IConsRefDoctorService _IConsRefDoctorService;

        public VisitDetailController(IVisitDetailsService repository, IGenericService<VisitDetail> repository1, IDoctorMasterService doctorMasterService, IConsRefDoctorService repository2)
        {
            _visitDetailsService = repository;
            _repository = repository1;
            _IDoctorMasterService = doctorMasterService;
            _IConsRefDoctorService = repository2;
        }
        [HttpPost("AppVisitList")]
        [Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<VisitDetailListDto> AppVisitList = await _visitDetailsService.GetListAsync(objGrid);
            return Ok(AppVisitList.ToGridResponse(objGrid, "App Visit List"));
        }

        [HttpGet("{id?}")]
        [Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {

            var data1 = await _repository.GetById(x => x.VisitId == id);
            return data1.ToSingleResponse<VisitDetail, VisitDetailModel>("VisitDetails");
        }
        [HttpGet("search-patient")]
        //[Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> SearchPatient(string Keyword)
        {
            var data = await _visitDetailsService.VisitDetailsListSearchDto(Keyword);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Patient Visit data", data);
        }

        [HttpPost("OPRegistrationList")]
        public async Task<IActionResult> OPRegistrationList(GridRequestModel objGrid)
        {
            IPagedList<OPRegistrationList> OpReglist = await _visitDetailsService.GeOPRgistrationListAsync(objGrid);
            return Ok(OpReglist.ToGridResponse(objGrid, "OP Registration List"));
        }

        [HttpPost("OPBillList")]
        public async Task<IActionResult> OPBillList(GridRequestModel objGrid)
        {
            IPagedList<OPBillListDto> OpBilllist = await _visitDetailsService.GetBillListAsync(objGrid);
            return Ok(OpBilllist.ToGridResponse(objGrid, "OP BILL List"));
        }


        [HttpPost("OPprevDoctorVisitList")]
        public async Task<IActionResult> OPPrevDrVisistList(GridRequestModel objGrid)
        {
            IPagedList<PrevDrVisistListDto> Oplist = await _visitDetailsService.GeOPPreviousDrVisitListAsync(objGrid);
            return Ok(Oplist.ToGridResponse(objGrid, "OP Previoud Dr Visit List"));
        }

        [HttpGet("DeptDoctorList")]
        public async Task<ApiResponse> DeptDoctorList(int DeptId)
        {
            var resultList = await _IDoctorMasterService.GetDoctorsByDepartment(DeptId);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Doctor List.", resultList.Select(x => new { value = x.DoctorId, text = x.FirstName + " " + x.LastName }));
        }

        [HttpPost("AppVisitInsert")]
        [Permission(PageCode = "Appointment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> AppVisitInsert(AppointmentReqDtovisit obj)
        {
            Registration model = obj.Registration.MapTo<Registration>();
            VisitDetail objVisitDetail = obj.Visit.MapTo<VisitDetail>();
            if (obj.Registration.RegId == 0)
            {
                model.RegTime = Convert.ToDateTime(obj.Registration.RegTime);
                model.AddedBy = CurrentUserId;

                if (obj.Visit.VisitId == 0)
                {
                    objVisitDetail.VisitTime = Convert.ToDateTime(obj.Visit.VisitTime);
                    objVisitDetail.AddedBy = CurrentUserId;
                    objVisitDetail.UpdatedBy = CurrentUserId;
                }
                await _visitDetailsService.InsertAsync(model, objVisitDetail, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.", objVisitDetail.VisitId);
        }



        [HttpPost("Insert")]
        [Permission(PageCode = "Appointment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(AppointmentReqDtovisit obj)
        {
            Registration model = obj.Registration.MapTo<Registration>();
            VisitDetail objVisitDetail = obj.Visit.MapTo<VisitDetail>();
            if (obj.Registration.RegId == 0)
            {
                model.RegTime = Convert.ToDateTime(obj.Registration.RegTime);
                model.AddedBy = CurrentUserId;

                if (obj.Visit.VisitId == 0)
                {
                    objVisitDetail.VisitTime = Convert.ToDateTime(obj.Visit.VisitTime);
                    objVisitDetail.AddedBy = CurrentUserId;
                    objVisitDetail.UpdatedBy = CurrentUserId;
                }
                await _visitDetailsService.InsertAsyncSP(model, objVisitDetail, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", objVisitDetail.VisitId);
        }


        [HttpPost("Update")]
        [Permission(PageCode = "Appointment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(AppointmentUpdate obj)
        {
            Registration model = obj.AppReistrationUpdate.MapTo<Registration>();
            VisitDetail objVisitDetail = obj.Visit.MapTo<VisitDetail>();
            if (obj.AppReistrationUpdate.RegId != 0)
            {
                model.AddedBy = CurrentUserId;

                if (obj.Visit.VisitId == 0)
                {
                    objVisitDetail.VisitTime = Convert.ToDateTime(obj.Visit.VisitTime);
                    objVisitDetail.AddedBy = CurrentUserId;
                    objVisitDetail.UpdatedBy = CurrentUserId;
                }
                await _visitDetailsService.UpdateAsyncSP(model, objVisitDetail, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Updated successfully.", objVisitDetail.VisitId);
        }

        [HttpPost("Cancel")]
        [Permission(PageCode = "Appointment", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(CancelAppointment obj)
        {
            VisitDetail model = new();
            if (obj.VisitId != 0)
            {
                model.VisitId = obj.VisitId;
                model.IsCancelled = true;
                model.IsCancelledBy = CurrentUserId;
                model.IsCancelledDate = DateTime.Now;
                await _visitDetailsService.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.", model);
        }
        
        [HttpGet("GetServiceListwithTraiff")]
        public async Task<ApiResponse> GetServiceListwithTraiff(int TariffId, int ClassId, string ServiceName)
        {
            var resultList = await _visitDetailsService.GetServiceListwithTraiff(TariffId, ClassId, ServiceName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service List.", resultList.Select(x => new
            {
                x.FormattedText,
                x.ServiceId,
                x.GroupId,
                x.ServiceShortDesc,
                x.ServiceName,
                x.ClassRate,
                x.TariffId,
                x.ClassId,
                x.IsEditable,
                x.CreditedtoDoctor,
                x.IsPathology,
                x.IsRadiology,
                x.IsActive,
                x.PrintOrder,
                x.IsPackage,
                x.DoctorId,
                x.IsDocEditable
            }));
        }


        //Edit EditVital
        [HttpPut("EditVital/{id:int}")]
        [Permission(PageCode = "Appointment", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(UpdateVitalInfModel obj)
        {
            VisitDetail model = obj.MapTo<VisitDetail>();
            if (obj.VisitId != 0)
            {
                model.VisitId = obj.VisitId;
                await _visitDetailsService.UpdateVitalAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record update successfully.");
        }
        [HttpPost("CrossConsultationInsert")]
        [Permission(PageCode = "Appointment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(CrossConsultationModel obj)
        {
            VisitDetail model = obj.MapTo<VisitDetail>();
            if (obj.VisitId == 0)
            {
                model.VisitDate = Convert.ToDateTime(obj.VisitDate);
                model.VisitTime = Convert.ToDateTime(obj.VisitTime);

                model.UpdatedBy = CurrentUserId;
                model = await _visitDetailsService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model);
        }
        [HttpPut("ConsultantDoctorUpdate/{id:int}")]
        [Permission(PageCode = "Appointment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> UpdateAsync(ConsRefDoctorModel obj)
        {
            VisitDetail model = obj.MapTo<VisitDetail>();
            if (obj.VisitId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                await _IConsRefDoctorService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }


        [HttpPut("RefDoctorUpdate")]
        [Permission(PageCode = "Appointment", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(RefDoctorModel obj)
        {
            VisitDetail model = obj.MapTo<VisitDetail>();
            if (obj.VisitId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                await _IConsRefDoctorService.Update(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        [HttpPut("ConsulationStartEndProcess")]
        [Permission(PageCode = "Appointment", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(ConsulationStartEndProcess obj)
        {
            VisitDetail model = obj.MapTo<VisitDetail>();
            if (obj.VisitId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ConStartTime = DateTime.Now;
                await _visitDetailsService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        [HttpPut("CheckOutProcess")]
        [Permission(PageCode = "Appointment", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Update(CheckOutProcessUpdate obj)
        {
            VisitDetail model = obj.MapTo<VisitDetail>();
            if (obj.VisitId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ConEndTime = DateTime.Now;
                model.CheckOutTime = DateTime.Now;
                await _visitDetailsService.UpdateAsyncv(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

    }
}
