using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
        public class BillingServiceController : BaseController
        {
        private readonly IBillingService _BillingService;
        public BillingServiceController(IBillingService repository)
        {
            _BillingService = repository;
        }

        [HttpPost("BillingList")]
        [Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<BillingServiceDto> BillingList = await _BillingService.GetListAsync(objGrid);
            return Ok(BillingList.ToGridResponse(objGrid, "Billing List"));
        }

      
        [HttpPost("InsertEDMX")]
        [Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(BillingServiceModel obj)
        {
            ServiceMaster model = obj.MapTo<ServiceMaster>();
            if (obj.ServiceId == 0)
            {
                model.CreatedDate = DateTime.Now;
                model.CreatedBy = CurrentUserId;
                model.IsActive = true;
                await _BillingService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service Name  added successfully.");
        }

        [HttpPut("Edit/{id:int}")]
        [Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(BillingServiceModel obj)
        {
            ServiceMaster model = obj.MapTo<ServiceMaster>();
            if (obj.ServiceId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _BillingService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service Name updated successfully.");
        }

         [HttpPost("ServiceCanceled")]
        [Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Cancel(BillingServiceModel obj)
        {
            ServiceMaster model = new();
            if (obj.ServiceId != 0)
            {
                model.ServiceId = obj.ServiceId;
                model.CreatedBy = CurrentUserId;
                model.CreatedDate = DateTime.Now;
                await _BillingService.CancelAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service Canceled successfully.");
        }
        
    }
}
