using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class SalesGstdateWise
    {
        public DateTime? SalesDate { get; set; }
        public decimal? Mrpamount { get; set; }
        public double? VatPer { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? TotalGstamount { get; set; }
    }
}
