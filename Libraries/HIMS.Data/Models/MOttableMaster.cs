using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MOttableMaster
    {
        public long OttableId { get; set; }
        public string? OttableName { get; set; }
        public long? LocationId { get; set; }
        public bool? IsActive { get; set; }
        public long? IsAddedBy { get; set; }
        public long? IsUpdatedBy { get; set; }
    }
}
