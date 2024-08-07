using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class DischargeTypeModel1
    {
        public long DischargeTypeId { get; set; }
        public string? DischargeTypeName { get; set; }
    }
    public class DischargeTypeModel1Validator : AbstractValidator<DischargeTypeModel1>
    {
        public DischargeTypeModel1Validator()
        {
            RuleFor(x => x.DischargeTypeName).NotNull().NotEmpty().WithMessage("Discharge Type  is required");
        }
    }
}
