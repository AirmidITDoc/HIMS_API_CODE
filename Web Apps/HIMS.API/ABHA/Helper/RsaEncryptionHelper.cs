using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace HIMS.API.ABHA.Helper
{
    /// <summary>
    /// ABDM mandates RSA-OAEP (SHA-1) encryption with the public certificate
    /// fetched from /v0.5/certs before sending Aadhaar number, mobile number, or OTP.
    /// </summary>
    public static class RsaEncryptionHelper
    {
        public static string Encrypt(string plainText, string publicKeyPem)
        {
            if (string.IsNullOrWhiteSpace(plainText))
                throw new ArgumentException("Plain text cannot be empty", nameof(plainText));

            if (string.IsNullOrWhiteSpace(publicKeyPem))
                throw new ArgumentException("Public key cannot be empty", nameof(publicKeyPem));

            // Strip PEM headers/footers if present
            var cleanKey = publicKeyPem
                .Replace("-----BEGIN PUBLIC KEY-----", string.Empty)
                .Replace("-----END PUBLIC KEY-----", string.Empty)
                .Replace("-----BEGIN CERTIFICATE-----", string.Empty)
                .Replace("-----END CERTIFICATE-----", string.Empty)
                .Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();

            var keyBytes = Convert.FromBase64String(cleanKey);

            using var rsa = RSA.Create();
            try
            {
                rsa.ImportSubjectPublicKeyInfo(keyBytes, out _);
            }
            catch
            {
                // Fallback if it's an X.509 cert blob
                rsa.ImportRSAPublicKey(keyBytes, out _);
            }

            var data = Encoding.UTF8.GetBytes(plainText);
            // ABDM expects OAEP with SHA-1
            var encrypted = rsa.Encrypt(data, RSAEncryptionPadding.OaepSHA1);
            return Convert.ToBase64String(encrypted);
        }
    }
    public static class AbhaHelper
    {
        public static string GetErrorMessage(string jsonString)
        {
            try
            {
                var root = JsonSerializer.Deserialize<JsonElement>(jsonString);

                // Case 1: { "error": { "code": "...", "message": "..." } }
                if (root.TryGetProperty("error", out var errorObj))
                {
                    if (errorObj.TryGetProperty("message", out var msg))
                        return msg.GetString();
                }

                // Case 2: { "loginId": "Invalid LoginId", "timestamp": "..." }
                // Return first property value that isn't "timestamp"
                foreach (var prop in root.EnumerateObject())
                {
                    if (!prop.Name.Equals("timestamp", StringComparison.OrdinalIgnoreCase))
                        return prop.Value.GetString();
                }

                return "An unknown error occurred.";
            }
            catch
            {
                return "Failed to parse error response.";
            }
        }
    }
}
