using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class PatientTypeModel
    {
        public int PatientTypeId { get; set; }
        public string PatientType { get; set;}

    }
    public class PatientTypeModelValidator : AbstractValidator<PatientTypeModel>
    {
        public PatientTypeModelValidator()
        {
            RuleFor(x => x.PatientType).NotNull().NotEmpty().WithMessage("Patient Type is required");
        }
    }
}
