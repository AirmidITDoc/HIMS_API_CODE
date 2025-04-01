using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MPathUnitMaster
    {
        public long UnitId { get; set; }
        public string? UnitName { get; set; }
        public bool? Isdeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
