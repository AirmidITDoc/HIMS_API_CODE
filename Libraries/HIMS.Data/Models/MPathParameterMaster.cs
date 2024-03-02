using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MPathParameterMaster
    {
        public long ParameterId { get; set; }
        public string? ParameterShortName { get; set; }
        public string? ParameterName { get; set; }
        public string? PrintParameterName { get; set; }
        public long? UnitId { get; set; }
        public long? IsNumeric { get; set; }
        public bool? Isdeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsPrintDisSummary { get; set; }
    }
}
