using Microsoft.AspNetCore.Mvc;
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
using HIMS.Data.DTO.Pathology;
using static HIMS.API.Models.OutPatient.TPrescriptionModel;

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
            return Ok(IPDiscList.ToGridResponse(objGrid, "IP Dischareg Summary Data "));
        }

        [HttpPost("IPPrescriptionDischargeData")]
        ////[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> IPPrescriptionDisc(GridRequestModel objGrid)
        {
            IPagedList<IPPrescriptiononDischargeListDto> IPPRDiscList = await _IDischargeSummaryService.IPPrescriptionDischargesummaryList(objGrid);
            return Ok(IPPRDiscList.ToGridResponse(objGrid, "IP Prescription On Dischareg  Data  "));
        }
        [HttpPost("PatientClearanceAprovViewList")]
        ////[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> GetListAsync(GridRequestModel objGrid)
        {
            IPagedList<PatientClearanceAprovViewListDto> IPDiscList = await _IDischargeSummaryService.GetListAsync(objGrid);
            return Ok(IPDiscList.ToGridResponse(objGrid, "PatientClearanceAprovViewList "));
        }
        [HttpPost("PatientClearanceApprovalList")]
        ////[Permission(PageCode = "Bill", Permission = PagePermission.View)]
        public async Task<IActionResult> GetListAsyncP(GridRequestModel objGrid)
        {
            IPagedList<PatientClearanceApprovalListDto> IPDiscList = await _IDischargeSummaryService.GetListAsyncP(objGrid);
            return Ok(IPDiscList.ToGridResponse(objGrid, "PatientClearanceApprovalList"));
        }

        [HttpGet("{id?}")]
        // [Permission(PageCode = "Appointment", Permission = PagePermission.View)]
        public async Task<ApiResponse> Get(int id)
        {

            var data1 = await _repository.GetById(x => x.AdmissionId == id);
            return data1.ToSingleResponse<Discharge, DischargeModel>("Discharge");
        } 

        [HttpPost("DischargeSummaryInsert")]
        [Permission(PageCode = "DischargeSummary", Permission = PagePermission.Add)]
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DischargeSummary added successfully.",model);
        }
        [HttpPut("DischargeSummaryUpdate")]
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DischargeSummary Update successfully.", model);
        }

       
        [HttpPost("DischargeTemplateInsert")]
        [Permission(PageCode = "DischargeSummary", Permission = PagePermission.Add)]
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
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DischargeTemplate added successfully.", model);
        }

        [HttpPut("DischargeTemplateUpdate")]
        [Permission(PageCode = "DischargeSummary", Permission = PagePermission.Add)]
        public async Task<ApiResponse> DischargeTemplateUpdate(DischargeTemUpdate obj)
        {
            DischargeSummary model = obj.Discharge.MapTo<DischargeSummary>();
            List<TIpPrescriptionDischarge> Prescription = obj.PrescriptionTemplate.MapTo<List<TIpPrescriptionDischarge>>();
            if (obj.Discharge.DischargeSummaryId != 0)
            {
                model.Followupdate = Convert.ToDateTime(obj.Discharge.Followupdate);
                model.AddedBy = CurrentUserId;

                Prescription.ForEach(x => { x.OpdIpdId = model.AdmissionId; x.CreatedBy = CurrentUserId; x.ModifiedBy = CurrentUserId; });

                await _IDischargeSummaryService.UpdateAsyncTemplate(model, Prescription, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DischargeTemplate Update successfully.", model);
        }


        [HttpPost("DischargeInsert")]
        [Permission(PageCode = "Discharge", Permission = PagePermission.Add)]
        public async Task<ApiResponse> IPDischargeInsert(DischargeModels obj)
        {
            Discharge model = obj.Discharge.MapTo<Discharge>();
            Admission ObjAdmission = obj.Admission.MapTo<Admission>();
            Bedmaster ObjBed = obj.Bed.MapTo<Bedmaster>();

            if (obj.Discharge.DischargeId == 0)
            {
                model.DischargeDate = Convert.ToDateTime(obj.Discharge.DischargeDate);
                model.DischargeTime = Convert.ToDateTime(obj.Discharge.DischargeTime);
                model.AddedBy = CurrentUserId;

                if (obj.Discharge.DischargeId == 0)
                {
                    ObjAdmission.DischargeTime = Convert.ToDateTime(obj.Admission.DischargeTime);
                    ObjAdmission.AddedBy = CurrentUserId;
                    ObjBed.CreatedBy = CurrentUserId;
                }
                await _IDischargeSummaryService.InsertDischargeSP(model, ObjAdmission, ObjBed, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Discharge added successfully."/*+ObjAdmission.AdmissionId*/);
        }

        [HttpPut("DischargeUpdate")]
        [Permission(PageCode = "Discharge", Permission = PagePermission.Add)]
        public async Task<ApiResponse> DischargeUpdate(DischargUpdate obj)
        {
            Discharge model = obj.Discharge.MapTo<Discharge>();
            Admission ObjAdmission = obj.Admission.MapTo<Admission>();

            if (obj.Discharge.DischargeId != 0)
            {
                model.DischargeDate = Convert.ToDateTime(obj.Discharge.DischargeDate);
                model.DischargeTime = Convert.ToDateTime(obj.Discharge.DischargeTime);
                model.AddedBy = CurrentUserId;

                if (obj.Discharge.DischargeId != 0)
                {
                    ObjAdmission.DischargeTime = Convert.ToDateTime(obj.Admission.DischargeTime);
                    ObjAdmission.AddedBy = CurrentUserId;
                }
                await _IDischargeSummaryService.UpdateDischargeSP(model, ObjAdmission ,CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Discharge Updated successfully."+ObjAdmission.AdmissionId);
        }

       
        [HttpPost("InitiateDischargeInsertsync")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(InitiateDModel obj)
        {
            InitiateDischarge model = obj.InitiateDischarge.MapTo<InitiateDischarge>();
            if (obj.InitiateDischarge.InitateDiscId == 0)
            {
                model.ApprovedDatetime = Convert.ToDateTime(obj.InitiateDischarge.ApprovedDatetime);
                model.ApprovedBy = CurrentUserId;
                await _IDischargeSummaryService.DischargeInsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Discharge added successfully.");
        }
        [HttpPut("DischargeInitiateApprovalUpdate")]
        //[Permission(PageCode = "DischargeSummary", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(InitiateDischargeModel obj)
        {
            InitiateDischarge model = obj.MapTo<InitiateDischarge>();
            if (obj.InitateDiscId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _IDischargeSummaryService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "InitiateDischarge  updated successfully.");
        }
    }
}

