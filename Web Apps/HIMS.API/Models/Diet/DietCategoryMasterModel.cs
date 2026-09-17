using FluentValidation;

namespace HIMS.API.Models.Diet
{
    public class DietCategoryMasterModel
    {
        public long DietCategoryId { get; set; }
        public string CategoryCode { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string? Description { get; set; }
    }
    public class DietCategoryMasterModelValidator : AbstractValidator<DietCategoryMasterModel>
    {
        public DietCategoryMasterModelValidator()
        {
            RuleFor(x => x.CategoryCode).NotNull().NotEmpty().WithMessage("CategoryCode  is required");
            RuleFor(x => x.CategoryName).NotNull().NotEmpty().WithMessage("CategoryName  is required");
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Description  is required");

        }
    }


}

