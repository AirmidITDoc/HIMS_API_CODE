using FluentValidation;

namespace HIMS.API.Models.Pharmacy
{
    public class PurchaseModel
    {
        public long PurchaseId { get; set; }
        public string? PurchaseNo { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierId { get; set; }     
        public decimal? TotalAmount { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? TaxAmount { get; set; }
        public decimal? FreightAmount { get; set; }
        public decimal? OctriAmount { get; set; }
        public decimal? GrandTotal { get; set; }
        public bool Isclosed { get; set; }
        public bool IsVerified { get; set; }
        public string? Remarks { get; set; }
        public long? TaxId { get; set; }
        public long? PaymentTermId { get; set; }
        public long? ModeofPayment { get; set; }
        public string? Worrenty { get; set; }
        public decimal? RoundVal { get; set; }
        public decimal? TotCgstamt { get; set; }
        public decimal? TotSgstamt { get; set; }
        public decimal? TotIgstamt { get; set; }
        public decimal? TransportChanges { get; set; }
        public decimal? HandlingCharges { get; set; }
        public decimal? FreightCharges { get; set; }
        public List<PurchaseDetailModel> TPurchaseDetails { get; set; }

    }
    public class PurchaseModelValidator : AbstractValidator<PurchaseModel>
    {
        public PurchaseModelValidator()
        {
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("Store is required");
            RuleFor(x => x.SupplierId).NotNull().NotEmpty().WithMessage("Supplier is required");
            //RuleFor(x => x.TPurchaseDetails).NotNull().NotEmpty().WithMessage("Purchase items is required");
        }
    }
}
