using System;
using System.Collections.Generic;

namespace HIMS.Data.Models
{
    public partial class LvwRetrieveDischargeSummary
    {
        public long DischargeSummaryId { get; set; }
        public string? History { get; set; }
        public string? Diagnosis { get; set; }
        public string? Investigation { get; set; }
        public string? ClinicalFinding { get; set; }
        public string? OpertiveNotes { get; set; }
        public string? TreatmentGiven { get; set; }
        public string? TreatmentAdvisedAfterDischarge { get; set; }
        public DateTime? Followupdate { get; set; }
        public string? Remark { get; set; }
        public DateTime? OpDate { get; set; }
        public DateTime? Optime { get; set; }
        public string? DischargeDoctorName1 { get; set; }
        public string? Education { get; set; }
        public long? DischargeDoctor1 { get; set; }
        public long? DischargeDoctor2 { get; set; }
        public long? DischargeDoctor3 { get; set; }
        public string? DischargeDoctorName2 { get; set; }
        public string? DischargeDoctorName2Education { get; set; }
        public string? DischargeDoctorName3 { get; set; }
        public string? DischargeDoctorName3Education { get; set; }
        public DateTime? DischargeSummaryTime { get; set; }
        public long DischargeId { get; set; }
        public string? DischargeTypeName { get; set; }
        public long? AdmissionId { get; set; }
        public string Dod { get; set; } = null!;
        public string Dot { get; set; } = null!;
        public DateTime? DischargeDate { get; set; }
        public DateTime? DischargeTime { get; set; }
        public string? DoctorAssistantName { get; set; }
    }
}
