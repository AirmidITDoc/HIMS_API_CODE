using FluentValidation;

namespace HIMS.API.Models.OTManagement
{
    public class EmergencyMedicalHistoryModel
    {
        public long EmgHistoryId { get; set; }
        public long? EmgId { get; set; }
        public string? Height { get; set; }
        public string? Pweight { get; set; }
        public string? Bmi { get; set; }
        public string? Bsl { get; set; }
        public string? SpO2 { get; set; }
        public string? Temp { get; set; }
        public string? Pulse { get; set; }
        public string? Bp { get; set; }
        public string? ChiefComplaint { get; set; }
        public string? Diagnosis { get; set; }
        public string? Examination { get; set; }
        public string? Advice { get; set; }

    }
    public class EmergencyMedicalHistoryModelValidator : AbstractValidator<EmergencyMedicalHistoryModel>
    {
        public EmergencyMedicalHistoryModelValidator()
        {
            RuleFor(x => x.Height).NotNull().NotEmpty().WithMessage("Height  is required");
            RuleFor(x => x.Pweight).NotNull().NotEmpty().WithMessage("Pweight  is required");
            RuleFor(x => x.Bmi).NotNull().NotEmpty().WithMessage("Bmi  is required");

        }
    }
}
