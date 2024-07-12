using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class InstructionMasterModel
    {
        public long InstructionId { get; set; }
        public string? InstructionDescription { get; set; }
        public string? InstructioninMarathi { get; set; }
        public bool? IsActive { get; set; }
    }
    public class InstructionMasterModelValidator : AbstractValidator<InstructionMasterModel>
    {
        public InstructionMasterModelValidator()
        {
            RuleFor(x => x.InstructionId).NotNull().NotEmpty().WithMessage("InstructionId is required");
        }
    }
}
