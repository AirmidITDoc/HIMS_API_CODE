using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class CurrencyMasterModel
    {
        public long CurrencyId { get; set; }
        public string? CurrencyName { get; set; }

    }
    public class CurrencyMasterModelValidator : CommonDynamicValidator<CurrencyMasterModel>
    {
        public CurrencyMasterModelValidator()
        {
            RuleFor(x => x.CurrencyName).NotNull().NotEmpty().WithMessage("CurrencyName is required");
        }
    }
}
