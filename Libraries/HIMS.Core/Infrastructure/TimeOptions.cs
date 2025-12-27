using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Infrastructure
{
    public static class AppTime
    {
        private static int _offsetMinutes;
        private static bool _initialized;
        public static void Configure(int offsetMinutes)
        {
            if (_initialized) return;

            _offsetMinutes = offsetMinutes;
            _initialized = true;
        }

        public static DateTime Now => DateTime.UtcNow.AddMinutes(_offsetMinutes);
        //public static double Offset => _options?.OffsetHours ?? 0;

        public static DateTime UtcNow => DateTime.UtcNow;
    }
}
