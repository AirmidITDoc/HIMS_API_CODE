namespace HIMS.Data.DTO.Inventory
{
    public class SalesReturnDetailsListDto
    {
        public string? Date { get; set; }
        public string? SalesReturnNo { get; set; }
        public string? ItemName { get; set; }
        public string? BatchNo { get; set; }
        public double? Qty { get; set; }
        public decimal? MRP { get; set; }
        public long? StoreID { get; set; }




    }
}
