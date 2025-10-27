namespace HIMS.Data.Models
{
    public partial class MSiteDescriptionMaster
    {
        public long SiteDescId { get; set; }
        public string? SiteDescriptionName { get; set; }
        public bool? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? SurgeryCategoryId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
