using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TMailOutgoing
    {
        public long Id { get; set; }
        public string? FromEmail { get; set; }
        public string? FromName { get; set; }
        public string? ToEmail { get; set; }
        public string? Cc { get; set; }
        public string? Bcc { get; set; }
        public string? MailSubject { get; set; }
        public string? MailBody { get; set; }
        public string? AttachmentName { get; set; }
        public string? AttachmentLink { get; set; }
        public long? TranNo { get; set; }
        public string? EmailType { get; set; }
        public DateTime? EmailDate { get; set; }
        public int? Status { get; set; }
        public DateTime? LastTry { get; set; }
        public string? LastResponse { get; set; }
        public int? Retry { get; set; }
        public DateTime? LastSendingTry { get; set; }
    }
}
