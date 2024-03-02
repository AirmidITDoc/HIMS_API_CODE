using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class SalesReturnGststoreWise
    {
        public string Lbl { get; set; } = null!;
        public DateTime? Date { get; set; }
        public float? Gstper { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public double? VatPer { get; set; }
        public decimal? Cgstamt { get; set; }
        public float? Sgstper { get; set; }
        public decimal? Sgstamt { get; set; }
        public decimal? TaxableAmount { get; set; }
        public float? Igstper { get; set; }
        public decimal? Igstamt { get; set; }
    }
}
