using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class ItemTypeModel
    {
        public long ItemTypeId { get; set; }
        public string? ItemTypeName { get; set; }
    }
    public class ItemTypeModelValidator : CommonDynamicValidator<ItemTypeModel>
    {
        public ItemTypeModelValidator()
        {
            RuleFor(x => x.ItemTypeName).NotNull().NotEmpty().WithMessage("ItemTypeName is required");
        }
    }
}
