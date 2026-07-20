using HIMS.ABHA.Configuration;
using HIMS.ABHA.Helper;
using HIMS.ABHA.Interface;
using HIMS.ABHA.Models;
using HIMS.ABHA.Models.M2;
using Microsoft.Extensions.Logging;

namespace HIMS.ABHA.Services
{
    public class AbdmAuthService : IAbdmAuthService
    {
        private readonly AbdmHttpClient _client;
        private readonly IAbdmTokenService _tokenService;
        private readonly ILogger<AbhaService> _logger;
        public AbdmAuthService(AbdmHttpClient client, IAbdmTokenService tokenService, ILogger<AbhaService> logger)
        {
            _client = client;
            _tokenService = tokenService;
            _logger = logger;
        }
        public async Task<ApiResult<string>> UpdateBridgeUrlAsync(UpdateBridgeUrlRequest request)
        {
            try
            {
                var publicKey = await _tokenService.GetPublicCertificateAsync();
                //var encryptedAadhaar = RsaEncryptionHelper.Encrypt(request.Url, publicKey);
                //request.Url = encryptedAadhaar;
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.BridgeUrl}";
                return await _client.PatchAsync<string>(url, request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RequestOtpAsync failed");
                return new ApiResult<string> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<string>> RegisterBridgeServicesAsync(RegisterBridgeRequest request)
        {
            try
            {
                var publicKey = await _tokenService.GetPublicCertificateAsync();
                var encryptedAadhaar = RsaEncryptionHelper.Encrypt(request.HRP[0].BridgeId, publicKey);
                request.HRP[0].BridgeId = encryptedAadhaar;
                var url = $"{AppSettings.Current.BaseUrls.BridgeBaseUrl}{AppSettings.Current.Endpoints.RegisterBridge}";
                return await _client.PostAsync<string>(url, request);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RequestOtpAsync failed");
                return new ApiResult<string> { Success = false, Error = ex.Message };
            }
        }
        public async Task<ApiResult<BridgeServiceDto>> FindServiceByServiceIdAsync(string serviceId)
        {
            try
            {
                var publicKey = await _tokenService.GetPublicCertificateAsync();
                var encryptedAadhaar = RsaEncryptionHelper.Encrypt(serviceId, publicKey);
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.BridgeServiceFindById}/{encryptedAadhaar}";
                return await _client.GetAsync<BridgeServiceDto>(url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RequestOtpAsync failed");
                return new ApiResult<BridgeServiceDto> { Success = false, Error = ex.Message };
            }
        }
        public async Task<ApiResult<BridgeResponseDto>> FindServicesByBridgeIdAsync()
        {
            try
            {
                var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.Bridges}";
                return await _client.GetAsync<BridgeResponseDto>(url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RequestOtpAsync failed");
                return new ApiResult<BridgeResponseDto> { Success = false, Error = ex.Message };
            }
        }
    }
}