using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class StoreMasterModel
    {
        public long StoreId { get; set; }
        public string? StoreShortName { get; set; }
        public string? StoreName { get; set; }
    }

    public class StoreMasterModelValidator : AbstractValidator<StoreMasterModel>
    {
        public StoreMasterModelValidator()
        {
            RuleFor(x => x.StoreName).NotNull().NotEmpty().WithMessage(" Store Name is required");
            RuleFor(x => x.StoreShortName).NotNull().NotEmpty().WithMessage(" StoreShortName  is required");

        }
    }

}
