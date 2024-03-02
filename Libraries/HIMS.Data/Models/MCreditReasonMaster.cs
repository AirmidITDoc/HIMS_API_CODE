using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MCreditReasonMaster
    {
        public long CreditId { get; set; }
        public string? CreditReason { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
