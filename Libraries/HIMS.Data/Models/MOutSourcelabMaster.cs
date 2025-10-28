namespace HIMS.Data.Models
{
    public partial class MOutSourcelabMaster
    {
        public long OutSourceId { get; set; }
        public string? OutSourceLabName { get; set; }
        public string? ContactPersonName { get; set; }
        public long? MobileNo { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
