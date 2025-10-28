using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TSalesDraftDet
    {
        public long SalDetId { get; set; }
        public long? DsalesId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public decimal? UnitMrp { get; set; }
        public float? Qty { get; set; }
        public decimal? TotalAmount { get; set; }
        public double? VatPer { get; set; }
        public decimal? VatAmount { get; set; }
        public double? DiscPer { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public decimal? LandedPrice { get; set; }
        public decimal? TotalLandedAmount { get; set; }
        public decimal? PurRateWf { get; set; }
        public decimal? PurTotAmt { get; set; }
    }
}
