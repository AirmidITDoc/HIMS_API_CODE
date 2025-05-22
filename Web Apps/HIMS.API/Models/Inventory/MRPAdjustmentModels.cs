using FluentValidation;

namespace HIMS.API.Models.Inventory
{
    public class MRPAdjustmentModels
    {
        public long? StoreId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public decimal? OldMrp { get; set; }
        public decimal? OldLandedRate { get; set; }
        public decimal? OldPurRate { get; set; }
        public float? Qty { get; set; }
        public decimal? Mrp { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? PurRate { get; set; }
        public long? AddedBy { get; set; }
        public DateTime? AddedDateTime { get; set; }

    }
    public class MRPAdjustmentModelsValidator : AbstractValidator<MRPAdjustmentModels>
    {
        public MRPAdjustmentModelsValidator()
        {
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("StoreId is required");
            RuleFor(x => x.BatchNo).NotNull().NotEmpty().WithMessage("BatchNo is required");
            RuleFor(x => x.OldLandedRate).NotNull().NotEmpty().WithMessage("OldLandedRate is required");
            RuleFor(x => x.OldPurRate).NotNull().NotEmpty().WithMessage("OldPurRate is required");
            RuleFor(x => x.Qty).NotNull().NotEmpty().WithMessage("Qty is required");
            RuleFor(x => x.Mrp).NotNull().NotEmpty().WithMessage("Mrp is required");


        }
    }
    public class CurStockModel
    {
        public long? StoreId { get; set; }
        public long StockId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public decimal? PerUnitMrp { get; set; }
        public decimal? PerUnitPurrate { get; set; }
        public decimal? PerUnitLanedrate { get; set; }
        public decimal? OldUnitMrp { get; set; }
        public decimal? OldUnitPur { get; set; }
        public decimal? OldUnitLanded { get; set; }

    }
    public class MRPAdjModel
        {
            public MRPAdjustmentModels MRPAdjustmentMod {  get; set; }
            public CurStockModel CurruntStockModel { get; set; }

        }
    }

