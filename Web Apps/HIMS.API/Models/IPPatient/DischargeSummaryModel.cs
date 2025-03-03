using FluentValidation;
namespace HIMS.API.Models.IPPatient
{
    public class DischargeSummaryModel
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
    public class DischargeSummaryModelValidator : AbstractValidator<DischargeSummaryModel>
    {
        public DischargeSummaryModelValidator()
        {
            RuleFor(x => x.DischargeSummaryDate).NotNull().NotEmpty().WithMessage("DischargeSummaryDate is required");
            RuleFor(x => x.DischargeSummaryTime).NotNull().NotEmpty().WithMessage("DischargeSummaryTime  is required");

        }
    }
    public class PrescriptionDischargeModel
    {
        public long? OpdIpdId { get; set; }
        public byte? OpdIpdType { get; set; }
        public DateTime? Date { get; set; }
        public string? Ptime { get; set; }
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
        public bool? IsEnglishOrIsMarathi { get; set; }
        public long? StoreId { get; set; }
        public int? CreatedBy { get; set; }
    }
    public class PrescriptionDischargeModelValidator : AbstractValidator<PrescriptionDischargeModel>
    {
        public PrescriptionDischargeModelValidator()
        {
            RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("Date is required");
            RuleFor(x => x.Ptime).NotNull().NotEmpty().WithMessage("Ptime  is required");

        }
    }
    public class DischargeSumModel
    {
        public DischargeSummaryModel DischargModel { get; set; }
        public List<PrescriptionDischargeModel> PrescriptionDischarge { get; set; }

    }
    public class DischargeSummaryUpdate
    {
        public long DischargeSummaryId { get; set; }

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
        public DateTime? OpDate { get; set; }
        public string? Optime { get; set; }
        public long? DischargeDoctor1 { get; set; }
        public long? DischargeDoctor2 { get; set; }
        public long? DischargeDoctor3 { get; set; }
        public string? DoctorAssistantName { get; set; }
        public string? ClaimNumber { get; set; }
        public string? PreOthNumber { get; set; }
        public long? UpdatedBy { get; set; }
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
    public class DischargeSummaryUpdateModelValidator : AbstractValidator<DischargeSummaryUpdate>
    {
        public DischargeSummaryUpdateModelValidator()
        {
            RuleFor(x => x.OpDate).NotNull().NotEmpty().WithMessage("OpDate is required");
            RuleFor(x => x.Optime).NotNull().NotEmpty().WithMessage("Optime  is required");

        }
    }
    public class DischargeUpdate
    {
        public DischargeSummaryUpdate DischargModel { get; set; }
        public List<PrescriptionDischargeModel> PrescriptionDischarge { get; set; }

    }
    public class DischargeTemplateModel
    {
        public long? AdmissionId { get; set; }
        public long? DischargeId { get; set; }
        public DateTime? Followupdate { get; set; }
        public long? DischargeDoctor1 { get; set; }
        public long? DischargeDoctor2 { get; set; }
        public long? DischargeDoctor3 { get; set; }
        public long? AddedBy { get; set; }
        public byte? IsNormalOrDeath { get; set; }
        public long DischargeSummaryId { get; set; }
        public string? TemplateDescriptionHtml { get; set; }

    }
    public class PrescriptionTemplateModel
    {
        public long? OpdIpdId { get; set; }
        public byte? OpdIpdType { get; set; }
        public DateTime? Date { get; set; }
        public string? Ptime { get; set; }
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
        public bool? IsEnglishOrIsMarathi { get; set; }
        public long? StoreId { get; set; }
        public int? CreatedBy { get; set; }
    }
    public class DischargeTemplateModelValidator : AbstractValidator<DischargeTemplateModel>
    {
        public DischargeTemplateModelValidator()
        {
            RuleFor(x => x.Followupdate).NotNull().NotEmpty().WithMessage("Followupdate is required");
        }
    }
    public class DischargeTemplate
    {
        public DischargeTemplateModel Discharge { get; set; }
        public List<PrescriptionTemplateModel> PrescriptionTemplate { get; set; }

    }
    public class DischargeTemplateUpdate
    {
        public long DischargeSummaryId { get; set; }
        public long? DischargeId { get; set; }
        public DateTime? Followupdate { get; set; }
        public long? DischargeDoctor1 { get; set; }
        public long? DischargeDoctor2 { get; set; }
        public long? DischargeDoctor3 { get; set; }
        public long? UpdatedBy { get; set; }
        public byte? IsNormalOrDeath { get; set; }
        public string? TemplateDescriptionHtml { get; set; }

    }
    public class DischargeTemplateUpdateValidator : AbstractValidator<DischargeTemplateUpdate>
    {
        public DischargeTemplateUpdateValidator()
        {
            RuleFor(x => x.Followupdate).NotNull().NotEmpty().WithMessage("Followupdate is required");
        }
    }
    public class PrescriptionTemplatUpdate
    {
        public long? OpdIpdId { get; set; }
        public byte? OpdIpdType { get; set; }
        public DateTime? Date { get; set; }
        public string? Ptime { get; set; }
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
        public bool? IsEnglishOrIsMarathi { get; set; }
        public long? StoreId { get; set; }
        public int? CreatedBy { get; set; }
    }
    public class PrescriptionTemplatUpdateValidator : AbstractValidator<PrescriptionTemplatUpdate>
    {
        public PrescriptionTemplatUpdateValidator()
        {
            RuleFor(x => x.Date).NotNull().NotEmpty().WithMessage("Date is required");
            RuleFor(x => x.Ptime).NotNull().NotEmpty().WithMessage("Ptime is required");

        }
    }
    public class DischargeTemUpdate
    {
        public DischargeTemplateUpdate Discharge { get; set; }
        public PrescriptionTemplatUpdate PrescriptionTemplate { get; set; }

    }
}

