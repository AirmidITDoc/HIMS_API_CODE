using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class OpIpCollection
    {
        public DateTime? PaymentDate { get; set; }
        public decimal TodaysCollection { get; set; }
    }
}
