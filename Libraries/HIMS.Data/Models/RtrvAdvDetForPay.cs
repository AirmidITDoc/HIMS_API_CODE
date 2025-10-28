using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class RtrvAdvDetForPay
    {
        public long AdvanceDetailId { get; set; }
        public string? Date { get; set; }
        public long? AdvanceId { get; set; }
        public long? OpdIpdId { get; set; }
        public string? Reason { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public decimal? UsedAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public decimal? RefundAmount { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public string? ChequeNo { get; set; }
        public string? ChequeBankName { get; set; }
        public decimal? CardPayAmount { get; set; }
        public string CardNo { get; set; } = null!;
        public string CardBankName { get; set; } = null!;
        public DateTime? CardDate { get; set; }
        public decimal? NeftpayAmount { get; set; }
        public string Neftno { get; set; } = null!;
        public string NeftbankMaster { get; set; } = null!;
        public DateTime? Neftdate { get; set; }
        public decimal? PayTmamount { get; set; }
        public string PayTmtranNo { get; set; } = null!;
        public DateTime? PayTmdate { get; set; }
        public long? TransactionType { get; set; }
        public long PaymentId { get; set; }
    }
}
