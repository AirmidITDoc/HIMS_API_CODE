using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class CurrencyMasterModel
    {
        public long CurrencyId { get; set; }
        public string? CurrencyName { get; set; }

    }
    public class CurrencyMasterModelValidator : AbstractValidator<CurrencyMasterModel>
    {
        public CurrencyMasterModelValidator()
        {
            RuleFor(x => x.CurrencyName).NotNull().NotEmpty().WithMessage("Patient Type is required");
        }
    }
}
