using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public  class LabOrRadRequestListDto
    {
        public string? PatientName { get; set; }
        public string? RegNo { get; set; }
        public DateTime ReqDate { get; set; }
        public DateTime ReqTime { get; set; }
        public long RequestId { get; set; }
        public long OP_IP_ID { get; set; }
        public long OP_IP_Type { get; set; }
        public string AdmDate { get; set; }
        public string? WardName { get; set; }
        public string? BedName { get; set; }
        public byte IsType { get; set; }
        public bool IsOnFileTest { get; set; }
        public bool IsTestCompted { get; set; }

    }
}
    