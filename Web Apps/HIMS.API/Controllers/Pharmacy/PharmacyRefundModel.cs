using FluentValidation;

namespace HIMS.API.Controllers.Pharmacy
{
    public class PharmacyRefundModel
    {
        public DateTime? RefundDate { get; set; }
        public string? RefundTime { get; set; }
        public long? BillId { get; set; }
        public long? AdvanceId { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public decimal? RefundAmount { get; set; }
        public string? Remark { get; set; }
        public byte? TransactionId { get; set; }
        public long? AddBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? StrId { get; set; }
        public long RefundId { get; set; }

    }
    public class PharmacyRefundModelValidator : AbstractValidator<PharmacyRefundModel>
    {
        public PharmacyRefundModelValidator()
        {
            RuleFor(x => x.RefundDate).NotNull().NotEmpty().WithMessage("RefundDate is required");
            RuleFor(x => x.RefundTime).NotNull().NotEmpty().WithMessage("RefundTime is required");
            RuleFor(x => x.BillId).NotNull().NotEmpty().WithMessage("BillId is required");
            RuleFor(x => x.AdvanceId).NotNull().NotEmpty().WithMessage("AdvanceId is required");
            RuleFor(x => x.RefundAmount).NotNull().NotEmpty().WithMessage("RefundAmount is required");
            RuleFor(x => x.TransactionId).NotNull().NotEmpty().WithMessage("TransactionId is required");
        }
    }
    public class PhAdvanceHeaderModel
    {
        public long AdvanceId { get; set; }
        public double? AdvanceUsedAmount { get; set; }
        public double? BalanceAmount { get; set; }

    }
    public class PHAdvRefundDetailModel
    {
        public double? AdvDetailId { get; set; }
        public DateTime? RefundDate { get; set; }
        public string? RefundTime { get; set; }
        public double? AdvRefundAmt { get; set; }

    }
    public class PHAdvanceDetailBalAmountModel
    {
        public long AdvanceDetailId { get; set; }
        public decimal? BalanceAmount { get; set; }
        public decimal? RefundAmount { get; set; }

    }
    public class PharmacyPaymentModel
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
        public long? UnitId { get;set; }
        public decimal? TdsAmount {get; set; }
        public decimal? WfAmount { get; set; }
    }

    public class PharRefundModel
    {
        public PharmacyRefundModel PharmacyRefund {  get; set; }
        public PhAdvanceHeaderModel PhAdvanceHeader { get; set; }
        public List<PHAdvRefundDetailModel> PHAdvRefundDetail { get; set; }
        public List<PHAdvanceDetailBalAmountModel> PHAdvanceDetailBalAmount { get; set; }
        public PharmacyPaymentModel PharPayment { get; set; }
    }

}
