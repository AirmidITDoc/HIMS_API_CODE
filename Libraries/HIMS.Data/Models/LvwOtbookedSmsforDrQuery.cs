using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwOtbookedSmsforDrQuery
    {
        public string? Smsstring { get; set; }
        public string? DrMobileNo { get; set; }
        public long OtbookingId { get; set; }
    }
}
