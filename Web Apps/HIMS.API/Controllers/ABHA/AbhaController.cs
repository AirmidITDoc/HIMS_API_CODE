using Asp.Versioning;
using HIMS.Api.Controllers;
using HIMS.API.ABHA.Interface;
using HIMS.API.ABHA.Models;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<IActionResult> RequestAadhaarOtp([FromBody] AadhaarOtpDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.AadhaarNumber) || dto.AadhaarNumber.Length != 12)
                return BadRequest(new { error = "Valid 12-digit Aadhaar number required" });

            var result = await _abhaService.RequestAadhaarOtpAsync(dto.AadhaarNumber);
            return result.Success ? Ok(result.Data) : StatusCode(result.StatusCode == 0 ? 500 : result.StatusCode, result);
        }

        [HttpPost("aadhaar/verify-otp")]
        public async Task<IActionResult> VerifyAadhaarOtp([FromBody] VerifyOtpDto dto)
        {
            var result = await _abhaService.VerifyAadhaarOtpAsync(dto.TxnId, dto.Otp, dto.Mobile);
            return result.Success ? Ok(result.Data) : StatusCode(result.StatusCode == 0 ? 500 : result.StatusCode, result);
        }

        // ===== Mobile flow =====
        [HttpPost("mobile/request-otp")]
        public async Task<IActionResult> RequestMobileOtp([FromBody] MobileOtpDto dto)
        {
            var result = await _abhaService.RequestMobileOtpAsync(dto.TxnId, dto.Mobile);
            return result.Success ? Ok(result.Data) : StatusCode(result.StatusCode == 0 ? 500 : result.StatusCode, result);
        }

        [HttpPost("mobile/verify-otp")]
        public async Task<IActionResult> VerifyMobileOtp([FromBody] VerifyOtpDto dto)
        {
            var result = await _abhaService.VerifyMobileOtpAsync(dto.TxnId, dto.Otp);
            return result.Success ? Ok(result.Data) : StatusCode(result.StatusCode == 0 ? 500 : result.StatusCode, result);
        }

        // ===== ABHA address =====
        [HttpGet("address/suggestions/{txnId}")]
        public async Task<IActionResult> GetSuggestions(string txnId)
        {
            var result = await _abhaService.GetAbhaAddressSuggestionsAsync(txnId);
            return result.Success ? Ok(result.Data) : StatusCode(result.StatusCode == 0 ? 500 : result.StatusCode, result);
        }

        [HttpPost("address/create")]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressDto dto)
        {
            var result = await _abhaService.CreateAbhaAddressAsync(dto.TxnId, dto.AbhaAddress);
            return result.Success ? Ok(result.Data) : StatusCode(result.StatusCode == 0 ? 500 : result.StatusCode, result);
        }

        // ===== Profile / Card / QR =====
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile([FromHeader(Name = "X-Token")] string xToken)
        {
            var result = await _abhaService.GetAbhaProfileAsync(xToken);
            return result.Success ? Ok(result.Data) : StatusCode(result.StatusCode == 0 ? 500 : result.StatusCode, result);
        }

        [HttpGet("card")]
        public async Task<IActionResult> GetCard([FromHeader(Name = "X-Token")] string xToken)
        {
            var result = await _abhaService.GetAbhaCardAsync(xToken);
            if (!result.Success) return StatusCode(result.StatusCode == 0 ? 500 : result.StatusCode, result);
            return File(result.Data!, "application/pdf", "abha-card.pdf");
        }

        [HttpGet("qr")]
        public async Task<IActionResult> GetQr([FromHeader(Name = "X-Token")] string xToken)
        {
            var result = await _abhaService.GetAbhaQrCodeAsync(xToken);
            if (!result.Success) return StatusCode(result.StatusCode == 0 ? 500 : result.StatusCode, result);
            return File(result.Data!, "image/png", "abha-qr.png");
        }
    }
}
