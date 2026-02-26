using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MedicalRecordConfig
    {
        public long Id { get; set; }
        public string Code { get; set; } = null!;
        public int IntervalHours { get; set; }
        public bool IsActive { get; set; }
    }
}
