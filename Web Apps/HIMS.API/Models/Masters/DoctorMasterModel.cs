using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class DoctorMasterModel
    {
        public long DoctorId { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
    }
    public class DoctorMasterModelValidator : AbstractValidator<DoctorMasterModel>
    {
        public DoctorMasterModelValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required");
        }
    }
}
