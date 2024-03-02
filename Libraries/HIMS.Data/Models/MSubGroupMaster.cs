using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MSubGroupMaster
    {
        public long SubGroupId { get; set; }
        public long? GroupId { get; set; }
        public string? SubGroupName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
