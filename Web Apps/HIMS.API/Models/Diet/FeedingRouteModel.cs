using FluentValidation;

namespace HIMS.API.Models.Diet
{
    public class FeedingRouteModel
    {
        public long FeedingRouteId { get; set; }
        public string? FeedingRouteName { get; set; }
        public string? Description { get; set; }
        public long? DietTypesId { get; set; }
    }

    public class FeedingRouteModelValidator : AbstractValidator<FeedingRouteModel>
    {
        public FeedingRouteModelValidator()
        {
            RuleFor(x => x.FeedingRouteName).NotNull().NotEmpty().WithMessage("FeedingRouteName is required");
            RuleFor(x => x.DietTypesId).NotNull().NotEmpty().WithMessage("DietTypesId is required");

        }
    }
}