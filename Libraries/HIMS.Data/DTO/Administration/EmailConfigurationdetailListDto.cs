using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class EmailConfigurationdetailListDto
    {
        //public long? Id { get; set; }
        public string? Displayname { get; set; }
        public string? Emailaddress { get; set; }
        public string? Mailserver { get; set; }

        //public int? smtpport { get; set; }
        //public long? servertimeout { get; set; }

        public bool? IsSendMail { get; set; }
        public bool? ReqAuthenticate { get; set; }

        public bool? passauthenticate { get; set; }

        public string? userName { get; set; }

        public string? Password { get; set; }
        public bool? IsActive { get; set; }
    }
}
