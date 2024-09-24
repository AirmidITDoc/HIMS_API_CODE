using FluentValidation;

namespace HIMS.API.Models.OutPatient
{
    public class IPRefundOfBillModel
    {
        public long RefundId { get; set; }
        public DateTime? RefundDate { get; set; }
        public DateTime? RefundTime { get; set; }
        public string? RefundNo { get; set; }
        public long? BillId { get; set; }
        public long? AdvanceId { get; set; }
        public int? Opdipdtype { get; set; }
        public long? Opdipdid { get; set; }
        public decimal? RefundAmount { get; set; }
        public string? Remark { get; set; }
        public int? TransactionId { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public bool? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? CashCounterId { get; set; }
        public int? IsRefundFlag { get; set; }


    }
    public class IPRefundOfBillModelValidator : AbstractValidator<IPRefundOfBillModel>
    {
        public IPRefundOfBillModelValidator()
        {
            RuleFor(x => x.RefundDate).NotNull().NotEmpty().WithMessage("RefundDate Date is required");
            RuleFor(x => x.RefundTime).NotNull().NotEmpty().WithMessage("RefundTime Time is required");
            RuleFor(x => x.RefundNo).NotNull().NotEmpty().WithMessage(" RefundNo is required");
            RuleFor(x => x.BillId).NotNull().NotEmpty().WithMessage(" BillId is required");
            RuleFor(x => x.AdvanceId).NotNull().NotEmpty().WithMessage(" AdvanceId is required");
            RuleFor(x => x.Opdipdtype).NotNull().NotEmpty().WithMessage(" Opdipdtype is required");
            RuleFor(x => x.Opdipdid).NotNull().NotEmpty().WithMessage("Opdipdid is required");
            RuleFor(x => x.RefundAmount).NotNull().NotEmpty().WithMessage(" RefundAmount is required");


        }
    }
    public class IPTRefundDetailModel
    {
        public long RefundDetId { get; set; }
        public long? RefundId { get; set; }
        public long? ServiceId { get; set; }
        public decimal? ServiceAmount { get; set; }
        public decimal? RefundAmount { get; set; }
        public long? DoctorId { get; set; }
        public string? Remark { get; set; }
        public long? ChargesId { get; set; }
        public decimal? HospitalAmount { get; set; }
        public decimal? DoctorAmount { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? RefundDetailsTime { get; set; }

    }
    public class IPTRefundDetailModelValidator : AbstractValidator<IPTRefundDetailModel>
    {
        public IPTRefundDetailModelValidator()
        {
            RuleFor(x => x.ServiceId).NotNull().NotEmpty().WithMessage("ServiceId Date is required");
            RuleFor(x => x.ServiceAmount).NotNull().NotEmpty().WithMessage("ServiceAmount Time is required");
            RuleFor(x => x.RefundAmount).NotNull().NotEmpty().WithMessage(" RefundAmount is required");
            RuleFor(x => x.DoctorId).NotNull().NotEmpty().WithMessage(" DoctorId is required");
            RuleFor(x => x.Remark).NotNull().NotEmpty().WithMessage(" Remark is required");
            RuleFor(x => x.ChargesId).NotNull().NotEmpty().WithMessage(" ChargesId is required");


        }
    }
    public class IPRefundBillModel
    {
        public OPRefundOfBillModel IPRefund { get; set; }
        public TRefundDetailModel IPRefundDetail { get; set; }
    }
}
