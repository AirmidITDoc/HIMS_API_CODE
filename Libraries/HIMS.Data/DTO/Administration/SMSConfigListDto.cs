using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class SMSConfigListDto
    {
        
        public long? SMSOutGoingID { get; set; }
        public string? SMSString { get; set; }

        //public long? SMSType { get; set; }
        //public string? SMSDate { get; set; }
        //public long? TemplateId { get; set; }
        //public string? MobileNumber { get; set; }

        //public bool? IsSent { get; set; }
    }
}
