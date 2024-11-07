using FluentValidation;

namespace HIMS.API.Models.Inventory.Masters
{
    public class ConcessionReasonMasterModel
    {
        public long ConcessionId { get; set; }
        public string? ConcessionReason { get; set; }

        public class ConcessionReasonMasterModelValidator : AbstractValidator<ConcessionReasonMasterModel>
        {
            public ConcessionReasonMasterModelValidator()
            {
                RuleFor(x => x.ConcessionReason).NotNull().NotEmpty().WithMessage("ConcessionReason is required");
            }
        }
    }
}
