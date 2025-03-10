using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.OPPatient
{
    public class OPRequestListDto
    {
        public long RequestTranId { get; set; }
        public long OP_IP_ID { get; set; }
        public long? ServiceId { get; set; }
        public string? ServiceName { get; set; }
       
    }
}
