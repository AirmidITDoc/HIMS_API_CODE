namespace HIMS.Data.DTO.Administration
{
    public class DirectPOListDto
    {
        public long PurchaseId { get; set; }
        public string? PurchaseNo { get; set; }
        public DateTime? PurchaseDate { get; set; }
        public long? SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? GrandTotal { get; set; }
        public long? StoreId { get; set; }
    }
}
