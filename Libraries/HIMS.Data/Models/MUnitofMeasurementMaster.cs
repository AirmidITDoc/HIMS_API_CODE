using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class MUnitofMeasurementMaster
    {
        public long UnitofMeasurementId { get; set; }
        public string? UnitofMeasurementName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
}
