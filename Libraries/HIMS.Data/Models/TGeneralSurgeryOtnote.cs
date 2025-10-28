using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class TGeneralSurgeryOtnote
    {
        public long OtgenSurId { get; set; }
        public long? AdmissionId { get; set; }
        public long? OpIpType { get; set; }
        public DateTime? TranDate { get; set; }
        public DateTime? TranTime { get; set; }
        public DateTime? Otdate { get; set; }
        public DateTime? Ottime { get; set; }
        public string? SurgeryName { get; set; }
        public long? SurgeonId { get; set; }
        public long? SurgeonId1 { get; set; }
        public string? Assistant { get; set; }
        public long? AnesthetishId { get; set; }
        public long? AnesthetishId1 { get; set; }
        public long? AnesthetishId2 { get; set; }
        public string? AnesthetishType { get; set; }
        public string? Incision { get; set; }
        public string? OperativeDiagnosis { get; set; }
        public string? OperativeFindings { get; set; }
        public string? OperativeProcedure { get; set; }
        public string? ExtraProPerformed { get; set; }
        public string? ClosureTechnique { get; set; }
        public string? PostOpertiveInstru { get; set; }
        public string? DetSpecimenForLab { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public string? SurgeryType { get; set; }
        public DateTime? FromTime { get; set; }
        public DateTime? ToTime { get; set; }
    }
}
