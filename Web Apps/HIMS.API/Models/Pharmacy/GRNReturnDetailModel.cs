namespace HIMS.API.Models.Pharmacy
{
    public class GRNReturnDetailModel
    {
        public long? GrnreturnId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime BatchExpiryDate { get; set; }
        public float? ReturnQty { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? Mrp { get; set; }
        public decimal? UnitPurchaseRate { get; set; }
        public float? GSTPercentage { get; set; }
        public decimal? GSTAmount { get; set; }
        public decimal? LandedTotalAmount { get; set; }
        public decimal? MrptotalAmount { get; set; }
        public decimal? PurchaseTotalAmount { get; set; }
        public short? Conversion { get; set; }
        public string? Remarks { get; set; }
        public long? StkId { get; set; }
        public float? Cf { get; set; }
        public float? TotalQty { get; set; }
        public long? Grnid { get; set; }
        public float? Cgstper { get; set; }
        public float? Sgstper { get; set; }
        public float? Igstper { get; set; }
        public float? DiscPercentage { get; set; }
        public decimal? DiscAmount { get; set; }
    }
}
