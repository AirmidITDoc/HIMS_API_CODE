using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class CompanyTypeMasterModel
    {
        public long CompanyTypeId { get; set; }
        public string? TypeName { get; set; }
    }
    public class CompanyTypeMasterModelValidator : AbstractValidator<CompanyTypeMasterModel>
    {
        public CompanyTypeMasterModelValidator()
        {
            RuleFor(x => x.TypeName).NotNull().NotEmpty().WithMessage("CompanyType is required");
        }
    }
}
