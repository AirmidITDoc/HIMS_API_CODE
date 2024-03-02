using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwStaffRefSmsquery
    {
        public string? Smsstring { get; set; }
        public string MobileNo { get; set; } = null!;
        public long AdmissionId { get; set; }
    }
}
