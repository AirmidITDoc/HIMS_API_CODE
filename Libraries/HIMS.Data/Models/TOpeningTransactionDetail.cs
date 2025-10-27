namespace HIMS.Data.Models
{
    public partial class TOpeningTransactionDetail
    {
        public long OpeningId { get; set; }
        public long? OpeningHeaderId { get; set; }
        public long? StoreId { get; set; }
        public DateTime? OpeningDate { get; set; }
        public DateTime? OpeningTime { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public decimal? PerUnitMrp { get; set; }
        public decimal? PerUnitPurRate { get; set; }
        public decimal? PerUnitLandedRate { get; set; }
        public float? Cgstper { get; set; }
        public float? Sgstper { get; set; }
        public float? Igstper { get; set; }
        public double? Gstper { get; set; }
        public float? Packing { get; set; }
        public float? StripQty { get; set; }
        public float? TotalQty { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
