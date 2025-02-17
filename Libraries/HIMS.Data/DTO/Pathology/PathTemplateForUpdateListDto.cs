using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Pathology
{
    public class PathTemplateForUpdateListDto
    {
        public long TestId { get; set; }
        public long IsTemplateTest { get; set; }
        public long TemplateId { get; set; }
        public string TemplateName { get; set; }
       

    }
}
