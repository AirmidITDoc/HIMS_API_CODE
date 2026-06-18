using Asp.Versioning;
using HIMS.ABHA.Helper;
using HIMS.ABHA.Interface;
using HIMS.ABHA.Models.M2;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace HIMS.API.Controllers.ABHA
{
    [Route("api/v{version:apiVersion}/m2/data-transfer")]
    [ApiController]
    [ApiVersion("1")]
    public class DataTransferController : BaseController
    {
        private readonly IDataTransferService _abhaService;

        public DataTransferController(IDataTransferService abhaService)
        {
            _abhaService = abhaService;
        }
        [HttpPost("consent/hip/on-notify")]
        public async Task<ApiResponse> ConsentHipOnNotify([FromBody] ConsentHipOnNotifyRequest req)
        {
            var result = await _abhaService.ConsentHipOnNotifyAsync(req);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service found.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });

        }

        [HttpPost("hip/hi/on-request")]
        public async Task<ApiResponse> HipHiOnRequest([FromBody] HipHiOnRequestRequest req)
        {
            var result = await _abhaService.HipHiOnRequestAsync(req);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service found.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }
        [HttpPost("hi/notify")]
        public async Task<ApiResponse> HiFlowNotify([FromBody] HiFlowNotifyRequest req)
        {
            var result = await _abhaService.HiFlowNotifyAsync(req);
            if (result.Success)
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service found.", result.Data);
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }
    }
}
