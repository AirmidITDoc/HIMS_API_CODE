using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class PathUnitMasterModel
    {
        public long UnitId { get; set; }
        public string? UnitName { get; set; }
    }
    public class PathUnitMasterModelValidator : AbstractValidator<PathUnitMasterModel>
    {
        public PathUnitMasterModelValidator()
        {
            RuleFor(x => x.UnitName).NotNull().NotEmpty().WithMessage("UnitName  is required");
        }
    }
}
