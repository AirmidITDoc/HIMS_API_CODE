using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwEndoscopyNote
    {
        public long Otid { get; set; }
        public long? AdmissionId { get; set; }
        public DateTime? Otdate { get; set; }
        public DateTime? Ottime { get; set; }
        public long? SurgeonId { get; set; }
        public long? SurgeonId1 { get; set; }
        public long? AnesthetishId { get; set; }
        public long? AnesthetishId1 { get; set; }
        public string? AnesthetishType { get; set; }
        public string? Incision { get; set; }
        public string? OperativeDiagnosis { get; set; }
        public string? OperativeFindings { get; set; }
        public string? OperativeProcedure { get; set; }
        public string? ExtraProPerformed { get; set; }
        public string? ClosureTechnique { get; set; }
        public string? PostOpertiveInstru { get; set; }
        public string? DetSpecimenForLab { get; set; }
        public string? Assistant { get; set; }
        public string? SurgeryName { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
    }
}
