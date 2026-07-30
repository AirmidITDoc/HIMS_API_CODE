using Asp.Versioning;
using DocumentFormat.OpenXml.Office2016.Drawing.ChartDrawing;
using HIMS.ABHA.Helper;
using HIMS.ABHA.Interface;
using HIMS.ABHA.Models.M2;
using HIMS.Api.Controllers;
using HIMS.Api.Models.Common;
using HIMS.API.Extensions;
using HIMS.API.Models.Masters;
using HIMS.Core;
using HIMS.Core.Domain.Grid;
using HIMS.Data;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static QRCoder.PayloadGenerator;

namespace HIMS.API.Controllers.ABHA.M2
{
    [Route("api/v{version:apiVersion}/m2/hip-linking")]
    [ApiController]
    [ApiVersion("1")]
    public class HipLinkingController : BaseController
    {
        private readonly IHipLinkingService _abhaService;
        private readonly IGenericService<TAbhaLinkTokenCallback> _TAbhaLinkTokenCallback;
        public HipLinkingController(IHipLinkingService abhaService, IGenericService<TAbhaLinkTokenCallback> TAbhaLinkTokenCallback)
        {
            _abhaService = abhaService;
            _TAbhaLinkTokenCallback = TAbhaLinkTokenCallback;
        }
        [HttpPost("token/generate")]
        public async Task<ApiResponse> GenerateLinkToken([FromBody] LinkTokenRequest req)
        {
            var result = await _abhaService.GenerateLinkTokenAsync(req);
            if (result.Success)
            {
                var objAbhaLinkToken = new TAbhaLinkTokenCallback
                {
                    AbhaAddress = req.AbhaAddress,
                    AbhaNumber = req.AbhaNumber.ToString(),
                    Name = req.Name,
                    YearOfBirth = req.YearOfBirth.ToString(),
                    RequestOn = DateTime.Now
                };

                await _TAbhaLinkTokenCallback.Add(objAbhaLinkToken, 1, "System");
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "Service found.", null);
            }
            else
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status200OK, "", new { TxnId = "", Message = AbhaHelper.GetErrorMessage(result.Error) });
        }

        [HttpGet("{id?}")]
        public async Task<ApiResponse> Get(string id, string abhaAddress)
        {
            if (id == "")
            {
                return ApiResponseHelper.GenerateResponse(ApiStatusCode.Status400BadRequest, "No data found.");
            }
            var data = await _TAbhaLinkTokenCallback.GetById(x => x.AbhaNumber == id.ToString() && x.AbhaAddress == abhaAddress);
            return data.ToSingleResponse<TAbhaLinkTokenCallback, LinkTokenResponseModel>("LinkTokenResponseModel");
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
