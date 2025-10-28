namespace HIMS.Data.Models
{
    public partial class ServiceWiseCompanyCode
    {
        public long ServiceDetCompId { get; set; }
        public long? ServiceId { get; set; }
        public long? TariffId { get; set; }
        public string? CompanyCode { get; set; }
        public string? CompanyServicePrint { get; set; }
        public bool? IsInclusionOrExclusion { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
