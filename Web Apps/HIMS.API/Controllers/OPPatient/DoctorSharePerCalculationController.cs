using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.Masters;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class DoctorSharePerCalculationController : BaseController
    {
        private readonly IDoctorSharePerCalculationService _IDoctorSharePerCalculationService;
        public DoctorSharePerCalculationController(IDoctorSharePerCalculationService repository)
        {
            _IDoctorSharePerCalculationService = repository;
        }
        [HttpPut("OPDoctorSharePerCalculation")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public ApiResponse UpdateAsyncOP(DoctorSharePerCalculationModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            if (obj.BillNo == 0)
            {
                _IDoctorSharePerCalculationService.UpdateOP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OPDoctorSharePerCalculation updated successfully.");
        }
        [HttpPut("IPDoctorSharePerCalculation")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public ApiResponse UpdateAsyncIP(DoctorSharePerCalculationModel obj)
        {
            Bill model = obj.MapTo<Bill>();
            if (obj.BillNo != 0)
            {
                _IDoctorSharePerCalculationService.UpdateIP(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "OPDoctorSharePerCalculation updated successfully.");
        }

    }
}
