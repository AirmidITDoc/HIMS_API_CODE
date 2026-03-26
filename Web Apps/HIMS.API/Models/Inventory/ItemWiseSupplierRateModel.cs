using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Inventory
{
    public class ItemWiseSupplierRateModel
    {
        public long DefId { get; set; }
        public long? ItemId { get; set; }
        public long? SupplierId { get; set; }
        public decimal? SupplierRate { get; set; }
    }
    public class ItemWiseSupplierRateModelValidator : AbstractValidator<ItemWiseSupplierRateModel>
    {
        public ItemWiseSupplierRateModelValidator()
        {
            RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId  is required");
        }
    }
}
