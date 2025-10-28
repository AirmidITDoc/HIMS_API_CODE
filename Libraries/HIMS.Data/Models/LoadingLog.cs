using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LoadingLog
    {
        public long? StockId { get; set; }
        public DateTime? LedgerDate { get; set; }
    }
}
