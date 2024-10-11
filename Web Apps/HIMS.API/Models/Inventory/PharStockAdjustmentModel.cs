using FluentValidation;
namespace HIMS.API.Models.Inventory
{
    public class PharStockAdjustmentModel
    {
        public long StockAdgId { get; set; }
        public long? StoreId { get; set; }
        public long? StkId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public int? AdDdType { get; set; }
        public float? AdDdQty { get; set; }
        public float? PreBalQty { get; set; }
        public float? AfterBalQty { get; set; }
        public long? AddedBy { get; set; }
        public List<TCurrentStockModell> TCurrentStock { get; set; }
    }
    public class PharStockAdjustmentModelValidator : AbstractValidator<PharStockAdjustmentModel>
    {
        public PharStockAdjustmentModelValidator()
        {
            RuleFor(x => x.StkId).NotNull().NotEmpty().WithMessage("StkId is required");
            RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId  is required");

        }
    }

    public class TCurrentStockModell
    {
        public long StockId { get; set; }
        public long? StoreId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
    }
    public class TCurrentStockModellValidator : AbstractValidator<TCurrentStockModell>
    {
        public TCurrentStockModellValidator()
        {
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("StoreId is required");
        }
    }
}
