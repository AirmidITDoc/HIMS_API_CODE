using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class SurgeryMasterModel
    {
        public long SurgeryId { get; set; }
        public long? SurgeryCategoryId { get; set; }
        public string? SurgeryName { get; set; }
        public long? DepartmentId { get; set; }
        public decimal? SurgeryAmount { get; set; }
        public long? SiteDescId { get; set; }
        public long? OttemplateId { get; set; }
        public long? ServiceId { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }

    }
    public class SurgeryMasterModelValidator : AbstractValidator<SurgeryMasterModel>
    {
        public SurgeryMasterModelValidator()
        {
            RuleFor(x => x.SurgeryCategoryId).NotNull().NotEmpty().WithMessage("SurgeryCategoryId is required");
            RuleFor(x => x.SurgeryName).NotNull().NotEmpty().WithMessage("SurgeryName  is required");
            RuleFor(x => x.IsCancelledDateTime).NotNull().NotEmpty().WithMessage(" IsCancelledDateTime required");


        }
    }
}