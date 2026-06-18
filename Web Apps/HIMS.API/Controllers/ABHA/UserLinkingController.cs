using Asp.Versioning;
using HIMS.ABHA.Helper;
using HIMS.ABHA.Interface;
using HIMS.ABHA.Models.M2;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.ABHA
{
    [Route("api/v{version:apiVersion}/m2/user-linking")]
    [ApiController]
    [ApiVersion("1")]
    public class UserLinkingController : BaseController
    {
        private readonly IUserLinkingService _abhaService;

        public UserLinkingController(IUserLinkingService abhaService)
        {
            _abhaService = abhaService;
        }
        [HttpPost("on-discover")]
        public async Task<ApiResponse> OnDiscover([FromBody] OnDiscoverRequest req)
        {
            var result = await _abhaService.OnDiscoverAsync(req);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service found.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }

        [HttpPost("link/on-init")]
        public async Task<ApiResponse> OnLinkInit([FromBody] LinkOnInitRequest req,
            [FromHeader(Name = "X-CM-ID")] string xCmId)
        {
            var result = await _abhaService.OnLinkInitAsync(req);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service found.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }

        [HttpPost("link/on-confirm")]
        public async Task<ApiResponse> OnLinkConfirm([FromBody] LinkOnConfirmRequest req,
            [FromHeader(Name = "X-CM-ID")] string xCmId)
        {
            var result = await _abhaService.OnLinkConfirmAsync(req);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service found.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }
    }
}
