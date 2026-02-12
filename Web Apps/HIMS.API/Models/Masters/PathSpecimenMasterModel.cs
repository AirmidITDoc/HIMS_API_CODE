using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class PathSpecimenMasterModel
    {
        public long SpecimenId { get; set; }
        public string? SpecimenName { get; set; }
        public long? UnitId { get; set; }
    }
    public class PathSpecimenMasterModelValidator : AbstractValidator<PathSpecimenMasterModel>
    {
        public PathSpecimenMasterModelValidator()
        {
            RuleFor(x => x.SpecimenName).NotNull().NotEmpty().WithMessage("SpecimenName  is required");
        }
    }
}
