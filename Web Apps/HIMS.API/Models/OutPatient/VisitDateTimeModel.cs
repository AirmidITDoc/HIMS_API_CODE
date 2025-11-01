using FluentValidation;

namespace HIMS.API.Models.OutPatient
{
    public class VisitDateTimeModel
    {
        public long VisitId { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? VisitTime { get; set; }
    }
    public class VisitDateTimeModelValidator : AbstractValidator<VisitDateTimeModel>
    {
        public VisitDateTimeModelValidator()
        {
            RuleFor(x => x.VisitDate).NotNull().NotEmpty().WithMessage("VisitDate is required");
            RuleFor(x => x.VisitTime).NotNull().NotEmpty().WithMessage("VisitTime is required");
        
        }
    }

}
