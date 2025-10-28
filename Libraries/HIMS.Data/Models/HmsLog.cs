using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class HmsLog
    {
        public long LogNumber { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string? Description { get; set; }
    }
}
