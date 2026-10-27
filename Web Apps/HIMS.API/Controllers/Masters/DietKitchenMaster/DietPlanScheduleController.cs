using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Diet;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Core.Infrastructure;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.DietKitchen;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace HIMS.API.Controllers.Masters.DietKitchenMaster
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DietPlanScheduleController : BaseController
    {
        private readonly IGenericService<TDietPlanSchedule> _repository;
        private readonly IDietPlanScheduleService _dietPlanScheduleService;

        public DietPlanScheduleController(IGenericService<TDietPlanSchedule> repository, IDietPlanScheduleService dietPlanScheduleService)
        {
            _repository = repository;
            _dietPlanScheduleService = dietPlanScheduleService;
        }

        [HttpPost("List")]
        //[Permission]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<DietPlanScheduleListDto> list = await _dietPlanScheduleService.GetListAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "DietPlanSchedule List"));
        }

        [HttpPost("DietplandetailsList")]
        //[Permission]
        public async Task<IActionResult> DietplandetailsList(GridRequestModel objGrid)
        {
            IPagedList<DietPlanDetailListDto> list = await _dietPlanScheduleService.DietplandetailsListAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "DietPlanDetails List"));
        }

        [HttpGet("{id?}")]
        //[Permission]
        public async Task<ApiResponse> Get(long id)
        {
            if (id == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _repository.GetById(x => x.DietPlanId == id);
            return data.ToSingleResponse<TDietPlanSchedule, DietPlanScheduleModel>("DietPlanSchedule");
        }

        [HttpPost("Insert")]
        //[Permission]
        public async Task<ApiResponse> Insert(DietPlanScheduleModel obj)
        {
            TDietPlanSchedule model = obj.MapTo<TDietPlanSchedule>();
            if (obj.DietPlanId == 0)
            {
                if (model.TDietPlanScheduleDetails != null)
                {
                    foreach (var q in model.TDietPlanScheduleDetails)
                    {
                        q.DietPlanDetId = 0;
                    }
                }

                model.CreatedDate = AppTime.Now;
                model.CreatedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                model.ModifiedBy = CurrentUserId;

                await _dietPlanScheduleService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.", model.DietPlanId);
        }

        [HttpPut("Edit/{id:int}")]
        //[Permission]
        public async Task<ApiResponse> Edit(DietPlanScheduleModel obj)
        {
            if (obj.DietPlanId == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            TDietPlanSchedule model = obj.MapTo<TDietPlanSchedule>();

            if (model.TDietPlanScheduleDetails != null)
            {
                foreach (var q in model.TDietPlanScheduleDetails)
                {
                    q.DietPlanDetId = 0;
                }
            }

            model.ModifiedDate = AppTime.Now;
            model.ModifiedBy = CurrentUserId;

            await _dietPlanScheduleService.UpdateAsync(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.", model.DietPlanId);
        }

        [HttpPost("DietPlanScheduleCancel")]
        //[Permission]
        public async Task<ApiResponse> Cancel(DietPlanScheduleCancelModel obj)
        {
            TDietPlanSchedule model = obj.MapTo<TDietPlanSchedule>();
            if (obj.DietPlanId != 0)
            {
                model.DietPlanId = obj.DietPlanId;
                model.IsCancelledBy = CurrentUserId;
                model.IsCancelledDate = AppTime.Now;
                await _dietPlanScheduleService.Cancel(model, CurrentUserId, CurrentUserName);
            }
            else
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.");
        }

        [HttpPost("DietPlanScheduleDetailCancel")]
        //[Permission]
        public async Task<ApiResponse> CancelD(DietPlanScheduleDetailsCancelModel obj)
        {
            TDietPlanScheduleDetail model = obj.MapTo<TDietPlanScheduleDetail>();
            if (obj.DietPlanDetId != 0)
            {
                model.DietPlanDetId = obj.DietPlanDetId;
                model.IsCancelledBy = CurrentUserId;
                model.IsCancelledDate = AppTime.Now;
                await _dietPlanScheduleService.CancelD(model, CurrentUserId, CurrentUserName);
            }
            else
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Canceled successfully.");
        }
    }
}