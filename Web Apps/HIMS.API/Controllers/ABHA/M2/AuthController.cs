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
//using System.Text.Json;
using System.IO;
using HIMS.Data.Models;
using HIMS.Data;

namespace HIMS.API.Controllers.ABHA.M2
{
    [Route("api/v{version:apiVersion}/m2/auth")]
    [ApiController]
    [ApiVersion("1")]
    public class AuthController : BaseController
    {
        private readonly IAbdmAuthService _abhaService;
        private readonly IConfiguration _configuration;
        private readonly IGenericService<TAbhaLinkTokenCallback> _TAbhaLinkTokenCallback;
        public AuthController(IAbdmAuthService abhaService, IConfiguration configuration, IGenericService<TAbhaLinkTokenCallback> genericService)
        {
            _abhaService = abhaService;
            _configuration = configuration;
            _TAbhaLinkTokenCallback = genericService;
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
        [HttpPost("~/api/v3/hip/token/on-generate-token")]
        public async Task<IActionResult> OnGenerateToken([FromBody] LinkTokenCallbackPayload payload)
        {
            string path = _configuration["ExceptionLogging:Directory"].ToString().Trim('\\') + "\\M2Callback";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string filename = $"{path}\\{AppTime.Now:dd_MM_yyyy}.txt";
            if (payload.IsSuccess)
            {
                await System.IO.File.AppendAllTextAsync(filename, $"\n[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] SUCCESS requestId={payload.Response.RequestId} abhaAddress={payload.AbhaAddress} linkToken={payload.LinkToken}");
            }
            else
            {
                await System.IO.File.AppendAllTextAsync(filename, $"\n[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] FAILURE code={payload.Error?.Code} message={payload.Error?.Message}");
            }

            // Insert into DB
            var callback = new TAbhaLinkTokenCallback
            {
                RequestId = payload.Response?.RequestId,
                AbhaAddress = payload.AbhaAddress,
                LinkToken = payload.LinkToken,
                ErrorCode = payload.Error?.Code,
                ErrorMessage = payload.Error?.Message,
                IsSuccess = payload.Error == null,
                CallbackDate = DateTime.Now,
                RawResponse = JsonConvert.SerializeObject(payload)
                //RawResponse = payload
            };

            await _TAbhaLinkTokenCallback.Add(callback, 1, "System");

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
