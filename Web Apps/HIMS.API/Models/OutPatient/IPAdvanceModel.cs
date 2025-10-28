using FluentValidation;

namespace HIMS.API.Models.OutPatient
{
    public class IPAdvanceHeaderModel
    {
        public long AdvanceId { get; set; }
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

        //  public virtual ICollection<AdvanceDetail> AdvanceDetails { get; set; }

    }

    public class IPAdvanceModelValidator : AbstractValidator<IPAdvanceHeaderModel>
    {
        public IPAdvanceModelValidator()
        {
            RuleFor(x => x.OpdIpdId).NotNull().NotEmpty().WithMessage("OpdIpdId is required");

        }
    }


    public class IPAdvanceDetail
    {
        public long AdvanceDetailId { get; set; }
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public long? AdvanceId { get; set; }
        public string? AdvanceNo { get; set; }
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
    }

    public class IPAdvanceDetailValidator : AbstractValidator<IPAdvanceDetail>
    {
        public IPAdvanceDetailValidator()
        {
            RuleFor(x => x.AdvanceId).NotNull().NotEmpty().WithMessage("AdvanceId is required");

        }
    }


    public class AdvancePaymentModel
    {
        public int PaymentId { get; set; }
        public int? BillNo { get; set; }
        public string? ReceiptNo { get; set; }
        public string? PaymentDate { get; set; }
        public string? PaymentTime { get; set; }
        public long? CashPayAmount { get; set; }
        public long? ChequePayAmount { get; set; }
        public string? ChequeNo { get; set; }
        public string? BankName { get; set; }
        public string? ChequeDate { get; set; }
        public long? CardPayAmount { get; set; }
        public string? CardNo { get; set; }
        public string? CardBankName { get; set; }
        public string? CardDate { get; set; }
        public long? AdvanceUsedAmount { get; set; }
        public int? AdvanceId { get; set; }
        public int? RefundId { get; set; }
        public int? TransactionType { get; set; }
        public string? Remark { get; set; }
        public int? AddBy { get; set; }
        public bool IsCancelled { get; set; }
        public int? IsCancelledBy { get; set; }
        public string? IsCancelledDate { get; set; }
        public long? NeftpayAmount { get; set; }
        public string? Neftno { get; set; }
        public string? NeftbankMaster { get; set; }
        public string? Neftdate { get; set; }
        public long? PayTmamount { get; set; }
        public string? PayTmtranNo { get; set; }
        public string? PayTmdate { get; set; }
        public long? Tdsamount { get; set; }
    }

    public class AdvancePaymentModelValidator : AbstractValidator<AdvancePaymentModel>
    {
        public AdvancePaymentModelValidator()
        {
            RuleFor(x => x.AdvanceId).NotNull().NotEmpty().WithMessage("AdvanceId is required");
        }
    }

    public class NewIPAdvance
    {

        public IPAdvanceHeaderModel IPAdvanceHeader { get; set; }
        public IPAdvanceDetail IPAdvanceDetail { get; set; }
        public AdvancePaymentModel IPPayments { get; set; }
    }
}
