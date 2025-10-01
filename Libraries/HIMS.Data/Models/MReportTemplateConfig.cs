using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MReportTemplateConfig
    {
        public long TemplateId { get; set; }
        public string? TemplateName { get; set; }
        public string? TemplateDescription { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? DepartmentId { get; set; }
        public string? CategoryName { get; set; }
        public string? TemplateHeader { get; set; }
        public string? TemplateFooter { get; set; }
        public bool? IsTemplateWithHeader { get; set; }
        public bool? IsTemplateHeaderWithImage { get; set; }
        public bool? IsTemplateWithFooter { get; set; }
        public bool? IsTemplateFooterWithImage { get; set; }
    }
}
