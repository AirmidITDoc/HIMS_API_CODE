using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class RegistrationSmsquery
    {
        public string? Smsstring { get; set; }
        public string? MobileNo { get; set; }
        public long RegId { get; set; }
    }
}
