using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class GenderModel
    {
        public long GenderId { get; set; }
        public string? GenderName { get; set; }
    }
    public class GenderModelValidator : CommonDynamicValidator<GenderModel>
    {
        public GenderModelValidator()
        {
            RuleFor(x => x.GenderName).NotNull().NotEmpty().WithMessage("Gender Name is required");
        }
    }
}
