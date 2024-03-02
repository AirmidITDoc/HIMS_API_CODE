using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwVisitAppPatientForSmsquery
    {
        public string? Smsstring { get; set; }
        public string? MobileNo { get; set; }
        public long VisitId { get; set; }
    }
}
