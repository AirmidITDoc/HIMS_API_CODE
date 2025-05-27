using FluentValidation;

namespace HIMS.API.Controllers.Pharmacy
{
    public class PharmacyRefundModel
    {
        public DateTime? RefundDate { get; set; }
        public string? RefundTime { get; set; }
        public long? BillId { get; set; }
        public long? AdvanceId { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public decimal? RefundAmount { get; set; }
        public string? Remark { get; set; }
        public byte? TransactionId { get; set; }
        public long? AddBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? StrId { get; set; }
        public long RefundId { get; set; }

    }
    public class PharmacyRefundModelValidator : AbstractValidator<PharmacyRefundModel>
    {
        public PharmacyRefundModelValidator()
        {
            RuleFor(x => x.RefundDate).NotNull().NotEmpty().WithMessage("RefundDate is required");
            RuleFor(x => x.RefundTime).NotNull().NotEmpty().WithMessage("RefundTime is required");
            RuleFor(x => x.BillId).NotNull().NotEmpty().WithMessage("BillId is required");
            RuleFor(x => x.AdvanceId).NotNull().NotEmpty().WithMessage("AdvanceId is required");
            RuleFor(x => x.RefundAmount).NotNull().NotEmpty().WithMessage("RefundAmount is required");
            RuleFor(x => x.TransactionId).NotNull().NotEmpty().WithMessage("TransactionId is required");
        }
    }
    public class PhAdvanceHeaderModel
    {
        public long AdvanceId { get; set; }
        public double? AdvanceUsedAmount { get; set; }
        public double? BalanceAmount { get; set; }

    }
    public class PHAdvRefundDetailModel
    {
        public double? AdvDetailId { get; set; }
        public DateTime? RefundDate { get; set; }
        public string? RefundTime { get; set; }
        public double? AdvRefundAmt { get; set; }

     

    }


    public class PharRefundModel
    {
        public PharmacyRefundModel PharmacyRefund {  get; set; }
        public PhAdvanceHeaderModel PhAdvanceHeader { get; set; }
        public List<PHAdvRefundDetailModel> PHAdvRefundDetail { get; set; }


    }

}
