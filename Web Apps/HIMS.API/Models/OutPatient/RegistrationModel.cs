using FluentValidation;

namespace HIMS.API.Models.OutPatient
{
    public class RegistrationModel
    {
        public long RegID { get; set; }
        public string? RegDate { get; set; }
        public string? RegTime { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PinNo { get; set; }
        public string DateOfBirth { get; set; }
        public string? Age { get; set; }
        public long? GenderID { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public long? AddedBy { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public long? CountryId { get; set; }
        public long? StateId { get; set; }
        public long? CityId { get; set; }
        public long? MaritalStatusId { get; set; }
        public int? ReligionId { get; set; }
        public bool? IsCharity { get; set; }
        public long? AreaId { get; set; }
        public bool? IsSeniorCitizen { get; set; }
        public string? AadharCardNo { get; set; }
        public string? PanCardNo { get; set; }
        public string? Photo { get; set; }
    }

    public class RegistrationModelValidator : AbstractValidator<RegistrationModel>
    {
        public RegistrationModelValidator()
        {
            RuleFor(x => x.PrefixId).NotNull().NotEmpty().WithMessage("Prefix is required");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("LastName is required");
            RuleFor(x => x.GenderID).NotNull().NotEmpty().WithMessage("Gender is required");
            RuleFor(x => x.CityId).NotNull().NotEmpty().WithMessage("City is required");
            RuleFor(x => x.StateId).NotNull().NotEmpty().WithMessage("State is required");
            RuleFor(x => x.CountryId).NotNull().NotEmpty().WithMessage("Country is required");
        }
    }
}
