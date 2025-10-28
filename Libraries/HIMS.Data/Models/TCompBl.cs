using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TCompBl
    {
        public long Drbno { get; set; }
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
        public double? TaxPer { get; set; }
        public decimal? TaxAmount { get; set; }
    }
}
