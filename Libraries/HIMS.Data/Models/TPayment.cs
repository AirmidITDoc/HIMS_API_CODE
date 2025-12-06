using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPayment
    {
        public long PaymentId { get; set; }
        public long? UnitId { get; set; }
        public long? BillNo { get; set; }
        public byte? Opdipdtype { get; set; }
        public string? ReceiptNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentTime { get; set; }
        public decimal? PayAmount { get; set; }
        public string? TranNo { get; set; }
        public string? BankName { get; set; }
        public DateTime? ValidationDate { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public string? Comments { get; set; }
        public string? PayMode { get; set; }
        public string? OnlineTranNo { get; set; }
        public string? OnlineTranResponse { get; set; }
        public long? CompanyId { get; set; }
        public long? AdvanceId { get; set; }
        public long? RefundId { get; set; }
        public long? CashCounterId { get; set; }
        public long? TransactionType { get; set; }
        public string? TransactionLabel { get; set; }
        public byte? IsSelfOrcompany { get; set; }
        public string? TranMode { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
