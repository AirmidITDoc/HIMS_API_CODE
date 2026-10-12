using HIMS.ABHA.Security.Exceptions;
using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace HIMS.ABHA.Security.Encryptions;

public interface IFideliusEncryptionService
{
    FideliusEncryptionResponse Encrypt(FideliusEncryptionRequest encryptionRequest);
}

public class FideliusEncryptionService : IFideliusEncryptionService
{
    public FideliusEncryptionService()
    {
    }

    public FideliusEncryptionResponse Encrypt(FideliusEncryptionRequest encryptionRequest)
    {
        try
        {
            ValidateKeys(encryptionRequest.SenderPrivateKey, encryptionRequest.RequesterPublicKey);
            byte[] xorOfNonces = FideliusUtils.CalculateXorOfBytes(
                FideliusUtils.FromBase64String(encryptionRequest.SenderNonce),
                FideliusUtils.FromBase64String(encryptionRequest.RequesterNonce)
            );

            byte[] iv = xorOfNonces.Skip(xorOfNonces.Length - 12).ToArray();
            byte[] salt = xorOfNonces.Take(20).ToArray();

            string encryptedData = Encrypt(
                iv,
                salt,
                encryptionRequest.SenderPrivateKey,
                encryptionRequest.RequesterPublicKey,
                encryptionRequest.StringToEncrypt
            );
            return new FideliusEncryptionResponse(encryptedData, Convert.ToBase64String(iv), Convert.ToBase64String(salt));
        }
        catch (Exception ex)
        {
            throw new EncryptionException("An error occurred during encryption.", ex);
        }
    }

    private static string Encrypt(byte[] iv, byte[] salt, string senderPrivateKey, string requesterPublicKey, string stringToEncrypt)
    {
        string sharedSecret = FideliusUtils.ComputeSharedSecret(senderPrivateKey, requesterPublicKey);

        byte[] aesEncryptionKey = FideliusUtils.Sha256Hkdf(salt, sharedSecret, 32);

        byte[] stringBytes = System.Text.Encoding.UTF8.GetBytes(stringToEncrypt);

        GcmBlockCipher cipher = new(new AesEngine());
        AeadParameters parameters = new(
            new KeyParameter(aesEncryptionKey),
            128,
            iv,
            null
        );

        cipher.Init(true, parameters);
        byte[] cipherBytes = new byte[cipher.GetOutputSize(stringBytes.Length)];
        int encryptedBytesLength = cipher.ProcessBytes(stringBytes, 0, stringBytes.Length, cipherBytes, 0);
        cipher.DoFinal(cipherBytes, encryptedBytesLength);

        return FideliusUtils.ToBase64String(cipherBytes);
    }

    private void ValidateKeys(string privateKey, string publicKey)
    {
        if (string.IsNullOrWhiteSpace(privateKey) || string.IsNullOrWhiteSpace(publicKey))
        {
            throw new EncryptionException("Private or public key cannot be empty.");
        }

        try
        {
            byte[] privateKeyBytes = FideliusUtils.FromBase64String(privateKey);
            byte[] publicKeyBytes = FideliusUtils.FromBase64String(publicKey);

            if (privateKeyBytes.Length != 32) // 256-bit private key
            {
                //throw new EncryptionException($"Invalid private key length. Expected 32 bytes but passed {privateKeyBytes.Length}.");
            }

            if (publicKeyBytes.Length != 65) // 520-bit uncompressed public key
            {
                //throw new EncryptionException($"Invalid public key length. Expected 65 bytes but passed {publicKeyBytes.Length}.");
            }
        }
        catch (FormatException)
        {
            throw new EncryptionException("Invalid Base64 encoding in private or public key.");
        }
        catch (Exception ex)
        {
            throw new EncryptionException("Invalid encryption keys.", ex);
        }
    }
}
