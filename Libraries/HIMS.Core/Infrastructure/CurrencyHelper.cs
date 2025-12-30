using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Core.Infrastructure
{
    public static class CurrencyHelper
    {
        private static string _symbol;
        private static bool _initialized;
        public static void Configure(string symbol)
        {
            if (_initialized) return;

            _symbol = symbol;
            _initialized = true;
        }

        public static string CurrencySymbol => _symbol;
    }
}
