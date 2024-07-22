using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class CityMasterModel
    {
        public long CityId { get; set; }
        public string? CityName { get; set; }
    }

    public class CityMasterModelValidator : AbstractValidator<CityMasterModel>
    {
        public CityMasterModelValidator()
        {
            RuleFor(x => x.CityName).NotNull().NotEmpty().WithMessage("Patient Type is required");
        }
    }
}
