using FluentValidation;

namespace HIMS.API.Models.Inventory
{
    public class SupplierPaymentModel
    {
        public long Grnid { get; set; }

        public decimal? PaidAmount { get; set; }
        public decimal? BalAmount { get; set; }


        public List<TGrnsupPaymentModel>? TGrnsupPayments { get; set; }
        public List<TSupPayDetModel>? TSupPayDets { get; set; }
    }
    public class SupplierPaymentModelValidator : AbstractValidator<SupplierPaymentModel>
    {
        public SupplierPaymentModelValidator()
        {
            RuleFor(x => x.PaidAmount).NotNull().NotEmpty().WithMessage("PaidAmount is required");
            RuleFor(x => x.BalAmount).NotNull().NotEmpty().WithMessage("BalAmount  is required");


        }
    }
    public class TGrnsupPaymentModel
    {
        public long SupPayId { get; set; }
        public DateTime? SupPayDate { get; set; }
        public DateTime? SupPayTime { get; set; }
        public string? SupPayNo { get; set; }
        public long? GrnId { get; set; }
        public decimal? CashPayAmt { get; set; }
        public decimal? ChequePayAmt { get; set; }
        public DateTime? ChequePayDate { get; set; }
        public string? ChequeBankName { get; set; }
        public string? ChequeNo { get; set; }
        public string? Remarks { get; set; }
        public long? IsAddedBy { get; set; }
        public long? IsUpdatedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }

        public string? PartyReceiptNo { get; set; }
        public decimal? NeftpayAmount { get; set; }
        public string? Neftno { get; set; }
        public string? NeftbankMaster { get; set; }

        public decimal? PayTmamount { get; set; }
        public string? PayTmtranNo { get; set; }
        //public DateTime? PayTmdate { get; set; }
    }
    public class TGrnsupPaymentModelValidator : AbstractValidator<TGrnsupPaymentModel>
    {
        public TGrnsupPaymentModelValidator()
        {
            RuleFor(x => x.SupPayNo).NotNull().NotEmpty().WithMessage("SupPayNo is required");
            RuleFor(x => x.ChequeNo).NotNull().NotEmpty().WithMessage("ChequeNo  is required");

        }
    }
    public class TSupPayDetModel
    {
        public long SupTranId { get; set; }
        public long? SupPayId { get; set; }
        public long? SupGrnId { get; set; }
    }
    public class TSupPayDetModelValidator : AbstractValidator<TSupPayDetModel>
    {
        public TSupPayDetModelValidator()
        {
            RuleFor(x => x.SupGrnId).NotNull().NotEmpty().WithMessage("SupGrnId is required");


        }
    }
}
