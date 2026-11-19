using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class CreditReasonModel
    {
        public long CreditId { get; set; }
        public string? CreditReason { get; set; }
        public bool? IsActive { get; set; }
    }

    public class CreditReasonModelValidator : CommonDynamicValidator<CreditReasonModel>
    {
        public CreditReasonModelValidator()
        {
            RuleFor(x => x.CreditReason).NotNull().NotEmpty().WithMessage("CreditReason is required");
        }
    }
}
