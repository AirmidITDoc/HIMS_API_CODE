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
                await _RadilogyService.RadiologyUpdate(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record updated successfully.");
        }
    }
}
