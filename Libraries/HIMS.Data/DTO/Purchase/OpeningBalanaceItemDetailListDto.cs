namespace HIMS.Data.DTO.Purchase
{
    public class OpeningBalanaceItemDetailListDto
    {
        public string? ItemName { get; set; }
        public string? BatchNo { get; set; }
        public string? BatchExpDate { get; set; }
        public decimal? PerUnitPurRate { get; set; }
        public decimal PerUnitLandedRate { get; set; }
        public decimal? PerUnitMrp { get; set; }
        public float CGSTPer { get; set; }
        public float SGSTPer { get; set; }
        public float IGSTPer { get; set; }
        public double Gstper { get; set; }
        public float TotalQty { get; set; }
        public long OpeningHeaderId { get; set; }




    }
}
