using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class InstructionMasterModel
    {
        public long InstructionId { get; set; }
        public string? InstructionDescription { get; set; }
    }
    public class InstructionMasterModelValidator : AbstractValidator<InstructionMasterModel>
    {
        public InstructionMasterModelValidator()
        {
            RuleFor(x => x.InstructionDescription).NotNull().NotEmpty().WithMessage("Instruction  is required");
        }
    }
}
