using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HIMS.Data.DTO.IPPatient
{
    public class DischrageSummaryListDTo
    {
        public long? AdmissionId { get; set; }
        public long? DischargeId { get; set; }
        public string? History { get; set; }
        public string? Diagnosis { get; set; }
        public string? Investigation { get; set; }
        public string? ClinicalFinding { get; set; }
        public string? OpertiveNotes { get; set; }
        public string? TreatmentGiven { get; set; }
        public string? TreatmentAdvisedAfterDischarge { get; set; }
        public DateTime? Followupdate { get; set; }
        public string? Remark { get; set; }
        public DateTime? DischargeSummaryDate { get; set; }
        public DateTime? OpDate { get; set; }
        public string? Optime { get; set; }
        public long? DischargeDoctor1 { get; set; }
        public long? DischargeDoctor2 { get; set; }
        public long? DischargeDoctor3 { get; set; }
        public string? DischargeSummaryTime { get; set; }
        public string? DoctorAssistantName { get; set; }
        public string? ClaimNumber { get; set; }
        public string? PreOthNumber { get; set; }
        public long? AddedBy { get; set; }
        public string? SurgeryProcDone { get; set; }
        public string? Icd10code { get; set; }
        public string? ClinicalConditionOnAdmisssion { get; set; }
        public string? OtherConDrOpinions { get; set; }
        public string? ConditionAtTheTimeOfDischarge { get; set; }
        public string? PainManagementTechnique { get; set; }
        public string? LifeStyle { get; set; }
        public string? WarningSymptoms { get; set; }
        public string? Radiology { get; set; }
        public byte? IsNormalOrDeath { get; set; }
        public long DischargeSummaryId { get; set; }
    }
}
