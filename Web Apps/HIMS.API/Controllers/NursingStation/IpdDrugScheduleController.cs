using HIMS.Api.Controllers;
using HIMS.Data.Models;
using HIMS.Data;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;
using HIMS.Services.Nursing;
using HIMS.API.Extensions;
using HIMS.Api.Models.Common;
using HIMS.API.Models.Masters;
using HIMS.Core.Infrastructure;
using HIMS.Core;
using HIMS.API.Models.Nursing;

namespace HIMS.API.Controllers.NursingStation
{
    public class IpdDrugScheduleController : BaseController
    {
        private readonly IIpdDrugScheduleService _IIpdDrugScheduleService;
        public IpdDrugScheduleController(IIpdDrugScheduleService repository)
        {
            _IIpdDrugScheduleService = repository;
        }
        [HttpPost("Insert")]
        //[Permission]
        public async Task<ApiResponse> Insert(IpdDrugScheduleModel obj)
        {
            IpdDrugSchedule model = obj.MapTo<IpdDrugSchedule>();
            model.IsActive = true;

            if (obj.IpdDrugScheduleId == 0)
            {
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
               
                await _IIpdDrugScheduleService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.IpdDrugScheduleId);
        }
    }
}
