using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TCustomerAmcinformation
    {
        public long Id { get; set; }
        public long? CustomerId { get; set; }
        public DateTime? AmcstartDate { get; set; }
        public int? Amcduration { get; set; }
        public DateTime AmcendDate { get; set; }
        public decimal? Amcamount { get; set; }
        public DateTime? AmcpaidDate { get; set; }
        public long? PaymentId { get; set; }
    }
}
