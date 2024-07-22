using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class StateMasterModel1
    {
        public long StateId { get; set; }
        public string? StateName { get; set; }
    }
    public class StateMasterModel1Validator : AbstractValidator<StateMasterModel1>
    {
        public StateMasterModel1Validator()
        {
            RuleFor(x => x.StateName).NotNull().NotEmpty().WithMessage("State Master  is required");
        }
    }
}
