namespace HIMS.Data.DTO.Inventory
{
    public class CurrentStockListDto
    {
        public long StoreId { get; set; }
        public string StoreName { get; set; }
        public long ItemId { get; set; }
        public string ItemName { get; set; }
        public string ReceivedQty { get; set; }
        public string IssueQty { get; set; }
        public string BalanceQty { get; set; }
        public float ReturnQty { get; set; }

    }
}
