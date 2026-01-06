namespace HIMS.Data.DTO.Inventory
{
    public class SalesDetailsListDto
    {
        public string? Date { get; set; }
        public string? SalesReturnNo { get; set; }
        public string? ItemName { get; set; }
        public string? BatchNo { get; set; }
        public double Qty { get; set; }
        public Decimal? MRP { get; set; }
        public long? StoreID { get; set; }
    }
    public class InPatientSalesDetailsListDto
    {
        public string? Date { get; set; }
        public string? SalesReturnNo { get; set; }
        public string? ItemName { get; set; }
        public string? BatchNo { get; set; }
        public double Qty { get; set; }
        public Decimal? MRP { get; set; }
        public long? StoreID { get; set; }
    }
}
