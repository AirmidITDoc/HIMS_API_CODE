namespace HIMS.Data.DTO.Inventory
{
    public class IssuetodeptItemListDto
    {



        public long? IssueDepId { get; set; }
        public long? ItemId { get; set; }
        public string? ItemName { get; set; }

        public long? Qty { get; set; }
        public string? BatchNo { get; set; }
        public string? BatchExpDate { get; set; }

        public long? IssueQty { get; set; }
        public long? IssueId { get; set; }

        public long? PerUnitLandedRate { get; set; }
        public long? VatPercentage { get; set; }
        public long? LandedTotalAmount { get; set; }

        public long? Status { get; set; }

        public long? StockId { get; set; }

        public long? StoreId { get; set; }
    }
}
