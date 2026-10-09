using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OTManagement
{
    public  class OtBillTemplateListDto
    {
        public long TemplateId { get; set; }
        public long ServiceId { get; set; }
        public double? Percentage { get; set; }
        public string? ServiceName { get; set; }
        public bool? IsActive { get; set; }
        public string? CreatedBy { get; set; }
        public string? CreatedDateTime { get; set; }
        public string? UpdatedBy { get; set; }
        public string? ModifiedDateTime { get; set; }
    }
}
