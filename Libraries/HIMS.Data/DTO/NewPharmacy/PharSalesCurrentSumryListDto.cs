namespace HIMS.Services.Pharmacy
{
    public class PharSalesCurrentSumryListDto
    {
        public string ItemName { get; set; }
        public string BatchNo { get; set; }
        public double SalesQty { get; set; }
        public long StoreId { get; set; }

    }
    public class SalesCurrentStockItemListDto
    {
        public long ItemId { get; set; }
        public long? StoreId { get; set; }
        public long? ConversionFactor { get; set; }
        public string? ItemName { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public long? IssueQty { get; set; }
        public decimal? UnitMRP { get; set; }
        public decimal? VatPercentage { get; set; }
        public decimal? MRPTotal { get; set; }
        public bool? IsBatchRequired { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? LandedTotal { get; set; }
        public float? BalanceQty { get; set; }
        public decimal? PurUnitRateWF { get; set; }
        public decimal? PurTotAmt { get; set; }
        public float? CGSTPer { get; set; }
        public float? SGSTPer { get; set; }
        public float? IGSTPer { get; set; }
        public bool? IsNarcotic { get; set; }
        public long StockId { get; set; }
        public int DiscPer { get; set; }
    }
}
