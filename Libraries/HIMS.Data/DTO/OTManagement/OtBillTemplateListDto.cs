using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OTManagement
{
    public  class OtBillTemplateListDto
    {
        public long OTBTempId { get; set; }
        public string? OtBillTemplateName { get; set; }
        public long? CreatedBy { get; set; }
        public DateTime? CreatedDateTime { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDateTime { get; set; }
        public string? CreatedByUserName { get; set; }
        public string? UpdatedByUserName { get; set; }
    }
    public class OtBillTemplateDetailDto
    {
        public long OTBTempId { get; set; }
        public long OtbtempDetId { get; set; }
        public long ServiceId { get; set; }
        public string? ServiceName { get; set; }
        public string? CreatedByName { get; set; }
        public string? UPdatedByName { get; set; }
        public double? Percentage { get; set; }
        public long? CreatedBy { get; set; }
        public string? CreatedDateTime { get; set; }
        public long? ModifiedBy { get; set; }
        public string? ModifiedDateTime { get; set; }
    }
}
