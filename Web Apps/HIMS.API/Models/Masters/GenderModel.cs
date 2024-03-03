using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class GenderModel
    {
        public long GenderId { get; set; }
        public string? GenderName { get; set; }
    }
    public class GenderModelValidator : AbstractValidator<GenderModel>
    {
        public GenderModelValidator()
        {
            RuleFor(x => x.GenderName).NotNull().NotEmpty().WithMessage("Gender Name is required");
        }
    }
}
