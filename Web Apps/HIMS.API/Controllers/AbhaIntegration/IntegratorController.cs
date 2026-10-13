using Asp.Versioning;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.AbhaIntegration;
using HIMS.Data.Models;
using HIMS.Services.AbhaIntegration;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.AbhaIntegration
{
    [Route("[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class IntegratorController : ControllerBase
    {
        private readonly IAbhaConnectService _abhaConnectService;
        public IntegratorController(IAbhaConnectService abhaConnectService)
        {
            _abhaConnectService = abhaConnectService;
        }

        [HttpPost("async/error")]
        public async Task<ApiResponse> InsertErrorLog(AbhaErroLog obj)
        {
            TAbhaErrorLog model = obj.MapTo<TAbhaErrorLog>();
            await _abhaConnectService.InsertErrorLogAsync(model);
            return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Error log added successfully.");
        }
    }
}
