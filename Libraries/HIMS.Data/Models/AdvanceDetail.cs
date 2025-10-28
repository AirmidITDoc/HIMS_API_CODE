using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class AdvanceDetail
    {
        public long AdvanceDetailId { get; set; }
        public long? UnitId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public long? AdvanceId { get; set; }
        public string? AdvanceNo { get; set; }
        public long? RefId { get; set; }
        public long? TransactionId { get; set; }
        public long? OpdIpdId { get; set; }
        public byte? OpdIpdType { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public decimal? UsedAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public decimal? RefundAmount { get; set; }
        public long? ReasonOfAdvanceId { get; set; }
        public string? Reason { get; set; }
        public long? CashCounterId { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledby { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? AddedBy { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
