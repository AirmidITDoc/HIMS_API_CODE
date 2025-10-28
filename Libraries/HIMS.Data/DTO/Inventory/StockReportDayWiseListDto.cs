namespace HIMS.Data.DTO.Inventory
{
    public class StockReportDayWiseListDto
    {
        public string ItemName { get; set; }
        public long StockId { get; set; }
        public long StoreId { get; set; }
        public long ItemId { get; set; }
        public float OpeningBalance { get; set; }
        public float ReceivedQty { get; set; }
        public float IssueQty { get; set; }
        public float BalanceQty { get; set; }
        public decimal UnitMRP { get; set; }
        public decimal PurchaseRate { get; set; }
        public decimal LandedRate { get; set; }
        public decimal VatPercentage { get; set; }
        public string BatchNo { get; set; }
        public DateTime BatchExpDate { get; set; }
        public decimal PurUnitRate { get; set; }
        public decimal PurUnitRateWF { get; set; }
        public float CGSTPer { get; set; }
        public float SGSTPer { get; set; }
        public float IGSTPer { get; set; }
        public long BarCodeSeqNo { get; set; }
        public long IStkId { get; set; }
        public float GrnRetQty { get; set; }
        public DateTime LedgerDate { get; set; }
        public float ClosingBalance { get; set; }

    }
}
