using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class CountryMasterModel
    {
        public long CountryId { get; set; }
        public string? CountryName { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddBy { get; set; }
        public long? UpdatedBy { get; set; }
    }
    public class CountryMasterModelValidator : AbstractValidator<CountryMasterModel>
    {
        public CountryMasterModelValidator()
        {
            RuleFor(x => x.CountryName).NotNull().NotEmpty().WithMessage("Country is required");
        }
    }

}
