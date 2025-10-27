namespace HIMS.Data.DTO.Inventory
{
    public class SalesSummaryListDto
    {
        public string? ItemName { get; set; }
        public string? BatchNo { get; set; }
        public double? SalesQty { get; set; }
        public long? StoreID { get; set; }
    }
}
