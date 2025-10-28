namespace HIMS.Data.Models
{
    public partial class MRadiologyTemplateMaster
    {
        public MRadiologyTemplateMaster()
        {
            MRadiologyTemplateDetails = new HashSet<MRadiologyTemplateDetail>();
        }

        public long TemplateId { get; set; }
        public string? TemplateName { get; set; }
        public string? TemplateDesc { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public string? TemplateDescInHtml { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual ICollection<MRadiologyTemplateDetail> MRadiologyTemplateDetails { get; set; }
    }
}
