using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class AllOutStanding
    {
        public DateTime? Date { get; set; }
        public decimal? TotalAmt { get; set; }
        public string Lbl { get; set; } = null!;
        public long? StoreId { get; set; }
    }
}
