using FluentValidation;

namespace HIMS.API.Models.Administration
{
    public class CompanyinformationModel
    {
        public string? PolicyNo { get; set; }
        public string? ClaimNo { get; set; }
        public decimal? EstimatedAmount { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public decimal? HosApreAmt { get; set; }
        public decimal? PathApreAmt { get; set; }
        public decimal? PharApreAmt { get; set; }
        public decimal? RadiApreAmt { get; set; }
        public float? PharDisc { get; set; }
        public decimal? CDisallowedAmt { get; set; }
        public decimal? HdiscAmt { get; set; }
        public decimal? COutsideInvestAmt { get; set; }
        public decimal? RecoveredByPatient { get; set; }
        public decimal? CFinalBillAmt { get; set; }
        public decimal? MedicalApreAmt { get; set; }
        public long AdmissionId { get; set; }

    }
    public class CompanyinformationModelValidator : AbstractValidator<CompanyinformationModel>
    {
        public CompanyinformationModelValidator()
        {
            RuleFor(x => x.EstimatedAmount).NotNull().NotEmpty().WithMessage("EstimatedAmount is required");
            RuleFor(x => x.ApprovedAmount).NotNull().NotEmpty().WithMessage("ApprovedAmount is required");
            RuleFor(x => x.HosApreAmt).NotNull().NotEmpty().WithMessage("HosApreAmt is required");
            RuleFor(x => x.PathApreAmt).NotNull().NotEmpty().WithMessage("PathApreAmt is required");
            RuleFor(x => x.RadiApreAmt).NotNull().NotEmpty().WithMessage("RadiApreAmt is required");
            RuleFor(x => x.COutsideInvestAmt).NotNull().NotEmpty().WithMessage("COutsideInvestAmt is required");

        }
    }
}
