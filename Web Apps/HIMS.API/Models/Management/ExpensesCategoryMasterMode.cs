using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Management
{
    public class ExpensesCategoryMasterMode
    {
        public long ExpCatId { get; set; }
        public string? CategoryName { get; set; }
        public bool? IsActive { get; set; }

    }
    public class ExpensesCategoryMasterModeValidator : AbstractValidator<ExpensesCategoryMasterMode>
    {
        public ExpensesCategoryMasterModeValidator()
        {
            RuleFor(x => x.CategoryName).NotNull().NotEmpty().WithMessage("CategoryName  is required");
        }
    }
}
