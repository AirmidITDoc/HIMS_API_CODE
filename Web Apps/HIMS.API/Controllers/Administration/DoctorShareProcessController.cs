using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Administration;
using HIMS.Data.Models;
using HIMS.Services.Administration;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.Administration
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DoctorShareProcessController : BaseController
    {
        private readonly IDoctorShareProcessService _IDoctorShareProcessService;
        public DoctorShareProcessController(IDoctorShareProcessService repository)
        {
            _IDoctorShareProcessService = repository;
        }

        [HttpPost("DoctorShareProcess")]
        //[Permission(PageCode = "Administration", Permission = PagePermission.Add)]
        public ApiResponse Insert(DoctorShareProcessModel obj)
        {
            if (obj.FromDate == new DateTime(1900 / 01 / 01))

            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            }
            //👇 Manually assign fields from LabRequestsModel to AddCharge
            var model = new AddCharge
            {
                //FromDate = obj.FromDate,
            };

            _IDoctorShareProcessService.DoctorShareInsert(model, CurrentUserId, CurrentUserName, obj.FromDate, obj.ToDate);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record Added successfully.");
        }

        [HttpPost("InsertDoctorPerMaster")]
        //[Permission(PageCode = "Administration", Permission = PagePermission.Add)]
        public async Task<ApiResponse> InsertEDMX(MDoctorPerMasterModel obj)
        {
            MDoctorPerMaster model = obj.MapTo<MDoctorPerMaster>();
            if (obj.DoctorShareId == 0)
            {

                await _IDoctorShareProcessService.InsertAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  added successfully.");
        }
        [HttpPut("UpdateDoctorPerMaster Edit/{id:int}")]
        //[Permission(PageCode = "Administration", Permission = PagePermission.Edit)]
        public async Task<ApiResponse> Edit(MDoctorPerMasterModel obj)
        {
            MDoctorPerMaster model = obj.MapTo<MDoctorPerMaster>();
            if (obj.DoctorShareId == 0)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            else
            {

                await _IDoctorShareProcessService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Record  updated successfully.");
        }

    }
}
