using FluentValidation;
namespace HIMS.API.Models.IPPatient
{
    public class DischargeSummaryModel
    {
        public long DischargeSummaryId { get; set; }
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
        public DateTime? AddedByDate { get; set; }
        public DateTime? UpdatedByDate { get; set; }
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
    }
    public class DischargeSummaryModelValidator : AbstractValidator<DischargeSummaryModel>
    {
        public DischargeSummaryModelValidator()
        {
            RuleFor(x => x.DischargeSummaryDate).NotNull().NotEmpty().WithMessage("DischargeSummaryDate is required");
            RuleFor(x => x.DischargeSummaryTime).NotNull().NotEmpty().WithMessage("DischargeSummaryTime  is required");


        }
    }
}
