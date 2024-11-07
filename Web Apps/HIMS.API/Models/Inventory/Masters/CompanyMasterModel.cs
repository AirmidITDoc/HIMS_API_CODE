using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class CompanyMasterModel
    {
        public long CompanyId { get; set; }
        public long? CompTypeId { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PinNo { get; set; }
        public string? PhoneNo { get; set; }
        
    }
    public class CompanyMasterModelValidator : AbstractValidator<CompanyMasterModel>
    {
        public CompanyMasterModelValidator()
        {
            RuleFor(x => x.CompanyName).NotNull().NotEmpty().WithMessage("Company is required");
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage("Address is required");
            RuleFor(x => x.City).NotNull().NotEmpty().WithMessage("City is required");
            RuleFor(x => x.PinNo).NotNull().NotEmpty().WithMessage("PinNo is required");
            RuleFor(x => x.PhoneNo).NotNull().NotEmpty().WithMessage("PhoneNo is required");




        }
    }
}
