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
        public String? DateofBirth { get; set; }
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
        public String? RegDate { get; set; }
        public string? MahRegNo { get; set; }
        public String? MahRegDate { get; set; }
        public long? Addedby { get; set; }
        public long? UpdatedBy { get; set; }
        public string? RefDocHospitalName { get; set; }
        public bool? IsInHouseDoctor { get; set; }
        public bool? IsOnCallDoctor { get; set; }
        public string? PanCardNo { get; set; }
        public string? AadharCardNo { get; set; }
        public int? CreatedBy { get; set; }
        public String? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public String? ModifiedDate { get; set; }

        public List<MDoctorDepartmentDet> MDoctorDepartmentDet { get; set; }
    }
    public class DoctorMasterModelValidator : AbstractValidator<DoctorMasterModel>
    {
        public DoctorMasterModelValidator()
        {
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName is required");
        }
    }
    public class MDoctorDepartmentDet
    {
        public long DocDeptId { get; set; }
        public long? DoctorId { get; set; }
        public long? DepartmentId { get; set; }
    }


    public class MDoctorDepartmentDetValidator : AbstractValidator<MDoctorDepartmentDet>
    {
        public MDoctorDepartmentDetValidator()
        {
            RuleFor(x => x.DoctorId).NotNull().NotEmpty().WithMessage("DoctorId  is required");
            RuleFor(x => x.DepartmentId).NotNull().NotEmpty().WithMessage("DepartmentId  is required");
           
        }
    }


}


