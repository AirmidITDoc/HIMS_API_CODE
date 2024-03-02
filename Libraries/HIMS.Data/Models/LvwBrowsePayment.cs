using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwBrowsePayment
    {
        public long PaymentId { get; set; }
        public long? BillNo { get; set; }
        public long? RegId { get; set; }
        public string? PatientName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public decimal? TotalAmt { get; set; }
        public decimal? BalanceAmt { get; set; }
        public string? Remark { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public decimal? CardPayAmount { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public long? AdvanceId { get; set; }
        public long? RefundId { get; set; }
        public bool? IsCancelled { get; set; }
        public long? AddBy { get; set; }
        public string? UserName { get; set; }
        public string? PbillNo { get; set; }
        public string? ReceiptNo { get; set; }
        public long? TransactionType { get; set; }
    }
}
