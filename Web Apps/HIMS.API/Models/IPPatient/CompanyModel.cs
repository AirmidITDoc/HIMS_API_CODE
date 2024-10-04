using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.IPPatient
{
    public class CompanyModel
    {
        public long CompanyId { get; set; }
        public long? CompTypeId { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PinNo { get; set; }
        public string? PhoneNo { get; set; }

    }
    public class CompanyModelValidator : AbstractValidator<CompanyModel>
    {
        public CompanyModelValidator()
        {
            RuleFor(x => x.CompTypeId).NotNull().NotEmpty().WithMessage("CompTypeId is required");
            RuleFor(x => x.CompanyName).NotNull().NotEmpty().WithMessage("Company is required");
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage("Address is required");
            RuleFor(x => x.City).NotNull().NotEmpty().WithMessage("City is required");

        }
    }
}

