using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwOtcharge
    {
        public long OtchargesId { get; set; }
        public long? OpdIpdId { get; set; }
        public long ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public double? Price { get; set; }
        public int? Qty { get; set; }
        public double? TotalAmt { get; set; }
        public DateTime? ChargesDate { get; set; }
        public long? ChargesId { get; set; }
        public bool? IsCancelled { get; set; }
        public byte? OpdIpdType { get; set; }
    }
}
