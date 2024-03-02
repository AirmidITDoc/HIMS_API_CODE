using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TCanteenBillDetail
    {
        public long CdetId { get; set; }
        public long? BillNo { get; set; }
        public long? ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public decimal? UnitMrp { get; set; }
        public double? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        public double? Gstper { get; set; }
        public decimal? Gstamount { get; set; }
        public double? DiscPer { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? LandedPrice { get; set; }
        public decimal? TotalLandedAmount { get; set; }
        public double? ReturnQty { get; set; }
        public long? ReqId { get; set; }
        public long? ReqDetId { get; set; }
    }
}
