using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TCanteenRequestDetail
    {
        public long ReqDetId { get; set; }
        public long? ReqId { get; set; }
        public long? ItemId { get; set; }
        public decimal? UnitMrp { get; set; }
        public double? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        public bool? IsBillGenerated { get; set; }
        public bool? IsCancelled { get; set; }
    }
}
