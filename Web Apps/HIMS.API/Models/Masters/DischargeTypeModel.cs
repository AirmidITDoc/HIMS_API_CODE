using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class DischargeTypeModel
    {
        public long DischargeTypeId { get; set; }
        public string? DischargeTypeName { get; set; }
    }
    public class DischargeTypeModel1Validator : CommonDynamicValidator<DischargeTypeModel>
    {
        public DischargeTypeModel1Validator()
        {
            RuleFor(x => x.DischargeTypeName).NotNull().NotEmpty().WithMessage("Discharge Type  is required");
        }
    }
}
