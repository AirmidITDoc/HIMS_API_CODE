using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class SalesGststoreWiseOpip
    {
        public string Lbl { get; set; } = null!;
        public DateTime? Date { get; set; }
        public float? Gstper { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? Cgstamt { get; set; }
        public decimal? Sgstamt { get; set; }
        public decimal? TaxableAmount { get; set; }
        public long? StoreId { get; set; }
        public long? OpIpType { get; set; }
    }
}
