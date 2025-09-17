using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.IPPatient
{
    public class MlcInformationModel
    {
        public long Mlcid { get; set; }
        public long? AdmissionId { get; set; }
        public bool IsEmgOrAdm { get; set; }
        public string? Mlcno { get; set; }
        public DateTime? ReportingDate { get; set; }
        public string? ReportingTime { get; set; }
        public string? AuthorityName { get; set; }
        public string? BuckleNo { get; set; }
        public string? PoliceStation { get; set; }
        public string? DetailGiven { get; set; }
        public string? Remark { get; set; }
    }
    public class MlcInformationModelValidator : AbstractValidator<MlcInformationModel>
    {
        public MlcInformationModelValidator()
        {
            RuleFor(x => x.AdmissionId).NotNull().NotEmpty().WithMessage("AdmissionId is required");
            RuleFor(x => x.Mlcno).NotNull().NotEmpty().WithMessage("Mlcno is required");
            RuleFor(x => x.ReportingDate).NotNull().NotEmpty().WithMessage("ReportingDate is required");
            RuleFor(x => x.ReportingTime).NotNull().NotEmpty().WithMessage("ReportingTime is required");

        }
    }
}
