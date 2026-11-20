using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class ItemGenericNameModel
    {
        public long ItemGenericNameId { get; set; }
        public string? ItemGenericName { get; set; }
    }
    public class ItemGenericNameModelValidator : CommonDynamicValidator<ItemGenericNameModel>
    {
        public ItemGenericNameModelValidator()
        {
            RuleFor(x => x.ItemGenericName).NotNull().NotEmpty().WithMessage("ItemGenericName is required");
        }
    }
}
