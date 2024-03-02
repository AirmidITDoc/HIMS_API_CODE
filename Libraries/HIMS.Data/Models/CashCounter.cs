using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class CashCounter
    {
        public long CashCounterId { get; set; }
        public string? CashCounterName { get; set; }
        public string? Prefix { get; set; }
        public string? BillNo { get; set; }
        public bool? IsActive { get; set; }
    }
}
