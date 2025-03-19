using FluentValidation;

namespace HIMS.API.Models.IPPatient
{
    public class AdmissionRegModel
    {

        public long RegId { get; set; }
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
        public long? GenderID { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public int? AddedBy { get; set; }
        public int? UpdatedBy { get; set; }
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

    }
    public class AdmissionRegModelValidator : AbstractValidator<AdmissionRegModel>
    {
        public AdmissionRegModelValidator()
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
    public class ADMISSIONModel
    {
        public long? RegId { get; set; }
        public DateTime? AdmissionDate { get; set; }
        public string? AdmissionTime { get; set; }
        public long? PatientTypeId { get; set; }
        public long? HospitalId { get; set; }
        public long? DocNameId { get; set; }
        public long? RefDocNameId { get; set; }
        public long? WardId { get; set; }
        public long? BedId { get; set; }
        public DateTime? DischargeDate { get; set; }
        public string? DischargeTime { get; set; }
        public byte? IsDischarged { get; set; }
        public byte? IsBillGenerated { get; set; }
        public long? CompanyId { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }
        public long? DepartmentId { get; set; }
        public string? RelativeName { get; set; }
        public string? RelativeAddress { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public long? RelationshipId { get; set; }
        public long? AddedBy { get; set; }
        public bool? IsMlc { get; set; }
        public string? MotherName { get; set; }
        public long? AdmittedDoctor1 { get; set; }
        public long? AdmittedDoctor2 { get; set; }
        public long? RefByTypeId { get; set; }
        public long? RefByName { get; set; }
        public long? SubTpaComId { get; set; }
        public string? PolicyNo { get; set; }
        public bool? IsOpToIpconv { get; set; }
        public decimal? AprovAmount { get; set; }
        public DateTime? CompDod { get; set; }
        public string? RefDoctorDept { get; set; }
        public byte? AdmissionType { get; set; }
        public long AdmissionId { get; set; }


    }
    public class AdmissModelValidator : AbstractValidator<ADMISSIONModel>
    {
        public AdmissModelValidator()
        {
            RuleFor(x => x.AdmissionDate).NotNull().NotEmpty().WithMessage("AdmissionDate is required");
            RuleFor(x => x.AdmissionTime).NotNull().NotEmpty().WithMessage("AdmissionTime is required");
            
        }
    }
    public class NewAdmission
    {
        public AdmissionRegModel AdmissionReg { get; set; }
        public ADMISSIONModel ADMISSION { get; set; }
    }
    public class BedMaster
    {
        public long BedId { get; set; }

    }
}

