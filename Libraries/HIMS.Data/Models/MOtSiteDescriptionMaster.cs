namespace HIMS.Data.Models
{
    public partial class MOtSiteDescriptionMaster
    {
        public long SiteDescId { get; set; }
        public string? SiteDescriptionName { get; set; }
        public long? SurgeryCategoryId { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
    }
}
