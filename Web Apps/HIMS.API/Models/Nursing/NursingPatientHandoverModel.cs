using FluentValidation;

namespace HIMS.API.Models.Nursing
{
    public class NursingPatientHandoverModel
    {
        public long PatHandId { get; set; }
        public long? AdmId { get; set; }
        public DateTime? Tdate { get; set; }
        public string? Ttime { get; set; }
        public string? ShiftInfo { get; set; }
        public string? PatHandI { get; set; }
        public string? PatHandS { get; set; }
        public string? PatHandB { get; set; }
        public string? PatHandA { get; set; }
        public string? PatHandR { get; set; }
        public string? Comments { get; set; }
    }
    public class NursingPatientHandoverModelValidator : AbstractValidator<NursingPatientHandoverModel>
    {
        public NursingPatientHandoverModelValidator()
        {
            RuleFor(x => x.Tdate).NotNull().NotEmpty().WithMessage("Tdate  is required");
            RuleFor(x => x.Ttime).NotNull().NotEmpty().WithMessage("Ttime  is required");

        }
    }
}
