using HIMS.API.ABHA.Configuration;
using HIMS.API.ABHA.Interface;
using HIMS.API.ABHA.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace HIMS.API.ABHA.Services
{

    public class AbdmTokenService : IAbdmTokenService
    {
        private readonly HttpClient _httpClient;
        private readonly ABDMSettings _settings;
        private readonly IMemoryCache _cache;
        private readonly ILogger<AbdmTokenService> _logger;
        private const string TokenCacheKey = "ABDM_ACCESS_TOKEN";
        private const string CertCacheKey = "ABDM_PUBLIC_CERT";
        public AbdmTokenService(HttpClient httpClient, IOptions<ABDMSettings> options, IMemoryCache cache, ILogger<AbdmTokenService> logger)
        {
            _httpClient = httpClient;
            _settings = options.Value;
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

            var url = $"{_settings.Endpoints.SessionToken}";
            var payload = new SessionTokenRequest
            {
                ClientId = _settings.Credentials.ClientId,
                ClientSecret = _settings.Credentials.ClientSecret,
                GrantType = _settings.Credentials.GrantType
            };

            var json = JsonSerializer.Serialize(payload);
            using var content = new StringContent(json, Encoding.UTF8, "application/json");

            using var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
            request.Headers.Add("REQUEST-ID", Guid.NewGuid().ToString());
            request.Headers.Add("TIMESTAMP", DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss.fffZ"));
            request.Headers.Add("X-CM-ID", _settings.Credentials.XCmId);

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
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(_settings.TokenCacheMinutes),
                Size = 1
            });

            _logger.LogInformation("ABDM access token fetched and cached for {Minutes} minutes",
                _settings.TokenCacheMinutes);

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
            var url = $"{_settings.BaseUrls.AbhaBaseUrl}{_settings.Endpoints.PublicCertificate}";

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
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(1),
                Size = 1
            });
            return certificate.PublicKey;
        }
    }
}