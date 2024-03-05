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
        public int? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
