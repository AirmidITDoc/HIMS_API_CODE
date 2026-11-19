using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class CountryMasterModel
    {
        public long CountryId { get; set; }
        public string? CountryName { get; set; }

    }
    public class CountryMasterModelValidator : CommonDynamicValidator<CountryMasterModel>
    {
        public CountryMasterModelValidator()
        {
            RuleFor(x => x.CountryName).NotNull().NotEmpty().WithMessage("CountryName is required");
        }
    }

}
