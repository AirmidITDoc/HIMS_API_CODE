using FluentValidation;

namespace HIMS.API.Models.IPPatient
{
    public class IPDraftBillModel
    {
        public long Drbno { get; set; }
        public long? OpdIpdId { get; set; }
        public decimal? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public decimal? PaidAmt { get; set; }
        public decimal? BalanceAmt { get; set; }
        public DateTime? BillDate { get; set; }
        public byte? OpdIpdType { get; set; }
        public decimal? TotalAdvanceAmount { get; set; }
        public long? AddedBy { get; set; }
        public string? BillTime { get; set; }
        public long? ConcessionReasonId { get; set; }
        public bool? IsSettled { get; set; }
        public bool? IsPrinted { get; set; }
        public bool? IsFree { get; set; }
        public long? CompanyId { get; set; }
        public long? TariffId { get; set; }
        public long? UnitId { get; set; }
        public long? InterimOrFinal { get; set; }
        public string? CompanyRefNo { get; set; }
        public long? ConcessionAuthorizationName { get; set; }
        public double? TaxPer { get; set; }
        public decimal? TaxAmount { get; set; }

        //public List<IpDraftChargeModel> DraftAddCharges { get; set; }
        public List<IPDraftBillModel> DraftBillDetails { get; set; }
    }

    public class IpDraftBillModelValidator : AbstractValidator<IPDraftBillModel>
    {
        public IpDraftBillModelValidator()
        {
            //  RuleFor(x => x.OPDIPDID).NotNull().NotEmpty().WithMessage("OPDIPDID is required");
            //RuleFor(x => x.TotalAmt).NotNull().NotEmpty().WithMessage("TotalAmt is required");
            //RuleFor(x => x.NetPayableAmt).NotNull().NotEmpty().WithMessage("NetPayableAmt is required");
            //RuleFor(x => x.UnitId).NotNull().NotEmpty().WithMessage("UnitId is required");
            //RuleFor(x => x.TariffId).NotNull().NotEmpty().WithMessage("TariffId is required");
        }
    }
    public class IpDraftBillDetailModel
    {
        public long DrbillDetId { get; set; }
        public long? Drno { get; set; }
        public long? ChargesId { get; set; }
    }

    public class IpDraftBillDetailsModelValidator : AbstractValidator<IpDraftBillDetailModel>
    {
        public IpDraftBillDetailsModelValidator()
        {
            RuleFor(x => x.Drno).NotNull().NotEmpty().WithMessage("BillNo is required");
            RuleFor(x => x.ChargesId).NotNull().NotEmpty().WithMessage("ChargesId is required");
        }
    }
    public class IpDraftChargeModel
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
    public class IpdraftChargesModelValidator : AbstractValidator<IpDraftChargeModel>
    {
        public IpdraftChargesModelValidator()
        {
            //RuleFor(x => x.ClassId).NotNull().NotEmpty().WithMessage("ClassId is required");
            //RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
        }
    }
}

