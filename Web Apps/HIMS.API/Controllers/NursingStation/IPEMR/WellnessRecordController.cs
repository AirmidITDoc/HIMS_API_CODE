using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Nursing.IPEMR;
using HIMS.Core;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.Nursing.IPEMR;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.NursingStation.IPEMR
{
    public class WellnessRecordController : BaseController
    {
        private readonly IGenericService<TOpipWellnessRecord> _repository;
        public WellnessRecordController(IGenericService<TOpipWellnessRecord> repository1)
        {
            _repository = repository1;
        }

        [HttpGet("{id?}")]
        public async Task<ApiResponse> Get(int id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.Opipid == id);
            return data.ToSingleResponse<TOpipWellnessRecord, WellnessRecordModel>("TOpipWellnessRecord");
        }
        //Add API
        [HttpPost("Insert")]
        public async Task<ApiResponse> Post(WellnessRecordModel obj)
        {
            TOpipWellnessRecord model = obj.MapTo<TOpipWellnessRecord>();
            if (obj.WlsId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                await _repository.Add(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        //Edit API
        [HttpPut("Edit/{id:int}")]
        public async Task<ApiResponse> Edit(WellnessRecordModel obj)
        {
            TOpipWellnessRecord model = obj.MapTo<TOpipWellnessRecord>();
            if (obj.WlsId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;

                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }
    }
}
