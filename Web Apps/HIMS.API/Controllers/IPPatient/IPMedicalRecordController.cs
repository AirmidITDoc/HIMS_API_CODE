using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Inventory;
using HIMS.Core;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;
using HIMS.Services.IPPatient;
using HIMS.API.Models.IPPatient;
using HIMS.Data;

namespace HIMS.API.Controllers.IPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class IPMedicalRecordController : BaseController
    {
        private readonly IMedicalRecordService _MedicalRecordService;
       
        
        public IPMedicalRecordController(IMedicalRecordService repository)
        {
            _MedicalRecordService = repository;
            
        }
        [HttpPost("InsertEDMX")]
        [Permission(PageCode = "MedicalRecords", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(TIPmedicalRecordModel obj)
        {
            TIpmedicalRecord model = obj.MapTo<TIpmedicalRecord>();
            if (obj.MedicalRecoredId == 0)
            {
                model.RoundVisitTime = Convert.ToDateTime(obj.RoundVisitTime);
                //model.IsAddBy = CurrentUserId;
                //model.IsActive = true;
                await _MedicalRecordService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record   added successfully.");
        }
        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "MedicalRecords", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(TIPmedicalRecordModel obj)
        {
            TIpmedicalRecord model = obj.MapTo<TIpmedicalRecord>();
            if (obj.MedicalRecoredId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.RoundVisitTime = Convert.ToDateTime(obj.RoundVisitTime);
                await _MedicalRecordService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }

       
    }
}


