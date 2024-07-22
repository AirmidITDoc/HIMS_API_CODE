using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class TarifMasterModel
    {
        public long TariffId { get; set; }
        public string? TariffName { get; set; }
        public bool? IsActive { get; set; }
    }
    public class TarifMasterModelValidator : AbstractValidator<TarifMasterModel>
    {
        public TarifMasterModelValidator()
        {
            RuleFor(x => x.TariffName).NotNull().NotEmpty().WithMessage("Tarrif is required");
        }
    }
}
