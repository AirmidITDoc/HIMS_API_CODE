using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class TermsOfPaymentModel
    {
        public long Id { get; set; }
        public string? TermsOfPayment { get; set; }

    }
    public class TermsOfPaymentModelValidator : CommonDynamicValidator<TermsOfPaymentModel>
    {
        public TermsOfPaymentModelValidator()
        {
            RuleFor(x => x.TermsOfPayment).NotNull().NotEmpty().WithMessage("TermsOfPayment Type is required");
        }
    }
}

