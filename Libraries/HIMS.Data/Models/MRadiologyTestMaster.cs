using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MRadiologyTestMaster
    {
        public long TestId { get; set; }
        public string? TestName { get; set; }
        public string? PrintTestName { get; set; }
        public long? CategoryId { get; set; }
        public bool? Isdeleted { get; set; }
        public long? Addedby { get; set; }
        public long? Updatedby { get; set; }
        public long? ServiceId { get; set; }
    }
}
