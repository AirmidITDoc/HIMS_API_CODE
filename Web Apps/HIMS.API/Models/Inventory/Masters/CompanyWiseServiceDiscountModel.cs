namespace HIMS.API.Models.Inventory.Masters
{
    public class CompanyWiseServiceDiscountModel
    {
        public long CompServiceDetailId { get; set; }
        public bool? IsGroupOrSubGroup { get; set; }
        public long? ServiceId { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }
        public decimal? DiscountAmount { get; set; }
        public double? DiscountPercentage { get; set; }
    }
}
