using FluentValidation;

namespace HIMS.API.Models.OutPatient
{
    public class RefundOfBillModel
    {
        public long RefundId { get; set; }
        public string? RefundDate { get; set; }
        public string? RefundTime { get; set; }
        public string? RefundNo { get; set; }
        public long? BillId { get; set; }
        public long? AdvanceId { get; set; }
        public int? Opdipdtype { get; set; }
        public long? Opdipdid { get; set; }
        public float? RefundAmount { get; set; }
        public string? Remark { get; set; }
        public int? TransactionId { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public bool? IsCancelledBy { get; set; }
        public string? IsCancelledDate { get; set; }
        public long? CashCounterId { get; set; }
        public bool? IsRefundFlag { get; set; }

    }
        public class RefundOfBillModelValidator : AbstractValidator<RefundOfBillModel>
        {
            public RefundOfBillModelValidator()
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
    public class TRefundDetailModel
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
        public string? RefundDetailsTime { get; set; }

    }
    public class TRefundDetailModelValidator : AbstractValidator<TRefundDetailModel>
    {
        public TRefundDetailModelValidator()
        {
            RuleFor(x => x.ServiceId).NotNull().NotEmpty().WithMessage("ServiceId Date is required");
            RuleFor(x => x.ServiceAmount).NotNull().NotEmpty().WithMessage("ServiceAmount Time is required");
            RuleFor(x => x.RefundAmount).NotNull().NotEmpty().WithMessage(" RefundAmount is required");
            RuleFor(x => x.DoctorId).NotNull().NotEmpty().WithMessage(" DoctorId is required");
            RuleFor(x => x.Remark).NotNull().NotEmpty().WithMessage(" Remark is required");
            RuleFor(x => x.ChargesId).NotNull().NotEmpty().WithMessage(" ChargesId is required");
            RuleFor(x => x.HospitalAmount).NotNull().NotEmpty().WithMessage("HospitalAmount is required");
            RuleFor(x => x.DoctorAmount).NotNull().NotEmpty().WithMessage(" DoctorAmount is required");


        }
    }
    public class RefundBillModel
    {
        public RefundOfBillModel Refund { get; set; }
        public TRefundDetailModel RefundDetail { get; set; }
    }
}

