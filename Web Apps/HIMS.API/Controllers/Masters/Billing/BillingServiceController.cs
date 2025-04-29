using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.DTO.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Masters.Billing
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class BillingServiceController : BaseController
    {
        private readonly IGenericService<ServiceMaster> _repository;
        private readonly IBillingService _BillingService;
        public BillingServiceController(IBillingService repository, IGenericService<ServiceMaster> repository1)
        {
            _BillingService = repository;
            _repository = repository1;
        }

        [HttpPost("BillingList")]
        [Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> List(GridRequestModel objGrid)
        {
            IPagedList<BillingServiceDto> BillingList = await _BillingService.GetListAsync(objGrid);
            return Ok(BillingList.ToGridResponse(objGrid, "Billing List"));
        }
        [HttpPost("PackageServiceInfoList")]
        [Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> Lists(GridRequestModel objGrid)
        {
            IPagedList<PackageServiceInfoListDto> ServiceList = await _BillingService.GetListAsync1(objGrid);
            return Ok(ServiceList.ToGridResponse(objGrid, "PackageService List"));
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
        [HttpDelete("ServicDelete")]
        [Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.Delete)]
        public async Task<ApiResponse> Delete(int Id)
        {
            ServiceMaster model = await _repository.GetById(x => x.ServiceId == Id);
            if ((model?.ServiceId ?? 0) > 0)
            {
                model.IsActive = false;
                model.ModifiedBy = CurrentUserId;
                model.ModifiedDate = DateTime.Now;
                await _repository.SoftDelete(model, CurrentUserId, CurrentUserName);
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Servic deleted successfully.");
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");

        }
        [HttpGet("GetServiceListwithGroupWise")]
        public async Task<ApiResponse> GetServiceListwithGroupWise(int TariffId, int ClassId, string IsPathRad, string ServiceName)
        {
            var resultList = await _BillingService.GetServiceListwithGroupWise(TariffId, ClassId, IsPathRad, ServiceName);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Get ServiceList with Group Wise List.", resultList);
        }

       

        [HttpPut("UpdateDifferTariff")]
        [Permission(PageCode = "BillingServiceMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(DifferTraiffModel obj)
        {
            if (obj.OldTariffId == 0 || obj.NewTariffId == 0)
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid parameters: Tariff IDs cannot be zero.");
            }

            var model = new ServiceDetail
            {
                TariffId = obj.OldTariffId 
            };

            int userId = 1; 
            string userName = "admin"; 

            await _BillingService.UpdateDifferTariff(model, obj.OldTariffId, obj.NewTariffId, userId, userName);

            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "UpdateDifferTraiff  successfully.");
        }


    }
}


