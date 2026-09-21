using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Models.OTManagement
{
    public class ConstantModel
    {
        public long ConstantId { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public string? ConstantType { get; set; }
    }
    public class ConstantModelValidator : AbstractValidator<ConstantModel>
    {
        public ConstantModelValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Name  is required");
            RuleFor(x => x.Value).NotNull().NotEmpty().WithMessage("Value  is required");
            RuleFor(x => x.ConstantType).NotNull().NotEmpty().WithMessage("ConstantType  is required");

        }
    }
}
