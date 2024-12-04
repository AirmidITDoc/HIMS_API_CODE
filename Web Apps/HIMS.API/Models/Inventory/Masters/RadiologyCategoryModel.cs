using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class RadiologyCategoryModel
    {
        public long CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
    public class CategoryMasterModelValidator : AbstractValidator<RadiologyCategoryModel>
    {
        public CategoryMasterModelValidator()
        {
            RuleFor(x => x.CategoryName).NotNull().NotEmpty().WithMessage("CategoryName  is required");
        }
    }

}
