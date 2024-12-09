using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MReportTemplateConfig
    {
        public long TemplateId { get; set; }
        public string? TemplateName { get; set; }
        public string? TemplateDescription { get; set; }
    }
}
