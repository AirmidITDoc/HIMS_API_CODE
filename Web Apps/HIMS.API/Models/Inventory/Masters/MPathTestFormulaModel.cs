using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class MPathTestFormulaModel
    {

        public long FormulaId { get; set; }
        public long? ParameterId { get; set; }
        public string? ParameterName { get; set; }
        public string? Formula { get; set; }
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        // public DateTime? CreatedDate { get; set; }

    }
    public class MPathTestFormulaModelValidator : AbstractValidator<MPathTestFormulaModel>
    {
        public MPathTestFormulaModelValidator()
        {
            RuleFor(x => x.ParameterId).NotNull().NotEmpty().WithMessage("ParameterId is required");
            RuleFor(x => x.ParameterName).NotNull().NotEmpty().WithMessage("ParameterName is required");

        }
    }
}

