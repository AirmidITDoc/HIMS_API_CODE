using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class ExternalDoctorModel
    {
        public long ExtDoctorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string DoctorName { get; set; } = null!;
  

    }
    public class ExternalDoctorModelValidator : AbstractValidator<ExternalDoctorModel>
    {
        public ExternalDoctorModelValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required");
            RuleFor(x => x.DoctorName).NotNull().NotEmpty().WithMessage("DoctorName is required");

        }
    }
}

