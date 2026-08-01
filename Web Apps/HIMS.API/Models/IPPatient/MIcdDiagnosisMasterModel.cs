using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.IPPatient
{
    public class MIcdDiagnosisMasterModel
    {
        public int Icdid { get; set; }
        public string Icdversion { get; set; } = null!;
        public string Icdcode { get; set; } = null!;
        public string DiagnosisName { get; set; } = null!;
        public string? ShortName { get; set; }
    }
    public class MIcdDiagnosisMasterModelValidator : AbstractValidator<MIcdDiagnosisMasterModel>
    {
        public MIcdDiagnosisMasterModelValidator()
        {
            RuleFor(x => x.Icdversion).NotNull().NotEmpty().WithMessage("Icdversion  is required");
            RuleFor(x => x.Icdcode).NotNull().NotEmpty().WithMessage("Icdcode  is required");
            RuleFor(x => x.DiagnosisName).NotNull().NotEmpty().WithMessage("DiagnosisName  is required");

        }
    }
}
