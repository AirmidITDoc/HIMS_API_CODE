namespace HIMS.Data.DTO.IPPatient
{
    public class ItemDetailsForGRNUpdateListDto
    {
        public long ItemId { get; set; }
        public string? ItemName { get; set; }
        public long UOMId { get; set; }
        public string? UnitofMeasurementName { get; set; }
        public float? ReceiveQty { get; set; }
        public float? FreeQty { get; set; }
        public decimal UnitMRP { get; set; }
        public decimal? MRP { get; set; }
        public decimal MRP_Strip { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TotalAmount { get; set; }
        public long? ConversionFactor { get; set; }
        public double? VatPercentage { get; set; }
        public decimal? VatAmount { get; set; }
        public double? DiscPercentage { get; set; }
        public decimal? DiscAmount { get; set; }
        public double? OtherTax { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? NetAmount { get; set; }
        public decimal? GrossAmount { get; set; }
        public float? TotalQty { get; set; }
        public long PONo { get; set; }
        public long GRNDetID { get; set; }
        public string? BatchNo { get; set; }
        public string? BatchExpDate { get; set; }
        public long PurDetId { get; set; }
        public double POQty { get; set; }
        public long PurchaseId { get; set; }
        public double POBalQty { get; set; }
        public decimal? PurUnitRate { get; set; }
        public decimal? PurUnitRateWf { get; set; }
        public double? Cgstper { get; set; }
        public decimal? Cgstamt { get; set; }
        public double? Sgstper { get; set; }
        public decimal? Sgstamt { get; set; }
        public double? Igstper { get; set; }
        public decimal? Igstamt { get; set; }
        public string? Hsncode { get; set; }
        public long StkID { get; set; }
        public bool Cash_CreditType { get; set; }
        public bool GRNType { get; set; }
        public long SupplierId { get; set; }
        public long StoreId { get; set; }
        public bool? IsVerified { get; set; }
        public DateTime IsVerifiedDatetime { get; set; }
        public long IsVerifiedUserId { get; set; }
        public double? DiscPerc2 { get; set; }
        public decimal? DiscAmt2 { get; set; }
    }
}
