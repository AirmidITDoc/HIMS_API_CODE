using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class PathCategoryMasterModel
    {
        public long CategoryId { get; set; }
        public string? CategoryName { get; set; }
    }
    public class PathCategoryMasterModelValidator : CommonDynamicValidator<PathCategoryMasterModel>
    {
        public PathCategoryMasterModelValidator()
        {
            RuleFor(x => x.CategoryName).NotNull().NotEmpty().WithMessage("Path Category is required");
        }
    }

}
