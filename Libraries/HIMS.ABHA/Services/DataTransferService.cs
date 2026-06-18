using HIMS.ABHA.Configuration;
using HIMS.ABHA.Helper;
using HIMS.ABHA.Interface;
using HIMS.ABHA.Models;
using HIMS.ABHA.Models.M2;
using Microsoft.Extensions.Logging;

namespace HIMS.ABHA.Services
{
    public class DataTransferService : IDataTransferService
    {
        private readonly AbdmHttpClient _client;
        private readonly ILogger<AbhaService> _logger;

        public DataTransferService(AbdmHttpClient client,  ILogger<AbhaService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<ApiResult<HttpResponseMessage>> ConsentHipOnNotifyAsync(ConsentHipOnNotifyRequest req)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.ConsentHIPNotifyUrl}";
                return await _client.PostAsync<HttpResponseMessage>(url, req, new() { ["X-CM-ID"] = req.XCmId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ConsentHipOnNotifyAsync failed");
                return new ApiResult<HttpResponseMessage> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<HttpResponseMessage>> HipHiOnRequestAsync(HipHiOnRequestRequest req)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.HipHiOnRequestUrl}";
                return await _client.PostAsync<HttpResponseMessage>(url, req, new() { ["X-CM-ID"] = req.XCmId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "HipHiOnRequestAsync failed");
                return new ApiResult<HttpResponseMessage> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<HttpResponseMessage>> HiFlowNotifyAsync(HiFlowNotifyRequest req)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.HiFlowNotifyUrl}";
                return await _client.PostAsync<HttpResponseMessage>(url, req, new() { ["X-CM-ID"] = req.XCmId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "HiFlowNotifyAsync failed");
                return new ApiResult<HttpResponseMessage> { Success = false, Error = ex.Message };
            }
        }
    }
}
