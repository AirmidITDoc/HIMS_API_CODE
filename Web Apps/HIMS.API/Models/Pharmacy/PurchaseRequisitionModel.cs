using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Pharmacy
{
    public class PurchaseRequisitionModel
    {
        public long PurchaseRequisitionId { get; set; }
        public DateTime? PurchaseRequisitionDate { get; set; }
        public string? PurchaseRequisitionTime { get; set; }
        public long? UnitId { get; set; }
        public bool? Priority { get; set; }
        public long? FromStoreId { get; set; }
        public long? ToStoreId { get; set; }
        public string? Comments { get; set; }
        public bool? Isclosed { get; set; }
        public bool? Isverify { get; set; }
        public bool? IsInchargeVerify { get; set; }
        public long? IsInchargeVerifyId { get; set; }
        public DateTime? IsInchargeVerifyDate { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
        public long? Addedby { get; set; }
        public bool? IsCancelled { get; set; }
        public List<PurchaseRequisitionDetailModel> TPurchaseRequisitionDetails { get; set; }
    }
    public class PurchaseRequisitionModelValidator : AbstractValidator<PurchaseRequisitionModel>
    {
        public PurchaseRequisitionModelValidator()
        {
            RuleFor(x => x.PurchaseRequisitionDate).NotNull().NotEmpty().WithMessage("PurchaseRequisitionDate  is required");
            RuleFor(x => x.PurchaseRequisitionTime).NotNull().NotEmpty().WithMessage("PurchaseRequisitionTime  is required");
            RuleFor(x => x.IsInchargeVerifyDate).NotNull().NotEmpty().WithMessage("IsInchargeVerifyDate  is required");
            RuleFor(x => x.IsCancelledDateTime).NotNull().NotEmpty().WithMessage("IsCancelledDateTime  is required");


        }
    }
    public class PurchaseRequisitionDetailModel
    {
        public long PurchaseRequisitionDetId { get; set; }
        public long PurchaseRequisitionId { get; set; }
        public long? ItemId { get; set; }
        public double? Qty { get; set; }
        public double? VerifiedQty { get; set; }
        public long? IndQty { get; set; }
        public long? IssQty { get; set; }
        public bool IsClosed { get; set; }
    }
    public class PurchaseRequisitionDetailModelValidator : AbstractValidator<PurchaseRequisitionDetailModel>
    {
        public PurchaseRequisitionDetailModelValidator()
        {
            //RuleFor(x => x.VerifiedQty).NotNull().NotEmpty().WithMessage("VerifiedQty  is required");
        }
    }
    public class PurchaseRequisitionCancel
    {
        public long PurchaseRequisitionId { get; set; }
        public long? IsCancelledBy { get; set; }

    }
    public class PurchaseRequisitionVarifyModel
    {
        public long PurchaseRequisitionId { get; set; }
        public long? IsInchargeVerifyId { get; set; }
    }
}
