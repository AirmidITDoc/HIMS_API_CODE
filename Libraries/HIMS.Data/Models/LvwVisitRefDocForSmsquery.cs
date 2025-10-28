using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwVisitRefDocForSmsquery
    {
        public string? Smsstring { get; set; }
        public string? RefDocMobileNo { get; set; }
        public long VisitId { get; set; }
    }
}
