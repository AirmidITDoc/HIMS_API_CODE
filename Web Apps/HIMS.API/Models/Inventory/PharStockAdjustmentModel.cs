using FluentValidation;
namespace HIMS.API.Models.Inventory
{
    public class PharStockAdjustmentModel
    {
        public long? StoreId { get; set; }
        public long? StkId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public int? AdDdType { get; set; }
        public float? AdDdQty { get; set; }
        public float? PreBalQty { get; set; }
        public float? AfterBalQty { get; set; }
        public long? AddedBy { get; set; }
        public long StockAdgId { get; set; }

        //public List<TCurrentStockModell> TCurrentStock { get; set; }
    }
    public class PharStockAdjustmentModelValidator : AbstractValidator<PharStockAdjustmentModel>
    {
        public PharStockAdjustmentModelValidator()
        {
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("StoreId  is required");
            RuleFor(x => x.StkId).NotNull().NotEmpty().WithMessage("StkId  is required");
            RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId  is required");
            RuleFor(x => x.BatchNo).NotNull().NotEmpty().WithMessage("BatchNo  is required");
            RuleFor(x => x.AdDdType).NotNull().NotEmpty().WithMessage("AdDdType  is required");



        }
    }
    public class GSTUpdateModel
    {
        public long? StoreId { get; set; }
        public long? StkId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public double? OldCgstper { get; set; }
        public double? OldSgstper { get; set; }
        public double? OldIgstper { get; set; }
        public double? Cgstper { get; set; }
        public double? Sgstper { get; set; }
        public double? Igstper { get; set; }
        public long? AddedBy { get; set; }

    }
    public class GSTUpdateModelValidator : AbstractValidator<GSTUpdateModel>
    {
        public GSTUpdateModelValidator()
        {
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
