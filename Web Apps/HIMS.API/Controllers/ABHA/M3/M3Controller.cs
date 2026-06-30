using Asp.Versioning;
using HIMS.ABHA.Interface;
using HIMS.ABHA.Models.M2;
using HIMS.ABHA.Models.M3;
using HIMS.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.ABHA.M3
{
    [Route("api/v{version:apiVersion}/m3")]
    [ApiController]
    [ApiVersion("1")]
    public class M3Controller : BaseController
    {
        private readonly IM3Service _m3Service;

        public M3Controller(IM3Service m3Service)
        {
            _m3Service = m3Service;
        }
        [HttpPatch("bridge/url")]
        public async Task<IActionResult> UpdateBridgeUrl([FromBody] UpdateBridgeUrlRequest request, [FromHeader(Name = "X-CM-ID")] string cmId = "sbx")
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Url))
                return BadRequest("Url is required.");
            var result = await _m3Service.UpdateBridgeUrlAsync(request, cmId);
            return Ok(result);
        }
        [HttpGet("bridge-service/serviceId/{serviceId}")]
        public async Task<IActionResult> FindBridgeServiceByServiceId(string serviceId, [FromHeader(Name = "X-CM-ID")] string cmId = "sbx")
        {
            if (string.IsNullOrWhiteSpace(serviceId))
                return BadRequest("serviceId is required.");
            var result = await _m3Service.FindBridgeServiceByServiceIdAsync(serviceId, cmId);
            return Ok(result);
        }
        [HttpGet("bridge-services")]
        public async Task<IActionResult> FindServicesByBridgeId([FromHeader(Name = "X-CM-ID")] string cmId = "sbx")
        {
            var result = await _m3Service.FindServicesByBridgeIdAsync(cmId);
            return Ok(result);
        }

        [HttpGet("certs")]
        public async Task<IActionResult> GetCerts([FromHeader(Name = "X-CM-ID")] string cmId = "sbx")
        {
            var result = await _m3Service.GetCertsAsync(cmId);
            return Ok(result);
        }

        [HttpGet("openid-configuration")]
        public async Task<IActionResult> GetOpenIdConfiguration([FromHeader(Name = "X-CM-ID")] string cmId = "sbx")
        {
            var result = await _m3Service.GetOpenIdConfigurationAsync(cmId);
            return Ok(result);
        }
        [HttpPost("consent/request/init")]
        public async Task<IActionResult> ConsentInitRequestAsync([FromBody] ConsentInitRequest request, [FromHeader(Name = "X-CM-ID")] string cmId = "sbx")
        {
            if (request?.Consent == null)
                return BadRequest("consent payload is required.");
            var result = await _m3Service.ConsentInitRequestAsync(request, cmId);
            return Accepted(result);
        }
        [HttpPost("consent/request/status")]
        public async Task<IActionResult> ConsentRequestStatus([FromBody] ConsentRequestStatusRequest request, [FromHeader(Name = "X-CM-ID")] string cmId = "sbx", [FromHeader(Name = "X-HIU-ID")] string hiuId = null)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.ConsentRequestId))
                return BadRequest("consentRequestId is required.");
            var result = await _m3Service.ConsentRequestStatusAsync(request, cmId, hiuId);
            return Accepted(result);
        }
        [HttpPost("consent/request/hiu/on-notify")]
        public async Task<IActionResult> ConsentHiuOnNotify([FromBody] ConsentHiuOnNotifyRequest request, [FromHeader(Name = "X-CM-ID")] string cmId = "sbx")
        {
            if (request == null)
                return BadRequest("Request body is required.");
            var result = await _m3Service.ConsentHiuOnNotifyAsync(request, cmId);
            return Ok(result);
        }
        [HttpPost("consent/fetch")]
        public async Task<IActionResult> ConsentFetch([FromBody] ConsentFetchRequest request, [FromHeader(Name = "X-CM-ID")] string cmId = "sbx", [FromHeader(Name = "X-HIU-ID")] string hiuId = null)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.ConsentId))
                return BadRequest("consentId is required.");
            var result = await _m3Service.ConsentFetchAsync(request, cmId, hiuId);
            return Accepted(result);
        }
        [HttpPost("health-information/request")]
        public async Task<IActionResult> HiuHealthInformationRequest([FromBody] HiuHealthInformationRequest request, [FromHeader(Name = "X-CM-ID")] string cmId = "sbx", [FromHeader(Name = "X-HIU-ID")] string hiuId = null)
        {
            if (request?.HiRequest == null)
                return BadRequest("hiRequest payload is required.");
            var result = await _m3Service.HiuHealthInformationRequestAsync(request, cmId, hiuId);
            return Accepted(result);
        }
        [HttpPost("health-information/notify")]
        public async Task<IActionResult> DataFlowNotification([FromBody] DataFlowNotificationRequest request, [FromHeader(Name = "X-CM-ID")] string cmId = "sbx")
        {
            if (request?.Notification == null)
                return BadRequest("notification payload is required.");
            var result = await _m3Service.DataFlowNotificationAsync(request, cmId);
            return Ok(result);
        }
    }
}