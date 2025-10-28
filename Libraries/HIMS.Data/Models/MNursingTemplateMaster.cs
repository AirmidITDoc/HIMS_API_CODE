namespace HIMS.Data.Models
{
    public partial class MNursingTemplateMaster
    {
        public long NursingId { get; set; }
        public string? NursTempName { get; set; }
        public string? TemplateDesc { get; set; }
        public bool? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Category { get; set; }
    }
}
