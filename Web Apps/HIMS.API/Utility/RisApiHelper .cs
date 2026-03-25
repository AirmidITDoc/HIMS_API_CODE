using HIMS.API.Models;
using HIMS.Core.Domain.Common;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HIMS.API.Utility
{
    public class RisApiHelper : IRisApiHelper
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<RisApiHelper> _logger;

        // Simple in-memory token cache — replace with IMemoryCache for multi-instance scenarios
        private static string? _cachedToken;
        private static DateTime _tokenExpiry = DateTime.MinValue;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            WriteIndented = false
        };

        public RisApiHelper(IHttpClientFactory httpClientFactory, ILogger<RisApiHelper> logger)
        {
            _httpClient = httpClientFactory.CreateClient("RisClient");
            _logger = logger;
        }

        // ──────────────────────────────────────────────────
        //  Authentication
        // ──────────────────────────────────────────────────

        public async Task<AuthResponse> AuthenticateAsync()
        {
            var url = $"{AppSettings.Settings.RisApi.BaseUrl}{AppSettings.Settings.RisApi.Endpoints.Auth}";

            var payload = new AuthRequest
            {
                Username = AppSettings.Settings.RisApi.Credentials.Username,
                Password = AppSettings.Settings.RisApi.Credentials.Password
            };

            var response = await PostAsync<AuthResponse>(url, payload, addAuth: false);
            return response ?? new AuthResponse { Error = "Empty response from auth endpoint" };
        }

        // ──────────────────────────────────────────────────
        //  Create Radiology Order  – POST /api/ris/radiology_order
        // ──────────────────────────────────────────────────

        public async Task<RisApiResponse> CreateRadiologyOrderAsync(CreateRadiologyOrderRequest request)
        {
            var url = $"{AppSettings.Settings.RisApi.BaseUrl}{AppSettings.Settings.RisApi.Endpoints.RadiologyOrder}";
            return await PostWithAuthAsync<RisApiResponse>(url, request)
                   ?? new RisApiResponse { Status = false, Message = "No response from RIS" };
        }

        // ──────────────────────────────────────────────────
        //  Update Radiology Order  – PUT /api/ris/radiology_order/{checkId}
        //  checkId can be accession_number OR external_id
        // ──────────────────────────────────────────────────

        public async Task<RisApiResponse> UpdateRadiologyOrderAsync(string checkId, UpdateRadiologyOrderRequest request)
        {
            var url = $"{AppSettings.Settings.RisApi.BaseUrl}{AppSettings.Settings.RisApi.Endpoints.RadiologyOrder}/{checkId}";
            return await PutWithAuthAsync<RisApiResponse>(url, request)
                   ?? new RisApiResponse { Status = false, Message = "No response from RIS" };
        }

        // ──────────────────────────────────────────────────
        //  Delete Radiology Order  – DELETE /api/ris/radiology_order
        // ──────────────────────────────────────────────────

        public async Task<RisApiResponse> DeleteRadiologyOrderAsync(DeleteRadiologyOrderRequest request)
        {
            var url = $"{AppSettings.Settings.RisApi.BaseUrl}{AppSettings.Settings.RisApi.Endpoints.RadiologyOrder}";
            return await DeleteWithAuthAsync<RisApiResponse>(url, request)
                   ?? new RisApiResponse { Status = false, Message = "No response from RIS" };
        }

        // ──────────────────────────────────────────────────
        //  Receive Patient History  – POST /api/ris/patient-previous-reports
        // ──────────────────────────────────────────────────

        public async Task<RisApiResponse> SendPatientHistoryAsync(PatientHistoryRequest request)
        {
            var url = $"{AppSettings.Settings.RisApi.BaseUrl}{AppSettings.Settings.RisApi.Endpoints.PatientPreviousReports}";
            return await PostWithAuthAsync<RisApiResponse>(url, request)
                   ?? new RisApiResponse { Status = false, Message = "No response from RIS" };
        }

        // ──────────────────────────────────────────────────
        //  Token Management
        // ──────────────────────────────────────────────────

        private async Task<string?> GetValidTokenAsync()
        {
            if (!string.IsNullOrEmpty(_cachedToken) && DateTime.UtcNow < _tokenExpiry)
                return _cachedToken;

            _logger.LogInformation("Fetching new RIS JWT token.");
            var authResult = await AuthenticateAsync();

            if (string.IsNullOrEmpty(authResult.AccessToken))
            {
                _logger.LogError("RIS authentication failed: {Error}", authResult.Error);
                return null;
            }

            _cachedToken = authResult.AccessToken;
            _tokenExpiry = DateTime.UtcNow.AddDays(AppSettings.Settings.RisApi.TokenExpiryDays - 1); // refresh 1 day early
            return _cachedToken;
        }

        // ──────────────────────────────────────────────────
        //  Private HTTP helpers
        // ──────────────────────────────────────────────────

        private async Task<T?> PostAsync<T>(string url, object payload, bool addAuth)
        {
            return await SendAsync<T>(HttpMethod.Post, url, payload, addAuth);
        }

        private async Task<T?> PostWithAuthAsync<T>(string url, object payload)
        {
            return await SendAsync<T>(HttpMethod.Post, url, payload, addAuth: true);
        }

        private async Task<T?> PutWithAuthAsync<T>(string url, object payload)
        {
            return await SendAsync<T>(HttpMethod.Put, url, payload, addAuth: true);
        }

        private async Task<T?> DeleteWithAuthAsync<T>(string url, object payload)
        {
            return await SendAsync<T>(HttpMethod.Delete, url, payload, addAuth: true);
        }

        private async Task<T?> SendAsync<T>(HttpMethod method, string url, object payload, bool addAuth)
        {
            try
            {
                var request = new HttpRequestMessage(method, url);

                if (addAuth)
                {
                    var token = await GetValidTokenAsync();
                    if (token == null)
                        throw new UnauthorizedAccessException("Could not obtain RIS JWT token.");
                    request.Headers.Authorization = new AuthenticationHeaderValue("JWT", token);
                }

                var json = JsonSerializer.Serialize(payload, JsonOptions);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await _httpClient.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();

                _logger.LogDebug("RIS {Method} {Url} → {Status}: {Body}", method, url, (int)response.StatusCode, responseBody);

                return JsonSerializer.Deserialize<T>(responseBody, JsonOptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "RIS API call failed: {Method} {Url}", method, url);
                throw;
            }
        }
    }
}
