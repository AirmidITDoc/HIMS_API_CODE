using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TSalesReturnHeader
    {
        public long SalesReturnId { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public string? SalesReturnNo { get; set; }
        public long? SalesId { get; set; }
        public long? OpIpId { get; set; }
        public long? OpIpType { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? VatAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public long? CashCounterId { get; set; }
        public bool? IsSellted { get; set; }
        public bool? IsPrint { get; set; }
        public bool? IsFree { get; set; }
        public long? UnitId { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? StoreId { get; set; }
        public bool? IsPurBill { get; set; }
        public bool? IsBillCheck { get; set; }
        public string? Narration { get; set; }
    }
}
