using FluentValidation;

namespace HIMS.API.Models.OutPatient
{
    public class OPSettlementCreditModel
    {
        public int BillNo { get; set; }

        public float BillBalAmount { get; set; }
    }
    public class OPSettlementCreditModelValidator : AbstractValidator<OPSettlementCreditModel>
    {
        public OPSettlementCreditModelValidator()
        {
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
            RuleFor(x => x.BillBalAmount).NotNull().NotEmpty().WithMessage("BillBalAmount is required");
          
        }
    }
    public class OPSettlementPaymentModel
    {
        public int PaymentId { get; set; }
        public int? BillNo { get; set; }
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
        public bool? IsCancelled { get; set; }
        public int? IsCancelledBy { get; set; }
        public string? IsCancelledDate { get; set; }
        public long? NEFTPayAmount { get; set; }

        public bool? OPD_IPD_Type { get; set; }
        public string? NEFTNo { get; set; }
        public string? NEFTBankMaster { get; set; }
        public string? NEFTDate { get; set; }
        public long? PayTMAmount { get; set; }
        public string? PayTMTranNo { get; set; }
        public string? PayTMDate { get; set; }
        //public long? TDSAmount { get; set; }
    }

    public class OPSettlementPaymentModelValidator : AbstractValidator<OPSettlementPaymentModel>
    {
        public OPSettlementPaymentModelValidator()
        {
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
        }
    }

    public class OPSettlementpayment
    {
        public OPSettlementCreditModel OPSettlementCredit { get; set; }
        public OPSettlementPaymentModel OPSettlementPayment { get; set; }
    }
}
