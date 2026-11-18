using HIMS.Api.Models.Login;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace HIMS.API.Security
{
    public static class LoginV2Crypto
    {
        public static string BuildAad(string context, string kid, string challengeId, string nonce, LoginType loginType)
        {
            return $"hims-login|1|{context}|{kid}|{challengeId}|{nonce}|{loginType}";
        }

        public static LoginV2Plaintext Decrypt(LoginV2Envelope envelope, LoginV2Challenge challenge, LoginV2Key key)
        {
            byte[] nonce = Decode(envelope.Nonce);
            if (nonce.Length != 12 || envelope.Nonce != challenge.Nonce)
                throw new CryptographicException("Invalid login nonce.");

            byte[] wrapped = Decode(envelope.WrappedKey);
            byte[] ciphertext = Decode(envelope.Ciphertext);
            byte[] tag = Decode(envelope.Tag);
            if (tag.Length != 16 || wrapped.Length != key.Rsa.KeySize / 8)
                throw new CryptographicException("Invalid login envelope.");

            byte[] aesKey = key.Rsa.Decrypt(wrapped, RSAEncryptionPadding.OaepSHA256);
            try
            {
                if (aesKey.Length != 32)
                    throw new CryptographicException("Invalid AES key.");
                byte[] plaintext = new byte[ciphertext.Length];
                string aad = BuildAad(challenge.Context, challenge.Kid, challenge.Id, challenge.Nonce, challenge.LoginType);
                using var aes = new AesGcm(aesKey);
                aes.Decrypt(nonce, ciphertext, tag, plaintext, Encoding.UTF8.GetBytes(aad));
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = false };
                options.Converters.Add(new JsonStringEnumConverter());
                return JsonSerializer.Deserialize<LoginV2Plaintext>(plaintext, options) ??
                    throw new JsonCryptographicException();
            }
            finally
            {
                CryptographicOperations.ZeroMemory(aesKey);
            }
        }

        public static byte[] Decode(string value) => Convert.FromBase64String(value);
    }

    public sealed class JsonCryptographicException : CryptographicException { }
}
