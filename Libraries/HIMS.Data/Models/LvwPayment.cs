using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwPayment
    {
        public decimal? CashPayAmount { get; set; }
        public long? BillNo { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public decimal? CardPayAmount { get; set; }
        public decimal? NeftpayAmount { get; set; }
        public decimal? PayTmpayAmount { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? AdvanceUseAmount { get; set; }
        public long? TransactionType { get; set; }
    }
}
