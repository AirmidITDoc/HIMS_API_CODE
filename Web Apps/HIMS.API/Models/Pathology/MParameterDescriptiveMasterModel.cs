using FluentValidation;

namespace HIMS.API.Models.Pathology
{
    public class MParameterDescriptiveMasterModel
    {
        public long DescriptiveId { get; set; }
        public long? ParameterId { get; set; }
        public string? ParameterValues { get; set; }
        public bool? IsDefaultValue { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public string? DefaultValue { get; set; }
    }
    public class MParameterDescriptiveMasterModelValidator : AbstractValidator<MParameterDescriptiveMasterModel>
    {
        public MParameterDescriptiveMasterModelValidator()
        {
            RuleFor(x => x.ParameterId).NotNull().NotEmpty().WithMessage("ParameterId is required");
            RuleFor(x => x.ParameterValues).NotNull().NotEmpty().WithMessage("ParameterValues is required");
        }
    }
}
