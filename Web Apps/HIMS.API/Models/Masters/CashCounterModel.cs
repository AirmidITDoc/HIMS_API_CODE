using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class CashCounterModel
    {
        public long CashCounterId { get; set; }
        public string? CashCounterName { get; set; }
        public string? Prefix { get; set; }
        public long? BillNo { get; set; }

    }
    public class CashCounterModelValidator : CommonDynamicValidator<CashCounterModel>
    {
        public CashCounterModelValidator()
        {
            RuleFor(x => x.CashCounterName).NotNull().NotEmpty().WithMessage("CashCounter Name is required");
        }
    }
}
