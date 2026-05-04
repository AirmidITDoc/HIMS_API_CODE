using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Pathology
{
    public class LabPatientAddressModel
    {
        public long AddressId { get; set; }
        public string? MobileNumber { get; set; }
        public string? Address { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public long? UnitId { get; set; }
    }
    public class LabPatientAddressModelValidator : AbstractValidator<LabPatientAddressModel>
    {
        public LabPatientAddressModelValidator()
        {
            RuleFor(x => x.MobileNumber).NotNull().NotEmpty().WithMessage("MobileNumber  is required");
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage("Address  is required");
            RuleFor(x => x.CityId).NotNull().NotEmpty().WithMessage("CityId  is required");
            RuleFor(x => x.StateId).NotNull().NotEmpty().WithMessage("StateId  is required");
            RuleFor(x => x.CountryId).NotNull().NotEmpty().WithMessage("CountryId  is required");


        }
    }
}
