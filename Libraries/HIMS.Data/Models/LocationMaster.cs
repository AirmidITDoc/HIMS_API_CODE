namespace HIMS.Data.Models
{
    public partial class LocationMaster
    {
        public long LocationId { get; set; }
        public string? LocationName { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
