using FluentValidation;
using HIMS.Data.Models;

namespace HIMS.API.Models.OPPatient
{
    public class OPSettlementCreditModel
    {
        public int BillNo { get; set; }

        public float BalanceAmt { get; set; }
    }
    public class OPSettlementCreditModelValidator : AbstractValidator<OPSettlementCreditModel>
    {
        public OPSettlementCreditModelValidator()
        {
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
            RuleFor(x => x.BalanceAmt).NotNull().NotEmpty().WithMessage("BillBalAmount is required");

        }
    }
    public class OPSettlementPaymentModel
    {
        public long PaymentId { get; set; }
        public long? BillNo { get; set; }
        public string? ReceiptNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentTime { get; set; }
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
        public long? CashCounterId { get; set; }
        public byte? IsSelfOrcompany { get; set; }
        public long? CompanyId { get; set; }
        public decimal? NeftpayAmount { get; set; }
        public string? Neftno { get; set; }
        public string? NeftbankMaster { get; set; }
        public DateTime? Neftdate { get; set; }
        public decimal? PayTmamount { get; set; }
        public string? PayTmtranNo { get; set; }
        public DateTime? PayTmdate { get; set; }
        public decimal? ChCashPayAmount { get; set; }
        public decimal? ChChequePayAmount { get; set; }
        public decimal? ChCardPayAmount { get; set; }
        public decimal? ChAdvanceUsedAmount { get; set; }
        public decimal? ChNeftpayAmount { get; set; }
        public decimal? ChPayTmamount { get; set; }
        public string? TranMode { get; set; }
        public decimal? Tdsamount { get; set; }

        public virtual Bill? BillNoNavigation { get; set; }
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
