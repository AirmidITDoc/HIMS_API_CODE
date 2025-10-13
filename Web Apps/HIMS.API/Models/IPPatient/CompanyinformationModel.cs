using FluentValidation;

namespace HIMS.API.Models.IPPatient
{
    public class CompanyinformationModel
    {
        public string? PolicyNo { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public long AdmissionId { get; set; }

    }
    public class CompanyinformationModelValidator : AbstractValidator<CompanyinformationModel>
    {
        public CompanyinformationModelValidator()
        {
            RuleFor(x => x.ApprovedAmount).NotNull().NotEmpty().WithMessage("ApprovedAmount is required");
            RuleFor(x => x.AdmissionId).NotNull().NotEmpty().WithMessage("AdmissionId is required");
            RuleFor(x => x.PolicyNo).NotNull().NotEmpty().WithMessage("PolicyNo is required");

        }
    }
    public class CompanyApprovalDetModel
    {
        public long Id { get; set; }
        public long? AdmissionId { get; set; }
        public decimal? EstimateAmount { get; set; }
        public decimal? ApprovedAmount { get; set; }
        public string? Alentry { get; set; }
        public DateTime? DateApproved { get; set; }
        public string? Comments { get; set; }

    }

    public class CompanyApprovalDetModelValidator : AbstractValidator<CompanyApprovalDetModel>
    {
        public CompanyApprovalDetModelValidator()
        {
            RuleFor(x => x.ApprovedAmount).NotNull().NotEmpty().WithMessage("ApprovedAmount is required");
            RuleFor(x => x.AdmissionId).NotNull().NotEmpty().WithMessage("AdmissionId is required");
            RuleFor(x => x.DateApproved).NotNull().NotEmpty().WithMessage("DateApproved is required");

        }
    }
}
