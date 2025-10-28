using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class AllOutStandingPatientWise
    {
        public DateTime? Date { get; set; }
        public decimal? TotalAmt { get; set; }
        public string Lbl { get; set; } = null!;
        public long? StoreId { get; set; }
        public long? OpIpId { get; set; }
        public long? OpIpType { get; set; }
        public string? Ipdno { get; set; }
        public string? PatientName { get; set; }
        public string? CompanyName { get; set; }
    }
}
