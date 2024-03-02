using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwOttableMasterList
    {
        public long OttableId { get; set; }
        public string? OttableName { get; set; }
        public long? LocationId { get; set; }
        public string? LocationName { get; set; }
        public bool? IsActive { get; set; }
        public long? IsAddedBy { get; set; }
    }
}
