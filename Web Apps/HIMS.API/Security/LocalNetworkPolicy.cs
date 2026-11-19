using System.Net;

namespace HIMS.API.Security
{
    public static class LocalNetworkPolicy
    {
        public static bool IsLoopbackOrPrivate(IPAddress? address)
        {
            if (address == null)
                return false;

            if (address.IsIPv4MappedToIPv6)
                address = address.MapToIPv4();
            if (IPAddress.IsLoopback(address))
                return true;

            byte[] bytes = address.GetAddressBytes();
            if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            {
                return bytes[0] == 10 ||
                    (bytes[0] == 172 && bytes[1] >= 16 && bytes[1] <= 31) ||
                    (bytes[0] == 192 && bytes[1] == 168) ||
                    (bytes[0] == 169 && bytes[1] == 254);
            }

            return address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6 &&
                ((bytes[0] & 0xfe) == 0xfc || (bytes[0] == 0xfe && (bytes[1] & 0xc0) == 0x80));
        }

        public static bool IsLoopbackOrPrivateHost(string? host)
        {
            if (string.IsNullOrWhiteSpace(host))
                return false;
            if (host.Equals("localhost", StringComparison.OrdinalIgnoreCase))
                return true;
            return IPAddress.TryParse(host.Trim('[', ']'), out var address) &&
                IsLoopbackOrPrivate(address);
        }
    }
}
