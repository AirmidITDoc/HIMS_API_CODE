using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class SubTpaCompanyModel
    {
        public long SubCompanyId { get; set; }
        public long? CompanyId { get; set; }
        public long? CompTypeId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyShortName { get; set; }
        public string? Address { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public string? PhoneNo { get; set; }
        public string? FaxNo { get; set; }
    }

    public class SubTpaCompanyModelValidator : AbstractValidator<SubTpaCompanyModel>
    {
        public SubTpaCompanyModelValidator()
        {
            RuleFor(x => x.CompTypeId).NotNull().NotEmpty().WithMessage("CompTypeId is required");
            RuleFor(x => x.CompanyName).NotNull().NotEmpty().WithMessage("CompanyName is required");
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage("Address is required");
            RuleFor(x => x.PhoneNo).NotNull().NotEmpty().WithMessage("PhoneNo is required");
        }
    }
}
