using HIMS.ABHA.Configuration;
using HIMS.ABHA.Helper;
using HIMS.ABHA.Interface;
using HIMS.ABHA.Models;
using HIMS.ABHA.Models.M2;
using HIMS.ABHA.Models.M3;
using Microsoft.Extensions.Logging;

namespace HIMS.ABHA.Services
{
    public class M3Service : IM3Service
    {

        private readonly AbdmHttpClient _client;
        private readonly IAbdmTokenService _tokenService;
        private readonly ILogger<AbhaService> _logger;
        public M3Service(AbdmHttpClient client, IAbdmTokenService tokenService, ILogger<AbhaService> logger)
        {
            _client = client;
            _tokenService = tokenService;
            _logger = logger;
        }
        public async Task<ApiResult<string>> UpdateBridgeUrlAsync(UpdateBridgeUrlRequest request, string cmId)
        {
            try
            {
                var publicKey = await _tokenService.GetPublicCertificateAsync();
                var encryptedAadhaar = RsaEncryptionHelper.Encrypt(request.Url, publicKey);
                request.Url = encryptedAadhaar;
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.BridgeUrl}";
                return await _client.PatchAsync<string>(url, request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "UpdateBridgeUrlAsync failed");
                return new ApiResult<string> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<BridgeServiceDto>> FindBridgeServiceByServiceIdAsync(string serviceId, string cmId)
        {
            try
            {
                var publicKey = await _tokenService.GetPublicCertificateAsync();
                var encryptedAadhaar = RsaEncryptionHelper.Encrypt(serviceId, publicKey);
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.BridgeServiceFindById}/{encryptedAadhaar}";
                return await _client.GetAsync<BridgeServiceDto>(url: url, CmId: cmId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FindBridgeServiceByServiceIdAsync failed");
                return new ApiResult<BridgeServiceDto> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<BridgeResponseDto>> FindServicesByBridgeIdAsync(string cmId)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.Bridges}";
                return await _client.GetAsync<BridgeResponseDto>(url: url, CmId: cmId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "FindServicesByBridgeIdAsync failed");
                return new ApiResult<BridgeResponseDto> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<string>> GetCertsAsync(string cmId)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.M3Certificate}";
                return await _client.GetAsync<string>(url: url, CmId: cmId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetCertsAsync failed");
                return new ApiResult<string> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<string>> GetOpenIdConfigurationAsync(string cmId)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.M3OpenIdConfig}";
                return await _client.GetAsync<string>(url: url, CmId: cmId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetOpenIdConfigurationAsync failed");
                return new ApiResult<string> { Success = false, Error = ex.Message };
            }
        }
        public async Task<ApiResult<string>> ConsentInitRequestAsync(ConsentInitRequest request, string cmId)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.M3ConsentInit}";
                return await _client.PostAsync<string>(url, request, new() { ["X-CM-ID"] = cmId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ConsentInitRequestAsync failed");
                return new ApiResult<string> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<string>> ConsentRequestStatusAsync(ConsentRequestStatusRequest request, string cmId, string hiuId)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.M3ConsentStatus}";
                return await _client.PostAsync<string>(url, request, new() { ["X-CM-ID"] = cmId, ["X-HIU-ID"] = hiuId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ConsentRequestStatusAsync failed");
                return new ApiResult<string> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<string>> ConsentHiuOnNotifyAsync(ConsentHiuOnNotifyRequest request, string cmId)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.M3ConsentNotify}";
                return await _client.PostAsync<string>(url, request, new() { ["X-CM-ID"] = cmId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ConsentHiuOnNotifyAsync failed");
                return new ApiResult<string> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<string>> ConsentFetchAsync(ConsentFetchRequest request, string cmId, string hiuId)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.M3ConsentFetch}";
                return await _client.PostAsync<string>(url, request, new() { ["X-CM-ID"] = cmId, ["X-HIU-ID"] = hiuId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ConsentFetchAsync failed");
                return new ApiResult<string> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<string>> HiuHealthInformationRequestAsync(HiuHealthInformationRequest request, string cmId, string hiuId)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.M3HealthRequest}";
                return await _client.PostAsync<string>(url, request, new() { ["X-CM-ID"] = cmId, ["X-HIU-ID"] = hiuId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "HiuHealthInformationRequestAsync failed");
                return new ApiResult<string> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<string>> DataFlowNotificationAsync(DataFlowNotificationRequest request, string cmId)
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.M3HealthNotify}";
                return await _client.PostAsync<string>(url, request, new() { ["X-CM-ID"] = cmId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DataFlowNotificationAsync failed");
                return new ApiResult<string> { Success = false, Error = ex.Message };
            }
        }

    }
}
