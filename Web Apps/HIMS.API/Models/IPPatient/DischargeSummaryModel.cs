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
        public DateTime? Optime { get; set; }
        public long? DischargeDoctor1 { get; set; }
        public long? DischargeDoctor2 { get; set; }
        public long? DischargeDoctor3 { get; set; }
        public DateTime? DischargeSummaryTime { get; set; }
        public string? DoctorAssistantName { get; set; }
        public string? ClaimNumber { get; set; }
        public string? PreOthNumber { get; set; }
        public long? AddedBy { get; set; }
        //public DateTime? AddedByDate { get; set; }
        //public long? UpdatedBy { get; set; }
        //public DateTime? UpdatedByDate { get; set; }
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
        public List<TIpPrescriptionDischargeModel> TIpPrescriptionDischarges { get; set; }
    }
    public class DischargeSummaryModelValidator : AbstractValidator<DischargeSummaryModel>
    {
        public DischargeSummaryModelValidator()
        {
            RuleFor(x => x.DischargeId).NotNull().NotEmpty().WithMessage("DischargeId is required");
            RuleFor(x => x.AdmissionId).NotNull().NotEmpty().WithMessage("AdmissionId  is required");


        }
    }
    public class TIpPrescriptionDischargeModel
    {
        public long PrecriptionId { get; set; }
        public long? OpdIpdId { get; set; }
        public byte? OpdIpdType { get; set; }
        public DateTime? Date { get; set; }
        public DateTime? Ptime { get; set; }
        public long? ClassId { get; set; }
        public long? GenericId { get; set; }
        public long? DrugId { get; set; }
        public long? DoseId { get; set; }
        public long? Days { get; set; }
        public long? InstructionId { get; set; }
        public double? QtyPerDay { get; set; }
        public double? TotalQty { get; set; }
        public string? Instruction { get; set; }
        public string? Remark { get; set; }
        public bool? IsClosed { get; set; }
        public bool? IsEnglishOrIsMarathi { get; set; }
        public long? StoreId { get; set; }
        //public int? CreatedBy { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public int? ModifiedBy { get; set; }
        //public DateTime? ModifiedDate { get; set; }


    }
    public class TIpPrescriptionDischargeModelValidator : AbstractValidator<TIpPrescriptionDischargeModel>
    {
        public TIpPrescriptionDischargeModelValidator()
        {
            RuleFor(x => x.OpdIpdType).NotNull().NotEmpty().WithMessage("OpdIpdType is required");
            RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("Date  is required");
            RuleFor(x => x.ClassId).NotNull().NotEmpty().WithMessage("ClassId  is required");
            RuleFor(x => x.GenericId).NotNull().NotEmpty().WithMessage("GenericId  is required");
            RuleFor(x => x.DrugId).NotNull().NotEmpty().WithMessage("DrugId  is required");
            RuleFor(x => x.DoseId).NotNull().NotEmpty().WithMessage("DoseId  is required");
        }
    }

}
