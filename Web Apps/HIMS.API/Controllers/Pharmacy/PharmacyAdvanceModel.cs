using FluentValidation;
using HIMS.API.Controllers.Pharmacy;
using HIMS.API.Models.Pharmacy;

namespace HIMS.API.Controllers.Pharmacy
{
    public class PharmacyAdvanceModel
    {
        public long? StoreId { get; set; }
        public long? UnitId { get; set; }
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
    }
    public class PharmacyAdvanceModelValidator : AbstractValidator<PharmacyAdvanceModel>
    {
        public PharmacyAdvanceModelValidator()
        {
            RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("Date is required");
            RuleFor(x => x.AdvanceAmount).NotNull().NotEmpty().WithMessage("AdvanceAmount is required");
            RuleFor(x => x.AdvanceUsedAmount).NotNull().NotEmpty().WithMessage("AdvanceUsedAmount is required");
            RuleFor(x => x.BalanceAmount).NotNull().NotEmpty().WithMessage("BalanceAmount is required");
            RuleFor(x => x.IsCancelledDate).NotNull().NotEmpty().WithMessage("IsCancelledDate is required");


        }
    }
    public class PharmacyAdvanceDetailsModel
    {
        public DateTime? Date { get; set; }
        public string? Time { get; set; }
        public long? UnitId { get; set; }
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
        public long? StoreId { get; set; }
        public long AdvanceDetailId { get; set; }

    }
    public class PaymentPharmacyModel
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
        public long? OPDIPDType { get; set; }
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
        public decimal? TdsAmount { get; set; }
        public decimal? WfAmount { get; set; }


    }
    public class PharmacyHeaderUpdateModel
    {
        public double? AdvanceAmount { get; set; }
        public double? BalanceAmount { get; set; }
        public long AdvanceId { get; set; }


    }


}
public class PharAdvanceModel
{
    public PharmacyAdvanceModel PharmacyAdvance { get; set; }
    public PharmacyAdvanceDetailsModel PharmacyAdvanceDetails { get; set; }
    public PaymentPharmacyModel PaymentPharmacy { get; set; }
    public List<TPaymentpharModelS> TPayments { get; set; }


}
public class PharmacyHeaderUpdate
{
    public PharmacyHeaderUpdateModel PharmacyHeader { get; set; }
    public PharmacyAdvanceDetailsModel PharmacyAdvanceDetails { get; set; }
    public PaymentPharmacyModel PaymentPharmacy { get; set; }
    public List<TPaymentpharModelS> TPayments { get; set; }


}


