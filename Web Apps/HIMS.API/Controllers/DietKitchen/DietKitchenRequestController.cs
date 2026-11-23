using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.Core;
using HIMS.Data;
using HIMS.Data.Models;
using HIMS.Services.DietKitchen;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static HIMS.API.Models.DietKitchen.DietKitchenRequestModel;

namespace HIMS.API.Controllers.DietKitchen
{
    
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DietKitchenRequestController : BaseController
    {
        private readonly IDietKitchenRequestService _canteenService;
        public DietKitchenRequestController(IDietKitchenRequestService canteenService)
        {
            _canteenService = canteenService;
        }

        [HttpPut("UpdateDietPlanScheduleStatus")]
        [Permission(PageCode = "DietRequest", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> UpdateDietPlanScheduleStatus(List<CanteenDietPlanScheduleStatusUpdateModel> obj)
        {
            if (obj == null || obj.Count == 0)
            {
                return ApiResponseHelper.GenerateResponse( ApiStatusCode.Status400BadRequest, "Invalid params");
            }

            foreach (var item in obj)
            {
                if (item.DietPlanDetId <= 0 || item.DietMealTypeId <= 0 ||item.PlanDate == default ||
                    !new[] { "Order", "Accepted", "Inprogress", "Delivered" }.Contains(item.Flag))
                {
                    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Invalid params");
                }
            }

            await _canteenService.UpdateDietPlanScheduleStatus(obj.Select(x => new TDietPlanScheduleDetail
             {
                 PlanDate = x.PlanDate,
                 DietPlanDetId = x.DietPlanDetId,
                 DietMealTypeId = x.DietMealTypeId
             }).ToList(),obj.First().Flag,CurrentUserId,CurrentUserName);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK,"Diet Plan Status updated successfully.");
        }
        [HttpPut("UpdateDietPlanDateStatus")]
        [Permission(PageCode = "DietRequest", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> UpdateDietPlanDateStatus(CanteenDietPlanDateStatusUpdateModel obj)
        {
            if (obj == null || obj.PlanDate == default ||
                !new[] { "Order", "Accepted", "Inprogress", "Delivered" }.Contains(obj.Flag))
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Invalid params");
            }

            await _canteenService.UpdateDietPlanDateStatus(obj.PlanDate,obj.Flag,CurrentUserId,CurrentUserName);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Status updated successfully");
        }
    }
}
