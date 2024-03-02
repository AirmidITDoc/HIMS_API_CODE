using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class EmailConfiguration
    {
        public int Id { get; set; }
        public string? DisplayName { get; set; }
        public string? EmailAddress { get; set; }
        public string? MailServerSmtp { get; set; }
        public short? SmtpPort { get; set; }
        public int? ServerTimeout { get; set; }
        public bool? SmtpRequiredAuthentication { get; set; }
        public bool? RequiredSquiredPasswordAuthentication { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool? IsActive { get; set; }
    }
}
