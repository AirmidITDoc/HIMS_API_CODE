using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwBillWiseMediniceTol
    {
        public long? ServiceId { get; set; }
        public double? TotalAmt { get; set; }
        public long BillNo { get; set; }
        public string? PbillNo { get; set; }
    }
}
