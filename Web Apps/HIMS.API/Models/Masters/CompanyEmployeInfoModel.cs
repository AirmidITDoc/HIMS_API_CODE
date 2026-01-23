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
