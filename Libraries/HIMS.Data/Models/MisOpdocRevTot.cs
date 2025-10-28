using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MisOpdocRevTot
    {
        public string Label { get; set; } = null!;
        public long? ConsultantDocId { get; set; }
        public string? DoctorName { get; set; }
        public byte? OpdIpdType { get; set; }
        public decimal? TotalBillAmount { get; set; }
        public double? DiscAmount { get; set; }
        public decimal? NetBillAmount { get; set; }
        public long? PatientOldNew { get; set; }
    }
}
