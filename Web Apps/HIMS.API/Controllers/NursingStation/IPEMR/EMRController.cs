using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
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
        private readonly IEMRService _emrService;

        public EMRController(IEMRService emrService)
        {
            _emrService = emrService;
        }

        [HttpGet("{id?}")]
        //[Permission]
        public async Task<ApiResponse> Get(long id)
        {
            if (id == 0) return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            var data = await _emrService.GetByIdAsync(id);
            return data.ToSingleResponse<TIpEmrhistory, EMRModel>("TIpEmrhistory");
        }

        [HttpPost("Insert")]
        //[Permission]
        public async Task<ApiResponse> Insert(EMRModel obj)
        {
            TIpEmrhistory model = obj.MapTo<TIpEmrhistory>();
            if (obj.IpdEmrId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;

                await _emrService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.IpdEmrId);
        }

        [HttpPut("Edit/{id:int}")]
        //[Permission]
        public async Task<ApiResponse> Edit(EMRModel obj)
        {
            if (obj.IpdEmrId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

            TIpEmrhistory model = obj.MapTo<TIpEmrhistory>();
            model.ModifiedBy = CurrentUserId;
            model.ModifiedDate = AppTime.Now;

            await _emrService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.IpdEmrId);
        }
    }
}