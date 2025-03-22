using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MConcessionReasonMaster
    {
        public long ConcessionId { get; set; }
        public string? ConcessionReason { get; set; }
        public bool? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
