using HIMS.ABHA.Configuration;
using HIMS.ABHA.Interface;
using HIMS.ABHA.Models;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace HIMS.ABHA.Helper
{
    public class AbdmHttpClient
    {
        private readonly HttpClient _httpClient;
        private readonly IAbdmTokenService _tokenService;
        private readonly ILogger<AbdmHttpClient> _logger;
        private static readonly JsonSerializerOptions JsonOpts = new()
        {
            PropertyNameCaseInsensitive = true,
            DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull
        };
        public AbdmHttpClient(HttpClient http, IAbdmTokenService abdmTokenService, ILogger<AbdmHttpClient> logger)
        {
            _httpClient = http;
            _tokenService = abdmTokenService;
            _logger = logger;
        }


        public async Task<ApiResult<T>> PostAsync<T>(string url, object payload, Dictionary<string, string>? headers = null)
        {
            var token = await _tokenService.GetAccessTokenAsync();
            var json = JsonSerializer.Serialize(payload, JsonOpts);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("REQUEST-ID", Guid.NewGuid().ToString());
            request.Headers.Add("TIMESTAMP", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }
            }

            if (AppSettings.Current.EnableRequestLogging)
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

        public async Task<ApiResult<T>> GetAsync<T>(string url, string? xToken = null, string? txnId = null)
        {
            var token = await _tokenService.GetAccessTokenAsync();
            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("REQUEST-ID", Guid.NewGuid().ToString());
            request.Headers.Add("TIMESTAMP", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            if (!string.IsNullOrEmpty(xToken))
                request.Headers.Add("X-Token", $"Bearer {xToken}");
            if (!string.IsNullOrEmpty(txnId))
                request.Headers.Add("Transaction_Id", txnId);

            var response = await _httpClient.SendAsync(request);
            var body = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
                return new ApiResult<T> { Success = false, Error = body, StatusCode = (int)response.StatusCode };

            var data = JsonSerializer.Deserialize<T>(body, JsonOpts);
            return new ApiResult<T> { Success = true, Data = data, StatusCode = (int)response.StatusCode };
        }


        public async Task<ApiResult<T>> PatchAsync<T>(string url, object payload)
        {
            var token = await _tokenService.GetAccessTokenAsync();
            var json = JsonSerializer.Serialize(payload, JsonOpts);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Patch, url) { Content = content };
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            request.Headers.Add("REQUEST-ID", Guid.NewGuid().ToString());
            request.Headers.Add("TIMESTAMP", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));

            if (AppSettings.Current.EnableRequestLogging)
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

        public async Task<ApiResult<byte[]>> GetBinaryAsync(string url, string xToken)
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
    }
}
