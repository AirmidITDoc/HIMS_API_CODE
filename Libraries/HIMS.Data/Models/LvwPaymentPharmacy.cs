using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwPaymentPharmacy
    {
        public long? BillNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public decimal? CardPayAmount { get; set; }
        public decimal? AdvanceUseAmount { get; set; }
        public decimal? NeftpayAmount { get; set; }
        public decimal? PayTmamount { get; set; }
        public long TransactionType { get; set; }
    }
}
