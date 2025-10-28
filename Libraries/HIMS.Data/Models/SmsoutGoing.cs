using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class SmsoutGoing
    {
        public long SmsoutGoingId { get; set; }
        public string? MobileNumber { get; set; }
        public string? Smsstring { get; set; }
        public bool? IsSent { get; set; }
        public DateTime? Smsdate { get; set; }
        public string? Smsurl { get; set; }
    }
}
