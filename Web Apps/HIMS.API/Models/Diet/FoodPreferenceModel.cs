using FluentValidation;

namespace HIMS.API.Models.Diet
{
    public class FoodPreferenceModel
    {
        public long FoodPreferenceId { get; set; }
        public string? FoodPreferenceCode { get; set; }
        public string? FoodPreferenceName { get; set; }
        public bool? Active { get; set; }
    }

    public class FoodPreferenceModelValidator : AbstractValidator<FoodPreferenceModel>
    {
        public FoodPreferenceModelValidator()
        {
            RuleFor(x => x.FoodPreferenceName).NotNull().NotEmpty().WithMessage("FoodPreferenceName is required");
        }
    }
}