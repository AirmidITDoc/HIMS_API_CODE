using FluentValidation;

namespace HIMS.API.Models.Diet
{
    public class DietRestrictionModel
    {
        public long RestrictionId { get; set; }
        //public string? RestrictionCode { get; set; }
        public string? RestrictionName { get; set; }
        public long? RestrictionTypeId { get; set; }
        public string? Description { get; set; }
    }

    public class DietRestrictionModelValidator : AbstractValidator<DietRestrictionModel>
    {
        public DietRestrictionModelValidator()
        {
            RuleFor(x => x.RestrictionName).NotNull().NotEmpty().WithMessage("RestrictionName is required");
        }
    }
}