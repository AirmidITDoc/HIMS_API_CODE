using System.IO.Compression;
using System.Text;

namespace HIMS.API.Extensions
{
    public static class PermissionClaimCodec
    {
        public const string ClaimType = "PermissionsBr";

        public static string Encode(string permissions)
        {
            using var output = new MemoryStream();
            using (var compressor = new BrotliStream(output, CompressionLevel.Optimal, leaveOpen: true))
            {
                var bytes = Encoding.UTF8.GetBytes(permissions);
                compressor.Write(bytes, 0, bytes.Length);
            }
            return Convert.ToBase64String(output.ToArray());
        }

        public static string Decode(string encoded)
        {
            using var input = new MemoryStream(Convert.FromBase64String(encoded));
            using var decompressor = new BrotliStream(input, CompressionMode.Decompress);
            using var reader = new StreamReader(decompressor, Encoding.UTF8);
            return reader.ReadToEnd();
        }
    }
}
