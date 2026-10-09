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
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
    }
}
