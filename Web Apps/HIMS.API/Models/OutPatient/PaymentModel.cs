using FluentValidation;

namespace HIMS.API.Models.OutPatient
{
    public class PaymentModel1
    {
        public long PaymentId { get; set; }
        public int? BillNo { get; set; }
        //public string? ReceiptNo { get; set; }
        public string? PaymentDate { get; set; }
        public string? PaymentTime { get; set; }
        public float? CashPayAmount { get; set; }
        public float? ChequePayAmount { get; set; }
        public string? ChequeNo { get; set; }
        public string? BankName { get; set; }
        public string? ChequeDate { get; set; }
        public float? CardPayAmount { get; set; }
        public string? CardNo { get; set; }
        public string? CardBankName { get; set; }
        public string? CardDate { get; set; }
        public float? AdvanceUsedAmount { get; set; }
        public long? AdvanceId { get; set; }
        public long? RefundId { get; set; }
        public long? TransactionType { get; set; }
        public string? Remark { get; set; }
        //public long? AddBy { get; set; }
        //public bool? IsCancelled { get; set; }
        //public long? IsCancelledBy { get; set; }
        //public string? IsCancelledDate { get; set; }
        //public long? CashCounterId { get; set; }
        //public byte? IsSelfOrcompany { get; set; }
        //public long? CompanyId { get; set; }
        public float? NeftpayAmount { get; set; }
        public string? Neftno { get; set; }
        public string? NeftbankMaster { get; set; }
        public string? Neftdate { get; set; }
        public float? PayTmamount { get; set; }
        public string? PayTmtranNo { get; set; }
        public string? PayTmdate { get; set; }
        //public decimal? ChCashPayAmount { get; set; }
        //public decimal? ChChequePayAmount { get; set; }
        //public decimal? ChCardPayAmount { get; set; }
        //public decimal? ChAdvanceUsedAmount { get; set; }
        //public decimal? ChNeftpayAmount { get; set; }
        //public decimal? ChPayTmamount { get; set; }
        //public string? TranMode { get; set; }
        public float? Tdsamount { get; set; }
    }

    public class PaymentModel1Validator : AbstractValidator<PaymentModel1>
    {
        public PaymentModel1Validator()
        {
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
            
          
        }
    }
}
