using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class ItemCompanyMasterModel
    {
        public long CompanyId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompShortName { get; set; }
    }
    public class ItemCompanyMasterModelValidator : CommonDynamicValidator<ItemCompanyMasterModel>
    {
        public ItemCompanyMasterModelValidator()
        {
            RuleFor(x => x.CompanyName).NotNull().NotEmpty().WithMessage("CompanyName  is required");
            RuleFor(x => x.CompShortName).NotNull().NotEmpty().WithMessage("CompShortName  is required");

        }
    }
}
