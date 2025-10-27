using FluentValidation;


namespace HIMS.API.Models.Inventory
{
    public class PurchaseOrderModel
    {
        public long PurchaseId { get; set; }
        public string? PurchaseNo { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public DateTime? PurchaseTime { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierId { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? FreightAmount { get; set; }
        public decimal? OctriAmount { get; set; }
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


        public List<IndentDetailModel2> TIndentDetails { get; set; }
    }
    public class PurchaseOrderModelValidator : AbstractValidator<PurchaseOrderModel>
    {
        public PurchaseOrderModelValidator()
        {
            RuleFor(x => x.PurchaseDate).NotNull().NotEmpty().WithMessage("PurchaseDate is required");
            RuleFor(x => x.PurchaseTime).NotNull().NotEmpty().WithMessage("PurchaseTime is required");

        }
    }

    public class IndentDetailModel2
    {
        public long IndentId { get; set; }
        public long ItemId { get; set; }
        public long Qty { get; set; }
    }
    public class IndentDetailModel2Validator : AbstractValidator<IndentDetailModel2>
    {
        public IndentDetailModel2Validator()
        {
            RuleFor(x => x.IndentId).NotNull().NotEmpty().WithMessage("Indent Id is required");
            RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("Item is required");
            RuleFor(x => x.Qty).NotNull().NotEmpty().WithMessage("Qty is required");
        }
    }
}
