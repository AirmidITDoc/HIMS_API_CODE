using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.Administration
{
    public class EmailSendoutListDto
    {
        public long? Id { get; set; }
        public long? NotificationType { get; set; }
        public DateTime? SendDate { get; set; }
        public string? ToAddress { get; set; }
        public string? Subject { get; set; }
        public string? EmailBody { get; set; }
        public string? EmailCC { get; set; }
        //public bool? IsSendMail { get; set; }
        public string? AttachmentPath { get; set; }
    }
}
