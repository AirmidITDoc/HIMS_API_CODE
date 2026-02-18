using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class EmailTemplateMaster
    {
        public long Id { get; set; }
        public string TemplateCode { get; set; } = null!;
        public string MailSubject { get; set; } = null!;
        public string MailBody { get; set; } = null!;
        public string? Wabody { get; set; }
        public string FromEmail { get; set; } = null!;
        public string FromName { get; set; } = null!;
        public string? ToMail { get; set; }
        public string? Cc { get; set; }
        public string? Bcc { get; set; }
        public bool IsWa { get; set; }
        public bool IsActive { get; set; }
    }
}
