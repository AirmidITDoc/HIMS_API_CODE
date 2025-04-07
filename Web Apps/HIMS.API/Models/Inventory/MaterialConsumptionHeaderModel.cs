using FluentValidation;
using HIMS.API.Models.Masters;
using HIMS.API.Models.Pharmacy;

namespace HIMS.API.Models.Inventory
{
    public class MaterialConsumptionHeaderModel
    {
        public long MaterialConsumptionId { get; set; }
      //  public string? ConsumptionNo { get; set; }
        public DateTime? ConsumptionDate { get; set; }
        public string? ConsumptionTime { get; set; }
        public long? FromStoreId { get; set; }
        public decimal? LandedTotalAmount { get; set; }
        public decimal? PurTotalAmount { get; set; }
        public decimal? MRPTotalAmount { get; set; }
        public string? Remark { get; set; }
        public long? AddedBy { get; set; }
      //  public List<MaterialConsumptionDetailModel> MaterialConsumptionDetail { get; set; }
        //   public long? UpdatedBy { get; set; }
        //  public long? AdmId { get; set; }


    }

    public class MaterialConsumptionHeaderModelValidator : AbstractValidator<MaterialConsumptionHeaderModel>
    {
        public MaterialConsumptionHeaderModelValidator()
        {
            RuleFor(x => x.ConsumptionDate).NotNull().NotEmpty().WithMessage("ConsumptionDate id is required");
            RuleFor(x => x.LandedTotalAmount).NotNull().NotEmpty().WithMessage("LandedTotalAmount  is required");
        }
    }

    public class MaterialConsumptionDetailModel
    {
     //   public long MaterialConDetId { get; set; }
        public long? MaterialConsumptionId { get; set; }
        public long? ItemId { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public long? Qty { get; set; }
        public decimal? PerUnitLandedRate { get; set; }
        public decimal? PerUnitPurchaseRate { get; set; }
        public decimal? PerUnitMrprate { get; set; }
        public decimal? LandedTotalAmount { get; set; }
        public decimal? PurTotalAmount { get; set; }
        public decimal? MrptotalAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Remark { get; set; }
  //      public long? AdmId { get; set; }

    
 
    }

    public class MaterialConsumptionDetailModelValidator : AbstractValidator<MaterialConsumptionDetailModel>
    {
        public MaterialConsumptionDetailModelValidator()
        {
            RuleFor(x => x.MaterialConsumptionId).NotNull().NotEmpty().WithMessage("MaterialConsumptionId id is required");
            RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId  is required");
        }
    }

    public class MaterialConsumptionModel
    {
        public MaterialConsumptionHeaderModel MaterialConsumptionHeader { get; set; }
        public List<MaterialConsumptionDetailModel> MaterialConsumptionDetail { get; set; }
     
    }


}