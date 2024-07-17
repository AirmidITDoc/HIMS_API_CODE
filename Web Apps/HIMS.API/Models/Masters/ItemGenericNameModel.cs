using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class ItemGenericNameModel
    {
        public long ItemGenericNameId { get; set; }
        public string? ItemGenericName { get; set; }
    }
    public class ItemGenericNameModelValidator : AbstractValidator<ItemGenericNameModel>
    {
        public ItemGenericNameModelValidator()
        {
            RuleFor(x => x.ItemGenericName).NotNull().NotEmpty().WithMessage("ItemGeneric master is required");
        }
    }
}
