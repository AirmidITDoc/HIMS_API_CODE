using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.IPPatient
{
    public class OTNotesTemplateModel
    {
        public long OtnoteTempId { get; set; }
        public string? OttemplateName { get; set; }
        public DateTime? Otdate { get; set; }
        public string? Ottime { get; set; }
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
        public string? FromTime { get; set; }
        public string? ToTime { get; set; }
        public long? OtreservationId { get; set; }
        public string? BloodLoss { get; set; }
        public long? SurgeryId { get; set; }
        public string? SorubNurse { get; set; }
        public string? Histopathology { get; set; }
        public string? BostOporders { get; set; }
        public long? AnestTypeId { get; set; }
        public long? SiteDescId { get; set; }
        public string? ComplicationMode { get; set; }
        public long? ServiceId { get; set; }
        public long? ProcedureId { get; set; }
    }
    public class OTNotesTemplateModelValidator : AbstractValidator<OTNotesTemplateModel>
    {
        public OTNotesTemplateModelValidator()
        {
            RuleFor(x => x.Otdate).NotNull().NotEmpty().WithMessage("Otdate  is required");
            RuleFor(x => x.Ottime).NotNull().NotEmpty().WithMessage("Ottime  is required");
            RuleFor(x => x.FromTime).NotNull().NotEmpty().WithMessage("FromTime  is required");
            RuleFor(x => x.ToTime).NotNull().NotEmpty().WithMessage("ToTime  is required");
           
        }
    }
}
