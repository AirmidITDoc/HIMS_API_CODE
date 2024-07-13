using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class CategoryMasterModel
    {
        public long ItemCategoryId { get; set; }
        public string? ItemCategoryName { get; set; }
    }
    public class CategoryMasterModelValidator : AbstractValidator<CategoryMasterModel>
    {
        public CategoryMasterModelValidator()
        {
            RuleFor(x => x.ItemCategoryName).NotNull().NotEmpty().WithMessage("Patient Type is required");
        }
    }

}
