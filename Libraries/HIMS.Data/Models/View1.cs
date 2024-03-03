using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class View1
    {
        public long PaymentId { get; set; }
        public long? BillNo { get; set; }
        public string? ReceiptNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentTime { get; set; }
        public decimal? CashPayAmount { get; set; }
        public long? RefundId { get; set; }
        public long? TransactionType { get; set; }
        public long? AddBy { get; set; }
        public long? CashCounterId { get; set; }
        public long? StrId { get; set; }
        public string? TranMode { get; set; }
        public long? Expr1 { get; set; }
        public long? StoreId { get; set; }
    }
}
