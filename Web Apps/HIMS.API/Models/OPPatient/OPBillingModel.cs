using FluentValidation;

namespace HIMS.API.Models.OPPatient
{


    public class OPBillIngModel
    {
        public int? OPDIPDID { get; set; }
        public long? RegNo { get; set; }
        public string? PatientName { get; set; }
        public string? Ipdno { get; set; }
        public long? AgeYear { get; set; }
        public long? AgeMonth { get; set; }
        public long? AgeDays { get; set; }
        public long? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public long? WardId { get; set; }
        public long? BedId { get; set; }
        public bool? PatientType { get; set; }
        public string? CompanyName { get; set; }
        public decimal? CompanyAmt { get; set; }
        public decimal? PatientAmt { get; set; }
        public float? TotalAmt { get; set; }
        public float? ConcessionAmt { get; set; }
        public float? NetPayableAmt { get; set; }
        public float? PaidAmt { get; set; }
        public float? BalanceAmt { get; set; }
        public string? BillDate { get; set; }
        public int? OPDIPDType { get; set; }
        public int? AddedBy { get; set; }
        public float? TotalAdvanceAmount { get; set; }
        public decimal? AdvanceUsedAmount { get; set; }
        public string? BillTime { get; set; }
        public int? ConcessionReasonId { get; set; }
        public bool? IsSettled { get; set; }
        public bool? IsPrinted { get; set; }
        public bool? IsFree { get; set; }
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
        public long? CreatedBy { get; set; }
        public int BillNo { get; set; }
        public List<ChargesModel> AddCharges { get; set; }
        public List<BillDetailsModel> BillDetails { get; set; }
        public List<Packcagechargesmodel?> Packcagecharges { get; set; }
        public OPPaymentModel? Payments { get; set; }
    }
    public class BillModelValidator : AbstractValidator<OPBillIngModel>
    {
        public BillModelValidator()
        {
            RuleFor(x => x.OPDIPDID).NotNull().NotEmpty().WithMessage("OPDIPDID is required");
            RuleFor(x => x.TotalAmt).NotNull().NotEmpty().WithMessage("TotalAmt is required");
            RuleFor(x => x.NetPayableAmt).NotNull().NotEmpty().WithMessage("NetPayableAmt is required");
            RuleFor(x => x.UnitId).NotNull().NotEmpty().WithMessage("UnitId is required");
            RuleFor(x => x.TariffId).NotNull().NotEmpty().WithMessage("TariffId is required");
        }
    }

    public class BillDetailsModel
    {
        public int BillNo { get; set; }
        public int ChargesId { get; set; }
    }

    public class BillDetailsModelValidator : AbstractValidator<BillDetailsModel>
    {
        public BillDetailsModelValidator()
        {
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
            RuleFor(x => x.ChargesId).NotNull().NotEmpty().WithMessage("ChargesId is required");
        }
    }

    public class ChargesModel
    {
        public long ChargesId { get; set; }
        public string? ChargesDate { get; set; }
        public int? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public long? ServiceId { get; set; }
        public float? Price { get; set; }
        public float? Qty { get; set; }
        public long? UnitId { get; set; }
        public float? TotalAmt { get; set; }
        public float? ConcessionPercentage { get; set; }
        public float? ConcessionAmount { get; set; }
        public float? NetAmount { get; set; }
        public long? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public float? DocPercentage { get; set; }
        public float? DocAmt { get; set; }
        public float? HospitalAmt { get; set; }
        public decimal? RefundAmount { get; set; }
        public bool? IsGenerated { get; set; }
        public bool? IsComServ { get; set; }
        public bool? IsPrintCompSer { get; set; }
        public int? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public string? IsCancelledDate { get; set; }
        public bool? IsPathology { get; set; }
        public bool? IsRadiology { get; set; }
        public bool? IsPackage { get; set; }
        public int? WardId { get; set; }
        public int? BedId { get; set; }
        public string? ServiceCode { get; set; }
        public string? ServiceName { get; set; }
        public string? CompanyServiceName { get; set; }
        public bool? IsInclusionExclusion { get; set; }
        public int? IsHospMrk { get; set; }
        public int? PackageMainChargeID { get; set; }
        public bool? IsSelfOrCompanyService { get; set; }
        public long? PackageId { get; set; }
        public string? ChargesTime { get; set; }
        public long? ClassId { get; set; }
        public long? TariffId { get; set; }
        public long? BillNo { get; set; }
        public long? CreatedBy { get; set; }

    }
    public class ChargesModelValidator : AbstractValidator<ChargesModel>
    {
        public ChargesModelValidator()
        {
            RuleFor(x => x.ClassId).NotNull().NotEmpty().WithMessage("ClassId is required");
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
        }
    }

    public class OPPaymentModel
    {
        public int PaymentId { get; set; }
        public long? UnitId { get; set; }
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
        public long? SalesId { get; set; }
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
        public decimal? Wfamount { get; set; }
    }

    public class OPPaymentModelValidator : AbstractValidator<OPPaymentModel>
    {
        public OPPaymentModelValidator()
        {
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
        }
    }


    public class Packcagechargesmodel
    {
        public long ChargesId { get; set; }
        public string? ChargesDate { get; set; }
        public int? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public long? ServiceId { get; set; }
        public float? Price { get; set; }
        public float? Qty { get; set; }
        public long? UnitId { get; set; }
        public float? TotalAmt { get; set; }
        public float? ConcessionPercentage { get; set; }
        public float? ConcessionAmount { get; set; }
        public float? NetAmount { get; set; }
        public long? DoctorId { get; set; }
        public string? DoctorName { get; set; }
        public float? DocPercentage { get; set; }
        public float? DocAmt { get; set; }
        public float? HospitalAmt { get; set; }
        public decimal? RefundAmount { get; set; }
        public bool? IsComServ { get; set; }
        public bool? IsPrintCompSer { get; set; }
        public long? SalesId { get; set; }
        public bool? IsGenerated { get; set; }
        public int? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public string? IsCancelledDate { get; set; }
        public bool? IsPathology { get; set; }
        public bool? IsRadiology { get; set; }
        public bool? IsPackage { get; set; }
        public int? WardId { get; set; }
        public int? BedId { get; set; }
        public string? ServiceCode { get; set; }
        public string? ServiceName { get; set; }
        public string? CompanyServiceName { get; set; }
        public bool? IsInclusionExclusion { get; set; }
        public int? IsHospMrk { get; set; }
        public int? PackageMainChargeID { get; set; }
        public bool? IsSelfOrCompanyService { get; set; }
        public long? PackageId { get; set; }
        public string? ChargesTime { get; set; }
        public long? ClassId { get; set; }
        public long? TariffId { get; set; }
        public long? BillNo { get; set; }
        public long? CreatedBy { get; set; }

    }
    public class PackcagechargesmodelValidator : AbstractValidator<Packcagechargesmodel>
    {
        public PackcagechargesmodelValidator()
        {
            RuleFor(x => x.ClassId).NotNull().NotEmpty().WithMessage("ClassId is required");
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
        }
    }


    public class NewOpBill
    {
        public OPBillIngModel OPBillIngModels { get; set; }
        public ChargesModel ChargesModels { get; set; }
        public BillDetailsModel BillDetailsModels { get; set; }
        public PaymentModell PaymentModells { get; set; }
        public Packcagechargesmodel ChargessModel { get; set; }

    }
}
