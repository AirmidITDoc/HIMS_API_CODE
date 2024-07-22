using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class DischargeTypeModel
    {
        public long DischargeTypeId { get; set; }
        public string? DischargeTypeName { get; set; }
    }

    public class DischargeTypeModelValidator : AbstractValidator<DischargeTypeModel>
    {
        public DischargeTypeModelValidator()
        {
            RuleFor(x => x.DischargeTypeName).NotNull().NotEmpty().WithMessage("Patient Type is required");
        }
    }
}
