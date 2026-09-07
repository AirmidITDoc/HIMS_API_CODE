using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class SurgeryMasterModel
    {
        
        public long SurgeryId { get; set; }
        public string? SurgeryCode { get; set; }
        public string? ShortName { get; set; }
        public string? SurgeryName { get; set; }
        public long? DepartmentId { get; set; }
        public long? SubSpecialty { get; set; }
        public long? SurgeryCategoryId { get; set; }
        public long? SurgeryTypeId { get; set; }
        public decimal? SurgeryAmount { get; set; }
        public long? SiteDescId { get; set; }
        public long? OttemplateId { get; set; }
        public long? ServiceId { get; set; }
        public long? ExpectedSurgeryTime { get; set; }
        public long? PreparationTime { get; set; }
        public long? CleaningTurnaroundTime { get; set; }
        public long? TotalDuration { get; set; }
        public bool? PreAnaesthesiaClearance { get; set; }
        public bool? SurgicalConsentRequired { get; set; }
        public bool? BloodArrangementRequired { get; set; }
        public long? GradeLevel { get; set; }
        public long? PreferredOtroom { get; set; }

    }
    public class SurgeryMasterModelValidator : AbstractValidator<SurgeryMasterModel>
    {
        public SurgeryMasterModelValidator()
        {
            RuleFor(x => x.SurgeryCode).NotNull().NotEmpty().WithMessage("SurgeryCode is required");
            RuleFor(x => x.ShortName).NotNull().NotEmpty().WithMessage("ShortName  is required");
            RuleFor(x => x.SurgeryName).NotNull().NotEmpty().WithMessage(" SurgeryName required");
            RuleFor(x => x.DepartmentId).NotNull().NotEmpty().WithMessage(" DepartmentId required");
            RuleFor(x => x.SubSpecialty).NotNull().NotEmpty().WithMessage(" SubSpecialty required");
            RuleFor(x => x.SurgeryCategoryId).NotNull().NotEmpty().WithMessage(" SurgeryCategoryId required");
            RuleFor(x => x.SurgeryTypeId).NotNull().NotEmpty().WithMessage(" SurgeryTypeId required");
            RuleFor(x => x.SurgeryAmount).NotNull().NotEmpty().WithMessage(" SurgeryAmount required");
            RuleFor(x => x.ExpectedSurgeryTime).NotNull().NotEmpty().WithMessage(" ExpectedSurgeryTime required");
            RuleFor(x => x.PreparationTime).NotNull().NotEmpty().WithMessage(" PreparationTime required");
            RuleFor(x => x.CleaningTurnaroundTime).NotNull().NotEmpty().WithMessage(" CleaningTurnaroundTime required");
            RuleFor(x => x.TotalDuration).NotNull().NotEmpty().WithMessage(" TotalDuration required");
            RuleFor(x => x.PreAnaesthesiaClearance).NotNull().NotEmpty().WithMessage(" PreAnaesthesiaClearance required");
            RuleFor(x => x.GradeLevel).NotNull().NotEmpty().WithMessage(" GradeLevel required");
            RuleFor(x => x.PreferredOtroom).NotNull().NotEmpty().WithMessage(" PreferredOtroom required");






        }
    }
    public class SurgeryModel
    {
        public long SurgeryId { get; set; }
        public string? SurgeryName { get; set; }
        public long? SiteDescId { get; set; }
        public bool? IsActive { get; set; }
    }
}