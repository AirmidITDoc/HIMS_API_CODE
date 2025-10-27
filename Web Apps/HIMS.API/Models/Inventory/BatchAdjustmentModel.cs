using FluentValidation;

namespace HIMS.API.Models.Inventory
{
    public class BatchAdjustmentModel
    {
        public long? StoreId { get; set; }
        public long? ItemId { get; set; }
        public string? OldBatchNo { get; set; }
        public DateTime? OldExpDate { get; set; }
        public string? NewBatchNo { get; set; }
        public DateTime? NewExpDate { get; set; }
        public long? AddedBy { get; set; }
        public long? StkId { get; set; }


    }
    public class BatchAdjustmentModelValidator : AbstractValidator<BatchAdjustmentModel>
    {
        public BatchAdjustmentModelValidator()
        {
            RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId  is required");

        }
    }

    public class TCurrentStockModelll
    {
        public long StockId { get; set; }
        public long? StoreId { get; set; }
        public long? ItemId { get; set; }

    }
    public class TCurrentStockModelllValidator : AbstractValidator<TCurrentStockModelll>
    {
        public TCurrentStockModelllValidator()
        {
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("StoreId is required");
        }
    }
}
