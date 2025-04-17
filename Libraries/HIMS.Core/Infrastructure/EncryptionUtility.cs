using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HIMS.Core.Infrastructure
{
    public class SecurityKeys
    {
        public const string EnDeKey = "wbugadmincomnetindiadevelopments";
    }

    public class EncryptionUtility
    {

        private static readonly string EncryptionKey = "airmidsoftwaredevelopmentat12345"; // Must match Angular key
        private static readonly string IvString = "airmidsoftware12"; // Must match Angular IV
        #region Utilities
        private static ICryptoTransform CreateCryptoByType(CryptoType cryptoType, string key)
        {
            byte[] keyBytes = Convert.FromBase64String(key);
            RijndaelManaged rijndaelAlgorithm = new() { Mode = CipherMode.ECB };
            if (cryptoType.Equals(CryptoType.Encrypt))
            {
                rijndaelAlgorithm.Padding = PaddingMode.Zeros;
                ICryptoTransform encryptor = rijndaelAlgorithm.CreateEncryptor(keyBytes, null);
                return encryptor;
            }
            else
            {
                rijndaelAlgorithm.Padding = PaddingMode.None;
                ICryptoTransform decryptor = rijndaelAlgorithm.CreateDecryptor(keyBytes, null);
                return decryptor;
            }
        }

        private enum CryptoType
        {
            Encrypt = 1,
            Decrypt = 2,
        }

        private const int INDEX_START = 0;
        #endregion

        #region Methods
        /// <summary>
        /// Create a SHA256 password hash
        /// </summary>
        /// <param name="password">Password</param>
        /// <returns>Password hash</returns>
        public static string CreatePasswordHash(string password)
        {
            StringBuilder sBuilder = new();
            using (SHA256 hash = SHA256Managed.Create())
            {
                byte[] result = hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                foreach (byte data in result)
                {
                    sBuilder.Append(data.ToString("x2"));
                }
            }
            return sBuilder.ToString();
        }

        /// <summary>
        /// Encrypt text
        /// </summary>
        /// <param name="plainText">Text to encrypt</param>
        /// <param name="encryptionPrivateKey">Encryption private key</param>
        /// <returns>Encrypted text</returns>
        public static string EncryptText(string plainText, string encryptionPrivateKey = "")
        {
            string encryptText = string.Empty;

            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            byte[] cipherTextBytes;
            ICryptoTransform encryptor = CreateCryptoByType(CryptoType.Encrypt, encryptionPrivateKey);
            using (MemoryStream memoryStream = new())
            {
                using (CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(plainTextBytes, INDEX_START, plainTextBytes.Length);
                    cryptoStream.FlushFinalBlock();
                    cipherTextBytes = memoryStream.ToArray();
                    cryptoStream.Close();
                    encryptText = Convert.ToBase64String(cipherTextBytes);
                }
                memoryStream.Close();
            }

            return encryptText;
        }

        /// <summary>
        /// Decrypt text
        /// </summary>
        /// <param name="cipherText">Text to decrypt</param>
        /// <param name="encryptionPrivateKey">Encryption private key</param>
        /// <returns>Decrypted text</returns>
        public static string DecryptText(string cipherText, string encryptionPrivateKey = "")
        {
            string decryptedText = string.Empty;
            cipherText = cipherText.Replace(" ", "+");
            byte[] cipherTextBytes = Convert.FromBase64String(cipherText);

            using (MemoryStream memoryStream = new(cipherTextBytes))
            {
                ICryptoTransform decryptor = CreateCryptoByType(CryptoType.Decrypt, encryptionPrivateKey);
                using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, INDEX_START, plainTextBytes.Length);
                decryptedText = Encoding.UTF8.GetString(plainTextBytes, INDEX_START, decryptedByteCount).TrimEnd("\0".ToCharArray());
            }
            return decryptedText;
        }

        public static string DecryptFromAngular(string cipherText)
        {

            var fullCipher = Convert.FromBase64String(cipherText);
            var iv = Encoding.UTF8.GetBytes(IvString);
            var cipherBytes = fullCipher;

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream(cipherBytes);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var sr = new StreamReader(cs);
            return sr.ReadToEnd();
        }
        public static string EncryptForAngular(string plainText)
        {
            var iv = Encoding.UTF8.GetBytes(IvString);

            using var aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(EncryptionKey);
            aes.IV = iv;
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                using var sw = new StreamWriter(cs);
                sw.Write(plainText);
            }

            var encryptedBytes = ms.ToArray();
            return Convert.ToBase64String(encryptedBytes);
        }

        /// <summary>
        /// Encrypt and URL encoded text
        /// </summary>
        /// <param name="plainText">Text to encrypt</param>
        /// <param name="encryptionPrivateKey">Encryption private key</param>
        /// <returns>Encrypted text</returns>
        public static string EncryptURLEncoded(string plainText, string encryptionPrivateKey)
        {
            return System.Web.HttpUtility.UrlEncode(EncryptText(plainText, encryptionPrivateKey));
        }

        /// <summary>
        /// Decrypt and URL decoded text
        /// </summary>
        /// <param name="cipherText">Text to decrypt</param>
        /// <param name="encryptionPrivateKey">Encryption private key</param>
        /// <returns>Decrypted text</returns>
        public static string DecryptURLDecoded(string cipherText, string encryptionPrivateKey)
        {
            return DecryptText(System.Web.HttpUtility.UrlDecode(cipherText), encryptionPrivateKey);
        }
        #endregion
    }
}
