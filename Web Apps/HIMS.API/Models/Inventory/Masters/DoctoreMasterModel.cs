using FluentValidation;

namespace HIMS.API.Models.Inventory.Masters
{
    public class DoctoreMasterModel
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
    }
    public class DoctoreMasterModelValidator : AbstractValidator<DoctoreMasterModel>
    {
        public DoctoreMasterModelValidator()
        {
            RuleFor(x => x.PrefixId).NotNull().NotEmpty().WithMessage("PrefixId Type is required");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName Type is required");
            RuleFor(x => x.MiddleName).NotNull().NotEmpty().WithMessage("MiddleName Type is required");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("LastName Type is required");



        }
    }
}
