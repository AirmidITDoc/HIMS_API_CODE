using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class Bill
    {
        public long BillNo { get; set; }
        public long? OpdIpdId { get; set; }
        public decimal? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public decimal? PaidAmt { get; set; }
        public decimal? BalanceAmt { get; set; }
        public DateTime? BillDate { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? IsCancelled { get; set; }
        public string? PbillNo { get; set; }
        public decimal? TotalAdvanceAmount { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public long? AddedBy { get; set; }
        public long? CashCounterId { get; set; }
        public DateTime? BillTime { get; set; }
        public long? ConcessionReasonId { get; set; }
        public bool? IsSettled { get; set; }
        public bool? IsPrinted { get; set; }
        public bool? IsFree { get; set; }
        public long? CompanyId { get; set; }
        public long? TariffId { get; set; }
        public long? UnitId { get; set; }
        public long? InterimOrFinal { get; set; }
        public string? CompanyRefNo { get; set; }
        public long? ConcessionAuthorizationName { get; set; }
        public bool? IsBillCheck { get; set; }
        public double? SpeTaxPer { get; set; }
        public decimal? SpeTaxAmt { get; set; }
        public bool? IsBillShrHold { get; set; }
        public string? DiscComments { get; set; }
        public decimal? ChTotalAmt { get; set; }
        public decimal? ChConcessionAmt { get; set; }
        public decimal? ChNetPayAmt { get; set; }
        public decimal? CompDiscAmt { get; set; }
        public string? BillPrefix { get; set; }
        public string? BillMonth { get; set; }
        public string? BillYear { get; set; }
        public string? PrintBillNo { get; set; }
    }
}
