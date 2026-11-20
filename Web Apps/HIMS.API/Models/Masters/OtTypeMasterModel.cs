using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class OtTypeMasterModel
    {
        public long OttypeId { get; set; }
        public string? TypeName { get; set; }

    }

    public class OtTypeMasterModelValidator : CommonDynamicValidator<OtTypeMasterModel>
    {
        public OtTypeMasterModelValidator()
        {
            RuleFor(x => x.TypeName).NotNull().NotEmpty().WithMessage("TypeName is required");
            //   RuleFor(x => x.LocationId).NotNull().NotEmpty().WithMessage("LocationId is required");

        }
    }
}