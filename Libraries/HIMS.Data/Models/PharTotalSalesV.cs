using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class PharTotalSalesV
    {
        public decimal? TotalSales { get; set; }
        public long? OpIpId { get; set; }
        public long? OpIpType { get; set; }
    }
}
