using Asp.Versioning;
using HIMS.Api.Models.Common;
using HIMS.API.Models.AbhaIntegration;
using HIMS.Services.AbhaIntegration;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HIMS.API.Controllers.AbhaIntegration
{
    //[ApiController]
   // [Route("api/[controller]")]

    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AbhaConnects : ControllerBase
    {
        private readonly IAbhaConnectService _abhaConnectService;
        public AbhaConnects(IAbhaConnectService abhaConnectService)
        {
            _abhaConnectService = abhaConnectService;
        }

        [HttpPost("InitiateClient")]
        public async Task<ApiResponse> InitiateClient(InitiateClientModel model)
        {
            var result = await _abhaConnectService.InitiateClient(model);

            var json = JsonSerializer.Serialize(result);
            var obj = JsonSerializer.Deserialize<JsonElement>(json);

            var responseCode = obj.GetProperty("ResponseCode").GetInt32();
            var responseMessage = obj.GetProperty("ResponseMessage").GetString();
            var responseData = obj.GetProperty("ResponseData");

            return ApiResponseHelper.GenerateResponse(
                (ApiStatusCode)responseCode,
                responseMessage,
                responseData
            );
        }
    }
}
