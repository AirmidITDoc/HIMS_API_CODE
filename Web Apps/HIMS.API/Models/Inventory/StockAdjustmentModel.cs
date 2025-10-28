using FluentValidation;

namespace HIMS.API.Models.Inventory
{

    public class TCurrentStockModel3
    {
        public long StockId { get; set; }
        public long? StoreId { get; set; }
    }
    public class TCurrentStockModel3Validator : AbstractValidator<TCurrentStockModel3>
    {
        public TCurrentStockModel3Validator()
        {
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("StoreId is required");
        }
    }

}



