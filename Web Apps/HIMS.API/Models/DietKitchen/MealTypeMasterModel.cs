using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.DietKitchen
{
    public class MealTypeMasterModel
    {
        public long MealId { get; set; }
        public string MealName { get; set; } = null!;
        public string? DefaultTime { get; set; }
        public string? OrderCutoffTime { get; set; }
        public string? PreparationStartTime { get; set; }
        public string? DispatchTime { get; set; }


    }
    public class MealTypeMasterModelValidator : AbstractValidator<MealTypeMasterModel>
    {
        public MealTypeMasterModelValidator()
        {
            RuleFor(x => x.MealName).NotNull().NotEmpty().WithMessage("MealName is required");
            RuleFor(x => x.DefaultTime).NotNull().NotEmpty().WithMessage("DefaultTime is required");

        }
    }
}