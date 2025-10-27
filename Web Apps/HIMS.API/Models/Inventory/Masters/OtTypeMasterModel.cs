using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class OtTypeMasterModel
    {
        public long OttypeId { get; set; }
        public string? TypeName { get; set; }

    }

    public class OtTypeMasterModelValidator : AbstractValidator<OtTypeMasterModel>
    {
        public OtTypeMasterModelValidator()
        {
            RuleFor(x => x.TypeName).NotNull().NotEmpty().WithMessage("TypeName is required");
            //   RuleFor(x => x.LocationId).NotNull().NotEmpty().WithMessage("LocationId is required");

        }
    }
}