using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.DietKitchen
{
    public class FoodCategorymasterModel
    {
        public long FoodCategoryId { get; set; }
        public string? FoodCategoryName { get; set; }

    }
    public class FoodCategorymasterModelValidator : AbstractValidator<FoodCategorymasterModel>
    {
        public FoodCategorymasterModelValidator()
        {
            RuleFor(x => x.FoodCategoryName).NotNull().NotEmpty().WithMessage("FoodCategoryName is required");
        }
    }
}