namespace HIMS.Data.Models
{
    public partial class MPackageDetail
    {
        public long PackageId { get; set; }
        public bool? IsPackageType { get; set; }
        public long? ServiceId { get; set; }
        public long? PackageServiceId { get; set; }
        public int? QtyLimit { get; set; }
        public decimal? Price { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
