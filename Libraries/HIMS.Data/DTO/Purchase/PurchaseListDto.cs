namespace HIMS.Data.DTO.Purchase
{
    public class PurchaseListDto
    {
        public long PurchaseID { get; set; }
        public String? PurchaseNo { get; set; }
        public string? PDate { get; set; }
        public string? PurchaseTime { get; set; }
        public long? StoreId { get; set; }
        public string? StoreName { get; set; }
        public long? SupplierID { get; set; }
        public string? SupplierName { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public double? FreightAmount { get; set; }
        public double? OctriAmount { get; set; }
        public decimal? GrandTotal { get; set; }
        public bool? Isclosed { get; set; }
        public bool? IsVerified { get; set; }

        public string? Remarks { get; set; }
        public long? AddedBy { get; set; }
        public string? AddedByName { get; set; }
        public long? UpdatedBy { get; set; }
        public string? VerifiedDateTime { get; set; }
        public decimal? TransportChanges { get; set; }
        public decimal? HandlingCharges { get; set; }

        public long? PaymentTermId { get; set; }
        public long? ModeOfPayment { get; set; }
        public String? Worrenty { get; set; }

        public String? Email { get; set; }
    }
}
