using FluentValidation;
using HIMS.Data.Models;


namespace HIMS.API.Models.IPPatient
{
    public class AdvanceModel
    {
        public DateTime? Date { get; set; }
        public long? RefId { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public double? AdvanceAmount { get; set; }
        public double? AdvanceUsedAmount { get; set; }
        public double? BalanceAmount { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long AdvanceId { get; set; }
    }
    public class AdvanceModelValidator : AbstractValidator<AdvanceModel>
    {
        public AdvanceModelValidator()
        {
            RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("Date is required");
            RuleFor(x => x.IsCancelledDate).NotNull().NotEmpty().WithMessage("IsCancelledDate is required");

        }
    }
    public class AdvanceDetailModel
    {
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public long? AdvanceId { get; set; }
        public long? RefId { get; set; }
        public long? TransactionId { get; set; }
        public long? OpdIpdId { get; set; }
        public byte? OpdIpdType { get; set; }
        public decimal? AdvanceAmount { get; set; }
        public decimal? UsedAmount { get; set; }
        public decimal? BalanceAmount { get; set; }
        public decimal? RefundAmount { get; set; }
        public long? ReasonOfAdvanceId { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledby { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public string? Reason { get; set; }
        public long AdvanceDetailId { get; set; }

    }
    public class AdvanceDetailModelValidator : AbstractValidator<AdvanceDetailModel>
    {
        public AdvanceDetailModelValidator()
        {
            RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("Date is required");
            RuleFor(x => x.Time).NotNull().NotEmpty().WithMessage("Time is required");

        }
    }
    public class AdvancePayment
    {
        public long? BillNo { get; set; }
        public string? ReceiptNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaymentTime { get; set; }
        public decimal? CashPayAmount { get; set; }
        public decimal? ChequePayAmount { get; set; }
        public string? ChequeNo { get; set; }
        public string? BankName { get; set; }
        public DateTime ChequeDate { get; set; }
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
        public DateTime IsCancelledDate { get; set; }
        public decimal? NeftpayAmount { get; set; }
        public string? Neftno { get; set; }
        public string? NeftbankMaster { get; set; }
        public DateTime? Neftdate { get; set; }
        public decimal? PayTmamount { get; set; }
        public string? PayTmtranNo { get; set; }
        public DateTime PayTmdate { get; set; }
        public decimal TDSAmount { get; set; }
    }

    public class AdvancePaymentValidator : AbstractValidator<AdvancePayment>
    {
        public AdvancePaymentValidator()
        {
            RuleFor(x => x.AdvanceId).NotNull().NotEmpty().WithMessage("AdvanceId is required");
        }
    }

    public class ModelAdvance1
    {
        public AdvanceModel Advance { get; set; }
        public AdvanceDetailModel AdvanceDetail { get; set; }
        public AdvancePayment AdvancePayment { get; set; }
    }

    public class UpdateAdvanceModel
    {
        public long AdvanceId {  get; set; }   
        public double? AdvanceAmount {  get; set; }
    }
    public class UpdateAdvanceModelValidator : AbstractValidator<UpdateAdvanceModel>
    {
        public UpdateAdvanceModelValidator()
        {
            RuleFor(x => x.AdvanceId).NotNull().NotEmpty().WithMessage("AdvanceId is required");
            RuleFor(x => x.AdvanceAmount).NotNull().NotEmpty().WithMessage("AdvanceAmount is required");

        }
    }
        public class AdvanceDetailModel2
        {
            public DateTime Date { get; set; }
            public string? Time { get; set; }
            public long? AdvanceId { get; set; }
            public long? RefId { get; set; }
            public long? TransactionId { get; set; }
            public long? OpdIpdId { get; set; }
            public byte? OpdIpdType { get; set; }
            public decimal? AdvanceAmount { get; set; }
            public decimal? UsedAmount { get; set; }
            public decimal? BalanceAmount { get; set; }
            public decimal? RefundAmount { get; set; }
            public long? ReasonOfAdvanceId { get; set; }
            public long? AddedBy { get; set; }
            public bool? IsCancelled { get; set; }
            public long? IsCancelledby { get; set; }
            public DateTime IsCancelledDate { get; set; }
            public string? Reason { get; set; }
            public long AdvanceDetailId { get; set; }

        }
    public class UpdateAdvanceModel2Validator : AbstractValidator<AdvanceDetailModel2>
    {
        public UpdateAdvanceModel2Validator()
        {
            RuleFor(x => x.AdvanceId).NotNull().NotEmpty().WithMessage("AdvanceId is required");
            RuleFor(x => x.AdvanceAmount).NotNull().NotEmpty().WithMessage("AdvanceAmount is required");

        }
    }
    public class UpdateAdvance
    {
        public UpdateAdvanceModel Advance { get; set; }
        public AdvanceDetailModel2 AdvanceDetail { get; set; }
        public AdvancePayment AdvancePayment { get; set; }
    }
    public class UpdateCancel
    {
        public bool IsCancelled { get; set; }
        public long AdvanceId { get; set; }
        public long AdvanceDetailId { get; set; }
        public long UserId { get; set; }
        public double AdvanceAmount { get; set; }


    }
   
}







