namespace HIMS.Data.DTO.Purchase
{
    public class LastthreeItemListDto
    {
        public long ItemId { get; set; }
        public String? ItemName { get; set; }
        public float? ReceiveQty { get; set; }
        public float? FreeQty { get; set; }
        public decimal? MRP { get; set; }
        public decimal? Rate { get; set; }
        public double? VatPercentage { get; set; }
        public string? SupplierName { get; set; }



    }
}
