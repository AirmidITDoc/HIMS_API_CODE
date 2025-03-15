using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public  class LabOrRadRequestDetailListDto
    {
        public long ReqDetId { get; set; }
        public string? ServiceName { get; set; }
        public long? ServiceId { get; set; }
        public long OP_IP_ID { get; set; }
        public long OP_IP_Type { get; set; }
        public string IsStatus { get; set; }
        public string ReqDate { get; set; }
        public long? RequestId { get; set; }
        public string ReqTime { get; set; }
        public string? AddedByName { get; set; }
        public string BillingUser { get; set; }
        public string AddedByDate { get; set; }
        public long IsPathology { get; set; }
        public long IsRadiology { get; set; }
        public long CharId { get; set; }
        public string IsTestCompted { get; set; }
        public string PBillNo { get; set; }

    }
}
    