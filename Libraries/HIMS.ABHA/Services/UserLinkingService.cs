using HIMS.ABHA.Configuration;
using HIMS.ABHA.Helper;
using HIMS.ABHA.Interface;
using HIMS.ABHA.Models;
using HIMS.ABHA.Models.M2;
using Microsoft.Extensions.Logging;

namespace HIMS.ABHA.Services
{
    public class UserLinkingService : IUserLinkingService
    {
        private readonly AbdmHttpClient _client;
        private readonly ILogger<AbhaService> _logger;

        public UserLinkingService(AbdmHttpClient client,  ILogger<AbhaService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<ApiResult<HttpResponseMessage>> OnDiscoverAsync(OnDiscoverRequest req)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.DiscoverReqUrl}";
                return await _client.PostAsync<HttpResponseMessage>(url, req, new() { ["X-CM-ID"] = req.XCmId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OnDiscoverAsync failed");
                return new ApiResult<HttpResponseMessage> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<HttpResponseMessage>> OnLinkInitAsync(LinkOnInitRequest req)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.UserInitLinking}";
                return await _client.PostAsync<HttpResponseMessage>(url, req, new() { ["X-CM-ID"] = req.XCmId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OnLinkInitAsync failed");
                return new ApiResult<HttpResponseMessage> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<HttpResponseMessage>> OnLinkConfirmAsync(LinkOnConfirmRequest req)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.UserConfirmLinking}";
                return await _client.PostAsync<HttpResponseMessage>(url, req, new() { ["X-CM-ID"] = req.XCmId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "OnLinkConfirmAsync failed");
                return new ApiResult<HttpResponseMessage> { Success = false, Error = ex.Message };
            }
        }
    }
}
