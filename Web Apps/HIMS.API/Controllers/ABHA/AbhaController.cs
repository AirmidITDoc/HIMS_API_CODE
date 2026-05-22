using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.ABHA.Helper;
using HIMS.API.ABHA.Interface;
using HIMS.API.ABHA.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace HIMS.API.Controllers.ABHA
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

        // ===== Mobile flow =====
        [HttpPost("mobile/request-otp")]
        public async Task<ApiResponse> RequestMobileOtp([FromBody] MobileOtpDto dto)
        {
            var result = await _abhaService.RequestMobileOtpAsync(dto.TxnId, dto.Mobile);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Otp Sent successfully.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }

        [HttpPost("mobile/verify-otp")]
        public async Task<ApiResponse> VerifyMobileOtp([FromBody] VerifyOtpDto dto)
        {
            var result = await _abhaService.VerifyMobileOtpAsync(dto.TxnId, dto.Otp);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Otp Sent successfully.", result.Data);
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
        [HttpGet("profile")]
        public async Task<ApiResponse> GetProfile([FromHeader(Name = "X-Token")] string xToken)
        {
            var result = await _abhaService.GetAbhaProfileAsync(xToken);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Profile retrieved successfully.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }

        [HttpGet("card")]
        public async Task<ApiResponse> GetCard([FromHeader(Name = "X-Token")] string xToken)
        {
            var result = await _abhaService.GetAbhaCardAsync(xToken);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Card retrieved successfully.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }

        [HttpGet("qr")]
        public async Task<ApiResponse> GetQr([FromHeader(Name = "X-Token")] string xToken)
        {
            var result = await _abhaService.GetAbhaQrCodeAsync(xToken);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "QR code retrieved successfully.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }
    }
}
