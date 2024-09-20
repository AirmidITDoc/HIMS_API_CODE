using FluentValidation;

namespace HIMS.API.Models.OutPatient
{
    public class IPBillIngModel
    {
        public int BillNo { get; set; }
        public int? OPDIPDID { get; set; }
        public float? TotalAmt { get; set; }
        public float? ConcessionAmt { get; set; }
        public float? NetPayableAmt { get; set; }
        public float? PaidAmt { get; set; }
        public float? BalanceAmt { get; set; }
        public string? BillDate { get; set; }
        public int? OPDIPDType { get; set; }
        public int? AddedBy { get; set; }
        public float? TotalAdvanceAmount { get; set; }
        public string? BillTime { get; set; }
        public int? ConcessionReasonId { get; set; }
        public Boolean? IsSettled { get; set; }
        public Boolean? IsPrinted { get; set; }
        public Boolean? IsFree { get; set; }
        public int? CompanyId { get; set; }
        public int? TariffId { get; set; }
        public int? UnitId { get; set; }
        public int? InterimOrFinal { get; set; }
        public int? CompanyRefNo { get; set; }
        public int? ConcessionAuthorizationName { get; set; }
        public float? SpeTaxPer { get; set; }
        public float? SpeTaxAmt { get; set; }
        public int? CompDiscAmt { get; set; }
        public string? DiscComments { get; set; }
        public long? CashCounterId { get; set; }
        public List<ChargesModel> AddCharges { get; set; }
        public List<BillDetailsModel> BillDetails { get; set; }
    }
    public class IPBillModelValidator : AbstractValidator<IPBillIngModel>
    {
        public IPBillModelValidator()
        {
            RuleFor(x => x.OPDIPDID).NotNull().NotEmpty().WithMessage("OPDIPDID is required");
            RuleFor(x => x.TotalAmt).NotNull().NotEmpty().WithMessage("TotalAmt is required");
            RuleFor(x => x.NetPayableAmt).NotNull().NotEmpty().WithMessage("NetPayableAmt is required");
            RuleFor(x => x.UnitId).NotNull().NotEmpty().WithMessage("UnitId is required");
            RuleFor(x => x.TariffId).NotNull().NotEmpty().WithMessage("TariffId is required");
        }
    }

    public class IPBillDetailsModel
    {
        public int BillNo { get; set; }
        public int ChargesId { get; set; }
    }

    public class IPBillDetailsModelValidator : AbstractValidator<IPBillDetailsModel>
    {
        public IPBillDetailsModelValidator()
        {
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
            RuleFor(x => x.ChargesId).NotNull().NotEmpty().WithMessage("ChargesId is required");
        }
    }

    public class IPChargesModel
    {
        public long ChargesId { get; set; }
        public string? ChargesDate { get; set; }
        public int? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public long? ServiceId { get; set; }
        public float? Price { get; set; }
        public float? Qty { get; set; }
        public float? TotalAmt { get; set; }
        public float? ConcessionPercentage { get; set; }
        public float? ConcessionAmount { get; set; }
        public float? NetAmount { get; set; }
        public long? DoctorId { get; set; }
        public float? DocPercentage { get; set; }
        public float? DocAmt { get; set; }
        public float? HospitalAmt { get; set; }
        public bool? IsGenerated { get; set; }
        public int? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public string? IsCancelledDate { get; set; }
        public bool? IsPathology { get; set; }
        public bool? IsRadiology { get; set; }
        public bool? IsPackage { get; set; }
        public int? PackageMainChargeID { get; set; }
        public bool? IsSelfOrCompanyService { get; set; }
        public long? PackageId { get; set; }
        public string? ChargesTime { get; set; }
        public long? ClassId { get; set; }
        public long? BillNo { get; set; }

    }
    public class IPChargesModelValidator : AbstractValidator<IPChargesModel>
    {
        public IPChargesModelValidator()
        {
            RuleFor(x => x.ClassId).NotNull().NotEmpty().WithMessage("ClassId is required");
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
        }
    }

    public class IPPaymentModel
    {
        public int PaymentId { get; set; }
        public int? BillNo { get; set; }
        public String? ReceiptNo { get; set; }
        public DateTime? PaymentDate { get; set; }
        public DateTime? PaymentTime { get; set; }
        public long? CashPayAmount { get; set; }
        public long? ChequePayAmount { get; set; }
        public String? ChequeNo { get; set; }
        public String? BankName { get; set; }
        public DateTime? ChequeDate { get; set; }
        public long? CardPayAmount { get; set; }
        public String? CardNo { get; set; }
        public String? CardBankName { get; set; }
        public DateTime? CardDate { get; set; }
        public long? AdvanceUsedAmount { get; set; }
        public int? AdvanceId { get; set; }
        public int? RefundId { get; set; }
        public int? TransactionType { get; set; }
        public String? Remark { get; set; }
        public int? AddBy { get; set; }
        public int IsCancelled { get; set; }
        public int? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? NEFTPayAmount { get; set; }
        public String? NEFTNo { get; set; }
        public String? NEFTBankMaster { get; set; }
        public DateTime? NEFTDate { get; set; }
        public long? PayTMAmount { get; set; }
        public String? PayTMTranNo { get; set; }
        public DateTime? PayTMDate { get; set; }
        public long? TDSAmount { get; set; }
    }

    public class IPPaymentModelValidator : AbstractValidator<OPPaymentModel>
    {
        public IPPaymentModelValidator()
        {
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("ClassId is required");
        }
    }
}
