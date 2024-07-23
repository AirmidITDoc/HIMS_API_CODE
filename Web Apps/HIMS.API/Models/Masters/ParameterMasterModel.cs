using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class ParameterMasterModel
    {
        public long ParameterId { get; set; }
        public string? ParameterShortName { get; set; }
    }
    public class ParameterMasterModelValidator : AbstractValidator<ParameterMasterModel>
    {
        public ParameterMasterModelValidator()
        {
            RuleFor(x => x.ParameterShortName).NotNull().NotEmpty().WithMessage("Patient Type is required");
        }
    }
}
