using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwPharmacyBill
    {
        public long SalesId { get; set; }
        public long? OpIpId { get; set; }
        public DateTime? Date { get; set; }
        public string? SalesNo { get; set; }
        public int? RegNo { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public long? OpIpType { get; set; }
        public long RegId { get; set; }
        public decimal? PaidAmountPayment { get; set; }
        public long TransactionType { get; set; }
        public long RefundId { get; set; }
        public string? PatientName { get; set; }
        public decimal? RefundAmt { get; set; }
        public long? StoreId { get; set; }
    }
}
