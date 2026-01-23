using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TPathDispatchReportHistory
    {
        public long DispatchId { get; set; }
        public long? LabPatientId { get; set; }
        public long? UnitId { get; set; }
        public long? DispatchModeId { get; set; }
        public string? Comments { get; set; }
        public long? DispatchBy { get; set; }
        public DateTime? DispatchOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
