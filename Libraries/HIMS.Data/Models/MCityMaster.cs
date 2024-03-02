using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MCityMaster
    {
        public long CityId { get; set; }
        public string? CityName { get; set; }
        public long? StateId { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
