using HIMS.ABHA.Configuration;
using HIMS.ABHA.Helper;
using HIMS.ABHA.Interface;
using HIMS.ABHA.Models;
using HIMS.ABHA.Models.M2;
using Microsoft.Extensions.Logging;

namespace HIMS.ABHA.Services
{
    public class HipLinkingService : IHipLinkingService
    {
        private readonly AbdmHttpClient _client;
        private readonly ILogger<AbhaService> _logger;

        public HipLinkingService(AbdmHttpClient client,  ILogger<AbhaService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<ApiResult<LinkTokenResponse>> GenerateLinkTokenAsync(LinkTokenRequest request)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.LinkTokenGenerate}";
                return await _client.PostAsync<LinkTokenResponse>(url, request, new() { ["X-HIP-ID"] = request.HipId, ["X-CM-ID"] = request.XCmId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RequestOtpAsync failed");
                return new ApiResult<LinkTokenResponse> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<string>> LinkCareContextAsync(LinkCareContextRequest request)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.InitLinkSingle}";
                return await _client.PostAsync<string>(url, request, new() { ["X-HIP-ID"] = request.HipId, ["X-CM-ID"] = request.XCmId, ["X-LINK-TOKEN"] = request.LinkToken });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RequestOtpAsync failed");
                return new ApiResult<string> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<string>> LinkContextNotifyAsync(object request, string hipId, string linkToken, string xCmId)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.LinkNotify}";
                return await _client.PostAsync<string>(url, request, new() { ["X-HIP-ID"] = hipId, ["X-CM-ID"] = xCmId, ["X-LINK-TOKEN"] = linkToken });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RequestOtpAsync failed");
                return new ApiResult<string> { Success = false, Error = ex.Message };
            }
        }
    }
}
