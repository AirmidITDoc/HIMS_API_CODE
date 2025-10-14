using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Marketing
{
    public class Market_DailyVisitInfoModel
    {
        public long Id { get; set; }
        public DateTime? VisitDate { get; set; }
        public string? VisitTime { get; set; }
        public long? MarketingPersonId { get; set; }
        public long? HospitalId { get; set; }
        public long? CityId { get; set; }
        public string? Location { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Radius { get; set; }
        public long? FollowupType { get; set; }
        public string? FollowupTypeName { get; set; }
        public string? Comment { get; set; }
    }
    public class Market_DailyVisitInfoModelValidator : AbstractValidator<Market_DailyVisitInfoModel>
    {
        public Market_DailyVisitInfoModelValidator()
        {
            RuleFor(x => x.VisitDate).NotNull().NotEmpty().WithMessage("VisitDate  is required");
            RuleFor(x => x.VisitTime).NotNull().NotEmpty().WithMessage("VisitTime  is required");
            RuleFor(x => x.HospitalId).NotNull().NotEmpty().WithMessage("HospitalId  is required");
            RuleFor(x => x.CityId).NotNull().NotEmpty().WithMessage("CityId  is required");
            RuleFor(x => x.MarketingPersonId).NotNull().NotEmpty().WithMessage("MarketingPersonId  is required");


        }
    }
   
}
