namespace HIMS.Data.Models
{
    public partial class TStockAdjustment
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
        public DateTime? CreatedOn { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
