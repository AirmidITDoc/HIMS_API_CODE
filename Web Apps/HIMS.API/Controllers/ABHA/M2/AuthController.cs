using Asp.Versioning;
using HIMS.ABHA.Helper;
using HIMS.ABHA.Interface;
using HIMS.ABHA.Models.M2;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Models.PaymentGateway;
using HIMS.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text.Json;

namespace HIMS.API.Controllers.ABHA.M2
{
    [Route("api/v{version:apiVersion}/m2/auth")]
    [ApiController]
    [ApiVersion("1")]
    public class AuthController : BaseController
    {
        private readonly IAbdmAuthService _abhaService;
        private readonly IConfiguration _configuration;
        public AuthController(IAbdmAuthService abhaService, IConfiguration configuration)
        {
            _abhaService = abhaService;
            _configuration = configuration;
        }
        [HttpPost("bridge/url")]
        public async Task<ApiResponse> UpdateBridgeUrl([FromBody] UpdateBridgeUrlRequest req)
        {
            var result = await _abhaService.UpdateBridgeUrlAsync(req);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Otp Sent successfully.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }

        [HttpPost("bridge/callback")]
        public async Task<IActionResult> Callback([FromBody] JsonElement payload)
        {
            string path = _configuration["ExceptionLogging:Directory"].ToString().Trim('\\') + "\\M2Callback";
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            string filename = path + "\\" + AppTime.Now.ToString("dd_MM_yyyy") + ".txt";
            System.IO.File.AppendAllText(filename, "\n New request come at" + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss") + " =>" + payload.ToString());
            return Ok();
        }

        [HttpPost("bridge/register")]
        public async Task<ApiResponse> RegisterBridgeServices([FromBody] RegisterBridgeRequest req)
        {
            var result = await _abhaService.RegisterBridgeServicesAsync(req);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Bridge services registered successfully.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }

        [HttpGet("bridge-service/{serviceId}")]
        public async Task<ApiResponse> FindByServiceId(string serviceId)
        {
            var result = await _abhaService.FindServiceByServiceIdAsync(serviceId);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service found.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }

        [HttpGet("bridge-services")]
        public async Task<ApiResponse> FindServicesByBridge()
        {
            var result = await _abhaService.FindServicesByBridgeIdAsync();
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Services found.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }
    }
}
