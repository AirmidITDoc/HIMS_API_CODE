using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Infrastructure
{
    public class TimeOptions
    {
        public double OffsetHours { get; set; }
    }
    public static class AppTime
    {
        private static TimeOptions _options;

        public static void Configure(TimeOptions options)
        {
            _options = options;
        }

        public static DateTime Now =>
            DateTime.UtcNow.AddHours(_options?.OffsetHours ?? 0);
        public static double Offset => _options?.OffsetHours ?? 0;

        public static DateTime UtcNow => DateTime.UtcNow;
    }
}
