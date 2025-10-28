namespace HIMS.Data.Models
{
    public partial class TPurchaseHeader
    {
        public TPurchaseHeader()
        {
            TPurchaseDetails = new HashSet<TPurchaseDetail>();
        }

        public long PurchaseId { get; set; }
        public string? PurchaseNo { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? PurchaseTime { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierId { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public double? FreightAmount { get; set; }
        public double? OctriAmount { get; set; }
        public decimal? GrandTotal { get; set; }
        public bool? Isclosed { get; set; }
        public bool? IsVerified { get; set; }
        public string? Remarks { get; set; }
        public long? PaymentTermId { get; set; }
        public long? TaxId { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? ModeOfPayment { get; set; }
        public string? Worrenty { get; set; }
        public double? RoundVal { get; set; }
        public string? Prefix { get; set; }
        public long? IsVerifiedId { get; set; }
        public DateTime? VerifiedDateTime { get; set; }
        public decimal? TotCgstamt { get; set; }
        public decimal? TotSgstamt { get; set; }
        public decimal? TotIgstamt { get; set; }
        public bool? IsInchVerified { get; set; }
        public long? IsVerifiedInchId { get; set; }
        public DateTime? InchVerifiedDateTime { get; set; }
        public decimal? TransportChanges { get; set; }
        public decimal? HandlingCharges { get; set; }
        public decimal? FreightCharges { get; set; }
        public bool? IsCancelled { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<TPurchaseDetail> TPurchaseDetails { get; set; }
    }
}
