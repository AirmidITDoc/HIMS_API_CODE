using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class ItemClassMasterModel
    {
        public long ItemClassId { get; set; }
        public string? ItemClassName { get; set; }
    }
    public class ItemClassMasterModelValidator : CommonDynamicValidator<ItemClassMasterModel>
    {
        public ItemClassMasterModelValidator()
        {
            RuleFor(x => x.ItemClassName).NotNull().NotEmpty().WithMessage("Item Class is required");
        }
    }

}
