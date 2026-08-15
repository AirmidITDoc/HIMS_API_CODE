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

            ////string path = Path.Combine(_configuration["ExceptionLogging:Directory"].TrimEnd('\\'), "M2Callback");
            //if (!Directory.Exists(path))
            //    Directory.CreateDirectory(path);

            //string filename = Path.Combine(path, $"{AppTime.Now:dd_MM_yyyy}.txt");

            // Convert complete payload object to JSON
            string rawResponse = JsonConvert.SerializeObject(payload, Formatting.Indented);

            string log = $@"
                =========================================================
                DateTime : {DateTime.Now:dd/MM/yyyy HH:mm:ss}
                Status   : {(payload.IsSuccess ? "SUCCESS" : "FAILURE")}
                RequestId: {payload.Response?.RequestId}
                ABHA     : {payload.AbhaAddress}
                LinkToken: {payload.LinkToken}

                Raw Response:
                {rawResponse}
                =========================================================";

            await System.IO.File.AppendAllTextAsync(filename, log);


            if (!string.IsNullOrWhiteSpace(payload.AbhaAddress))
            {
                var lstToken = await _TAbhaLinkTokenCallback.GetAll(x => x.AbhaAddress == payload.AbhaAddress && x.LinkToken == null);
                if (lstToken.Any())
                {
                    // Update existing record
                    var existingToken = lstToken.FirstOrDefault();
                    existingToken.RequestId = payload.Response?.RequestId;
                    existingToken.LinkToken = payload.LinkToken;
                    existingToken.ErrorCode = payload.Error?.Code;
                    existingToken.ErrorMessage = payload.Error?.Message;
                    existingToken.IsSuccess = payload.Error == null;
                    existingToken.CallbackDate = DateTime.Now;
                    existingToken.RawResponse = JsonConvert.SerializeObject(payload);

                    await _TAbhaLinkTokenCallback.Update(existingToken, 1, "System", null);
                }
            }
            return Ok();
        }

        [HttpPost("~/api/v3/link/on_carecontext")]
        public async Task<IActionResult> OnCareContextToken([FromBody] LinkTokenCallbackPayload payload)
        {
            string path = _configuration["ExceptionLogging:Directory"].ToString().Trim('\\') + "\\M2Callback";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string filename = $"{path}\\{AppTime.Now:dd_MM_yyyy}.txt";

            // Convert complete payload object to JSON
            string rawResponse = JsonConvert.SerializeObject(payload, Formatting.Indented);

            string log = $@"
                =========================================================
                DateTime : {DateTime.Now:dd/MM/yyyy HH:mm:ss}
                Status   : {(payload.IsSuccess ? "SUCCESS" : "FAILURE")}
                RequestId: {payload.Response?.RequestId}
                ABHA     : {payload.AbhaAddress}
                LinkToken: {payload.LinkToken}
                Status   : {payload.Status}

                Raw Response:
                {rawResponse}
                =========================================================";

            await System.IO.File.AppendAllTextAsync(filename, log);


            if (!string.IsNullOrWhiteSpace(payload.AbhaAddress))
            {
                var lstToken = await _TAbhaLinkTokenCallback.GetAll(x => x.AbhaAddress == payload.AbhaAddress);
                if (lstToken.Any())
                {
                    // Update existing record
                    var existingToken = lstToken.FirstOrDefault();
                    existingToken.OnCareRequestId = payload.Response?.RequestId;
                    existingToken.Status = payload.Status;
                    existingToken.ErrorCode = payload.Error?.Code;
                    existingToken.ErrorMessage = payload.Error?.Message;
                    existingToken.IsSuccess = payload.Error == null;
                    existingToken.OnCareRawResponse = JsonConvert.SerializeObject(payload);

                    await _TAbhaLinkTokenCallback.Update(existingToken, 1, "System", null);
                }
            }
            return Ok();
        }

        [HttpPost("~/api/v3/consent/request/hip/notify")]
        public async Task<IActionResult> OnConsentNotification([FromBody] ConsentNotificationPayload payload)
        {
            string path = _configuration["ExceptionLogging:Directory"].ToString().Trim('\\') + "\\M2Callback";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string filename = $"{path}\\{AppTime.Now:dd_MM_yyyy}.txt";
            // Convert complete payload object to JSON
            string rawResponse = JsonConvert.SerializeObject(payload, Formatting.Indented);

            string log = $@"
                    =========================================================
                    DateTime        : {DateTime.Now:dd/MM/yyyy HH:mm:ss}
                    Status          : {payload.Notification?.Status}
                    ConsentId       : {payload.Notification?.ConsentId}
                    Patient         : {payload.Notification?.ConsentDetail?.Patient?.Id}
                    SchemaVersion   : {payload.Notification?.ConsentDetail?.SchemaVersion}
                    CreatedAt       : {payload.Notification?.ConsentDetail?.CreatedAt}

                    Raw Response:
                    {rawResponse}
                    =========================================================";

            await System.IO.File.AppendAllTextAsync(filename, log);
            return Ok();
        }
        [HttpPost("~/api/v3/links/context/on-notify")]
        public async Task<IActionResult> OnNotify([FromBody] OnNotifyResponse payload)
        {
            string path = _configuration["ExceptionLogging:Directory"].ToString().Trim('\\') + "\\M2Callback";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string filename = $"{path}\\{AppTime.Now:dd_MM_yyyy}.txt";
            // Convert complete payload object to JSON
            string rawResponse = JsonConvert.SerializeObject(payload, Formatting.Indented);

            string log = $@"
                    =========================================================
                    Raw Response:
                    {rawResponse}
                    =========================================================";

            await System.IO.File.AppendAllTextAsync(filename, log);
            return Ok();
        }

        [HttpPost("~/api/v3/hip/patient/care-context/discover")]
        public async Task<IActionResult> onCareContextDiscoverRequest([FromBody] CareContextDiscoverRequest payload)
        {
            string path = _configuration["ExceptionLogging:Directory"].ToString().Trim('\\') + "\\M2Callback";
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            string filename = $"{path}\\{AppTime.Now:dd_MM_yyyy}.txt";
            // Convert complete payload object to JSON
            string rawResponse = JsonConvert.SerializeObject(payload, Formatting.Indented);

            string log = $@"
                    =========================================================
                    Raw Response:
                    {rawResponse}
                    =========================================================";

            await System.IO.File.AppendAllTextAsync(filename, log);
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
