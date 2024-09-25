using FluentValidation;
using HIMS.API.Models.Inventory;
using System;

namespace HIMS.API.Models.OutPatient
{
    public class OPRefundOfBillModel
    {
        public long RefundId { get; set; }
        public DateTime? RefundDate { get; set; }
        public string? RefundTime { get; set; }
        public string? RefundNo { get; set; }
        public long? BillId { get; set; }
        public long? AdvanceId { get; set; }
        public bool? Opdipdtype { get; set; }
        public long? Opdipdid { get; set; }
        public decimal? RefundAmount { get; set; }
        public string? Remark { get; set; }
        public long? TransactionId { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public List<TRefundDetailModel>? TRefundDetails { get; set; }

    }
    public class OPRefundOfBillModelValidator : AbstractValidator<OPRefundOfBillModel>
        {
            public OPRefundOfBillModelValidator()
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
        public long? AddBy { get; set; }
        public long? ChargesId { get; set; }
        
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


        }
    }
    //public class RefundBillModel
    //{
    //    public OPRefundOfBillModel Refund { get; set; }
    //    public TRefundDetailModel TRefundDetails { get; set; }
    //}
}

