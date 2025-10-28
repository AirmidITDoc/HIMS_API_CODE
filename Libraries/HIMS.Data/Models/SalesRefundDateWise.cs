using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class SalesRefundDateWise
    {
        public DateTime? Date { get; set; }
        public decimal? CashPay { get; set; }
        public long? StoreId { get; set; }
    }
}
