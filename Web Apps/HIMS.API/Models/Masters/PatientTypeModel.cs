using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class PatientTypeModel
    {
        public int PatientTypeId { get; set; }
        public string PatientType { get; set; }

    }
    public class PatientTypeModelValidator : CommonDynamicValidator<PatientTypeModel>
    {
        public PatientTypeModelValidator()
        {
            RuleFor(x => x.PatientType).NotNull().NotEmpty().WithMessage("Patient Type is required");
        }
    }
}
