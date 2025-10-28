using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class EmailNotificationDatum
    {
        public long Id { get; set; }
        public int? NotificationType { get; set; }
        public DateTime? SendDate { get; set; }
        public string? ToAddress { get; set; }
        public string? Subject { get; set; }
        public string? EmailBody { get; set; }
        public string? EmailCc { get; set; }
        public int? IsSendMail { get; set; }
        public string? AttachmentPath { get; set; }
    }
}
