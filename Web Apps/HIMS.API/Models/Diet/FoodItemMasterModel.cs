using FluentValidation;

namespace HIMS.API.Models.Diet
{
    public class FoodItemMasterModel
    {
        public long FoodItemId { get; set; }
        //public string? FoodCode { get; set; }
        public string? FoodName { get; set; }
        public long? FoodCategoryId { get; set; }
        public string? LocalName { get; set; }
        public long? Unit { get; set; }
        public bool? IsVegetarian { get; set; }
    }

    public class FoodItemMasterModelValidator : AbstractValidator<FoodItemMasterModel>
    {
        public FoodItemMasterModelValidator()
        {
            RuleFor(x => x.FoodName).NotNull().NotEmpty().WithMessage("FoodName is required");
            RuleFor(x => x.FoodCategoryId).NotNull().NotEmpty().WithMessage("FoodCategoryId is required");
        }
    }
}