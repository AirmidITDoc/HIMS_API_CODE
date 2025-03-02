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

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DischargeSummaryController : BaseController
    {
        private readonly IDischargeSummaryService _IDischargeSummaryService;
        public DischargeSummaryController(IDischargeSummaryService repository)
        {
            _IDischargeSummaryService = repository;
        }

        [HttpPost("DischargeInsert")]
        [Permission(PageCode = "DischargeSummay", Permission = PagePermission.Add)]
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
        [Permission(PageCode = "DischargeSummay", Permission = PagePermission.Add)]
        public async Task<ApiResponse> UPDATESP(DischargeUpdate obj)
        {
            DischargeSummary model = obj.DischargModel.MapTo<DischargeSummary>();
            TIpPrescriptionDischarge Prescription = obj.PrescriptionDischarge.MapTo<TIpPrescriptionDischarge>();


            if (obj.DischargModel.DischargeSummaryId != 0)
            {
                model.OpDate = Convert.ToDateTime(obj.DischargModel.OpDate);
                model.Optime = Convert.ToDateTime(obj.DischargModel.Optime);
                model.AddedBy = CurrentUserId;
                Prescription.Date = Convert.ToDateTime(obj.PrescriptionDischarge.Date);
                Prescription.Ptime = Convert.ToDateTime(obj.PrescriptionDischarge.Ptime);

                Prescription.CreatedBy = CurrentUserId;

                await _IDischargeSummaryService.UpdateAsyncSP(model, Prescription, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DischargeSummary Update successfully.");
        }

    }

}
