using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.API.Models.OutPatient;
using HIMS.Data.Models;
using HIMS.Services.Masters;
using HIMS.Services.OPPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DoctorShareMasterController : BaseController
    {
        private readonly IDoctorShareMasterService _DoctorShareMasterService;
        public DoctorShareMasterController(IDoctorShareMasterService repository)
        {
            _DoctorShareMasterService = repository;
        }
        [HttpPost("InsertEDMX")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
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
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
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
