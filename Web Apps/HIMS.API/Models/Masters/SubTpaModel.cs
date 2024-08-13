using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class SubTpaModel
    {
        public long SubCompanyId { get; set; }
        public long? CompTypeId { get; set; }
        public string? CompanyName { get; set; }
    }

    public class SubTpaModelValidator : AbstractValidator<SubTpaModel>
    {
        public SubTpaModelValidator()
        {
            RuleFor(x => x.CompanyName).NotNull().NotEmpty().WithMessage("CompanyName is required");
        }
    }
}
