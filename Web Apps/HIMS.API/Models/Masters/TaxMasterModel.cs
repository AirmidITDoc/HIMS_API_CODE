using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class TaxMasterModel
    {
        public long Id { get; set; }
        public string? TaxNature { get; set; }
    }
    public class TaxMasterModelValidator : CommonDynamicValidator<TaxMasterModel>
    {
        public TaxMasterModelValidator()
        {
            RuleFor(x => x.TaxNature).NotNull().NotEmpty().WithMessage("TaxNature is required");
        }
    }
}
