using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwCanteenRequstItemDet
    {
        public long ReqId { get; set; }
        public long? ItemId { get; set; }
        public string? ItemName { get; set; }
        public decimal? Price { get; set; }
        public double? Qty { get; set; }
        public double? TotalAmount { get; set; }
        public long ReqDetId { get; set; }
    }
}
