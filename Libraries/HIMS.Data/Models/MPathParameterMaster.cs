using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MPathParameterMaster
    {
        public MPathParameterMaster()
        {
            MParameterDescriptiveMasters = new HashSet<MParameterDescriptiveMaster>();
            MPathParaRangeMasters = new HashSet<MPathParaRangeMaster>();
        }

        public long ParameterId { get; set; }
        public string? ParameterShortName { get; set; }
        public string? ParameterName { get; set; }
        public string? PrintParameterName { get; set; }
        public long? UnitId { get; set; }
        public long? IsNumeric { get; set; }
        public bool? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsPrintDisSummary { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
  

        public virtual ICollection<MParameterDescriptiveMaster> MParameterDescriptiveMasters { get; set; }
        public virtual ICollection<MPathParaRangeMaster> MPathParaRangeMasters { get; set; }
    }
}
