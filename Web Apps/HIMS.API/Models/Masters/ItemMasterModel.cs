using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class ItemMasterModel
    {

        public long ItemId { get; set; }
        public string? ItemShortName { get; set; }
        public string? ItemName { get; set; }
    }

    public class ItemMasterModelValidator : AbstractValidator<ItemMasterModel>
    {
        public ItemMasterModelValidator()
        {
            RuleFor(x => x.ItemName).NotNull().NotEmpty().WithMessage("ItemShortName is required");
        }
    }
}
