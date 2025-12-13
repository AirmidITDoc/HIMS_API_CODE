using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Administration
{
    public class ExpensesHeadModel
    {
        public long ExpHedId { get; set; }
        public string? HeadName { get; set; }

    }
    public class ExpensesHeadModelValidator : AbstractValidator<ExpensesHeadModel>
    {
        public ExpensesHeadModelValidator()
        {
            //RuleFor(x => x.HeadName).NotNull().NotEmpty().WithMessage("HeadName  is required");
        }
    }
}
