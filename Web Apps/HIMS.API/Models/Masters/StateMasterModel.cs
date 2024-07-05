using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class StateMasterModel
    {
        public long StateId { get; set; }
        public string? StateName { get; set; }
        public long? CountryId { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }

    }
    public class StateMasterModelValidator : AbstractValidator<StateMasterModel>
    {
        public StateMasterModelValidator()
        {
            RuleFor(x => x.StateName).NotNull().NotEmpty().WithMessage("State is required");
        }
    }
}
