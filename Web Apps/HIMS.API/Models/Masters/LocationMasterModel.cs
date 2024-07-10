using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class LocationMasterModel
    {
        public long LocationId { get; set; }
        public string? LocationName { get; set; }
        public bool? IsActive { get; set; }
        public string? UserToken { get; set; }
        
    }
    public class LocationMasterModelValidator : AbstractValidator<LocationMasterModel>
    {
        public LocationMasterModelValidator()
        {
            RuleFor(x => x.LocationName).NotNull().NotEmpty().WithMessage("Location is required");
        }
    }
}
