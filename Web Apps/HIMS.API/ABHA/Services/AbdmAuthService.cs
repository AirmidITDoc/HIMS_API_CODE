using HIMS.API.ABHA.Configuration;
using HIMS.API.ABHA.Helper;
using HIMS.API.ABHA.Interface;
using HIMS.API.ABHA.Models;
using HIMS.API.ABHA.Models.M2;
using Microsoft.Extensions.Options;

namespace HIMS.API.ABHA.Services
{
    public class AbdmAuthService : IAbdmAuthService
    {
        private readonly AbdmHttpClient _client;
        private readonly IAbdmTokenService _tokenService;
        private readonly ABDMSettings _settings;
        private readonly ILogger<AbhaService> _logger;
        public AbdmAuthService(AbdmHttpClient client, IAbdmTokenService tokenService, IOptions<ABDMSettings> options, ILogger<AbhaService> logger)
        {
            _client = client;
            _tokenService = tokenService;
            _settings = options.Value;
            _logger = logger;
        }
        public async Task<ApiResult<string>> UpdateBridgeUrlAsync(UpdateBridgeUrlRequest request)
        {
            try
            {
                var publicKey = await _tokenService.GetPublicCertificateAsync();
                var encryptedAadhaar = RsaEncryptionHelper.Encrypt(request.Url, publicKey);
                request.Url = encryptedAadhaar;
                var url = $"{_settings.BaseUrls.GatewayBaseUrl}{_settings.Endpoints.BridgeUrl}";
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
                var url = $"{_settings.BaseUrls.BridgeBaseUrl}{_settings.Endpoints.RegisterBridge}";
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
                var url = $"{_settings.BaseUrls.GatewayBaseUrl}{_settings.Endpoints.BridgeServiceFindById}/{encryptedAadhaar}";
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
                var url = $"{_settings.BaseUrls.GatewayBaseUrl}{_settings.Endpoints.Bridges}";
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