using HIMS.Api.Models.Login;

namespace HIMS.API.Security
{
    public static class LoginSessionPolicy
    {
        public const int MinimumMobileSessionDays = 90;
        public const int MaximumMobileSessionDays = 180;
        public const int DefaultMobileSessionDays = 180;
        private const int WebTokenValidityMinutes = 12 * 60;

        public static int GetTokenValidityMinutes(IConfiguration configuration, LoginType loginType)
        {
            if (loginType != LoginType.Mobile)
                return WebTokenValidityMinutes;

            int configuredDays = configuration.GetValue("LoginV2:MobileSessionDays", DefaultMobileSessionDays);
            int mobileSessionDays = Math.Clamp(configuredDays, MinimumMobileSessionDays, MaximumMobileSessionDays);
            return mobileSessionDays * 24 * 60;
        }

        public static string RotateSessionToken(HIMS.Data.Models.LoginManager user, LoginType loginType)
        {
            string token = Guid.NewGuid().ToString();
            if (loginType == LoginType.Mobile)
                user.MobileToken = token;
            else
                user.UserToken = token;
            return token;
        }
    }
}
