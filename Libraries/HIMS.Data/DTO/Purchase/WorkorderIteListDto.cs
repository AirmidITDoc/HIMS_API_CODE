namespace HIMS.Data.DTO.Purchase
{
    public class WorkorderIteListDto
    {

        public string? ItemName { get; set; }
        public long? Qty { get; set; }
        public decimal? Rate { get; set; }
        public decimal? TotalAmount { get; set; }
        public decimal? DiscPer { get; set; }
        public decimal? DiscAmount { get; set; }
        public decimal? VATPer { get; set; }
        public decimal? VATAmount { get; set; }
        public decimal? NetAmount { get; set; }
        public string? Remark { get; set; }
        public long? PendQty { get; set; }
        public long? ItemID { get; set; }
    }
}
