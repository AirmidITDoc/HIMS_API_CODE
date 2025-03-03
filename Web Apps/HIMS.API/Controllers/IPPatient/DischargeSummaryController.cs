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

        [HttpPost("DischargeInsert")]
        //[Permission(PageCode = "DischargeSummay", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(DischargeSumModel obj)
        {
            DischargeSummary model = obj.DischargModel.MapTo <DischargeSummary>();
            List<TIpPrescriptionDischarge> Prescription = obj.PrescriptionDischarge.MapTo <List<TIpPrescriptionDischarge>>();


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

        [HttpPost("DischargeUpdate")]
        //[Permission(PageCode = "DischargeSummay", Permission = PagePermission.Add)]
        public async Task<ApiResponse> UPDATESP(DischargeUpdate obj)
        {
            DischargeSummary model = obj.DischargModel.MapTo<DischargeSummary>();
            List<TIpPrescriptionDischarge> Prescription = obj.PrescriptionDischarge.MapTo <List<TIpPrescriptionDischarge>>();


            if (obj.DischargModel.DischargeSummaryId != 0)
            {
                model.OpDate = Convert.ToDateTime(obj.DischargModel.OpDate);
                model.Optime = Convert.ToDateTime(obj.DischargModel.Optime);
                model.AddedBy = CurrentUserId;
                //Prescription.Date = Convert.ToDateTime(obj.PrescriptionDischarge.Date);
                //Prescription.Ptime = Convert.ToDateTime(obj.PrescriptionDischarge.Ptime);

                //Prescription.CreatedBy = CurrentUserId;

                //Prescription.ForEach(x => { x.OpdIpdId = obj.DischargModel.AdmissionId; x.CreatedBy = CurrentUserId;});


                await _IDischargeSummaryService.UpdateAsyncSP(model, Prescription, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DischargeSummary Update successfully.");
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

                await _IDischargeSummaryService.UpdateAsyncTemplate(model, Prescription ,CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DischargeTemplate Update successfully.");
        }
        [HttpPost("DischargInsert")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> DischargInsert(DischargeModels obj)
        {
            Discharge model = obj.Discharge.MapTo<Discharge>();
            Admission objAdmission = obj.Admission.MapTo<Admission>();
            if (obj.Discharge.DischargeId == 0)
            {
                model.DischargeDate = Convert.ToDateTime(obj.Discharge.DischargeDate);
                model.DischargeTime = Convert.ToDateTime(obj.Discharge.DischargeTime);
                model.AddedBy = CurrentUserId;

                if (obj.Discharge.DischargeId == 0)
                {
                    objAdmission.DischargeTime = Convert.ToDateTime(obj.Admission.DischargeTime);
                    objAdmission.AddedBy = CurrentUserId;
                    // objVisitDetail.UpdatedBy = CurrentUserId;
                }
                await _IDischargeSummaryService.InsertAsyncDischarge(model, objAdmission, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Discharge added successfully.");
        }
    }
}
