using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class DoctorTypeMasterModel
    {
        public long Id { get; set; }
        public string? DoctorType { get; set; }
    }
    public class DoctorTypeMasterModelValidator : AbstractValidator<DoctorTypeMasterModel>
    {
        public DoctorTypeMasterModelValidator()
        {
            RuleFor(x => x.DoctorType).NotNull().NotEmpty().WithMessage("DoctorType is required");
        }
    }
}
