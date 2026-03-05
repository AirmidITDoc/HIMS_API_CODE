using FluentValidation;

namespace HIMS.API.Models.Pharmacy
{
    public class PurchaseRequisitionFinalModel
    {
        public long Prid { get; set; }
        public DateTime? Prdate { get; set; }
        public DateTime? Prtime { get; set; }
        public long? UnitId { get; set; }
        public bool? Priority { get; set; }
        public long? StoreId { get; set; }
        public string? Comments { get; set; }
        public bool? Isclosed { get; set; }
        public bool? Isverify { get; set; }
        public long? IsVerifyById { get; set; }
        public DateTime? IsVerifyDateTime { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }

        public List<PurchaseRequisitionDetailFinalModel> TPrdetails { get; set; }
    }
    public class PurchaseRequisitionFinalModelValidator : AbstractValidator<PurchaseRequisitionFinalModel>
    {
        public PurchaseRequisitionFinalModelValidator()
        {
            RuleFor(x => x.Prdate).NotNull().NotEmpty().WithMessage("PurchaseRequisitionDate  is required");
            RuleFor(x => x.Prtime).NotNull().NotEmpty().WithMessage("PurchaseRequisitionTime  is required");
            //RuleFor(x => x.IsVerifyDateTime).NotNull().NotEmpty().WithMessage("IsVerifyDateTime  is required");
           // RuleFor(x => x.IsCancelledDateTime).NotNull().NotEmpty().WithMessage("IsCancelledDateTime  is required");
        }
    }
    public class PurchaseRequisitionDetailFinalModel
    {
        public long PrdetId { get; set; }
        public long Prid { get; set; }
        public long? FromStoreId { get; set; }
        public long? ToStoreId { get; set; }
        public long? ItemId { get; set; }
        public double? Qty { get; set; }
        public long? PrrequestHeaderId { get; set; }
        public long? PrrequestDetId { get; set; }
    }
    public class PurchaseRequisitionDetailFinalModelValidator : AbstractValidator<PurchaseRequisitionDetailFinalModel>
    {
        public PurchaseRequisitionDetailFinalModelValidator()
        {
            //RuleFor(x => x.VerifiedQty).NotNull().NotEmpty().WithMessage("VerifiedQty  is required");
        }
    }
}
