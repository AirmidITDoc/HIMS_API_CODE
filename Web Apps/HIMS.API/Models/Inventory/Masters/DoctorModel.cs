using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class DoctorModel
    {
        public long DoctorId { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public DateTime? DateofBirth { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Pin { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public long? GenderId { get; set; }
        public string? Education { get; set; }
        public bool? IsConsultant { get; set; }
        public bool? IsRefDoc { get; set; }
        public bool? IsActive { get; set; }
        public long DoctorTypeId { get; set; }
        public string? AgeYear { get; set; }
        public string? AgeMonth { get; set; }
        public string? AgeDay { get; set; }
        public string? PassportNo { get; set; }
        public string? Esino { get; set; }
        public string? RegNo { get; set; }
        public DateTime? RegDate { get; set; }
        public string? MahRegNo { get; set; }
        public DateTime? MahRegDate { get; set; }
        public string? RefDocHospitalName { get; set; }
        public bool? IsInHouseDoctor { get; set; }
        public bool? IsOnCallDoctor { get; set; }
        public string? PanCardNo { get; set; }
        public string? AadharCardNo { get; set; }
        public string? Signature { get; set; }
       
        public List<MDoctorDepartmentDetModel> MDoctorDepartmentDets { get; set; }
        public List<DoctorQualificationDetailModel> MDoctorQualificationDetails { get; set; }
        public List<DoctorExperienceDetailsModel> MDoctorExperienceDetails { get; set; }
        public List<MDoctorScheduleDetailModel> MDoctorScheduleDetails { get; set; }
        public List<MDoctorChargesDetailModel> MDoctorChargesDetails { get; set; }
        public List<DoctorLeaveDetailsModel> MDoctorLeaveDetails { get; set; }
        public List<DoctorSignPageDetailModel> MDoctorSignPageDetails { get; set; }

    }
    public class DoctorModelValidator : AbstractValidator<DoctorModel>
    {
        public DoctorModelValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required");
            RuleFor(x => x.MiddleName).NotNull().NotEmpty().WithMessage("MiddleName is required");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("LastName is required");
        }
    }
    public class MDoctorDepartmentDetModel
    {
        public long DocDeptId { get; set; }
        public long? DoctorId { get; set; }
        public long? DepartmentId { get; set; }
    }
    public class MDoctorDepartmentDetValidator : AbstractValidator<MDoctorDepartmentDetModel>
    {
        public MDoctorDepartmentDetValidator()
        {
            RuleFor(x => x.DepartmentId).NotNull().NotEmpty().WithMessage("DepartmentId  is required");
           
        }
    }
    public  class DoctorQualificationDetailModel
    {
        public long DocQualfiId { get; set; }
        public long? DoctorId { get; set; }
        public long? QualificationId { get; set; }
        public string? PassingYear { get; set; }
        public long? InstitutionNameId { get; set; }
        public long? CityId { get; set; }
        public long? CountryId { get; set; }

    }
    public class DoctorQualificationDetailModelValidator : AbstractValidator<DoctorQualificationDetailModel>
    {
        public DoctorQualificationDetailModelValidator()
        {
            RuleFor(x => x.QualificationId).NotNull().NotEmpty().WithMessage("QualificationId  is required");
            RuleFor(x => x.PassingYear).NotNull().NotEmpty().WithMessage("PassingYear  is required");

        }
    }
    public class DoctorExperienceDetailsModel
    {
        public long DocExpId { get; set; }
        public long? DoctorId { get; set; }
        public string? HospitalName { get; set; }
        public string? Designation { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
    public class MDoctorScheduleDetailModel
    {
        public long DocSchedId { get; set; }
        public long? DoctorId { get; set; }
        public string? ScheduleDays { get; set; }
        public string? StartTime { get; set; }
        public string? EndTime { get; set; }
        public int? Slot { get; set; }
    }
    public class MDoctorChargesDetailModel
    {
        public long DocChargeId { get; set; }
        public long? DoctorId { get; set; }
        public long? ServiceId { get; set; }
        public long? TariffId { get; set; }
        public long? ClassId { get; set; }
        public decimal? Price { get; set; }
        public int? Days { get; set; }

    }
    public class DoctorLeaveDetailsModel
    {
        public long DocLeaveId { get; set; }
        public long? DoctorId { get; set; }
        public long? LeaveTypeId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? LeaveOption { get; set; }

    }
    public class DoctorSignPageDetailModel
    {
        public long DocSignId { get; set; }
        public long? DoctorId { get; set; }
        public long? PageId { get; set; }
    }

}


