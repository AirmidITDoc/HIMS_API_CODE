using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.Pathology
{
    public class PathParameterMasterModel
    {
        public long ParameterId { get; set; }
        public string? ParameterShortName { get; set; }
        public string? ParameterName { get; set; }
        public string? PrintParameterName { get; set; }
        public long? UnitId { get; set; }
        public long? IsNumeric { get; set; }
        public bool? IsPrintDisSummary { get; set; }

    }
    public class PathParameterMasterModelValidator : AbstractValidator<PathParameterMasterModel>
    {
        public PathParameterMasterModelValidator()
        {
            RuleFor(x => x.ParameterShortName).NotNull().NotEmpty().WithMessage("ParameterShortName Type is required");
            RuleFor(x => x.ParameterName).NotNull().NotEmpty().WithMessage("ParameterName Type is required");
            RuleFor(x => x.PrintParameterName).NotNull().NotEmpty().WithMessage("PrintParameterName Type is required");

        }
    }
}
