using FluentValidation;
namespace HIMS.API.Models.Inventory
{
    public class ItemMasterModel
    {
        public long ItemId { get; set; }
        public string? ItemShortName { get; set; }
        public string? ItemName { get; set; }
        public long? ItemTypeId { get; set; }
        public long? ItemCategaryId { get; set; }
        public long? ItemGenericNameId { get; set; }
        public long? ItemClassId { get; set; }
        public long? PurchaseUomid { get; set; }
        public long? StockUomid { get; set; }
        public string? ConversionFactor { get; set; }
        public long? CurrencyId { get; set; }
        public double? TaxPer { get; set; }
        public bool? IsBatchRequired { get; set; }
        public float? MinQty { get; set; }
        public float? MaxQty { get; set; }
        public float? ReOrder { get; set; }
        public string? Hsncode { get; set; }
        public double? Cgst { get; set; }
        public double? Sgst { get; set; }
        public double? Igst { get; set; }
        public long? ManufId { get; set; }
        public bool? IsNarcotic { get; set; }
        public bool? IsH1drug { get; set; }
        public bool? IsScheduleH { get; set; }
        public bool? IsHighRisk { get; set; }
        public bool? IsScheduleX { get; set; }
        public bool? IsLasa { get; set; }
        public bool? IsEmgerency { get; set; }
        public long? DrugType { get; set; }
        public string? DrugTypeName { get; set; }
        public string? ProdLocation { get; set; }
        public long? ItemCompnayId { get; set; }
        public string? ItemTime { get; set; }
        public List<AssignItemToStoreModel> MAssignItemToStores { get; set; }
    }
    public class ItemMasterModelValidator : AbstractValidator<ItemMasterModel>
    {
        public ItemMasterModelValidator()
        {
            RuleFor(x => x.ItemShortName).NotNull().NotEmpty().WithMessage("ItemShortName is required");
            RuleFor(x => x.ItemName).NotNull().NotEmpty().WithMessage("ItemName is required");
            RuleFor(x => x.ItemTypeId).NotNull().NotEmpty().WithMessage("ItemTypeId is required");
            RuleFor(x => x.ItemCategaryId).NotNull().NotEmpty().WithMessage("ItemCategaryId is required");
            RuleFor(x => x.ItemGenericNameId).NotNull().NotEmpty().WithMessage("ItemGenericNameId is required");
            RuleFor(x => x.ItemClassId).NotNull().NotEmpty().WithMessage("ItemClassId is required");
            RuleFor(x => x.PurchaseUomid).NotNull().NotEmpty().WithMessage("PurchaseUomid is required");
            RuleFor(x => x.StockUomid).NotNull().NotEmpty().WithMessage("StockUomid is required");
            RuleFor(x => x.ConversionFactor).NotNull().NotEmpty().WithMessage("ConversionFactor is required");
            RuleFor(x => x.CurrencyId).NotNull().NotEmpty().WithMessage("CurrencyId is required");
            RuleFor(x => x.IsBatchRequired).NotNull().NotEmpty().WithMessage("IsBatchRequired is required");
            RuleFor(x => x.DrugType).NotNull().NotEmpty().WithMessage("DrugType is required");
            RuleFor(x => x.ItemCompnayId).NotNull().NotEmpty().WithMessage("ItemCompnayId is required");
            RuleFor(x => x.DrugType).NotNull().NotEmpty().WithMessage("DrugType is required");
        }
    }
        public class AssignItemToStoreModel
        {
            public long AssignId { get; set; }
            public long? StoreId { get; set; }
            public long? ItemId { get; set; }
        }
        public class AssignItemToStoreModelValidator : AbstractValidator<AssignItemToStoreModel>
        {
            public AssignItemToStoreModelValidator()
            {
                RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("StoreId is required");
                RuleFor(x => x.ItemId).NotNull().NotEmpty().WithMessage("ItemId is required");
            }
        }
    public class DeleteAssignItemToStore
    {
        public long ItemId { get; set; }
    }
}

