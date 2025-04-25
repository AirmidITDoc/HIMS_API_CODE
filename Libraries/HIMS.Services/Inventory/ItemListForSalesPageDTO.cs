namespace HIMS.Services.Inventory
{
    public class ItemListForSalesPageDTO
    {
        public long? StoreId { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public float BalanceQty { get; set; }
        public decimal? LandedRate { get; set; }
        public decimal? UnitMRP { get; set; }
        public decimal? PurchaseRate { get; set; }
        public float? CGSTPer { get; set; }
        public float? SGSTPer { get; set; }
        public float? IGSTPer { get; set; }
        public string UOM { get; set; }
    }
}