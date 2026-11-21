using System.Security.Cryptography;
using System.Text;

namespace HIMS.Core.Infrastructure
{
    public static class PasswordHash
    {
        private const int Iterations = 600_000;
        private const string Marker = "PBKDF2-SHA256";

        public static string Create(string password)
        {
            if (password == null)
                throw new ArgumentNullException(nameof(password));

            byte[] salt = RandomNumberGenerator.GetBytes(16);
            byte[] hash = Pbkdf2(password, salt, Iterations);
            return $"{Marker}${Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
        }

        public static bool IsHash(string? value)
        {
            return value?.StartsWith(Marker + "$", StringComparison.Ordinal) == true;
        }

        public static bool Verify(string password, string? stored)
        {
            if (string.IsNullOrEmpty(stored) || password == null)
                return false;

            if (!IsHash(stored))
            {
                // Support passwords stored by the legacy SHA-256 implementation.
                if (IsLegacySha256(stored))
                    return FixedTimeEquals(
                        Encoding.ASCII.GetBytes(stored.ToLowerInvariant()),
                        CreateLegacySha256(password));

                return FixedTimeEquals(Encoding.UTF8.GetBytes(stored), Encoding.UTF8.GetBytes(password));
            }

            string[] parts = stored.Split('$');
            if (parts.Length != 4 ||
                !int.TryParse(parts[1], out int iterations) ||
                iterations < 100_000 ||
                iterations > 2_000_000)
                return false;

            try
            {
                byte[] salt = Convert.FromBase64String(parts[2]);
                byte[] expected = Convert.FromBase64String(parts[3]);
                byte[] actual = Pbkdf2(password, salt, iterations);
                return expected.Length == actual.Length && FixedTimeEquals(expected, actual);
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static bool NeedsUpgrade(string? stored)
        {
            if (!IsHash(stored))
                return true;

            string[] parts = stored!.Split('$');
            return parts.Length != 4 || !int.TryParse(parts[1], out int iterations) || iterations < Iterations;
        }

        private static bool IsLegacySha256(string value)
        {
            if (value.Length != 64)
                return false;

            foreach (char c in value)
            {
                if (!((c >= '0' && c <= '9') ||
                      (c >= 'a' && c <= 'f') ||
                      (c >= 'A' && c <= 'F')))
                    return false;
            }
            return true;
        }

        private static byte[] CreateLegacySha256(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hash = SHA256.HashData(passwordBytes);
            string hex = Convert.ToHexString(hash).ToLowerInvariant();
            return Encoding.ASCII.GetBytes(hex);
        }

        private static byte[] Pbkdf2(string password, byte[] salt, int iterations)
        {
            return Rfc2898DeriveBytes.Pbkdf2(password, salt, iterations, HashAlgorithmName.SHA256, 32);
        }

        private static bool FixedTimeEquals(ReadOnlySpan<byte> left, ReadOnlySpan<byte> right)
        {
            if (left.Length != right.Length)
                return false;
            return CryptographicOperations.FixedTimeEquals(left, right);
        }
    }
}
