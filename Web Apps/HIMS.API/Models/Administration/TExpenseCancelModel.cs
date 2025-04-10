using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.Administration
{
    public class TExpenseCancelModel
    {
        public long ExpId { get; set; }
        public long IsCancelledBy { get; set; } 

    }

    public class TExpenseCancelModelValidator : AbstractValidator<TExpenseCancelModel>
    {
        public TExpenseCancelModelValidator()
        {
            RuleFor(x => x.IsCancelledBy).NotNull().NotEmpty().WithMessage("IsCancelledBy id is required");
          //  RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId  is required");
        }
    }
}