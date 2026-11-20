using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class ItemManufactureModel
    {
        public long ItemManufactureId { get; set; }
        public string? ManufactureName { get; set; }
    }
    public class ItemManufactureModelValidator : CommonDynamicValidator<ItemManufactureModel>
    {
        public ItemManufactureModelValidator()
        {
            RuleFor(x => x.ManufactureName).NotNull().NotEmpty().WithMessage("ManufactureName is required");
        }
    }

}
