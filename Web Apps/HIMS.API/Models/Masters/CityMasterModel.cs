using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class CityMasterModel
    {
        public long CityId { get; set; }
        public string? CityName { get; set; }
        public long? StateId { get; set; }
        public bool IsActive { get; set; }

    }

    public class CityMasterModelValidator : CommonDynamicValidator<CityMasterModel>
    {
        public CityMasterModelValidator()
        {
            RuleFor(x => x.CityName).NotNull().NotEmpty().WithMessage("CityName is required");
            RuleFor(x => x.StateId).NotNull().NotEmpty().WithMessage("StateId is required");

        }
    }
}
