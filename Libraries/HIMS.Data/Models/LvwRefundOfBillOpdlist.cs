using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwRefundOfBillOpdlist
    {
        public long BillNo { get; set; }
        public decimal? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public string? PbillNo { get; set; }
        public decimal RefundAmount { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public DateTime? BillDate { get; set; }
        public decimal? BalanceAmt { get; set; }
        public long RegId { get; set; }
        public long VisitId { get; set; }
        public string? BilDate { get; set; }
    }
}
