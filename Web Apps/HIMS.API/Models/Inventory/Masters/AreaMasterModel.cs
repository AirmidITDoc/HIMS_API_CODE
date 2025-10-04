using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class AreaMasterModel
    {
        public long AreaId { get; set; }
        public string? AreaName { get; set; }
        public long? CityId { get; set; }

    }
    public class AreaMasterModelValidator : AbstractValidator<AreaMasterModel>
    {
        public AreaMasterModelValidator()
        {
            RuleFor(x => x.AreaName).NotNull().NotEmpty().WithMessage("AreaName is required");
            RuleFor(x => x.CityId).NotNull().NotEmpty().WithMessage("CityId is required");

        }
    }
}

