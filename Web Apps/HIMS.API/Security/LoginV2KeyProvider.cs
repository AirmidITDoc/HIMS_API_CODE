using System.Security.Cryptography;

namespace HIMS.API.Security
{
    public sealed class LoginV2Key
    {
        public string Kid { get; init; } = string.Empty;
        public RSA Rsa { get; init; } = null!;
        public string PublicKey => Convert.ToBase64String(Rsa.ExportSubjectPublicKeyInfo());
    }

    public interface ILoginV2KeyProvider
    {
        bool IsConfigured { get; }
        string? ConfigurationError { get; }
        LoginV2Key Current { get; }
        IEnumerable<LoginV2Key> All { get; }
        bool TryGet(string kid, out LoginV2Key key);
    }

    public sealed class LoginV2KeyProvider : ILoginV2KeyProvider, IDisposable
    {
        private readonly Dictionary<string, LoginV2Key> _keys = new(StringComparer.Ordinal);

        public LoginV2KeyProvider(IConfiguration configuration, bool allowEphemeralDevelopmentKey = false)
        {
            try
            {
                var configuredKeys = configuration.GetSection("LoginV2:Keys").GetChildren()
                    .Where(section => !string.IsNullOrWhiteSpace(section["Kid"]))
                    .Select(section => section.Get<LoginV2KeyConfiguration>())
                    .Where(key => key != null)
                    .Cast<LoginV2KeyConfiguration>()
                    .ToArray();
                var envKey = new LoginV2KeyConfiguration
                {
                    Kid = Environment.GetEnvironmentVariable("HIMS_LOGIN_V2_KID") ??
                        configuration["LoginV2:Kid"] ?? string.Empty,
                    PrivateKeyPkcs8Base64 = Environment.GetEnvironmentVariable("HIMS_LOGIN_V2_PRIVATE_KEY_PKCS8_BASE64") ??
                        configuration["LoginV2:PrivateKeyPkcs8Base64"] ?? string.Empty,
                    PrivateKeyPem = Environment.GetEnvironmentVariable("HIMS_LOGIN_V2_PRIVATE_KEY_PEM") ??
                        configuration["LoginV2:PrivateKeyPem"] ?? string.Empty,
                    PrivateKeyPath = Environment.GetEnvironmentVariable("HIMS_LOGIN_V2_PRIVATE_KEY_PATH") ??
                        configuration["LoginV2:PrivateKeyPath"] ?? string.Empty
                };

                IEnumerable<LoginV2KeyConfiguration> sources = configuredKeys;
                if (configuredKeys.Length == 0)
                    sources = new[] { envKey };

                foreach (var source in sources.Where(x => !string.IsNullOrWhiteSpace(x.Kid)))
                {
                    var rsa = RSA.Create();
                    if (!string.IsNullOrWhiteSpace(source.PrivateKeyPkcs8Base64))
                    {
                        byte[] der = Convert.FromBase64String(source.PrivateKeyPkcs8Base64);
                        rsa.ImportPkcs8PrivateKey(der, out _);
                    }
                    else if (!string.IsNullOrWhiteSpace(source.PrivateKeyPem))
                    {
                        rsa.ImportFromPem(source.PrivateKeyPem);
                    }
                    else if (!string.IsNullOrWhiteSpace(source.PrivateKeyPath))
                    {
                        rsa.ImportFromPem(File.ReadAllText(source.PrivateKeyPath));
                    }
                    else
                    {
                        rsa.Dispose();
                        continue;
                    }

                    if (rsa.KeySize < 2048)
                        throw new CryptographicException("Login V2 RSA keys must be at least 2048 bits.");
                    _keys[source.Kid] = new LoginV2Key { Kid = source.Kid, Rsa = rsa };
                }

                Current = _keys.TryGetValue(configuration["LoginV2:CurrentKid"] ?? envKey.Kid, out var current)
                    ? current
                    : _keys.Values.FirstOrDefault()!;
                if (Current == null && allowEphemeralDevelopmentKey)
                {
                    const string developmentKid = "hims-development-ephemeral";
                    Current = new LoginV2Key
                    {
                        Kid = developmentKid,
                        Rsa = RSA.Create(2048)
                    };
                    _keys[developmentKid] = Current;
                }
                IsConfigured = Current != null;
                if (!IsConfigured)
                    ConfigurationError = "Login V2 is not configured. Set HIMS_LOGIN_V2_KID and HIMS_LOGIN_V2_PRIVATE_KEY_PKCS8_BASE64 (or configure LoginV2 key provider inputs).";
            }
            catch (Exception ex)
            {
                if (allowEphemeralDevelopmentKey)
                {
                    const string developmentKid = "hims-local-fallback";
                    var fallbackRsa = RSA.Create(2048);
                    Current = new LoginV2Key
                    {
                        Kid = developmentKid,
                        Rsa = fallbackRsa
                    };
                    _keys[developmentKid] = Current;
                    IsConfigured = true;
                    ConfigurationError = null;
                }
                else
                {
                    IsConfigured = false;
                    ConfigurationError = "Login V2 key provider configuration is invalid: " + ex.GetType().Name;
                }
            }
        }

        public bool IsConfigured { get; }
        public string? ConfigurationError { get; }
        public LoginV2Key Current { get; } = null!;
        public IEnumerable<LoginV2Key> All => _keys.Values;
        public bool TryGet(string kid, out LoginV2Key key) => _keys.TryGetValue(kid, out key!);

        public void Dispose()
        {
            foreach (var key in _keys.Values)
                key.Rsa.Dispose();
        }
    }

    public sealed class LoginV2KeyConfiguration
    {
        public string Kid { get; set; } = string.Empty;
        public string PrivateKeyPkcs8Base64 { get; set; } = string.Empty;
        public string PrivateKeyPem { get; set; } = string.Empty;
        public string PrivateKeyPath { get; set; } = string.Empty;
    }
}
