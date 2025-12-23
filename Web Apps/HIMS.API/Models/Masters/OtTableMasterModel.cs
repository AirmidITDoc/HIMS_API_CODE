using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class OtTableMasterModel
    {
        public long OttableId { get; set; }
        public string? OttableName { get; set; }
        public long? LocationId { get; set; }

    }
    public class OtTableMasterModelValidator : AbstractValidator<OtTableMasterModel>
    {
        public OtTableMasterModelValidator()
        {
            RuleFor(x => x.OttableName).NotNull().NotEmpty().WithMessage("OttableName is required");
            RuleFor(x => x.LocationId).NotNull().NotEmpty().WithMessage("LocationId is required");

        }
    }
}