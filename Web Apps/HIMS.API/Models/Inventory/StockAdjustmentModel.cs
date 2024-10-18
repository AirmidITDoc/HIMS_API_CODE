using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.Inventory
{
    public class StockAdjustmentModel
    {
        public long IssueDepId { get; set; }
        public long? IssueId { get; set; }
        public float? ReturnQty { get; set; }
        public long? StkId { get; set; }

        public List<TCurrentStockModel> TCurrentStock { get; set; }
    }
  
    public class StockAdjustmentModelValidator : AbstractValidator<StockAdjustmentModel>
    {
        public StockAdjustmentModelValidator()
        {
            RuleFor(x => x.ReturnQty).NotNull().NotEmpty().WithMessage("ReturnQty is required");
            RuleFor(x => x.StkId).NotNull().NotEmpty().WithMessage("StkId  is required");

        }
    }

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



