using FluentValidation;

namespace HIMS.API.Models.Inventory
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
            RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId is required");
            RuleFor(x => x.ItemShortName).NotNull().NotEmpty().WithMessage("ItemShortname is required");
            RuleFor(x => x.ItemName).NotNull().NotEmpty().WithMessage("ItemName is required");
            
         }

    }

    

}
