using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.Core.Domain.Grid;
using HIMS.Core;
using HIMS.Data.DTO.Inventory;
using HIMS.Data.Models;
using HIMS.Services.Masters;
using HIMS.Services.OPPatient;
using Microsoft.AspNetCore.Mvc;
using HIMS.Data.DTO.Administration;
using HIMS.Services.Inventory;
using HIMS.Services.IPPatient;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DoctorShareMasterController : BaseController
    {
        private readonly IDoctorShareMasterService _DoctorShareMasterService;
        private readonly IBillingService _BillingService;

        public DoctorShareMasterController(IDoctorShareMasterService repository, IBillingService repository1)
        {
            _DoctorShareMasterService = repository;
            _BillingService = repository1;

        }
        //[HttpPost("DoctorShareList")]
        //[Permission(PageCode = "Doctorshare", Permission = PagePermission.View)]
        //public async Task<IActionResult> List(GridRequestModel objGrid)
        //{
        //    IPagedList<DoctorShareListDto> DoctorShareList = await _BillingService.GetListAs(objGrid);
        //    return Ok(DoctorShareList.ToGridResponse(objGrid, "DoctorShareList "));
        //}
        [HttpPost("InsertEDMX")]
        [Permission(PageCode = "Doctorshare", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Insert(DoctorShareMasterModel obj)
        {
            MDoctorPerMaster model = obj.MapTo<MDoctorPerMaster>();
            if (obj.DoctorShareId == 0)
            {
                 await _DoctorShareMasterService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DoctorSharemaster added successfully.");
        }

        [HttpPut("Update")]
        [Permission(PageCode = "Doctorshare", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Update(DoctorShareMasterModel obj)
        {
            MDoctorPerMaster model = obj.MapTo<MDoctorPerMaster>();
            if (obj.DoctorShareId != 0)
            {
                await _DoctorShareMasterService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "DoctorShareMaster updated successfully.");
        }
        
    }
}
