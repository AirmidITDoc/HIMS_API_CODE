using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class CompanyEmployeInfoModel
    {
        public long ExecutiveId { get; set; }
        public long? PrefixId { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string? LastName { get; set; }
        public long? GenderId { get; set; }
        public long? MobileNo { get; set; }
        public string? EmailId { get; set; }
        public string? Address { get; set; }
        public long? CityId { get; set; }
        public long? Designation { get; set; }
        public long? EmpDepartment { get; set; }
        public DateTime? DateOfJoin { get; set; }
        public long? UnitId { get; set; }
        public long? AdharCardNo { get; set; }
        public long? Pfno { get; set; }
        public string? ExperienceYear { get; set; }
        public string? PreviousSalary { get; set; }
        public string? PreviousCompany { get; set; }
        public string? PreviousDesignation { get; set; }
        public bool? IsRepresentative { get; set; }
    }
    public class CompanyEmployeInfoModelValidator : AbstractValidator<CompanyEmployeInfoModel>
    {
        public CompanyEmployeInfoModelValidator()
        {
            RuleFor(x => x.PrefixId).NotNull().NotEmpty().WithMessage("PrefixId  is required");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().WithMessage("FirstName  is required");
            RuleFor(x => x.LastName).NotNull().NotEmpty().WithMessage("LastName  is required");
            RuleFor(x => x.GenderId).NotNull().NotEmpty().WithMessage("GenderId  is required");
        }
    }
}
