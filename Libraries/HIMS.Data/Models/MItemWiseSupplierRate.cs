namespace HIMS.Data.Models
{
    public partial class MItemWiseSupplierRate
    {
        public long DefId { get; set; }
        public long? ItemId { get; set; }
        public long? SupplierId { get; set; }
        public decimal? SupplierRate { get; set; }
    }
}
