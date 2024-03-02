using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TWorkOrderDetail
    {
        public long WodetId { get; set; }
        public long? Woid { get; set; }
        public string? ItemName { get; set; }
        public long? Qty { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscPer { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? Vatper { get; set; }
        public decimal? Vatamount { get; set; }
        public decimal? NetAmount { get; set; }
        public string? Remark { get; set; }
        public long? PendQty { get; set; }
    }
}
