using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class SsConfigInitiateDischarge
    {
        public long SsInitateDiscId { get; set; }
        public string? DepartmentName { get; set; }
        public long? DepartmentId { get; set; }
        public long? AddedBy { get; set; }
        public DateTime? AddedByDatetime { get; set; }
        public long? UpdatedBy { get; set; }
        public DateTime? UpdatedByDatetime { get; set; }
    }
}
