using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class ItemClassMasterModel
    {
        public long ItemClassId { get; set; }
        public string? ItemClassName { get; set; }
    }
    public class ItemClassMasterModelValidator : AbstractValidator<ItemClassMasterModel>
    {
        public ItemClassMasterModelValidator()
        {
            RuleFor(x => x.ItemClassName).NotNull().NotEmpty().WithMessage("Item Class is required");
        }
    }

}
