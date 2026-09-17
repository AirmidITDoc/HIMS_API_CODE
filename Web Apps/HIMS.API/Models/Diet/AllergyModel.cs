using FluentValidation;

namespace HIMS.API.Models.Diet
{
    public class AllergyModel
    {
        public long AllergyId { get; set; }
        public string? AllergyCode { get; set; }
        public string? AllergyName { get; set; }
        public long? CategoryId { get; set; }
        public long? SeverityId { get; set; }
        public string? Reaction { get; set; }
        public bool? IsKitchenAlert { get; set; }
        public bool? Active { get; set; }
    }

    public class AllergyModelValidator : AbstractValidator<AllergyModel>
    {
        public AllergyModelValidator()
        {
            RuleFor(x => x.AllergyName).NotNull().NotEmpty().WithMessage("AllergyName is required");
        }
    }
}