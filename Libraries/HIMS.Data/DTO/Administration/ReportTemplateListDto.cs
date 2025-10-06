using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class ReportTemplateListDto
    {
        public long TemplateId { get; set; }
        public string? TemplateName { get; set; }
        public string? TemplateDescription { get; set; }
        public bool? IsActive { get; set; }
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
