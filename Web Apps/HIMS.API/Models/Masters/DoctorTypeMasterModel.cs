using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class DoctorTypeMasterModel
    {
        public long Id { get; set; }
        public string? DoctorType { get; set; }
    }
    public class DoctorTypeMasterModelValidator : CommonDynamicValidator<DoctorTypeMasterModel>
    {
        public DoctorTypeMasterModelValidator()
        {
            RuleFor(x => x.DoctorType).NotNull().NotEmpty().WithMessage("DoctorType is required");
        }
    }
}
