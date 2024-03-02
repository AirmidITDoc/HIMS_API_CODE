using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LocationMaster
    {
        public long LocationId { get; set; }
        public string? LocationName { get; set; }
        public bool? IsActive { get; set; }
    }
}
