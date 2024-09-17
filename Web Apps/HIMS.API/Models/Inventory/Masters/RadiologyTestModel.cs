using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class RadiologyTestModel
    {
        public long TestId { get; set; }
        public string? TestName { get; set; }
    }

    public class RadiologyTestModelValidator : AbstractValidator<RadiologyTestModel>
    {
        public RadiologyTestModelValidator()
        {
            RuleFor(x => x.TestName).NotNull().NotEmpty().WithMessage("Radiology Test is required");
        }
    }
}
