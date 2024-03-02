using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwBillIpd
    {
        public long BillNo { get; set; }
        public long? OpdIpdId { get; set; }
        public decimal? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public decimal? BalanceAmt { get; set; }
        public DateTime? BillDate { get; set; }
        public byte? OpdIpdType { get; set; }
        public decimal? PaidAmount { get; set; }
        public long? IsCancelled { get; set; }
        public string? PbillNo { get; set; }
        public long? RegId { get; set; }
        public long TransactionType { get; set; }
        public double AdvanceAmount { get; set; }
        public double AdvanceUsedAmount { get; set; }
        public double AdvBalanceAmount { get; set; }
        public long? AddedBy { get; set; }
        public long? CashCounterId { get; set; }
        public long PaymentBillNo { get; set; }
        public long? CompanyId { get; set; }
        public DateTime? BillTime { get; set; }
        public string? RegNo { get; set; }
        public decimal? CashPay { get; set; }
        public decimal? ChequePay { get; set; }
        public decimal? CardPay { get; set; }
        public decimal? NeftPay { get; set; }
        public decimal? PayTmpay { get; set; }
        public decimal? AdvUsdPay { get; set; }
        public long? InterimOrFinal { get; set; }
    }
}
