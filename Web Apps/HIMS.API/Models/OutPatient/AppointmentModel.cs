using FluentValidation;

namespace HIMS.API.Models.OutPatient
{
    public class RegistrationSaveModel
    {
        public long RegID { get; set; }
        public DateTime? RegDate { get; set; }
        public DateTime? RegTime { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PinNo { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Age { get; set; }
        public long? GenderId { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }

        public long? AddedBy { get; set; }
        //public long UpdatedBy { get; set; }

        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public long? CountryId { get; set; }
        public long? StateId { get; set; }
        public long? CityId { get; set; }
        public long? MaritalStatusId { get; set; }

        public int? ReligionId { get; set; }
        public bool? IsCharity { get; set; }
        //  public string RegPrefix { get; set; }
        public long? AreaId { get; set; }
        //  public long VillageId { get; set; }
        public bool? IsSeniorCitizen { get; set; }

        //public long TalukaId { get; set; }
        // public double PatientWeight { get; set; }
        public string? AadharCardNo { get; set; }
        public string? PanCardNo { get; set; }
        // public String ImgFile { get; set; }
        public string? Photo { get; set; }

    }
    public class RegistrationSaveModelValidator : AbstractValidator<RegistrationSaveModel>
    {
        public RegistrationSaveModelValidator()
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

    public class VisitDetailModel
    {
        public long VisitID { get; set; }
        public long? RegId { get; set; }
        public DateTime? VisitDate { get; set; }
        public DateTime? VisitTime { get; set; }
        public long? UnitId { get; set; }
        public long? PatientTypeId { get; set; }
        public long? ConsultantDocId { get; set; }
        public long? RefDocId { get; set; }
        public long? TariffId { get; set; }
        public long? CompanyId { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public long? ClassId { get; set; }
        public long? DepartmentId { get; set; }
        public long? PatientOldNew { get; set; }
        public int? FirstFollowupVisit { get; set; }
        public long? AppPurposeId { get; set; }
        public DateTime? FollowupDate { get; set; }
        public int? CrossConsulFlag { get; set; }
        public long? PhoneAppId { get; set; }

    }
    public class VisitDetailModelValidator : AbstractValidator<VisitDetailModel>
    {
        public VisitDetailModelValidator()
        {
            RuleFor(x => x.RegId).NotNull().NotEmpty().WithMessage("regId is required");
            RuleFor(x => x.UnitId).NotNull().NotEmpty().WithMessage("UnitId is required");
            RuleFor(x => x.PatientTypeId).NotNull().NotEmpty().WithMessage("PatientTypeId is required");
            RuleFor(x => x.ConsultantDocId).NotNull().NotEmpty().WithMessage("ConsultantDocId is required");
            RuleFor(x => x.TariffId).NotNull().NotEmpty().WithMessage("TariffId is required");
            RuleFor(x => x.CompanyId).NotNull().NotEmpty().WithMessage("CompanyId is required");
            RuleFor(x => x.DepartmentId).NotNull().NotEmpty().WithMessage("DepartmentId is required");
            RuleFor(x => x.ClassId).NotNull().NotEmpty().WithMessage("ClassId is required");
        }
    }
    }
