namespace HIMS.Services.Pharmacy
{
    public class ItemNameBalanceQtyListDto
    {
        public long StockId { get; set; }
        public long StoreId { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public float? BalanceQty { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? UnitMrp { get; set; }
        public decimal? PurchaseRate { get; set; }
        public decimal? VatPercentage { get; set; }
        public bool? IsBatchRequired { get; set; }
        public string? BatchNo { get; set; }
        public string? BatchExpDate { get; set; }
        public string? ConversionFactor { get; set; }
        public float? Cgstper { get; set; }
        public float? Sgstper { get; set; }
        public float? Igstper { get; set; }
        public string? ManufactureName { get; set; }
        public bool? IsHighRisk { get; set; }
        public bool? isEmgerency { get; set; }
        public bool? IsLASA { get; set; }
        public bool? IsH1Drug { get; set; }
        public float? GrnRetQty { get; set; }
        public string? ExpDays { get; set; }
        public string? DaysFlag { get; set; }


    }
}
