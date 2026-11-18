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
                return FixedTimeEquals(Encoding.UTF8.GetBytes(stored), Encoding.UTF8.GetBytes(password));

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
