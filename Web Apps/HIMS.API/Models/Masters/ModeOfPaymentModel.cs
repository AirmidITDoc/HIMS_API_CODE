using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class ModeOfPaymentModel
    {
        public long Id { get; set; }
        public string? ModeOfPayment { get; set; }
    }
    public class ModeOfPaymentModelValidator : AbstractValidator<ModeOfPaymentModel>
    {
        public ModeOfPaymentModelValidator()
        {
            RuleFor(x => x.ModeOfPayment).NotNull().NotEmpty().WithMessage("Payment is required required");
        }
    }
}
