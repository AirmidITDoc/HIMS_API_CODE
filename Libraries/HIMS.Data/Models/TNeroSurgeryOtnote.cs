using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TNeroSurgeryOtnote
    {
        public long OtneroId { get; set; }
        public long? AdmissionId { get; set; }
        public long? OpIpType { get; set; }
        public DateTime? TranDate { get; set; }
        public DateTime? TranTime { get; set; }
        public DateTime? Otdate { get; set; }
        public DateTime? Ottime { get; set; }
        public string? SurgeryName { get; set; }
        public long? SurgeonId { get; set; }
        public long? SurgeonId1 { get; set; }
        public long? SurgeonId2 { get; set; }
        public long? AnesthetishId { get; set; }
        public long? AnesthetishId1 { get; set; }
        public long? AnesthetishId2 { get; set; }
        public string? AnesthetishType { get; set; }
        public string? Position { get; set; }
        public string? BloodLoss { get; set; }
        public string? Findings { get; set; }
        public string? Surgery { get; set; }
        public string? AdviceonDischarge { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public string? SurgeryType { get; set; }
    }
}
