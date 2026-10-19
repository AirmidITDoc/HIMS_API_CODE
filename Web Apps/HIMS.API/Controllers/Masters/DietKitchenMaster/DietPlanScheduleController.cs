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
        public async Task<ApiResponse> Post(List<DietPlanScheduleModel> objList)
        {
            if (objList == null || !objList.Any())
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }

            var models = objList.Select(obj => {
                var model = obj.MapTo<TDietPlanSchedule>();
                model.PlanStartDate = Convert.ToDateTime(obj.PlanStartDate);
                model.PlanEndDate = Convert.ToDateTime(obj.PlanEndDate);
                model.PlanDate = Convert.ToDateTime(obj.PlanDate);
                return model;
            }).ToList();

            await _dietPlanScheduleService.InsertAsync(models, CurrentUserId, CurrentUserName);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Records added successfully.");
        }


        [HttpPut("Edit/{id:int}")]
        //[Permission]
        public async Task<ApiResponse> Edit(DietPlanScheduleModel obj)
        {
            TDietPlanSchedule model = obj.MapTo<TDietPlanSchedule>();
            if (obj.DietPlanId == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
            else
            {
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = AppTime.Now;
                await _repository.Update(model, CurrentUserId, CurrentUserName, new string[2] { "CreatedBy", "CreatedDate" });
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        [HttpDelete]
        //[Permission]
        public async Task<ApiResponse> Delete(long id)
        {
            TDietPlanSchedule model = await _repository.GetById(x => x.DietPlanId == id);
            if ((model?.DietPlanId ?? 0) > 0)
            {
                model.IsCancelled = model.IsCancelled == true ? false : true;
                model.IsCancelledBy = CurrentUserId;
                model.IsCancelledDate = AppTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record deleted successfully.");
            }
            else
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
        }
    }
}