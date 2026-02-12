using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MPathSpecimenPreservativeMaster
    {
        public long SpecimenPreservativeId { get; set; }
        public string? PreservativeUsed { get; set; }
        public long? UnitId { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
