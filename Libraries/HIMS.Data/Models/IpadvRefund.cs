using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class IpadvRefund
    {
        public long? OpdIpdId { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public string? ChequeNo { get; set; }
        public string? BankName { get; set; }
        public DateTime? ChequeDate { get; set; }
        public decimal? NeftpayAmount { get; set; }
        public string? Neftno { get; set; }
        public string? NeftbankMaster { get; set; }
        public DateTime? Neftdate { get; set; }
        public decimal? RefundAmount { get; set; }
        public long? TransactionType { get; set; }
        public long? RefundId { get; set; }
        public long? AdvanceId { get; set; }
    }
}
