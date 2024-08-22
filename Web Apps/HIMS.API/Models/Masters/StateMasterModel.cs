using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class StateMasterModel
    {
        public long StateId { get; set; }
        public string? StateName { get; set; }
        public long? CountryId { get; set; }
    }
    public class StateMasterModel1Validator : AbstractValidator<StateMasterModel>
    {
        public StateMasterModel1Validator()
        {
            RuleFor(x => x.StateName).NotNull().NotEmpty().WithMessage("State Name  is required");
            RuleFor(x => x.CountryId).NotNull().NotEmpty().WithMessage("CountryId is required");

        }
    }
}
