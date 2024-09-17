using FluentValidation;

namespace HIMS.API.Models.Customer
{
    public class CustomerPaymentSummaryModel
    {
        public long CustPayTranId { get; set; }
        public long? CustomerId { get; set; }
        public string? PaymentDate { get; set; }
        public string? PaymentTime { get; set; }
        public decimal? Amount { get; set; }
        public string? Comments { get; set; }
    }
    public class CustomerPaymentSummaryModelValidator : AbstractValidator<CustomerPaymentSummaryModel>
    {
        public CustomerPaymentSummaryModelValidator()
        {
            RuleFor(x => x.PaymentDate).NotNull().NotEmpty().WithMessage("PaymentDate  is required");
            RuleFor(x => x.PaymentTime).NotNull().NotEmpty().WithMessage("PaymentTime  is required");
            RuleFor(x => x.Amount).NotNull().NotEmpty().WithMessage("Amount  is required");
            RuleFor(x => x.Comments).NotNull().NotEmpty().WithMessage("Comments  is required");

        }
    }
}
