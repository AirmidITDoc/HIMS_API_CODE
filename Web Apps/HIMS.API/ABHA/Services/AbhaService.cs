using HIMS.API.ABHA.Configuration;
//using HIMS.API.ABHA.Headers;
using HIMS.API.ABHA.Helper;
using HIMS.API.ABHA.Interface;
using HIMS.API.ABHA.Models;
using HIMS.API.ABHA.Models.M2;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HIMS.API.ABHA.Services
{
    public class AbhaService : IAbhaService
    {
        private readonly HttpClient _httpClient;
        private readonly ABDMSettings _settings;
        private readonly IAbdmTokenService _tokenService;
        private readonly ILogger<AbhaService> _logger;

        private static readonly JsonSerializerOptions JsonOpts = new()
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
        public AbhaService(HttpClient httpClient, IOptions<ABDMSettings> options, IAbdmTokenService tokenService, ILogger<AbhaService> logger)
        {
            _httpClient = httpClient;
            _settings = options.Value;
            _tokenService = tokenService;
            _logger = logger;
        }

        #region M1 APIS
        // ===== AADHAAR FLOW =====
        public async Task<ApiResult<OtpResponse>> RequestAadhaarOtpAsync(string aadhaarNumber)
        {
            try
            {
                var publicKey = await _tokenService.GetPublicCertificateAsync();
                var encryptedAadhaar = RsaEncryptionHelper.Encrypt(aadhaarNumber, publicKey);

                var payload = new AadhaarOtpRequest
                {
                    TxnId = string.Empty, // first call has empty txnId
                    LoginId = encryptedAadhaar,
                    LoginHint = "aadhaar",
                    OtpSystem = "aadhaar",
                    Scope = new List<string> { "abha-enrol" }
                };

                var url = $"{_settings.BaseUrls.AbhaBaseUrl}{_settings.Endpoints.AadhaarOtpRequest}";
                return await PostAsync<OtpResponse>(url, payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RequestAadhaarOtpAsync failed");
                return new ApiResult<OtpResponse> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<AbhaEnrolmentResponse>> VerifyAadhaarOtpAsync(string txnId, string otp, string? mobile = null)
        {
            try
            {
                var publicKey = await _tokenService.GetPublicCertificateAsync();
                var encryptedOtp = RsaEncryptionHelper.Encrypt(otp, publicKey);

                var payload = new AadhaarOtpVerifyRequest
                {
                    AuthData = new AuthData
                    {
                        AuthMethods = new List<string> { "otp" },
                        Otp = new OtpData
                        {
                            TimeStamp = DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                            TxnId = txnId,
                            OtpValue = encryptedOtp,
                            Mobile = mobile
                        }
                    },
                    Consent = new Consent { Code = "abha-enrollment", Version = "1.4" }
                };

                var url = $"{_settings.BaseUrls.AbhaBaseUrl}{_settings.Endpoints.AadhaarOtpVerify}";
                return await PostAsync<AbhaEnrolmentResponse>(url, payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "VerifyAadhaarOtpAsync failed");
                return new ApiResult<AbhaEnrolmentResponse> { Success = false, Error = ex.Message };
            }
        }

        // ===== MOBILE FLOW =====
        public async Task<ApiResult<OtpResponse>> RequestOtpAsync(string aadharno, List<string> scope, string LoginHint, string OtpSystem)
        {
            try
            {
                //List<string> scope = isMobile ? new List<string> { "abha-login", "mobile-verify" } : new List<string> { "abha-login", "aadhaar-verify" };
                var publicKey = await _tokenService.GetPublicCertificateAsync();
                var encryptedAadhaar = RsaEncryptionHelper.Encrypt(aadharno, publicKey);

                var payload = new OtpRequest
                {
                    LoginId = encryptedAadhaar,
                    LoginHint = LoginHint,
                    OtpSystem = OtpSystem,
                    Scope = scope
                };

                var url = $"{_settings.BaseUrls.AbhaBaseUrl}{_settings.Endpoints.OtpRequest}";
                return await PostAsync<OtpResponse>(url, payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RequestOtpAsync failed");
                return new ApiResult<OtpResponse> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<VerifyOtpResponse>> VerifyOtpAsync(string txnId, string otp, List<string> scope)
        {
            try
            {
                var publicKey = await _tokenService.GetPublicCertificateAsync();
                var encryptedOtp = RsaEncryptionHelper.Encrypt(otp, publicKey);

                var payload = new
                {
                    scope,
                    authData = new
                    {
                        authMethods = new[] { "otp" },
                        otp = new
                        {
                            txnId,
                            otpValue = encryptedOtp
                        }
                    }
                };

                var url = $"{_settings.BaseUrls.AbhaBaseUrl}{_settings.Endpoints.OtpVerify}";
                return await PostAsync<VerifyOtpResponse>(url, payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "VerifyMobileOtpAsync failed");
                return new ApiResult<VerifyOtpResponse> { Success = false, Error = ex.Message };
            }
        }

        // ===== ABHA ADDRESS =====
        public async Task<ApiResult<AbhaAddressSuggestionResponse>> GetAbhaAddressSuggestionsAsync(string txnId)
        {
            try
            {
                var token = await _tokenService.GetAccessTokenAsync();
                var url = $"{_settings.BaseUrls.AbhaBaseUrl}{_settings.Endpoints.AbhaAddressSuggestion}";

                using var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                request.Headers.Add("REQUEST-ID", Guid.NewGuid().ToString());
                request.Headers.Add("TIMESTAMP", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
                request.Headers.Add("Transaction_Id", txnId);

                var response = await _httpClient.SendAsync(request);
                var body = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return new ApiResult<AbhaAddressSuggestionResponse>
                    { Success = false, Error = body, StatusCode = (int)response.StatusCode };

                var data = JsonSerializer.Deserialize<AbhaAddressSuggestionResponse>(body, JsonOpts);
                return new ApiResult<AbhaAddressSuggestionResponse>
                { Success = true, Data = data, StatusCode = (int)response.StatusCode };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "GetAbhaAddressSuggestionsAsync failed");
                return new ApiResult<AbhaAddressSuggestionResponse> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<AbhaEnrolmentResponse>> CreateAbhaAddressAsync(string txnId, string abhaAddress)
        {
            var payload = new CreateAbhaAddressRequest
            {
                TxnId = txnId,
                AbhaAddress = abhaAddress,
                Preferred = 1
            };

            var url = $"{_settings.BaseUrls.AbhaBaseUrl}{_settings.Endpoints.CreateAbhaAddress}";
            return await PostAsync<AbhaEnrolmentResponse>(url, payload);
        }

        // ===== PROFILE / CARD / QR =====
        public async Task<ApiResult<AbhaProfile>> GetAbhaProfileAsync(string xToken)
        {
            var url = $"{_settings.BaseUrls.AbhaBaseUrl}{_settings.Endpoints.AbhaProfile}";
            return await GetAsync<AbhaProfile>(url, xToken);
        }

        public async Task<ApiResult<byte[]>> GetAbhaCardAsync(string xToken)
        {
            var url = $"{_settings.BaseUrls.AbhaBaseUrl}{_settings.Endpoints.AbhaCard}";
            return await GetBinaryAsync(url, xToken);
        }

        public async Task<ApiResult<byte[]>> GetAbhaQrCodeAsync(string xToken)
        {
            var url = $"{_settings.BaseUrls.AbhaBaseUrl}{_settings.Endpoints.AbhaQrCode}";
            return await GetBinaryAsync(url, xToken);
        }
        #endregion
        #region M2 APIS
        public async Task<ApiResult<string>> UpdateBridgeUrlAsync(UpdateBridgeUrlRequest request)
        {
            try
            {
                var publicKey = await _tokenService.GetPublicCertificateAsync();
                var encryptedAadhaar = RsaEncryptionHelper.Encrypt(request.Url, publicKey);
                request.Url = encryptedAadhaar;
                var url = $"{_settings.BaseUrls.GatewayBaseUrl}{_settings.Endpoints.BridgeUrl}";
                return await PatchAsync<string>(url, request);
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
                return await PostAsync<string>(url, request);
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
                return await GetAsync<BridgeServiceDto>(url);
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
                return await GetAsync<BridgeResponseDto>(url);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RequestOtpAsync failed");
                return new ApiResult<BridgeResponseDto> { Success = false, Error = ex.Message };
            }
        }


        #endregion

        // ===== Internal helpers =====
        private async Task<ApiResult<T>> PostAsync<T>(string url, object payload)
        {
            var token = await _tokenService.GetAccessTokenAsync();
            var json = JsonSerializer.Serialize(payload, JsonOpts);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("REQUEST-ID", Guid.NewGuid().ToString());
            request.Headers.Add("TIMESTAMP", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));

            if (_settings.EnableRequestLogging)
                _logger.LogInformation("ABDM POST {Url}", url);

            var response = await _httpClient.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("ABDM POST failed {Status} {Body}", response.StatusCode, body);
                return new ApiResult<T> { Success = false, Error = body, StatusCode = (int)response.StatusCode };
            }

            var data = JsonSerializer.Deserialize<T>(body, JsonOpts);
            return new ApiResult<T> { Success = true, Data = data, StatusCode = (int)response.StatusCode };
        }

        private async Task<ApiResult<T>> GetAsync<T>(string url, string? xToken = null)
        {
            var token = await _tokenService.GetAccessTokenAsync();
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("REQUEST-ID", Guid.NewGuid().ToString());
            request.Headers.Add("TIMESTAMP", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            if (!string.IsNullOrEmpty(xToken))
                request.Headers.Add("X-Token", $"Bearer {xToken}");

            var response = await _httpClient.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return new ApiResult<T> { Success = false, Error = body, StatusCode = (int)response.StatusCode };

            var data = JsonSerializer.Deserialize<T>(body, JsonOpts);
            return new ApiResult<T> { Success = true, Data = data, StatusCode = (int)response.StatusCode };
        }


        private async Task<ApiResult<T>> PatchAsync<T>(string url, object payload)
        {
            var token = await _tokenService.GetAccessTokenAsync();
            var json = JsonSerializer.Serialize(payload, JsonOpts);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Patch, url) { Content = content };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("REQUEST-ID", Guid.NewGuid().ToString());
            request.Headers.Add("TIMESTAMP", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));

            if (_settings.EnableRequestLogging)
                _logger.LogInformation("ABDM POST {Url}", url);

            var response = await _httpClient.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogWarning("ABDM POST failed {Status} {Body}", response.StatusCode, body);
                return new ApiResult<T> { Success = false, Error = body, StatusCode = (int)response.StatusCode };
            }

            var data = JsonSerializer.Deserialize<T>(body, JsonOpts);
            return new ApiResult<T> { Success = true, Data = data, StatusCode = (int)response.StatusCode };
        }

        private async Task<ApiResult<byte[]>> GetBinaryAsync(string url, string xToken)
        {
            var token = await _tokenService.GetAccessTokenAsync();
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("REQUEST-ID", Guid.NewGuid().ToString());
            request.Headers.Add("TIMESTAMP", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            request.Headers.Add("X-Token", $"Bearer {xToken}");

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var err = await response.Content.ReadAsStringAsync();
                return new ApiResult<byte[]> { Success = false, Error = err, StatusCode = (int)response.StatusCode };
            }

            var bytes = await response.Content.ReadAsByteArrayAsync();
            return new ApiResult<byte[]> { Success = true, Data = bytes, StatusCode = (int)response.StatusCode };
        }

        public async Task<ApiResult<VerifyUserResponse>> VerifyUser(VerifyUserRequest obj)
        {
            var publicKey = await _tokenService.GetPublicCertificateAsync();
            obj.ABHANumber = RsaEncryptionHelper.Encrypt(obj.ABHANumber, publicKey);
            var url = $"{_settings.BaseUrls.AbhaBaseUrl}{_settings.Endpoints.OtpVerify}";
            return await PostAsync<VerifyUserResponse>(url, obj);
            //var url = $"{_settings.BaseUrls.AbhaBaseUrl}{_settings.Endpoints.VerifyUser}";
            //return PostAsync<VerifyUserResponse>(url, obj);
        }
    }
}