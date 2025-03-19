using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MTemplateMaster
    {
        public long TemplateId { get; set; }
        public string? TemplateName { get; set; }
        public string? TemplateDesc { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public string? TemplateDescInHtml { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? CreatedBy { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string? Category { get; set; }
        public bool? IsActive { get; set; }
    }
}
