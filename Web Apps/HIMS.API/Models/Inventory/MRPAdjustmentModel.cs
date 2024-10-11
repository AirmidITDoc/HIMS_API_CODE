using FluentValidation;
using HIMS.API.Models.Inventory;


namespace HIMS.API.Models.Inventory
{
    public class MRPAdjustmentModel
    {
        public long MrpAdjId { get; set; }
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
        public List<TCurrentStockModellll> TCurrentStock { get; set; }
    }
  
    public class MRPAdjustmentModelValidator : AbstractValidator<MRPAdjustmentModel>
    {
        public MRPAdjustmentModelValidator()
        {
            RuleFor(x => x.BatchNo).NotNull().NotEmpty().WithMessage("BatchNo is required");
            RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId  is required");

        }
    }

    public class TCurrentStockModellll
    {
        public long StockId { get; set; }
        public long? StoreId { get; set; }
        public long? ItemId { get; set; }
      
        public string? BatchNo { get; set; }
      
        public decimal? PurUnitRate { get; set; }
        public decimal? PurUnitRateWf { get; set; }
        

    }
    public class TCurrentStockModellllValidator : AbstractValidator<TCurrentStockModellll>
    {
        public TCurrentStockModellllValidator()
        {
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("StoreId is required");
        }
    }
}

}
