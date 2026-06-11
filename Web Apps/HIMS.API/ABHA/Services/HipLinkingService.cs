using HIMS.API.ABHA.Configuration;
using HIMS.API.ABHA.Helper;
using HIMS.API.ABHA.Interface;
using HIMS.API.ABHA.Models;
using HIMS.API.ABHA.Models.M2;
using Microsoft.Extensions.Options;

namespace HIMS.API.ABHA.Services
{
    public class HipLinkingService : IHipLinkingService
    {
        private readonly AbdmHttpClient _client;
        private readonly IAbdmTokenService _tokenService;
        private readonly ABDMSettings _settings;
        private readonly ILogger<AbhaService> _logger;

        public HipLinkingService(AbdmHttpClient client, IAbdmTokenService tokenService, IOptions<ABDMSettings> options, ILogger<AbhaService> logger)
        {
            _client = client;
            _tokenService = tokenService;
            _settings = options.Value;
            _logger = logger;
        }

        public async Task<ApiResult<LinkTokenResponse>> GenerateLinkTokenAsync(LinkTokenRequest request, string hipId, string xCmId)
        {
            try
            {
                var url = $"{_settings.BaseUrls.GatewayBaseUrl}{_settings.Endpoints.LinkTokenGenerate}";
                return await _client.PostAsync<LinkTokenResponse>(url, request, new() { ["X-HIP-ID"] = hipId, ["X-CM-ID"] = xCmId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RequestOtpAsync failed");
                return new ApiResult<LinkTokenResponse> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<string>> LinkCareContextAsync(LinkCareContextRequest request, string hipId, string linkToken, string xCmId)
        {
            try
            {
                var url = $"{_settings.BaseUrls.GatewayBaseUrl}{_settings.Endpoints.InitLinkSingle}";
                return await _client.PostAsync<string>(url, request, new() { ["X-HIP-ID"] = hipId, ["X-CM-ID"] = xCmId, ["X-LINK-TOKEN"] = linkToken });
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
                var url = $"{_settings.BaseUrls.GatewayBaseUrl}{_settings.Endpoints.LinkNotify}";
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
