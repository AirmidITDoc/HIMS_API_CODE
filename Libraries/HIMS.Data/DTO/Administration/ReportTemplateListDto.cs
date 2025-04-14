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
    }
}
