using Asp.Versioning;
using HIMS.ABHA.Helper;
using HIMS.ABHA.Interface;
using HIMS.ABHA.Models.M2;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.ABHA
{
    [Route("api/v{version:apiVersion}/m2/hip-linking")]
    [ApiController]
    [ApiVersion("1")]
    public class HipLinkingController : BaseController
    {
        private readonly IHipLinkingService _abhaService;

        public HipLinkingController(IHipLinkingService abhaService)
        {
            _abhaService = abhaService;
        }
        [HttpPost("token/generate")]
        public async Task<ApiResponse> GenerateLinkToken([FromBody] LinkTokenRequest req)
        {
            var result = await _abhaService.GenerateLinkTokenAsync(req);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service found.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }

        [HttpPost("link/carecontext")]
        public async Task<ApiResponse> LinkCareContext([FromBody] LinkCareContextRequest req)
        {
            var result = await _abhaService.LinkCareContextAsync(req);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service found.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }

        [HttpPost("link/context/notify")]
        public async Task<ApiResponse> LinkContextNotify([FromBody] object req, string hipId, string linkToken, string xCmId)
        {
            var result = await _abhaService.LinkContextNotifyAsync(req, hipId, linkToken, xCmId);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service found.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }
    }
}
