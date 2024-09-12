using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.Core.Domain.Grid;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;
using static HIMS.API.Models.Inventory.PathTestDetailModelModelValidator;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
        public class BillingServiceController : BaseController
        {
        private readonly IBillingService _IBillingService;
        public BillingServiceController(IBillingService repository)
        {
            _IBillingService = repository;
        }
        //List API
        [HttpPost]
        [Route("[action]")]
        //[Permission(PageCode = "TestMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            List<ServiceMaster> ServiceMasterList = await _IBillingService.GetAllRadiologyTest();
            return Ok(ServiceMasterList.ToList());
        }
        [HttpPost("Insert")]
        //[Permission(PageCode = "TestMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(BillingServiceModel obj)
        {
            ServiceMaster model = obj.MapTo<ServiceMaster>();
            if (obj.ServiceId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.IsActive = true;
                await _IBillingService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service Name added successfully.");
        }
        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "ItemMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(BillingServiceModel obj)
        {
            ServiceMaster model = obj.MapTo<ServiceMaster>();
            if (obj.ServiceId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.IsActive = true;
                await _IBillingService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service Name  added successfully.");
        }

        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "TestMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(BillingServiceModel obj)
        {
            ServiceMaster model = obj.MapTo<ServiceMaster>();
            if (obj.ServiceId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                await _IBillingService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service Name updated successfully.");
        }
        [HttpPost("ServiceCanceled")]
        //[Permission(PageCode = "TestMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(BillingServiceModel obj)
        {
            ServiceMaster model = new();
            if (obj.ServiceId != 0)
            {
                model.ServiceId = obj.ServiceId;
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _IBillingService.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service Canceled successfully.");
        }
    }
}
