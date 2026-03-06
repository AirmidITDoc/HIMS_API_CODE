using HIMS.API.Models;
using HIMS.Core;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace HIMS.API.Utility
{
    public class LicenseFile
    {
        public LicenseModel Data { get; set; } = default!;
        public string Signature { get; set; } = string.Empty;
    }
    public class LicenseService
    {
        private static readonly string PublicKey;

        static LicenseService()
        {
            PublicKey = ConfigurationHelper.config?.GetSection("Licence:PublicKey").Value ?? "";
        }

        public string Validate()
        {
            if (!File.Exists("license.lic"))
                return "License file missing.";

            var content = File.ReadAllText("license.lic");

            var licenseFile = JsonSerializer.Deserialize<LicenseFile>(content);

            if (licenseFile == null)
                return "Invalid license format.";

            // 🔐 Re-serialize using same settings
            var json = JsonSerializer.Serialize(licenseFile.Data);

            using var rsa = RSA.Create();
            rsa.ImportRSAPublicKey(Convert.FromBase64String(PublicKey), out _);

            var valid = rsa.VerifyData(
                Encoding.UTF8.GetBytes(json),
                Convert.FromBase64String(licenseFile.Signature),
                HashAlgorithmName.SHA256,
                RSASignaturePadding.Pkcs1);

            if (!valid)
                return "License signature invalid.";

            if (licenseFile.Data.ExpiryDate < DateTime.UtcNow)
                return "License expired.";

            var machineHash = MachineFingerprint.Generate();

            if (licenseFile.Data.MachineHash != machineHash)
                return "License not valid for this machine.";

            return "Ok";
        }
    }
    public static class MachineFingerprint
    {
        public static string Generate()
        {
            var mac = NetworkInterface
                .GetAllNetworkInterfaces()
                .FirstOrDefault(n => n.OperationalStatus == OperationalStatus.Up)?
                .GetPhysicalAddress()
                .ToString();

            var machineName = Environment.MachineName;
            var raw = $"{machineName}-{mac}";

            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(raw));
            return Convert.ToBase64String(bytes);
        }
    }
}
