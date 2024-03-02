using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwCurrentBalQtyCheck
    {
        public double? BalanceQty { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public long? StoreId { get; set; }
        public decimal? UnitMrp { get; set; }
        public decimal? PurUnitRateWf { get; set; }
        public decimal? LandedRate { get; set; }
    }
}
