using System.Threading;

namespace HIMS.Core.Infrastructure
{
    public static class CurrentUserAccessor
    {
        private static readonly AsyncLocal<int?> _userId = new();
        private static readonly AsyncLocal<string?> _username = new();

        public static int? UserId
        {
            get => _userId.Value;
            private set => _userId.Value = value;
        }

        public static string? Username
        {
            get => _username.Value;
            private set => _username.Value = value;
        }

        public static void Set(int? userId, string? username)
        {
            UserId = userId;
            Username = username;
        }

        public static void Clear()
        {
            UserId = null;
            Username = null;
        }
    }
}
