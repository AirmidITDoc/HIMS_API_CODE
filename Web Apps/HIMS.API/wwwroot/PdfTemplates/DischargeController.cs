using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Inventory
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DischargeController : BaseController
    {
        private readonly IDischargeService _IDischargeService;
        public DischargeController(IDischargeService repository)
        {
            _IDischargeService = repository;
        }
        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(DischargeModel obj)
        {
            Discharge model = obj.MapTo<Discharge>();
            if (obj.DischargeId == 0)
            {
                model.DischargeDate = Convert.ToDateTime(obj.DischargeDate);
                model.AddedBy = CurrentUserId;

                await _IDischargeService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Discharge  added successfully.");
        }

        [HttpPut("Edit/{id:int}")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(DischargeModel obj)
        {
            Discharge model = obj.MapTo<Discharge>();
            if (obj.DischargeId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.DischargeDate = Convert.ToDateTime(obj.DischargeDate);
                await _IDischargeService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Discharge  updated successfully.");
        }

    }
}
