using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class GroupMaster
    {
        public long GroupId { get; set; }
        public string? GroupName { get; set; }
        public bool? IsActive { get; set; }
        public bool? Isconsolidated { get; set; }
        public bool? IsConsolidatedDr { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
