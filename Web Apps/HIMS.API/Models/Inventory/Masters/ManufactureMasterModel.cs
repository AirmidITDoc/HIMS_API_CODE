using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Inventory.Masters
{
    public class ManufactureMasterModel
    {
        public long ManufId { get; set; }
        public string? ManufName { get; set; }
        public string? ManufShortName { get; set; }
    }
    public class ManufactureMasterModelValidator : AbstractValidator<ManufactureMasterModel>
    {
        public ManufactureMasterModelValidator()
        {
            RuleFor(x => x.ManufName).NotNull().NotEmpty().WithMessage("ManufName  is required");
            RuleFor(x => x.ManufShortName).NotNull().NotEmpty().WithMessage("ManufShortName  is required");

        }
    }
}
