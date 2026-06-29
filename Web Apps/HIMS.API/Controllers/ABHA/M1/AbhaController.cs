using Asp.Versioning;
using HIMS.ABHA.Helper;
using HIMS.ABHA.Interface;
using HIMS.ABHA.Models;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.ABHA.M1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1")]
    public class AbhaController : BaseController
    {
        private readonly IAbhaService _abhaService;

        public AbhaController(IAbhaService abhaService)
        {
            _abhaService = abhaService;
        }

        // ===== Aadhaar flow =====
        [HttpPost("aadhaar/request-otp")]
        public async Task<ApiResponse> RequestAadhaarOtp([FromBody] AadhaarOtpDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.AadhaarNumber) || dto.AadhaarNumber.Length != 12)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "Valid 12-digit Aadhaar number required");

            var result = await _abhaService.RequestAadhaarOtpAsync(dto.AadhaarNumber);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Otp Sent successfully.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }

        [HttpPost("aadhaar/verify-otp")]
        public async Task<ApiResponse> VerifyAadhaarOtp([FromBody] VerifyOtpDto dto)
        {
            var result = await _abhaService.VerifyAadhaarOtpAsync(dto.TxnId, dto.Otp, dto.Mobile);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Otp Sent successfully.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }

        // ===== Existing ABHA =====
        [HttpPost("existing/request-abha-otp")]
        public async Task<ApiResponse> RequestAbhaOtp([FromBody] AadhaarOtpDto dto)
        {
            List<string> scope = new();
            string otpsystem;
            string loginhint;
            if (dto.OtpType == 1)
            {
                scope.Add("abha-login");
                scope.Add("aadhaar-verify");
                loginhint = "abha-number";
                otpsystem = "aadhaar";
            }
            else if (dto.OtpType == 2)
            {
                scope.Add("abha-login");
                scope.Add("mobile-verify");
                loginhint = "abha-number";
                otpsystem = "abdm";
            }
            else if (dto.OtpType == 3)
            {
                scope.Add("abha-address-login");
                scope.Add("aadhaar-verify");
                loginhint = "abha-address";
                otpsystem = "aadhaar";
            }
            else
            {
                scope.Add("abha-address-login");
                scope.Add("mobile-verify");
                loginhint = "abha-address";
                otpsystem = "abdm";
            }

            var result = await _abhaService.RequestOtpAsync(dto.AadhaarNumber, scope, loginhint, otpsystem);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Otp Sent successfully.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }
        [HttpPost("existing/request-aadhar-otp")]
        public async Task<ApiResponse> RequestOtp([FromBody] AadhaarOtpDto dto)
        {
            var result = await _abhaService.RequestOtpAsync(dto.AadhaarNumber, new List<string> { "abha-login", "aadhaar-verify" }, "aadhaar", "aadhaar");
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Otp Sent successfully.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }
        [HttpPost("existing/mobile-otp")]
        public async Task<ApiResponse> RequestMobileOtp([FromBody] AadhaarOtpDto dto)
        {
            var result = await _abhaService.RequestOtpAsync(dto.AadhaarNumber, new List<string> { "abha-login", "mobile-verify" }, "mobile", "abdm");
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Otp Sent successfully.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }


        [HttpPost("existing/verify-abha-otp")]
        public async Task<ApiResponse> VerifyAbhaOtpOtp([FromBody] VerifyOtpDto dto)
        {
            List<string> scope = new();
            bool isAbhaAddress = false;
            if (dto.OtpType == 1)
            {
                scope.Add("abha-login");
                scope.Add("aadhaar-verify");
            }
            else if (dto.OtpType == 2)
            {
                scope.Add("abha-login");
                scope.Add("mobile-verify");
            }
            else if (dto.OtpType == 3)
            {
                scope.Add("abha-address-login");
                scope.Add("aadhaar-verify");
                isAbhaAddress = true;
            }
            else
            {
                scope.Add("abha-address-login");
                scope.Add("mobile-verify");
                isAbhaAddress = true;
            }
            var result = await _abhaService.VerifyOtpAsync(dto.TxnId, dto.Otp, scope, isAbhaAddress);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Otp Sent successfully.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }
        [HttpPost("existing/verify-aadhar-otp")]
        public async Task<ApiResponse> VerifyAadharOtpOtp([FromBody] VerifyOtpDto dto)
        {
            var result = await _abhaService.VerifyOtpAsync(dto.TxnId, dto.Otp, new List<string> { "abha-login", "aadhaar-verify" });
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Otp Sent successfully.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }
        [HttpPost("existing/verify-mobile-otp")]
        public async Task<ApiResponse> VerifyMobileOtp([FromBody] VerifyOtpDto dto)
        {
            var result = await _abhaService.VerifyOtpAsync(dto.TxnId, dto.Otp, new List<string> { "abha-login", "mobile-verify" });
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Otp Sent successfully.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }
        [HttpPost("existing/verify-user")]
        public async Task<ApiResponse> VerifyUser([FromBody] VerifyUserRequest obj)
        {
            var result = await _abhaService.VerifyUser(obj);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "User verified successfully.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }

        // ===== ABHA address =====
        [HttpGet("address/suggestions/{txnId}")]
        public async Task<ApiResponse> GetSuggestions(string txnId)
        {
            var result = await _abhaService.GetAbhaAddressSuggestionsAsync(txnId);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Suggestions retrieved successfully.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }

        [HttpPost("address/create")]
        public async Task<ApiResponse> CreateAddress([FromBody] CreateAddressDto dto)
        {
            var result = await _abhaService.CreateAbhaAddressAsync(dto.TxnId, dto.AbhaAddress);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Address created successfully.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }

        // ===== Profile / Card / QR =====
        [HttpPost("aadhaar/profile")]
        public async Task<ApiResponse> GetProfile([FromBody] ProfileRequestDto dto)
        {
            var result = await _abhaService.GetAbhaProfileAsync(dto.Token, dto.IsAddress);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Profile retrieved successfully.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }

        [HttpPost("aadhaar/card")]
        public async Task<IActionResult> GetCard([FromBody] ProfileRequestDto dto)
        {
            var result = await _abhaService.GetAbhaCardAsync(dto.Token);
            if (!result.Success)
            {
                return BadRequest(AbhaHelper.GetErrorMessage(result.Error));
            }
            return File(result.Data, "image/png", "abha-card.png");
            //if (result.Success)
            //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Card retrieved successfully.", Convert.ToBase64String(result.Data));
            //else
            //    return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }

        [HttpPost("aadhaar/qr")]
        public async Task<ApiResponse> GetQr([FromBody] ProfileRequestDto dto)
        {
            var result = await _abhaService.GetAbhaQrCodeAsync(dto.Token, dto.IsAddress);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "QR code retrieved successfully.", Convert.ToBase64String(result.Data));
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }
    }
}
