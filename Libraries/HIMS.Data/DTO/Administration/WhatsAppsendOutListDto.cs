using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class WhatsAppsendOutListDto
    {
        public long? SMSOutGoingID { get; set; }
        public string? MobileNumber { get; set; }
        public string? SMSString { get; set; }
        //public bool? IsSent { get; set; }
        public string? SMSType { get; set; }
        //public bool? SMSFlag { get; set; }
        public DateTime? SMSDate { get; set; }
        public long? TranNo { get; set; }
        public long? TemplateId { get; set; }
        public string? SMSurl { get; set; }
        public string? FilePath { get; set; }


        //public int Status { get; set; }
        public DateTime? LastTry { get; set; }
        public string? LastResponse { get; set; }
        //public long? Retry { get; set; }


    }
}
