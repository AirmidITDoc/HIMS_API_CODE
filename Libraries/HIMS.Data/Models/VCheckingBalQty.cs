namespace HIMS.Data.Models
{
    public partial class VCheckingBalQty
    {
        public long StockId { get; set; }
        public long? StoreId { get; set; }
        public long? ItemId { get; set; }
        public float? ReceivedQty { get; set; }
        public float? IssueQty { get; set; }
        public float? BalanceQty { get; set; }
        public float? BalanceQtyChecking { get; set; }
    }
}
