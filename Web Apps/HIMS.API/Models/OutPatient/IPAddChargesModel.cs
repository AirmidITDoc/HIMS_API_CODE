using FluentValidation;
using HIMS.API.Models.OPPatient;

namespace HIMS.API.Models.OutPatient
{
    public class IPAddChargesModel
    {

        public long ChargesId { get; set; }
        public DateTime? ChargesDate { get; set; }
        public byte? OpdIpdType { get; set; }
        public long? OpdIpdId { get; set; }
        public long? ServiceId { get; set; }
        public double? Price { get; set; }
        public double? Qty { get; set; }
        public double? TotalAmt { get; set; }
        public double? ConcessionPercentage { get; set; }
        public decimal? ConcessionAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public long? DoctorId { get; set; }
        public double? DocPercentage { get; set; }
        public double? DocAmt { get; set; }
        public double? HospitalAmt { get; set; }
        public bool? IsGenerated { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? IsPathology { get; set; }
        public long? IsRadiology { get; set; }
        public long? IsPackage { get; set; }
        public long? IsSelfOrCompanyService { get; set; }
        public long? PackageId { get; set; }
        public int? WardId { get; set; }
        public int? BedId { get; set; }
        public string? ChargesTime { get; set; }
        public long? PackageMainChargeId { get; set; }
        public long? ClassId { get; set; }

    }
    public class IPAddChargesModelValidator : AbstractValidator<IPAddChargesModel>
    {
        public IPAddChargesModelValidator()
        {
            RuleFor(x => x.OpdIpdId).NotNull().NotEmpty().WithMessage("OpdIpdId is required");

        }
    }
    public class IPAddChargesBillModel
    {
        public long? BillNo { get; set; }
        public DateTime? ChargesDate { get; set; }
        public double? Price { get; set; }
        public double? Qty { get; set; }
        public double? TotalAmt { get; set; }
        public double? ConcessionPercentage { get; set; }
        public decimal? ConcessionAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public long? AddedBy { get; set; }
        public string? ChargesTime { get; set; }
        public bool? IsInclusionExclusion { get; set; }
        public long ChargesId { get; set; }
        public bool? IsApprovedByCamp { get; set; }


    }
    public class BillUpdateModels
    {
        public long BillNo { get; set; }
        public decimal? TotalAmt { get; set; }
        public double? ConcessionAmt { get; set; }
        public decimal? NetPayableAmt { get; set; }
        public decimal? PaidAmt { get; set; }
        public decimal? BalanceAmt { get; set; }
        public decimal? CompanyAmt { get; set; }
        public decimal? PatientAmt { get; set; }
        public double? SpeTaxPer { get; set; }
        public decimal? SpeTaxAmt { get; set; }
        public long? ConcessionReasonId { get; set; }
        public string? DiscComments { get; set; }
        public long? ModifiedBy { get; set; }
      


    }
    public class BillUpdate
    {
        public List<IPAddChargesBillModel> IPAddChargesBill { get; set; }

        public BillUpdateModels BillUpdates { get; set; }
        //public List<AddChargesModell> AddCharges { get; set; }
    }



}