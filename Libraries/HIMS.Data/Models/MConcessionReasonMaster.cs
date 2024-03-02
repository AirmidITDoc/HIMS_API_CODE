using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MConcessionReasonMaster
    {
        public long ConcessionId { get; set; }
        public string? ConcessionReason { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
