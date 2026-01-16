
namespace HIMS.Data.DTO.Inventory
{
    public class ItemMasterListDto
    {
        public long ItemID { get; set; }
        public string ItemShortName { get; set; }
        public string ItemName { get; set; }
        public string ItemTypeName { get; set; }
        public long ItemTypeID { get; set; }
        public long ItemCategaryId { get; set; }
        public string ItemCategoryName { get; set; }
        public long ItemClassId { get; set; }
        public string ItemClassName { get; set; }
        public long ItemGenericNameId { get; set; }
        public string ItemGenericName { get; set; }
        public long PurchaseUOMId { get; set; }
        public string PuchaseUOM { get; set; }
        public long StockUOMId { get; set; }
        public string StockUOM { get; set; }
        public string ConversionFactor { get; set; }
        public long CurrencyId { get; set; }
        public string CurrencyName { get; set; }
        public double TaxPer { get; set; }
        public bool IsActive { get; set; }
        public long Addedby { get; set; }
        public bool IsBatchRequired { get; set; }
        public float MinQty { get; set; }
        public float MaxQty { get; set; }
        public float ReOrder { get; set; }
        public string StoreName { get; set; }
        public long StoreId { get; set; }
        public string HSNcode { get; set; }
        public double CGST { get; set; }
        public double SGST { get; set; }
        public double IGST { get; set; }
        public long ManufId { get; set; }
        public bool IsNarcotic { get; set; }
        public string ProdLocation { get; set; }
        public bool IsH1Drug { get; set; }
        public bool IsScheduleH { get; set; }
        public bool IsHighRisk { get; set; }
        public bool IsScheduleX { get; set; }
        public bool IsLASA { get; set; }
        public bool IsEmgerency { get; set; }
        public long DrugType { get; set; }
        public string DrugTypeName { get; set; }
        public long ItemCompnayId { get; set; }
        public string ManufName { get; set; }
        public string UserName { get; set; }
        public string? Content { get; set; }
        public bool IsValidContent { get; set; }
        public string? DoseName { get; set; }



    }
    public class ItemListForSearch
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }
         public double? BalanceQty { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? UnitMRP { get; set; }
        public decimal? PurchaseRate { get; set; }
        public decimal? VatPercentage { get; set; }
        public bool? IsBatchRequired { get; set; }
        public float? ReOrder { get; set; }
        public bool? IsNarcotic { get; set; }
        public float CGSTPer { get; set; }
        public float SGSTPer { get; set; }
        public float? IGSTPer { get; set; }
        public string? U0M { get; set; }
        public long? ItemGenericNameId { get; set; }
        public string? DoseName { get; set; }
        public int? DoseDay { get; set; }

        public bool? IsH1Drug { get; set; }
        public bool? IsHighRisk { get; set; }
        public bool? isEmgerency { get; set; }
        public bool? IsLASA { get; set; }
        public string FormattedText { get { return this.ItemName + " | " + this.BalanceQty; } }

    }


    public class ItemListForSearchDTO
    {
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public float? BalanceQty { get; set; }
        public long? LandedRate { get; set; }
        public long? UnitMRP { get; set; }
        public long? PurchaseRate { get; set; }
        public string? DoseName { get; set; }
        public long? DoseDay { get; set; }
        public string? Instruction { get; set; }
        public string? HSNcode { get; set; }
        public double? CGSTPer { get; set; }
        public double? SGSTPer { get; set; }
        public double? IGSTPer { get; set; }
        public string? ConverFactor { get; set; }
        public long? StoreId { get; set; }
        public long? UMOId { get; set; }
        public string UMOName { get; set; }
        public string? ItemCompanyName { get; set; }
        //public string FormattedText { get { return this.ItemName + " | " + this.BalanceQty + " | " + this.UnitMRP + " | " + this.PurchaseRate; } }
        public string FormattedText { get { return this.ItemName + " | " + this.BalanceQty; } }
        public double? TaxPer { get; set; }
    }
    public class ItemListForBatchPopDTO
    {
        public long StockId { get; set; }
        public long? StoreId { get; set; }
        public long? ItemId { get; set; }
        public string? ItemName { get; set; }
        public float? BalanceQty { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? UnitMRP { get; set; }
        public decimal? PurchaseRate { get; set; }
        public decimal? VatPercentage { get; set; }
        public double? CGSTPer { get; set; }
        public double? SGSTPer { get; set; }
        public double? IGSTPer { get; set; }
        public string? ConverFactor { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public float? GrnRetQty { get; set; }
        public string DrugTypeName { get; set; }
        public string ManufactureName { get; set; }
        public string FormattedText { get { return this.ItemName + " | " + this.BalanceQty + " | " + this.UnitMRP + " | " + this.PurchaseRate; } }
        public string ConversionFactor { get; set; }
        public long? ExpDays { get; set; }
        public long? DaysFlag { get; set; }
        public double? MinQty { get; set; }
        public double? MaxQty { get; set; }
        public long? ItemGenericNameId { get; set; }
        public string? ItemGenericName { get; set; }
        public string? ProdLocation { get; set; }
    }
    public class ItemListForBatchDTO
    {
        public long StockId { get; set; }
        public long? StoreId { get; set; }
        public long? ItemId { get; set; }
        public string? ItemName { get; set; }
        public float? BalanceQty { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? UnitMRP { get; set; }
        public decimal? PurchaseRate { get; set; }
        public decimal? VatPercentage { get; set; }
        public bool? IsBatchRequired { get; set; }
        public string? BatchNo { get; set; }
        public float? CGSTPer { get; set; }
        public float? SGSTPer { get; set; }
        public float? IGSTPer { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public float? GrnRetQty { get; set; }
        public string? DrugTypeName { get; set; }
        public string? ManufactureName { get; set; }
        public string FormattedText { get { return this.ItemName + " | " + this.BalanceQty + " | " + this.UnitMRP + " | " + this.PurchaseRate; } }
        public string? ConversionFactor { get; set; }
        public string? ProdLocation { get; set; }
        public long? ItemGenericNameId { get; set; }
        public string? ItemGenericName { get; set; }
        public string? ExpDays { get; set; }
        public string? DaysFlag { get; set; }
    }


    public class ItemListForSalesPageDTO
    {
        public long StockId { get; set; }
        public long? StoreId { get; set; }
        public long? ItemId { get; set; }
        public string? ItemName { get; set; }
        public float? BalanceQty { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? UnitMRP { get; set; }
        public decimal? PurchaseRate { get; set; }
        public double? VatPercentage { get; set; }
        public double? CGSTPer { get; set; }
        public double? SGSTPer { get; set; }
        public double? IGSTPer { get; set; }
        public string? ConverFactor { get; set; }
        public string? BatchNo { get; set; }
        public DateTime? BatchExpDate { get; set; }
        public float? GrnRetQty { get; set; }
        public string DrugTypeName { get; set; }
        public string UOM { get; set; }
        public string FormattedText { get { return this.ItemName + " | " + this.BalanceQty + " | " + this.UnitMRP + " | " + this.PurchaseRate; } }
    }

    public class ItemListForGRNOrPO
    {
        public long ItemId { get; set; }                 
        public string? ItemName { get; set; }            

        public long? UMOId { get; set; }                 
        public string? UMOName { get; set; }             

        public string? ConverFactor { get; set; }        

        public long? StoreId { get; set; }               

        public float? BalanceQty { get; set; }           

        public double? CGSTPer { get; set; }             
        public double? SGSTPer { get; set; }             
        public double? IGSTPer { get; set; }             

        public string? HSNcode { get; set; }             

        public string? ItemCompanyName { get; set; }     

        public long? TaxPer { get; set; }
    }
}
