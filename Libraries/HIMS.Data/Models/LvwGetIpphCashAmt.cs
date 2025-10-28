using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwGetIpphCashAmt
    {
        public long AdmissionId { get; set; }
        public decimal? CashPay { get; set; }
    }
}
