using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class PathSpecimenConditionMasterModel
    {
        public long SpecimenConditionId { get; set; }
        public string? SpecimenCondition { get; set; }
        public long? UnitId { get; set; }
    }
    public class PathSpecimenConditionMasterModelValidator : AbstractValidator<PathSpecimenConditionMasterModel>
    {
        public PathSpecimenConditionMasterModelValidator()
        {
            RuleFor(x => x.SpecimenCondition).NotNull().NotEmpty().WithMessage("SpecimenCondition  is required");
        }
    }
}
