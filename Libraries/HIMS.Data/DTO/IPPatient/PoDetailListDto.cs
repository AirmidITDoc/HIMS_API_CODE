namespace HIMS.Data.DTO.IPPatient
{
    public class PoDetailListDto
    {
        public long PurchaseId { get; set; }
        public string? PurchaseNo { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public long? SupplierId { get; set; }
        public decimal? TransportChanges { get; set; }
        public decimal? HandlingCharges { get; set; }
        public decimal? FreightCharges { get; set; }
        public double? OctriAmount { get; set; }
        public long? IsVerifiedId { get; set; }
        public DateTime? VerifiedDateTime { get; set; }
        public DateTime? PurchaseTime { get; set; }
        public long? ItemId { get; set; }
        public long? Uomid { get; set; }
        public String? ItemName { get; set; }
        public String? UnitofMeasurementName { get; set; }
        public double? Qty { get; set; }
        public double? Rate { get; set; }
        public double? ItemTotalAmount { get; set; }
        public double? ItemDiscAmount { get; set; }
        public double? DiscPer { get; set; }
        public double? VatAmount { get; set; }
        public double? VatPer { get; set; }
        public double? GrandTotalAmount { get; set; }
        public double? Mrp { get; set; }
        public string? Specification { get; set; }
        public double? GrossAmount { get; set; }
        public double? LandedRate { get; set; }
        public long PurDetId { get; set; }
        public double? POQty { get; set; }
        public double? POBalQty { get; set; }
        public double? Cgstper { get; set; }
        public decimal? Cgstamt { get; set; }
        public double? Sgstper { get; set; }
        public decimal? Sgstamt { get; set; }
        public double? Igstper { get; set; }
        public decimal? Igstamt { get; set; }
        public string? HSNcode { get; set; }
        public string? ConversionFactor { get; set; }
        public string? SupplierName { get; set; }




    }
}
