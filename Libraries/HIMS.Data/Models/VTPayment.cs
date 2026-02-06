using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class VTPayment
    {
        public long PaymentId { get; set; }
        public long? UnitId { get; set; }
        public long? BillNo { get; set; }
        public byte? Opdipdtype { get; set; }
        public string? ReceiptNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentTime { get; set; }
        public long? CompanyId { get; set; }
        public long? AdvanceId { get; set; }
        public long? RefundId { get; set; }
        public long? CashCounterId { get; set; }
        public long? TransactionType { get; set; }
        public string? TransactionLabel { get; set; }
        public byte? IsSelfOrcompany { get; set; }
        public string? TranMode { get; set; }
        public bool? IsCancelled { get; set; }
        public long? CreatedBy { get; set; }
        public decimal? CashAmount { get; set; }
        public decimal? CardAmount { get; set; }
        public decimal? ChequeAmount { get; set; }
        public decimal? OnlineAmount { get; set; }
        public string? UpitranNo { get; set; }
    }
}
