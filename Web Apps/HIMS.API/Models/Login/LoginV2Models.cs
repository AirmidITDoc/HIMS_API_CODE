using System.Text.Json.Serialization;

namespace HIMS.Api.Models.Login
{
    public sealed class LoginV2Envelope
    {
        public int Version { get; set; }
        public string Kid { get; set; } = string.Empty;
        public string ChallengeId { get; set; } = string.Empty;
        public string Nonce { get; set; } = string.Empty;
        public string WrappedKey { get; set; } = string.Empty;
        public string Ciphertext { get; set; } = string.Empty;
        public string Tag { get; set; } = string.Empty;
        public LoginType LoginType { get; set; }
    }

    public sealed class LoginV2Plaintext
    {
        [JsonPropertyName("username")]
        public string Username { get; set; } = string.Empty;
        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;
        [JsonPropertyName("captchaCode")]
        public string CaptchaCode { get; set; } = string.Empty;
        [JsonPropertyName("loginType")]
        public LoginType LoginType { get; set; }
    }

    public sealed class ConfirmAuthenticateV2Model
    {
        public string Token { get; set; } = string.Empty;
        public LoginType LoginType { get; set; }
    }

    public sealed class LoginV2Challenge
    {
        public string Id { get; init; } = string.Empty;
        public string Kid { get; init; } = string.Empty;
        public string Context { get; init; } = string.Empty;
        public string Nonce { get; init; } = string.Empty;
        public LoginType LoginType { get; init; }
        public DateTimeOffset ExpiresAt { get; init; }
        public byte[] CaptchaHash { get; init; } = Array.Empty<byte>();
    }
}
