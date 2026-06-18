using HIMS.ABHA.Configuration;
using HIMS.ABHA.Interface;
using HIMS.ABHA.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.Json;

namespace HIMS.ABHA.Services
{

    public class AbdmTokenService : IAbdmTokenService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private const string TokenCacheKey = "ABDM_ACCESS_TOKEN";
        private const string CertCacheKey = "ABDM_PUBLIC_CERT";
        private readonly ILogger _logger;
        public AbdmTokenService(HttpClient httpClient, IMemoryCache cache, ILogger<AbdmTokenService> logger)
        {
            _httpClient = httpClient;
            _cache = cache;
            _logger = logger;
        }

        public async Task<string> GetAccessTokenAsync(bool forceRefresh = false)
        {
            if (!forceRefresh && _cache.TryGetValue(TokenCacheKey, out string? cachedToken)
                && !string.IsNullOrEmpty(cachedToken))
            {
                return cachedToken;
            }

            var url = $"{AppSettings.Current.BaseUrls.GatewayBaseUrl}{AppSettings.Current.Endpoints.SessionToken}";
            var payload = new SessionTokenRequest
            {
                ClientId = AppSettings.Current.Credentials.ClientId,
                ClientSecret = AppSettings.Current.Credentials.ClientSecret,
                GrantType = AppSettings.Current.Credentials.GrantType
            };

            var json = JsonSerializer.Serialize(payload);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
            request.Headers.Add("REQUEST-ID", Guid.NewGuid().ToString());
            request.Headers.Add("TIMESTAMP", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            request.Headers.Add("X-CM-ID", AppSettings.Current.Credentials.XCmId);

            var response = await _httpClient.SendAsync(request);
            var responseBody = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to fetch ABDM access token. Status: {Status}, Body: {Body}",
                    response.StatusCode, responseBody);
                throw new HttpRequestException($"ABDM token fetch failed: {response.StatusCode} - {responseBody}");
            }

            var tokenResponse = JsonSerializer.Deserialize<SessionTokenResponse>(responseBody,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (tokenResponse == null || string.IsNullOrEmpty(tokenResponse.AccessToken))
                throw new InvalidOperationException("ABDM returned empty access token");

            //_cache.Set(TokenCacheKey, tokenResponse.AccessToken,
            //    TimeSpan.FromMinutes(_settings.TokenCacheMinutes));
            _cache.Set(TokenCacheKey, tokenResponse.AccessToken, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(AppSettings.Current.TokenCacheMinutes),
                Size = 1
            });

            _logger.LogInformation("ABDM access token fetched and cached for {Minutes} minutes",
                AppSettings.Current.TokenCacheMinutes);

            return tokenResponse.AccessToken;
        }

        public async Task<string> GetPublicCertificateAsync()
        {
            if (_cache.TryGetValue(CertCacheKey, out string? cachedCert)
                && !string.IsNullOrEmpty(cachedCert))
            {
                return cachedCert;
            }

            var token = await GetAccessTokenAsync();
            var url = $"{AppSettings.Current.BaseUrls.AbhaBaseUrl}{AppSettings.Current.Endpoints.PublicCertificate}";

            using var request = new HttpRequestMessage(HttpMethod.Get, url);
            request.Headers.Add("Authorization", $"Bearer {token}");
            request.Headers.Add("REQUEST-ID", Guid.NewGuid().ToString());
            request.Headers.Add("TIMESTAMP", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));

            var response = await _httpClient.SendAsync(request);
            var cert = await response.Content.ReadAsStringAsync();

            if (!response.IsSuccessStatusCode)
            {
                _logger.LogError("Failed to fetch ABDM public certificate. Status: {Status}", response.StatusCode);
                throw new HttpRequestException($"ABDM cert fetch failed: {response.StatusCode}");
            }
            var certificate = JsonSerializer.Deserialize<CertificateDto>(cert,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            // Certs typically rotate; cache for an hour
            //_cache.Set(CertCacheKey, cert, TimeSpan.FromHours(1));
            _cache.Set(CertCacheKey, certificate.PublicKey, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(AppSettings.Current.CertificateCacheMinutes),
                Size = 1
            });
            return certificate.PublicKey;
        }
    }
}