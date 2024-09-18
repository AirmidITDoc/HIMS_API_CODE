using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class PrefixModel
    {
        public long PrefixId { get; set; }
        public string? PrefixName { get; set; }
        public long? SexId { get; set; }
    }
    public class PrefixModelValidator : AbstractValidator<PrefixModel>
    {
        public PrefixModelValidator()
        {
            RuleFor(x => x.PrefixName).NotNull().NotEmpty().WithMessage("Prefix Name is required");
            RuleFor(x => x.SexId).GreaterThanOrEqualTo(0).WithMessage("Enter valid sex");
        }
    }
}
