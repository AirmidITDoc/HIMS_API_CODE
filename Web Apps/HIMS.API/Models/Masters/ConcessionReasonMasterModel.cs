using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class ConcessionReasonMasterModel
    {
        public long ConcessionId { get; set; }
        public string? ConcessionReason { get; set; }

        public class ConcessionReasonMasterModelValidator : CommonDynamicValidator<ConcessionReasonMasterModel>
        {
            public ConcessionReasonMasterModelValidator()
            {
                RuleFor(x => x.ConcessionReason).NotNull().NotEmpty().WithMessage("ConcessionReason is required");
            }
        }
    }
}
