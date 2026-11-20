using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class CompanyTypeMasterModel
    {
        public long CompanyTypeId { get; set; }
        public string? TypeName { get; set; }
    }
    public class CompanyTypeMasterModelValidator : CommonDynamicValidator<CompanyTypeMasterModel>
    {
        public CompanyTypeMasterModelValidator()
        {
            RuleFor(x => x.TypeName).NotNull().NotEmpty().WithMessage("CompanyType is required");
        }
    }
}
