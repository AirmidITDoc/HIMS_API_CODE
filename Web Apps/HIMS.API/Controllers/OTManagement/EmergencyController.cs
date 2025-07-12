using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.OTManagement;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data.DTO.IPPatient;
using HIMS.Data.Models;
using HIMS.Services;
using HIMS.Services.IPPatient;
using HIMS.Services.OTManagment;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OTManagement
{

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class EmergencyController : BaseController
    {
        private readonly IEmergencyService _EmergencyService;
        public EmergencyController(IEmergencyService repository)
        {
            _EmergencyService = repository;
        }
        [HttpPost("Emergencylist")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.View)]
        public async Task<IActionResult> ListE(GridRequestModel objGrid)
        {
            IPagedList<EmergencyListDto> Emergencylist = await _EmergencyService.GetListAsync(objGrid);
            return Ok(Emergencylist.ToGridResponse(objGrid, "Emergencylist "));
        }
        [HttpPost("InsertSP")]
        //[Permission(PageCode = "SupplierMaster", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(EmergencyMode obj)
        {
            TEmergencyAdm model = obj.MapTo<TEmergencyAdm>();
            if (obj.EmgId == 0)
            {
                model.EmgTime = Convert.ToDateTime(obj.EmgTime);
                model.AddedBy = CurrentUserId;
                //model.IsActive = true;
                await _EmergencyService.InsertAsyncSP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record added successfully.");
        }

    }
}
