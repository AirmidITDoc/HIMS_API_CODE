using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Inventory.Masters
{
    public class DriverModel
    {
        public long DriverId { get; set; }
        public string DriverName { get; set; } = null!;
        public string? Address { get; set; }
        public long? CityId { get; set; }
        public string? MobileNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime? JoinDate { get; set; }
        public long? Experience { get; set; }
        public string? LicenceNo { get; set; }
        public string? CityName { get; set; }
    }
    public class DriverModelValidator : AbstractValidator<DriverModel>
    {
        public DriverModelValidator()
        {
            RuleFor(x => x.DateOfBirth).NotNull().NotEmpty().WithMessage("DateOfBirth  is required");
            RuleFor(x => x.JoinDate).NotNull().NotEmpty().WithMessage("JoinDate  is required");

        }
    }
}
