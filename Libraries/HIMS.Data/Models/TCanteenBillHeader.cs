using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TCanteenBillHeader
    {
        public long BillNo { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Time { get; set; }
        public long? StoreId { get; set; }
        public long? OpIpId { get; set; }
        public string? CustomerName { get; set; }
        public string? PbillNo { get; set; }
        public decimal? TotalAmount { get; set; }
        public float? Gstper { get; set; }
        public decimal? Gstamount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public long? ConcessionReasonId { get; set; }
        public long? ConcessionAuthorizationId { get; set; }
        public long? CashCounterId { get; set; }
        public bool? IsPrint { get; set; }
        public bool? IsFree { get; set; }
        public long? UnitId { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? ReqId { get; set; }
        public bool? IsOtherOrIsEmpBill { get; set; }
    }
}
