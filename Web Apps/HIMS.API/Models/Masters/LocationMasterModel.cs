using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class LocationMasterModel
    {
        public long LocationId { get; set; }
        public string? LocationName { get; set; }
    }
    public class LocationMasterModelValidator : CommonDynamicValidator<LocationMasterModel>
    {
        public LocationMasterModelValidator()
        {
            RuleFor(x => x.LocationName).NotNull().NotEmpty().WithMessage("Location name is  required");
        }
    }

}

