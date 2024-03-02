using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MComplaintMaster
    {
        public long ComplaintId { get; set; }
        public string? ComplaintDescr { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
