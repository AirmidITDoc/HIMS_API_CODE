using FluentValidation;

namespace HIMS.API.Models.OutPatient
{
    public class PaymenntModel
    {
        public long PaymentId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string? PaymentTime { get; set; }
    }
    public class PaymenntModelValidator : AbstractValidator<PaymenntModel>
    {
        public PaymenntModelValidator()
        {
            RuleFor(x => x.PaymentTime).NotNull().NotEmpty().WithMessage("PaymentTime is required");
        }
    }

}