using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.DTO.Pathology;
using HIMS.Data.Models;
using HIMS.Services.Inventory;
using HIMS.Services.Radiology;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Radiology
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class RadiologyController : BaseController
    {
        private readonly IRadilogyService _RadilogyService;
        public RadiologyController(IRadilogyService repository)
        {
            _RadilogyService = repository;
        }

       
        [HttpPost("RadiologyList")]
        //[Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> Lists(GridRequestModel objGrid)
        {
            IPagedList<RadiologyListDto> RadiologyList = await _RadilogyService.GetListAsync(objGrid);
            return Ok(RadiologyList.ToGridResponse(objGrid, "RadiologyList "));
        }

        [HttpPut("RadiologyUpdate/{id:int}")]
        //[Permission(PageCode = "RadiologyTestMaster", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Update(TRadiologyReportModel obj)
        {
            TRadiologyReportHeader model = obj.MapTo<TRadiologyReportHeader>();
            if (obj.RadReportId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.RadDate = DateTime.Now;
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _RadilogyService.RadiologyUpdate(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
        //shilpa 26-09-2025//
        [HttpPut("RadiologyOutsourceUpdate/{id:int}")]
        //[Permission(PageCode = "Pathology", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(TRadiologyReportUpdate obj)
        {
            TRadiologyReportHeader model = obj.MapTo<TRadiologyReportHeader>();

            if (obj.RadReportId == 0)

                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {
                model.ModifiedDate = DateTime.Now;
                model.ModifiedBy = CurrentUserId;
                await _RadilogyService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }

        [HttpPost("Verify")]
        //[Permission(PageCode = "Pathology", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Verify(RadiologyVerifyModel obj)
        {
            TRadiologyReportHeader model = obj.MapTo<TRadiologyReportHeader>();
            if (obj.RadReportId != 0)
            {
                model.IsVerified = true;
                model.IsVerifyedDate = DateTime.Now.Date;

                await _RadilogyService.VerifyAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record verify successfully.");
        }
    }
}
