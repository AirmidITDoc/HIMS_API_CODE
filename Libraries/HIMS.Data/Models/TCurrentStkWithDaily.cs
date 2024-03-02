using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TCurrentStkWithDaily
    {
        public long StockId { get; set; }
        public long? StoreId { get; set; }
        public long? ItemId { get; set; }
        public float? OpeningBalance { get; set; }
        public float? ReceivedQty { get; set; }
        public float? IssueQty { get; set; }
        public float? BalanceQty { get; set; }
        public decimal? UnitMrp { get; set; }
        public decimal? PurchaseRate { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? VatPercentage { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public DateTime? InsertDate { get; set; }
        public string? StoreMaster { get; set; }
        public string? StoreName { get; set; }
        public string? MItemMaster { get; set; }
        public string? ItemName { get; set; }
        public string? PurUnitRateWf { get; set; }
    }
}
