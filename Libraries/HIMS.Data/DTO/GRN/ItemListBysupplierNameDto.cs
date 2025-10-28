namespace HIMS.Data.DTO.GRN
{
    public class ItemListBysupplierNameDto
    {
        public long GRNID { get; set; }
        public string? GrnNumber { get; set; }
        public string GRNDate { get; set; }
        public string? GRNTime { get; set; }
        public long? GRNDetID { get; set; }
        public long? ItemId { get; set; }
        public string? ItemName { get; set; }
        public string? BatchNo { get; set; }
        public string? BatchExpDate { get; set; }
        public long? UOMId { get; set; }
        public decimal? Mrp { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TotalAmount { get; set; }
        public long? ConversionFactor { get; set; }
        public double? VatPer { get; set; }
        public decimal? VatAmount { get; set; }
        public double? DiscPercentage { get; set; }
        public double? OtherTax { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public double? BalanceQty { get; set; }
        public float? TotalQty { get; set; }
        public bool? IsBatchRequired { get; set; }
        public long? StoreId { get; set; }
        public long? StkId { get; set; }
        public long ReturnQty { get; set; }
        public float? ReceiveQty { get; set; }



    }
}
