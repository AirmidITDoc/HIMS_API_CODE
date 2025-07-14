using FluentValidation;

namespace HIMS.API.Models.OutPatient
{
    public class RegistrationModel
    {
        public DateTime? RegDate { get; set; }
        public string? RegTime { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PinNo { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Age { get; set; }
        public long? GenderId { get; set; }
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
        public bool? IsCharity { get; set; }
        public long? ReligionId { get; set; }
        public long? AreaId { get; set; }
        public bool? IsSeniorCitizen { get; set; }
        public string? AadharCardNo { get; set; }
        public string? PanCardNo { get; set; }
        public string? Photo { get; set; }
        public string? EmgContactPersonName { get; set; }
        public long? EmgRelationshipId { get; set; }
        public string? EmgMobileNo { get; set; }
        public string? EmgLandlineNo { get; set; }
        public string? EngAddress { get; set; }
        public string? EmgAadharCardNo { get; set; }
        public string? EmgDrivingLicenceNo { get; set; }
        public string? MedTourismPassportNo { get; set; }
        public DateTime? MedTourismVisaIssueDate { get; set; }
        public DateTime? MedTourismVisaValidityDate { get; set; }
        public string? MedTourismNationalityId { get; set; }
        public long? MedTourismCitizenship { get; set; }
        public string? MedTourismPortOfEntry { get; set; }
        public DateTime? MedTourismDateOfEntry { get; set; }
        public string? MedTourismResidentialAddress { get; set; }
        public string? MedTourismOfficeWorkAddress { get; set; }
        public long RegId { get; set; }



    }

    public class RegistrationModelValidator : AbstractValidator<RegistrationModel>
    {
        public RegistrationModelValidator()
        {
            RuleFor(x => x.PrefixId).NotNull().NotEmpty().WithMessage("Prefix is required");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("LastName is required");
            RuleFor(x => x.GenderId).NotNull().NotEmpty().WithMessage("Gender is required");
            RuleFor(x => x.CityId).NotNull().NotEmpty().WithMessage("City is required");
            RuleFor(x => x.StateId).NotNull().NotEmpty().WithMessage("State is required");
            RuleFor(x => x.CountryId).NotNull().NotEmpty().WithMessage("Country is required");
        }
    }
}
