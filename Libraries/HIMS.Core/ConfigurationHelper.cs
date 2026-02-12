using Microsoft.Extensions.Configuration;

namespace HIMS.Core
{
    public static class ConfigurationHelper
    {
        public static IConfiguration config;
        public static void Initialize(IConfiguration Configuration)
        {
            config = Configuration;
        }
    }

}
