using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Inventory.Masters
{
    public class SiteDescriptionModel
    {
        public long SiteDescId { get; set; }
        public string? SiteDescriptionName { get; set; }
        public long? SurgeryCategoryId { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }

    }
    public class SiteDescriptionModelValidator : AbstractValidator<SiteDescriptionModel>
    {
        public SiteDescriptionModelValidator()
        {
            RuleFor(x => x.SiteDescriptionName).NotNull().NotEmpty().WithMessage("SiteDescriptionName is required");
            RuleFor(x => x.SurgeryCategoryId).NotNull().NotEmpty().WithMessage("SurgeryCategoryId is required");

        }
    }
}
