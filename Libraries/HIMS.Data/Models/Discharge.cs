using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class Discharge
    {
        public long DischargeId { get; set; }
        public long? AdmissionId { get; set; }
        public DateTime? DischargeDate { get; set; }
        public DateTime? DischargeTime { get; set; }
        public long? IsCancelled { get; set; }
        public long? DischargeTypeId { get; set; }
        public long? DischargedDocId { get; set; }
        public long? DischargedRmoid { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? IsCancelledby { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public bool? IsMrdreceived { get; set; }
        public DateTime? MrdreceivedDate { get; set; }
        public DateTime? MrdreceivedTime { get; set; }
        public long? MrdreceivedUserId { get; set; }
        public string? MrdreceivedName { get; set; }
    }
}
