using FluentValidation;

namespace HIMS.API.Models.OPPatient
{
    public class OPBillModelShilpa
    {
        public long? OpdIpdId { get; set; }
        public decimal? TotalAmt { get; set; }
        public decimal? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public decimal? PaidAmt { get; set; }
        public decimal? BalanceAmt { get; set; }
        public DateTime? BillDate { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? AddedBy { get; set; }
        public decimal? TotalAdvanceAmount { get; set; }
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
        public double? SpeTaxPer { get; set; }
        public decimal? SpeTaxAmt { get; set; }
        public string? DiscComments { get; set; }
        public decimal? CompDiscAmt { get; set; }
        public long BillNo { get; set; }

        public List<BillDetailModel> BillDetails { get; set; }
        //public List<AddChargeModel> AddCharges { get; set; }


    }
    public class OPBillModelShilpaValidator : AbstractValidator<OPBillModelShilpa>
    {
        public OPBillModelShilpaValidator()
        {
            RuleFor(x => x.OpdIpdId).NotNull().NotEmpty().WithMessage("OpdIpdId is required");
            RuleFor(x => x.TotalAmt).NotNull().NotEmpty().WithMessage("TotalAmt is required");
            RuleFor(x => x.NetPayableAmt).NotNull().NotEmpty().WithMessage("NetPayableAmt is required");
        }
    }
    public class BillDetailModel
    {
        public int BillNo { get; set; }
        public int ChargesId { get; set; }
    }

    public class BillDetailModelValidator : AbstractValidator<BillDetailModel>
    {
        public BillDetailModelValidator()
        {
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
            RuleFor(x => x.ChargesId).NotNull().NotEmpty().WithMessage("ChargesId is required");
        }
    }
    public class AddChargeModel
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
    public class AddChargeModelValidator : AbstractValidator<AddChargeModel>
    {
        public AddChargeModelValidator()
        {
            RuleFor(x => x.ClassId).NotNull().NotEmpty().WithMessage("ClassId is required");
            RuleFor(x => x.BillNo).NotNull().NotEmpty().WithMessage("BillNo is required");
        }
    }


    //public class OPBillModel
    //{
    //    public OPBillModelShilpa OPBill { get; set; }
    //    public BillDetailModel BillDetail { get; set; }
    //}
}
