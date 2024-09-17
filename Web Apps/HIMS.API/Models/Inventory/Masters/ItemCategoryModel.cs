using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class ItemCategoryModel
    {
        public long ItemCategoryId { get; set; }
        public string? ItemCategoryName { get; set; }
        public long? ItemTypeId { get; set; }

    }
    public class ItemCategoryModelValidator : AbstractValidator<ItemCategoryModel>
    {
        public ItemCategoryModelValidator()
        {
            RuleFor(x => x.ItemCategoryName).NotNull().NotEmpty().WithMessage("ItemCategoryName is required");
            RuleFor(x => x.ItemTypeId).NotNull().NotEmpty().WithMessage("ItemTypeId is required");

        }
    }
}
