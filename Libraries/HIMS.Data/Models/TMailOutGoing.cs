using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TMailOutGoing
    {
        public long MailId { get; set; }
        public DateTime? MailDate { get; set; }
        public DateTime? MailTime { get; set; }
        public string? MailFrom { get; set; }
        public string? MailTo { get; set; }
        public string? Subject { get; set; }
        public string? MailDescription { get; set; }
        public string? Attachment { get; set; }
        public bool? IsSent { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
