using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class SalesReturnGstdateWise
    {
        public DateTime? SalesReturnDate { get; set; }
        public decimal? Mrpamount { get; set; }
        public double? VatPer { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? TotalGstamount { get; set; }
    }
}
