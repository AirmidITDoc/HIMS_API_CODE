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

        [HttpPost("DietMainList")]
        //[Permission]
        public async Task<IActionResult> DietMainList(GridRequestModel objGrid)
        {
            IPagedList<DietMainlListDto> list = await _dietPlanScheduleService.GetMainListAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "DietPlanSchedule List"));
        }

        [HttpPost("DietMainSubList")]
        //[Permission]
        public async Task<IActionResult> DietMainSubList(GridRequestModel objGrid)
        {
            IPagedList<DietMainSublListDto> list = await _dietPlanScheduleService.GetMainSubListAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "DietPlanSchedule List"));
        }

        [HttpPost("DietPlanScheduleDetailList")]
        //[Permission]
        public async Task<IActionResult> DietPlanScheduleDetailList(GridRequestModel objGrid)
        {
            IPagedList<DietPlanDetailListDto> list = await _dietPlanScheduleService.DietPlanScheduleDetailListAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "Diet Plan Schedule Details List"));
        }
        [HttpPost("DietPlanSchedulePatientlList")]
        //[Permission]
        public async Task<IActionResult> DietPlanSchedulePatientList(GridRequestModel objGrid)
        {
            IPagedList<DietSchedulePatientListDto> list = await _dietPlanScheduleService.DietPlanSchedulePatientListAsync(objGrid);
            return Ok(list.ToGridResponse(objGrid, "Diet Plan Schedule Patient List"));
        }

        [HttpPut("UpdateFinal/{id:long}")]
        public async Task<ApiResponse> Final(long id)
        {
            var model = await _repository.GetById(x => x.DietPlanId == id);

            if (model == null)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }

            model.DraftOrFinal = true;
            model.ModifiedBy = CurrentUserId;
            model.ModifiedDate = AppTime.Now;

            await _repository.Update(model, CurrentUserId, CurrentUserName, null);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Diet Plan finalized successfully.", id);
        }

        [HttpPut("UpdateDietPlanScheduleStatus")]
        public async Task<ApiResponse> UpdateDietPlanScheduleStatus(List<DietPlanScheduleStatusUpdateModel> obj)
        {
            if (obj == null || obj.Count == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Invalid params");
            }

            foreach (var item in obj)
            {
                if (item.DietPlanIds == 0 ||
                    !new[] { "Order", "Accepted", "Inprogress", "Delivered" }.Contains(item.Flag))
                {
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Invalid params");
                }
            }

            await _dietPlanScheduleService.UpdateDietPlanScheduleStatus(obj, CurrentUserId, CurrentUserName);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Diet Plan Status updated successfully.");
        }
    }
}