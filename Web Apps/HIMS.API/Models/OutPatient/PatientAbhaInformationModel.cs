using FluentValidation;

namespace HIMS.API.Models.OutPatient
{
    public class PatientAbhaInformationModel
    {
        public long AbhaTranId { get; set; }
        public long RegId { get; set; }
        public long AbhaNumber { get; set; }
        public string AbhaFullName { get; set; } = null!;
        public string AbhaAddress { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateTime YearOfBirth { get; set; }
        public bool Verified { get; set; }
        public DateTime? VerifiedDateTime { get; set; }
        public int CreatedBy { get; set; }

    }
    public class PatientAbhaInformationModelValidator : AbstractValidator<PatientAbhaInformationModel>
    {
        public PatientAbhaInformationModelValidator()
        {
            RuleFor(x => x.RegId).NotNull().NotEmpty().WithMessage("RegId is required");
            RuleFor(x => x.AbhaNumber).NotNull().NotEmpty().WithMessage(" AbhaNumber required");
            RuleFor(x => x.AbhaFullName).NotNull().NotEmpty().WithMessage("AbhaFullName is required");
            RuleFor(x => x.AbhaAddress).NotNull().NotEmpty().WithMessage("AbhaAddress is required");
            RuleFor(x => x.Gender).NotNull().NotEmpty().WithMessage("Gender  is required");
            RuleFor(x => x.YearOfBirth).NotNull().NotEmpty().WithMessage(" YearOfBirth required");
            //RuleFor(x => x.Verified).NotNull().NotEmpty().WithMessage(" Verified required");

        }
    }
    public class PatientAbhaInformationUpdateModel
    {

        public long AbhaTranId { get; set; }
        public long RegId { get; set; }
        public long AbhaNumber { get; set; }
        public string AbhaFullName { get; set; } = null!;
        public string AbhaAddress { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public DateTime YearOfBirth { get; set; }
        public bool Verified { get; set; }
        public DateTime? VerifiedDateTime { get; set; }
        public int? ModifiedBy { get; set; }
    }
    public class PatientAbhaInformationUpdateModelValidator : AbstractValidator<PatientAbhaInformationUpdateModel>
    {
        public PatientAbhaInformationUpdateModelValidator()
        {
            RuleFor(x => x.RegId).NotNull().NotEmpty().WithMessage("RegId is required");
            RuleFor(x => x.AbhaNumber).NotNull().NotEmpty().WithMessage(" AbhaNumber required");
            RuleFor(x => x.AbhaFullName).NotNull().NotEmpty().WithMessage("AbhaFullName is required");
            RuleFor(x => x.AbhaAddress).NotNull().NotEmpty().WithMessage("AbhaAddress is required");
            RuleFor(x => x.Gender).NotNull().NotEmpty().WithMessage("Gender  is required");
            RuleFor(x => x.YearOfBirth).NotNull().NotEmpty().WithMessage(" YearOfBirth required");
            //RuleFor(x => x.Verified).NotNull().NotEmpty().WithMessage(" Verified required");

        }
    }
}
