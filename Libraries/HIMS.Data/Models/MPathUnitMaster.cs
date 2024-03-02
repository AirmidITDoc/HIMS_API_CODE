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
    }
}
