using FluentValidation;
using HIMS.API.Models.Masters;

namespace HIMS.API.Controllers.Masters.Personal_Information
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
