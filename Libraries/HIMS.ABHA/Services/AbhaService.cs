using HIMS.ABHA.Configuration;
using HIMS.ABHA.Helper;
using HIMS.ABHA.Interface;
using HIMS.ABHA.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace HIMS.ABHA.Services
{
    public class AbhaService : IAbhaService
    {
        private readonly AbdmHttpClient _httpClient;
        private readonly IAbdmTokenService _tokenService;
        private readonly ILogger<AbhaService> _logger;

        private static readonly JsonSerializerOptions JsonOpts = new()
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
        public AbhaService(AbdmHttpClient httpClient, IAbdmTokenService tokenService, ILogger<AbhaService> logger)
        {
            _httpClient = httpClient;
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

                var url = $"{AppSettings.Current.BaseUrls.AbhaBaseUrl}{AppSettings.Current.Endpoints.AadhaarOtpRequest}";
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

                var url = $"{AppSettings.Current.BaseUrls.AbhaBaseUrl}{AppSettings.Current.Endpoints.AadhaarOtpVerify}";
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

                var url = $"{AppSettings.Current.BaseUrls.AbhaBaseUrl}{(LoginHint == "abha-address" ? AppSettings.Current.Endpoints.AbhaAddressOtpRequest : AppSettings.Current.Endpoints.OtpRequest)}";
                return await _httpClient.PostAsync<OtpResponse>(url, payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RequestOtpAsync failed");
                return new ApiResult<OtpResponse> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<OtpResponse>> RequestOtpAbhaFindAsync(string aadharno, List<string> scope, string LoginHint, string OtpSystem, string txnId)
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
                    Scope = scope,
                    TxnId = txnId
                };

                var url = $"{AppSettings.Current.BaseUrls.AbhaBaseUrl}{(AppSettings.Current.Endpoints.AbhaFindSendOtpRequest)}";
                return await _httpClient.PostAsync<OtpResponse>(url, payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RequestOtpAsync failed");
                return new ApiResult<OtpResponse> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<VerifyOtpResponse>> VerifyOtpAsync(string txnId, string otp, List<string> scope, bool isAbhaAddress = false)
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

                var url = $"{AppSettings.Current.BaseUrls.AbhaBaseUrl}{(isAbhaAddress ? AppSettings.Current.Endpoints.AbhaAddressOtpVerify : AppSettings.Current.Endpoints.OtpVerify)}";
                ApiResult<VerifyOtpResponse> resData = new();
                if (isAbhaAddress)
                {
                    var data = await _httpClient.PostAsync<VerifyAddressOtpResponse>(url, payload);
                    resData.Success = true;
                    resData.Data = new VerifyOtpResponse
                    {
                        authResult = data.Data?.AuthResult ?? "",
                        message = data.Data?.Message ?? "",
                        token = data.Data?.Tokens?.Token ?? "",
                        expiresIn = data.Data?.Tokens?.ExpiresIn ?? 0,
                        refreshToken = data.Data?.Tokens?.RefreshToken ?? "",
                        refreshExpiresIn = data.Data?.Tokens?.RefreshExpiresIn ?? 0,
                        accounts = data.Data?.Users.Select(a => new Account
                        {
                            preferredAbhaAddress = a.preferredAbhaAddress,
                            status = a.status,
                        }).ToList() ?? new List<Account>()
                    };
                }
                else
                    resData = await _httpClient.PostAsync<VerifyOtpResponse>(url, payload);

                return resData;
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
                var url = $"{AppSettings.Current.BaseUrls.AbhaBaseUrl}{AppSettings.Current.Endpoints.AbhaAddressSuggestion}";
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

            var url = $"{AppSettings.Current.BaseUrls.AbhaBaseUrl}{AppSettings.Current.Endpoints.CreateAbhaAddress}";
            return await _httpClient.PostAsync<AbhaEnrolmentResponse>(url, payload);
        }

        public async Task<ApiResult<List<FindAbhaResponse>>> FindAbhaMobileAsync(string mobile)
        {
            try
            {
                var publicKey = await _tokenService.GetPublicCertificateAsync();
                var encryptedOtp = RsaEncryptionHelper.Encrypt(mobile, publicKey);

                var payload = new
                {
                    scope = new[] { "search-abha" },
                    mobile = encryptedOtp
                };

                var url = $"{AppSettings.Current.BaseUrls.AbhaBaseUrl}{AppSettings.Current.Endpoints.FindAbha}";
                return await _httpClient.PostAsync<List<FindAbhaResponse>>(url, payload);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "search-abha failed");
                return new ApiResult<List<FindAbhaResponse>> { Success = false, Error = ex.Message };
            }
        }

        public async Task<ApiResult<AbhaProfileResponse>> AbhaAddressSearchAsync(string mobile)
        {
            try
            {
                var payload = new
                {
                    abhaAddress = mobile
                };

                var url = $"{AppSettings.Current.BaseUrls.AbhaBaseUrl}{AppSettings.Current.Endpoints.AbhaAddressSearch}";
                return await _httpClient.PostAsync<AbhaProfileResponse>(url, payload);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "search-abha failed");
                return new ApiResult<AbhaProfileResponse> { Success = false, Error = ex.Message };
            }
        }

        // ===== PROFILE / CARD / QR =====
        public async Task<ApiResult<AbhaProfile>> GetAbhaProfileAsync(string xToken, bool isAddress)
        {
            var url = isAddress ? $"{AppSettings.Current.BaseUrls.AbhaBaseUrl}{AppSettings.Current.Endpoints.AbhaAddressProfile}" : $"{AppSettings.Current.BaseUrls.AbhaBaseUrl}{AppSettings.Current.Endpoints.AbhaProfile}";
            return await _httpClient.GetAsync<AbhaProfile>(url, xToken);
        }

        public async Task<ApiResult<byte[]>> GetAbhaCardAsync(string xToken)
        {
            var url = $"{AppSettings.Current.BaseUrls.AbhaBaseUrl}{AppSettings.Current.Endpoints.AbhaCard}";
            return await _httpClient.GetBinaryAsync(url, xToken);
        }

        public async Task<ApiResult<byte[]>> GetAbhaQrCodeAsync(string xToken, bool isAddress)
        {
            var url = isAddress ? $"{AppSettings.Current.BaseUrls.AbhaBaseUrl}{AppSettings.Current.Endpoints.AbhaAddressQrCode}" : $"{AppSettings.Current.BaseUrls.AbhaBaseUrl}{AppSettings.Current.Endpoints.AbhaQrCode}";
            return await _httpClient.GetBinaryAsync(url, xToken);
        }
        #endregion
        public async Task<ApiResult<VerifyUserResponse>> VerifyUser(VerifyUserRequest obj)
        {
            var publicKey = await _tokenService.GetPublicCertificateAsync();
            obj.ABHANumber = RsaEncryptionHelper.Encrypt(obj.ABHANumber, publicKey);
            var url = $"{AppSettings.Current.BaseUrls.AbhaBaseUrl}{AppSettings.Current.Endpoints.OtpVerify}";
            return await _httpClient.PostAsync<VerifyUserResponse>(url, obj);
            //var url = $"{AppSettings.Current.BaseUrls.AbhaBaseUrl}{AppSettings.Current.Endpoints.VerifyUser}";
            //return PostAsync<VerifyUserResponse>(url, obj);
        }
    }
}