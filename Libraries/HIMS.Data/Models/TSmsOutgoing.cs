using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TSmsOutgoing
    {
        public long SmsoutGoingId { get; set; }
        public string? MobileNumber { get; set; }
        public string? Smsstring { get; set; }
        public bool? IsSent { get; set; }
        public string? Smstype { get; set; }
        public string? Smsflag { get; set; }
        public DateTime? Smsdate { get; set; }
        public long? TranNo { get; set; }
        public long? TemplateId { get; set; }
        public string? Smsurl { get; set; }
    }
}
