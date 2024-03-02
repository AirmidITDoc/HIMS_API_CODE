using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPhadvanceHeader
    {
        public long AdvanceId { get; set; }
        public DateTime? Date { get; set; }
        public long? RefId { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public double? AdvanceAmount { get; set; }
        public double? AdvanceUsedAmount { get; set; }
        public double? BalanceAmount { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? StoreId { get; set; }
    }
}
