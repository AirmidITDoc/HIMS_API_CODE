using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class ItemManufactureModel
    {
        public long ItemManufactureId { get; set; }
        public string? ManufactureName { get; set; }
    }
    public class ItemManufactureModelValidator : AbstractValidator<ItemManufactureModel>
    {
        public ItemManufactureModelValidator()
        {
            RuleFor(x => x.ManufactureName).NotNull().NotEmpty().WithMessage("ManufactureName is required");
        }
    }

}
