using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwItemWithoutBatchno
    {
        public long? ItemId { get; set; }
        public double? BalanceQty { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? UnitMrp { get; set; }
        public decimal? PurchaseRate { get; set; }
        public decimal? VatPercentage { get; set; }
        public bool? IsBatchRequired { get; set; }
        public long? StoreId { get; set; }
    }
}
