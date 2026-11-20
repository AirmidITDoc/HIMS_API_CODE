using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class InstructionMasterModel
    {
        public long InstructionId { get; set; }
        public string? InstructionDescription { get; set; }
        public string? InstructioninMarathi { get; set; }
    }
    public class InstructionMasterModelValidator : CommonDynamicValidator<InstructionMasterModel>
    {
        public InstructionMasterModelValidator()
        {
            RuleFor(x => x.InstructionDescription).NotNull().NotEmpty().WithMessage("InstructionDescription  is required");
            RuleFor(x => x.InstructioninMarathi).NotNull().NotEmpty().WithMessage("InstructioninMarathi  is required");

        }
    }
}
