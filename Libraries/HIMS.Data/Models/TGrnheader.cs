using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TGrnheader
    {
        public long Grnid { get; set; }
        public string? GrnNumber { get; set; }
        public DateTime? Grndate { get; set; }
        public DateTime? Grntime { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierId { get; set; }
        public string? InvoiceNo { get; set; }
        public string? DeliveryNo { get; set; }
        public string? GateEntryNo { get; set; }
        public bool? CashCreditType { get; set; }
        public bool? Grntype { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? TotalDiscAmount { get; set; }
        public decimal? TotalVatamount { get; set; }
        public decimal? NetAmount { get; set; }
        public string? Remark { get; set; }
        public string? ReceivedBy { get; set; }
        public bool? IsVerified { get; set; }
        public long? IsClosed { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public string? Prefix { get; set; }
        public bool? IsCancelled { get; set; }
        public bool? IsPaymentProcess { get; set; }
        public DateTime? PaymentPrcDate { get; set; }
        public string? ProcessDes { get; set; }
        public DateTime? InvDate { get; set; }
        public decimal? DebitNote { get; set; }
        public decimal? CreditNote { get; set; }
        public decimal? OtherCharge { get; set; }
        public decimal? RoundingAmt { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalAmount { get; set; }
        public decimal? TotCgstamt { get; set; }
        public decimal? TotSgstamt { get; set; }
        public decimal? TotIgstamt { get; set; }
        public byte? TranProcessId { get; set; }
        public string? TranProcessMode { get; set; }
        public decimal? BillDiscAmt { get; set; }
    }
}
