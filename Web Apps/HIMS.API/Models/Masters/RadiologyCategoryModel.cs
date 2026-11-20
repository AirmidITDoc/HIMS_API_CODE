using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class RadiologyCategoryModel
    {
        public long CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public bool? IsActive { get; set; }
    }
    public class CategoryMasterModelValidator : CommonDynamicValidator<RadiologyCategoryModel>
    {
        public CategoryMasterModelValidator()
        {
            RuleFor(x => x.CategoryName).NotNull().NotEmpty().WithMessage("CategoryName  is required");
        }
    }

}
