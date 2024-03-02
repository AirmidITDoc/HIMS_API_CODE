using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPhRefund
    {
        public long RefundId { get; set; }
        public DateTime? RefundDate { get; set; }
        public DateTime? RefundTime { get; set; }
        public string? RefundNo { get; set; }
        public long? BillId { get; set; }
        public long? AdvanceId { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public decimal? RefundAmount { get; set; }
        public string? Remark { get; set; }
        public byte? TransactionId { get; set; }
        public long? AddBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? CashCounterId { get; set; }
        public bool? IsRefundFlag { get; set; }
        public long? StrId { get; set; }
    }
}
