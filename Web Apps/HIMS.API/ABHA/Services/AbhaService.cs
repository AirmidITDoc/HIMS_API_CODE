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
        private readonly AbdmHttpClient _httpClient;
        private readonly ABDMSettings _settings;
        private readonly IAbdmTokenService _tokenService;
        private readonly ILogger<AbhaService> _logger;

        private static readonly JsonSerializerOptions JsonOpts = new()
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
        public AbhaService(AbdmHttpClient httpClient, IOptions<ABDMSettings> options, IAbdmTokenService tokenService, ILogger<AbhaService> logger)
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
                return await _httpClient.PostAsync<OtpResponse>(url, payload);
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
                return await _httpClient.PostAsync<AbhaEnrolmentResponse>(url, payload);
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
                return await _httpClient.PostAsync<OtpResponse>(url, payload);
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
                return await _httpClient.PostAsync<VerifyOtpResponse>(url, payload);
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
                var url = $"{_settings.BaseUrls.AbhaBaseUrl}{_settings.Endpoints.AbhaAddressSuggestion}";
                return await _httpClient.GetAsync<AbhaAddressSuggestionResponse>(url, null, txnId);
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
            return await _httpClient.PostAsync<AbhaEnrolmentResponse>(url, payload);
        }

        // ===== PROFILE / CARD / QR =====
        public async Task<ApiResult<AbhaProfile>> GetAbhaProfileAsync(string xToken)
        {
            var url = $"{_settings.BaseUrls.AbhaBaseUrl}{_settings.Endpoints.AbhaProfile}";
            return await _httpClient.GetAsync<AbhaProfile>(url, xToken);
        }

        public async Task<ApiResult<byte[]>> GetAbhaCardAsync(string xToken)
        {
            var url = $"{_settings.BaseUrls.AbhaBaseUrl}{_settings.Endpoints.AbhaCard}";
            return await _httpClient.GetBinaryAsync(url, xToken);
        }

        public async Task<ApiResult<byte[]>> GetAbhaQrCodeAsync(string xToken)
        {
            var url = $"{_settings.BaseUrls.AbhaBaseUrl}{_settings.Endpoints.AbhaQrCode}";
            return await _httpClient.GetBinaryAsync(url, xToken);
        }
        #endregion
        public async Task<ApiResult<VerifyUserResponse>> VerifyUser(VerifyUserRequest obj)
        {
            var publicKey = await _tokenService.GetPublicCertificateAsync();
            obj.ABHANumber = RsaEncryptionHelper.Encrypt(obj.ABHANumber, publicKey);
            var url = $"{_settings.BaseUrls.AbhaBaseUrl}{_settings.Endpoints.OtpVerify}";
            return await _httpClient.PostAsync<VerifyUserResponse>(url, obj);
            //var url = $"{_settings.BaseUrls.AbhaBaseUrl}{_settings.Endpoints.VerifyUser}";
            //return PostAsync<VerifyUserResponse>(url, obj);
        }
    }
}