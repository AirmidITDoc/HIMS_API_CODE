using FluentValidation;

namespace HIMS.API.Models.Marketing
{
    public class MarketHospitalMasterModel
    {
        public long HospitalId { get; set; }
        public string? HospitalName { get; set; }
        public string? HospitalAddress { get; set; }
        public string? Location { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
        public string? Radius { get; set; }
        public long? CityId { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactMobileNo { get; set; }
        public string? DoctorName { get; set; }
        public string? DoctorMobileNo { get; set; }
    }
    public class MarketHospitalMasterModelValidator : AbstractValidator<MarketHospitalMasterModel>
    {
        public MarketHospitalMasterModelValidator()
        {
            RuleFor(x => x.HospitalName).NotNull().NotEmpty().WithMessage("HospitalName  is required");
            RuleFor(x => x.HospitalAddress).NotNull().NotEmpty().WithMessage("HospitalAddress  is required");
            RuleFor(x => x.Location).NotNull().NotEmpty().WithMessage("Location  is required");
            RuleFor(x => x.Latitude).NotNull().NotEmpty().WithMessage("Latitude  is required");
            RuleFor(x => x.Longitude).NotNull().NotEmpty().WithMessage("Longitude  is required");


        }
    }
}
