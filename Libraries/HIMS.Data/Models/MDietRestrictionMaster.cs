using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MDietRestrictionMaster
    {
        public long RestrictionId { get; set; }
        public string RestrictionCode { get; set; } = null!;
        public string RestrictionName { get; set; } = null!;
        public long RestrictionTypeId { get; set; }
        public string? Description { get; set; }
        public bool? Active { get; set; }
        public long CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
