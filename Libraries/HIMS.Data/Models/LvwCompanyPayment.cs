using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwCompanyPayment
    {
        public long? BillNo { get; set; }
        public long PaymentId { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? Tdsamount { get; set; }
        public decimal? PayAmount { get; set; }
        public decimal? WrfAmount { get; set; }
        public decimal? PaidAmount { get; set; }
        public DateTime? SettlementDate { get; set; }
        public DateTime? SettlementTime { get; set; }
        public string? ReceiptNo { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public decimal? CardPayAmount { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public decimal? NeftpayAmount { get; set; }
        public decimal? PayTmamount { get; set; }
    }
}
