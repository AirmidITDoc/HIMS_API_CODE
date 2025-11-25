using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Inventory
{
    public  class TemplateByCategoryListDto
    {
        public long? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public string? TemplateName { get; set; }
        public string? HospitalHeader { get; set; }
        public string? TemplateHeader { get; set; }
        public string? TemplateDescription { get; set; }
        public bool? IsTemplateWithHeader { get; set; }
        public bool? IsTemplateHeaderWithImage { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public long TemplateId { get; set; }



    }
}
