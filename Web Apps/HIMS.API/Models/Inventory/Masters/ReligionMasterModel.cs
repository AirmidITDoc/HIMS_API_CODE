using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class ReligionMasterModel
    {
        public long ReligionId { get; set; }
        public string? ReligionName { get; set; }

    }

    public class ReligionMasterModelValidator : AbstractValidator<ReligionMasterModel>
    {
        public ReligionMasterModelValidator()
        {
            RuleFor(x => x.ReligionName).NotNull().NotEmpty().WithMessage("Religion is required");


        }
    }
}
