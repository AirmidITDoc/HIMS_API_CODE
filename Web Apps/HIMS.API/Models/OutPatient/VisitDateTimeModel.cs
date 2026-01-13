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
    public class VisitUpdateModel
    {
        public long VisitId { get; set; }
        public long? PatientTypeId { get; set; }
        public long? ConsultantDocId { get; set; }
        public long? RefDocId { get; set; }
        public long? TariffId { get; set; }
        public long? CompanyId { get; set; }
        public long? ClassId { get; set; }


    }

}
