using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Nursing.IPEMR;
using HIMS.Core.Infrastructure;
using HIMS.Data.Models;
using HIMS.Services.Nursing.IPEMR;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.NursingStation.IPEMR
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class EMRController : BaseController
    {
        private readonly IEMRService _EmrService;

        public EMRController(IEMRService emrService)
        {
            _EmrService = emrService;
        }

        [HttpGet("{id?}")]
        //[Permission]
        public async Task<ApiResponse> Get(long id)
        {
            if (id == 0) return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            var data = await _EmrService.GetByIdAsync(id);
            return data.ToSingleResponse<TIpEmrhistory, EMRModel>("TIpEmrhistory");
        }

       

        [HttpPost("Insert")]
        [Permission]
        public async Task<ApiResponse> Insert(EMRModel obj)
        {
            TIpEmrhistory model = obj.MapTo<TIpEmrhistory>();
            if (obj.IpdEmrId == 0)
            {
                foreach (var q in model.TIpEmrdiagnosisInfos)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }
                foreach (var q in model.TIpEmrdignosisHistories)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }
                foreach (var q in model.TIpEmrfamilyMedicalHistories)
                {
                    q.CreatedBy = CurrentUserId;
                    q.CreatedDate = AppTime.Now;

                }

                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _EmrService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.IpdEmrId);
        }

        [HttpPut("Edit/{id:int}")]
        [Permission]
        public async Task<ApiResponse> Edit(EMRModel obj)
        {
            TIpEmrhistory model = obj.MapTo<TIpEmrhistory>();
            if (obj.IpdEmrId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                foreach (var q in model.TIpEmrdiagnosisInfos)
                {
                    if (q.IpemrdiagnId == 0)
                    {
                        q.CreatedBy = CurrentUserId;
                        q.CreatedDate = AppTime.Now;
                    }
                    q.ModifiedBy = CurrentUserId;
                    q.ModifiedDate = AppTime.Now;
                    q.IpemrdiagnId = 0;
                }

                foreach (var v in model.TIpEmrdignosisHistories)
                {
                    if (v.EmrdignId == 0)
                    {
                        v.CreatedBy = CurrentUserId;
                        v.CreatedDate = AppTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = AppTime.Now;
                    v.EmrdignId = 0;
                }
                foreach (var v in model.TIpEmrfamilyMedicalHistories)
                {
                    if (v.FhistId == 0)
                    {
                        v.CreatedBy = CurrentUserId;
                        v.CreatedDate = AppTime.Now;
                    }
                    v.ModifiedBy = CurrentUserId;
                    v.ModifiedDate = AppTime.Now;
                    v.FhistId = 0;
                }
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _EmrService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.IpdEmrId);
        }
    }
}