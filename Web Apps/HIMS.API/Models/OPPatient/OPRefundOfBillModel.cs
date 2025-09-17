using FluentValidation;
using HIMS.API.Models.Inventory;
using System;

namespace HIMS.API.Models.OPPatient
{
    public class OPRefundOfBillModel
    {
        public DateTime? RefundDate { get; set; }
        public string? RefundTime { get; set; }
        public string? RefundNo { get; set; }
        public long? BillId { get; set; }
        public long? AdvanceId { get; set; }
        public long? Opdipdtype { get; set; }
        public long? Opdipdid { get; set; }
        public decimal? RefundAmount { get; set; }
        public string? Remark { get; set; }
        public long? TransactionId { get; set; }
        public long AddedBy { get; set; }
        public int IsCancelled { get; set; }
        public long IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long RefundId { get; set; }
       

    }
    public class OPRefundOfBillModelValidator : AbstractValidator<OPRefundOfBillModel>
    {
        public OPRefundOfBillModelValidator()
        {
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



    public class AddChargesModell
    {
        public long ChargesId { get; set; }

        public decimal? RefundAmount { get; set; }
    }
    public class AddChargesModellValidator : AbstractValidator<AddChargesModell>
    {
        public AddChargesModellValidator()
        {
            RuleFor(x => x.RefundAmount).NotNull().NotEmpty().WithMessage("RefundAmount  is required");

        }
    }

    public class PaymentModell
    {
        public long? BillNo { get; set; }
        public string? ReceiptNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentTime { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public string? ChequeNo { get; set; }
        public string? BankName { get; set; }
        public DateTime? ChequeDate { get; set; }
        public decimal? CardPayAmount { get; set; }
        public string? CardNo { get; set; }
        public string? CardBankName { get; set; }
        public DateTime? CardDate { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public long? AdvanceId { get; set; }
        public long? RefundId { get; set; }
        public long? TransactionType { get; set; }
        public string? Remark { get; set; }
        public long? AddBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public decimal? NeftpayAmount { get; set; }
        public string? Neftno { get; set; }
        public string? NeftbankMaster { get; set; }
        public DateTime? Neftdate { get; set; }
        public decimal? PayTmamount { get; set; }
        public string? PayTmtranNo { get; set; }
        public DateTime? PayTmdate { get; set; }
        public decimal? Tdsamount { get; set; }
        public decimal? WFAmount{ get; set; }
        public long? UnitId { get; set; }
    }
    public class PaymentModellValidator : AbstractValidator<PaymentModell>
    {
        public PaymentModellValidator()
        {
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");

        }
    }
    public class RefundBillModel
    {
        public OPRefundOfBillModel Refund { get; set; }
        public List<TRefundDetailModel> TRefundDetails { get; set; }
        public List<AddChargesModell> AddCharges { get; set; }
        public PaymentModell Payment { get; set; }
    }
}

