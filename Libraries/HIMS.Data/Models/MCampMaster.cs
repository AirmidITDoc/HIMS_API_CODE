namespace HIMS.Data.Models
{
    public partial class MCampMaster
    {
        public long CampId { get; set; }
        public string? CampName { get; set; }
        public string? CampLocation { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
