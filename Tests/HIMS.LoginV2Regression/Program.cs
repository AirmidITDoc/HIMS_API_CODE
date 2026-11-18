using HIMS.Api.Models.Login;
using HIMS.API.Security;
using HIMS.Core.Infrastructure;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

static void Require(bool value, string message)
{
    if (!value) throw new InvalidOperationException(message);
}

var password = "correct horse battery staple";
var hash = PasswordHash.Create(password);
Require(PasswordHash.IsHash(hash), "Password hash marker missing.");
Require(PasswordHash.Verify(password, hash), "Password hash verification failed.");
Require(!PasswordHash.Verify("wrong", hash), "Wrong password accepted.");
Require(!PasswordHash.NeedsUpgrade(hash), "Fresh hash incorrectly needs upgrade.");
Require(PasswordHash.Verify(password, password), "Legacy password migration compatibility failed.");

using var rsa = RSA.Create(2048);
var key = new LoginV2Key { Kid = "test-key", Rsa = rsa };
var nonce = RandomNumberGenerator.GetBytes(12);
var challenge = new LoginV2Challenge
{
    Id = "challenge-1",
    Kid = key.Kid,
    Context = "server-context",
    Nonce = Convert.ToBase64String(nonce),
    LoginType = LoginType.Web,
    ExpiresAt = DateTimeOffset.UtcNow.AddMinutes(5)
};
var aesKey = RandomNumberGenerator.GetBytes(32);
var plaintext = JsonSerializer.Serialize(new LoginV2Plaintext
{
    Username = "test",
    Password = password,
    CaptchaCode = "1234",
    LoginType = LoginType.Web
});
var ciphertext = new byte[Encoding.UTF8.GetByteCount(plaintext)];
var tag = new byte[16];
var aad = Encoding.UTF8.GetBytes(LoginV2Crypto.BuildAad(
    challenge.Context, challenge.Kid, challenge.Id, challenge.Nonce, challenge.LoginType));
using (var aes = new AesGcm(aesKey))
    aes.Encrypt(nonce, Encoding.UTF8.GetBytes(plaintext), ciphertext, tag, aad);
var envelope = new LoginV2Envelope
{
    Version = 1,
    Kid = key.Kid,
    ChallengeId = challenge.Id,
    Nonce = challenge.Nonce,
    WrappedKey = Convert.ToBase64String(rsa.Encrypt(aesKey, RSAEncryptionPadding.OaepSHA256)),
    Ciphertext = Convert.ToBase64String(ciphertext),
    Tag = Convert.ToBase64String(tag),
    LoginType = LoginType.Web
};
Require(LoginV2Crypto.Decrypt(envelope, challenge, key).Password == password,
    "V2 envelope did not decrypt.");
envelope.Tag = Convert.ToBase64String(tag.Select(b => (byte)(b ^ 0xff)).ToArray());
try
{
    LoginV2Crypto.Decrypt(envelope, challenge, key);
    throw new InvalidOperationException("Authenticated encryption accepted tampered tag.");
}
catch (CryptographicException)
{
}

var jsonOptions = new JsonSerializerOptions();
jsonOptions.Converters.Add(new JsonStringEnumConverter());
Require(JsonSerializer.Deserialize<LoginV2Plaintext>(
    "{\"username\":\"u\",\"password\":\"p\",\"captchaCode\":\"1234\",\"loginType\":\"Web\"}", jsonOptions)!
    .LoginType == LoginType.Web, "String loginType contract failed.");

Console.WriteLine("PASS: V2 hybrid encryption/AAD, tamper rejection, loginType JSON, and password hashing.");
