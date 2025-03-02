﻿using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using HIMS.API.Models.IPPatient;
using HIMS.Services.IPPatient;
using HIMS.API.Models.Nursing;
using HIMS.Core;
using HIMS.API.Models.OPPatient;
using HIMS.Services.OPPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data;
using HIMS.Data.DataProviders;
using System.Data;

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DischargeSummaryController : BaseController
    {
        private readonly IDischargeSummaryService _IDischargeSummaryService;
        private readonly IGenericService<Discharge> _repository;
        public DischargeSummaryController(IDischargeSummaryService repository, IGenericService<Discharge> repository1)
        {
            _IDischargeSummaryService = repository;
            _repository = repository1;
        }
        [HttpPost("IPDischargeSummaryData")]
        ////[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> List1(GridRequestModel objGrid)
        {
            IPagedList<DischrageSummaryListDTo> IPDiscList = await _IDischargeSummaryService.IPDischargesummaryList(objGrid);
            return Ok(IPDiscList.ToGridResponse(objGrid, "IP Dischareg Summary Data  "));
        }

        [HttpPost("IPPrescriptionDischargeData")]
        ////[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> IPPrescriptionDisc(GridRequestModel objGrid)
        {
            IPagedList<IPPrescriptiononDischargeListDto> IPPRDiscList = await _IDischargeSummaryService.IPPrescriptionDischargesummaryList(objGrid);
            return Ok(IPPRDiscList.ToGridResponse(objGrid, "IP Prescription On Dischareg  Data  "));
        }

        [HttpGet("{id?}")]
        // [Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {

            var data1 = await _repository.GetById(x => x.AdmissionId == id);
            return data1.ToSingleResponse<Discharge, DischargeModel>("Discharge");
        }

        [HttpPost("DischargeSummaryInsert")]
        //[Permission(PageCode = "DischargeSummary", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(DischargeSumModel obj)
        {
            DischargeSummary model = obj.DischargModel.MapTo<DischargeSummary>();
            List<TIpPrescriptionDischarge> Prescription = obj.PrescriptionDischarge.MapTo<List<TIpPrescriptionDischarge>>();
            if (obj.DischargModel.DischargeSummaryId == 0)
            {
                model.DischargeSummaryTime = Convert.ToDateTime(obj.DischargModel.DischargeSummaryTime);
                model.DischargeSummaryDate = Convert.ToDateTime(obj.DischargModel.DischargeSummaryDate);
                model.AddedBy = CurrentUserId;
                Prescription.ForEach(x => { x.OpdIpdId = obj.DischargModel.AdmissionId; x.CreatedBy = CurrentUserId; x.ModifiedBy = CurrentUserId; });

                await _IDischargeSummaryService.InsertAsyncSP(model, Prescription, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DischargeSummary added successfully.");
        }
        [HttpPost("DischargeSummaryUpdate")]
        //[Permission(PageCode = "DischargeSummay", Permission = PagePermission.Add)]
        public async Task<ApiResponse> UPDATESP(DischargeUpdate obj)
        {
            DischargeSummary model = obj.DischargModel.MapTo<DischargeSummary>();
            List<TIpPrescriptionDischarge> Prescription = obj.PrescriptionDischarge.MapTo<List<TIpPrescriptionDischarge>>();


            if (obj.DischargModel.DischargeSummaryId != 0)
            {
                model.OpDate = Convert.ToDateTime(obj.DischargModel.OpDate);
                model.Optime = Convert.ToDateTime(obj.DischargModel.Optime);
                model.AddedBy = CurrentUserId;

                await _IDischargeSummaryService.UpdateAsyncSP(model, Prescription, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DischargeSummary Update successfully.");
        }

       
        [HttpPost("DischargeTemplateInsert")]
        //[Permission(PageCode = "DischargeSummary", Permission = PagePermission.Add)]
        public async Task<ApiResponse> DischargeTemplateInsert(DischargeTemplate obj)
        {
            DischargeSummary model = obj.Discharge.MapTo<DischargeSummary>();
            List<TIpPrescriptionDischarge> Prescription = obj.PrescriptionTemplate.MapTo<List<TIpPrescriptionDischarge>>();
            if (obj.Discharge.DischargeSummaryId == 0)
            {
                model.Followupdate = Convert.ToDateTime(obj.Discharge.Followupdate);
                model.AddedBy = CurrentUserId;
                Prescription.ForEach(x => { x.OpdIpdId = obj.Discharge.AdmissionId; x.CreatedBy = CurrentUserId; x.ModifiedBy = CurrentUserId; });

                await _IDischargeSummaryService.InsertAsyncTemplate(model, Prescription, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DischargeTemplate added successfully.");
        }

        [HttpPost("DischargeTemplateUpdate")]
        //[Permission(PageCode = "DischargeSummary", Permission = PagePermission.Add)]
        public async Task<ApiResponse> DischargeTemplateUpdate(DischargeTemUpdate obj)
        {
            DischargeSummary model = obj.Discharge.MapTo<DischargeSummary>();
            TIpPrescriptionDischarge Prescription = obj.PrescriptionTemplate.MapTo<TIpPrescriptionDischarge>();
            if (obj.Discharge.DischargeSummaryId != 0)
            {
                model.Followupdate = Convert.ToDateTime(obj.Discharge.Followupdate);
                model.AddedBy = CurrentUserId;
                
                Prescription.CreatedBy = CurrentUserId;

                await _IDischargeSummaryService.UpdateAsyncTemplate(model, Prescription, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DischargeTemplate Update successfully.");
        }

    }
}

