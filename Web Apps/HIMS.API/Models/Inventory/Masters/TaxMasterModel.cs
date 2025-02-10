using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Inventory.Masters
{
    public class TaxMasterModel
    {
        public long Id { get; set; }
        public string? TaxNature { get; set; }
        public bool IsActive { get; set; }
    }
    public class TaxMasterModelValidator : AbstractValidator<TaxMasterModel>
    {
        public TaxMasterModelValidator()
        {
            RuleFor(x => x.TaxNature).NotNull().NotEmpty().WithMessage("TaxNature is required");
        }
    }
}
