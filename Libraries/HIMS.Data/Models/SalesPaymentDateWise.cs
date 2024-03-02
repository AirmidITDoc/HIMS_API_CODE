using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class SalesPaymentDateWise
    {
        public DateTime? PaymentDate { get; set; }
        public decimal? PaidAmount { get; set; }
        public long? StrId { get; set; }
    }
}
