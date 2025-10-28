namespace HIMS.Data.Models
{
    public partial class TGrnreturnDetail
    {
        public long GrnreturnDetailId { get; set; }
        public long? GrnreturnId { get; set; }
        public long? Grnid { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpiryDate { get; set; }
        public float? ReturnQty { get; set; }
        public short? Conversion { get; set; }
        public float? Cf { get; set; }
        public float? TotalQty { get; set; }
        public float? DiscPercentage { get; set; }
        public decimal? DiscAmount { get; set; }
        public float? Gstpercentage { get; set; }
        public decimal? Gstamount { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? LandedTotalAmount { get; set; }
        public decimal? Mrp { get; set; }
        public decimal? MrptotalAmount { get; set; }
        public decimal? UnitPurchaseRate { get; set; }
        public decimal? PurchaseTotalAmount { get; set; }
        public float? Cgstper { get; set; }
        public float? Sgstper { get; set; }
        public float? Igstper { get; set; }
        public string? Remarks { get; set; }
        public long? StkId { get; set; }

        public virtual TGrnreturnHeader? Grnreturn { get; set; }
    }
}
