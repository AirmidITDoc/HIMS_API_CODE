using System.Text.Json;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Hubs;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Nursing;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.DTO.Nursing;
using HIMS.Data.Models;
using HIMS.Services.Notification;
using HIMS.Services.Nursing;
//using HIMS.Services.NursingStation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace HIMS.API.Controllers.NursingStation
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class PrescriptionController : BaseController
    {
        private readonly IPrescriptionService _IPrescriptionService;
        public PrescriptionController(IPrescriptionService IPrescriptionService)
        {
            _IPrescriptionService = IPrescriptionService;

        }


        [HttpPost("InsertPrescription")]
        [Permission(PageCode = "MedicalRecord", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(MedicalPrescriptionModel obj)
        {
            TIpmedicalRecord model = obj.MapTo<TIpmedicalRecord>();
            if (obj.MedicalRecoredId == 0)
            {
                model.RoundVisitDate = Convert.ToDateTime(obj.RoundVisitDate);
                model.RoundVisitTime = Convert.ToDateTime(obj.RoundVisitTime);

                await _IPrescriptionService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", new { model.AdmissionId, model.MedicalRecoredId });
        }


        [HttpPost("PrescriptionReturnInsert")]
        [Permission(PageCode = "PrescriptionReturn", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(PriscriptionReturnModel obj)
        {
            TIpprescriptionReturnH model = obj.MapTo<TIpprescriptionReturnH>();
            if (obj.PresReId == 0)
            {
                model.PresTime = Convert.ToDateTime(obj.PresTime);
                model.Addedby = CurrentUserId;
                await _IPrescriptionService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", new { model.PresReId,model.PresNo});
        }

    }
}
