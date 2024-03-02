using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwOpdrefundAgainstBillList
    {
        public long RefundId { get; set; }
        public DateTime? RefundDate { get; set; }
        public DateTime? RefundTime { get; set; }
        public string? RefundNo { get; set; }
        public string? PbillNo { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public decimal? RefundAmount { get; set; }
        public long PaymentId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentTime { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public decimal? CardPayAmount { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public string? Remark { get; set; }
        public string? PaymentRemark { get; set; }
        public byte? TransactionId { get; set; }
        public long? TransactionType { get; set; }
        public long? BillId { get; set; }
        public string? RefunDate { get; set; }
    }
}
