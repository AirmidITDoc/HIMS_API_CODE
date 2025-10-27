namespace HIMS.Data.DTO.Inventory
{
    public class ItemMovementListDto
    {
        public string TransactionDate { get; set; }
        public string TranDate { get; set; }
        public DateTime TransactionTime { get; set; }
        public string MovementNo { get; set; }
        public string FromStoreName { get; set; }
        public string ToStoreName { get; set; }
        public string TransactionType { get; set; }
        public string DocumentNo { get; set; }
        public string ItemName { get; set; }
        public string BatchNo { get; set; }
        public DateTime BatchExpDate { get; set; }
        public double ReceiptQty { get; set; }
        public double IssueQty { get; set; }
        public double BalQty { get; set; }
        public long MovementId { get; set; }
        public string SupplierName { get; set; }
        public double ReturnQty { get; set; }

    }
}
