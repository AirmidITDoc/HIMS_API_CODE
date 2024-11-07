using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.IPPatient;
using HIMS.API.Models.OPPatient;
using HIMS.Data.Models;
using HIMS.Services.IPPatient;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.OPPatient
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]

    public class DischargeCancellationController : BaseController
    {
        private readonly IDischargeCancellationService _IDischargeCancellationService;
        public DischargeCancellationController(IDischargeCancellationService repository)
        {
            _IDischargeCancellationService = repository;
        }
        [HttpPut("IPDischarge")]
        //[Permission(PageCode = "Indent", Permission = PagePermission.Add)]
        public async Task<ApiResponse> Cancel(DischargeCancellationModel obj)
        {
            Admission model = obj.MapTo<Admission>();
            if (obj.AdmissionId != 0)
            {
                await _IDischargeCancellationService.UpdateAsync(model, CurrentUserId, CurrentUserName);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status500InternalServerError, "Invalid params");
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "IPDischarge updated successfully.");
        }
    }
}
