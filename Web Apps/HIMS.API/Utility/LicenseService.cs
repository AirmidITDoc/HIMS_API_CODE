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

            var machineHashExp = LicenseSecurity.DecryptString(licenseFile.Data.MachineHash).Split(new string[] { "||" }, StringSplitOptions.None);
            if (machineHashExp.Length == 2)
            {
                DateTime ExpDate = Convert.ToDateTime(machineHashExp[1]);
                if (ExpDate < DateTime.UtcNow)
                    return "License expired.";

                var machineHash = MachineFingerprint.Generate();

                if (machineHashExp[0] != machineHash)
                    return "License not valid for this machine.";

                return "Ok";
            }
            else
            {
                return "Invalid machine hash in license.";
            }
        }
    }
    public static class MachineFingerprint
    {
        public static string Generate()
        {
            var mac = FormatMac(NetworkInterface.GetAllNetworkInterfaces().FirstOrDefault(n => n.OperationalStatus == OperationalStatus.Up)?.GetPhysicalAddress().ToString());
            using var sha = SHA256.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(mac));
            return Convert.ToBase64String(bytes);
        }
        private static string FormatMac(string raw)
        {
            if (raw == "Unknown" || raw.Length != 12)
                return raw;

            return string.Join(":", Enumerable.Range(0, 6)
                .Select(i => raw.Substring(i * 2, 2)));
        }
    }
}
