using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Nursing
{
    public partial class TDoctorPatientHandoverModel
    {
        public long DocHandId { get; set; }
        public long? AdmId { get; set; }
        public DateTime? Tdate { get; set; }    
        public string? Ttime { get; set; }
        public string? ShiftInfo { get; set; }
        public string? PatHandI { get; set; }
        public string? PatHandS { get; set; }
        public string? PatHandB { get; set; }
        public string? PatHandA { get; set; }
        public string? PatHandR { get; set; }
        public long? IsAddedBy { get; set; }
    }
    public class TDoctorPatientHandoverModelValidator : AbstractValidator<TDoctorPatientHandoverModel>
    {
        public TDoctorPatientHandoverModelValidator()
        {
            RuleFor(x => x.Tdate).NotNull().NotEmpty().WithMessage("Tdate  is required");
            RuleFor(x => x.Ttime).NotNull().NotEmpty().WithMessage("Ttime  is required");

        }
    }

}
