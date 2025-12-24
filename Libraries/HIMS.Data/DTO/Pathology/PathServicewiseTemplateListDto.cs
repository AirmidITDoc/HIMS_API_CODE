using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class PathServicewiseTemplateListDto
    {
        public long TemplateId { get; set; }
        public string? TemplateName { get; set; }
        public string TemplateDesc { get; set; }
        public long ServiceId { get; set; }

    }
}
