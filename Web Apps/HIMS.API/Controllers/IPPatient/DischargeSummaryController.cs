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


        [HttpPost("Insert")]
        //[Permission(PageCode = "DischargeSummay", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(DischargeSummaryModel obj)
        {
            DischargeSummary model = obj.MapTo<DischargeSummary>();
            if (obj.DischargeSummaryId == 0)
            {
                model.DischargeSummaryDate = Convert.ToDateTime(obj.DischargeSummaryDate);
                model.DischargeSummaryTime = Convert.ToDateTime(obj.DischargeSummaryTime);
                model.AddedBy = CurrentUserId;
                await _IDischargeSummaryService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DischargeSummary added successfully.");
        }

        [HttpPost("InsertSP")]
        //[Permission(PageCode = "Sales", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertSP(DischargeSumModel obj)
        {
            DischargeSummary model = obj.DischargModel.MapTo <DischargeSummary>();
            TIpPrescriptionDischarge Prescription = obj.PrescriptionDischarge.MapTo<TIpPrescriptionDischarge>();


            if (obj.DischargModel.DischargeSummaryId == 0)
            {
                model.DischargeSummaryTime = Convert.ToDateTime(obj.DischargModel.DischargeSummaryTime);
                model.DischargeSummaryDate = Convert.ToDateTime(obj.DischargModel.DischargeSummaryDate);
                model.AddedBy = CurrentUserId;
                Prescription.Date = Convert.ToDateTime(obj.PrescriptionDischarge.Date);
                Prescription.Ptime = Convert.ToDateTime(obj.PrescriptionDischarge.Ptime);

                Prescription.CreatedBy = CurrentUserId;

                await _IDischargeSummaryService.InsertAsyncSP(model, Prescription, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DischargeSummary added successfully.");
        }


        //[HttpPut("Update")]
        ////[Permission(PageCode = "DischargeSummay", Permission = PagePermission.Edit)]
        //public async Task<ApiResponse> Edit(DischargeSumModel obj)
        //{
        //    DischargeSummary model = obj.DischargModel.MapTo<DischargeSummary>();

        //    if (obj.DischargeSummaryId == 0)
        //        return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
        //    else
        //    {
        //        model.DischargeSummaryDate = Convert.ToDateTime(obj.DischargeSummaryDate);
        //        model.DischargeSummaryTime = Convert.ToDateTime(obj.DischargeSummaryTime);
        //        await _IDischargeSummaryService.UpdateAsync(model, CurrentUserId, CurrentUserName);
        //    }
        //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DischargeSummary updated successfully.");
        //}

    }

}
