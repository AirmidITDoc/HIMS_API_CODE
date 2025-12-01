using FluentValidation;
using HIMS.API.Models.OPPatient;

namespace HIMS.API.Models.OutPatient
{
    public class OPCreditPaymentModel
    {

        public long? BillNo { get; set; }
        public long? UnitId { get; set; }
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
        public long? Tdsamount { get; set; }
        public decimal? Wfamount { get; set; }
        public int? OPDIPDType { get; set; }
        public long PaymentId { get; set; }
        public long? CompanyId { get; set; }


        //public List<BilModel> Bill { get; set; }

    }
    public class OPSettlementModelValidator : AbstractValidator<OPCreditPaymentModel>
    {
        public OPSettlementModelValidator()
        {
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
            //RuleFor(x => x.ReceiptNo).NotNull().NotEmpty().WithMessage("ReceiptNo is required");
            RuleFor(x => x.PaymentDate).NotNull().NotEmpty().WithMessage("PaymentDate is required");
            RuleFor(x => x.PaymentTime).NotNull().NotEmpty().WithMessage("PaymentTime is required");

        }
    }
    public class BillUpdateModel
    {
        public long BillNo { get; set; }
        public decimal? BalanceAmt { get; set; }

    }
    public class BilModelValidator : AbstractValidator<BillUpdateModel>
    {
        public BilModelValidator()
        {
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");

        }
    }

    public class OPSettlementMultipleModel
    {
        public List<OPCreditPaymentModel> OPCreditPayment { get; set; }
        public List<BillUpdateModel> BillUpdate { get; set; }
    }
    public class OPSettlementModel
    {
        public OPCreditPaymentModel OPCreditPayment { get; set; }
        public BillUpdateModel BillUpdate { get; set; }
        public List<TPaymentModel> TPayments{ get; set; }

        
    }

}

