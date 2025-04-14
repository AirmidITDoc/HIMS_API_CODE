using FluentValidation;
using HIMS.API.Models.IPPatient;
using HIMS.Data.Models;
using Microsoft.AspNetCore.Mvc;

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
            RuleFor(x => x.PaymentDate).NotNull().NotEmpty().WithMessage("PaymentDate is required");
        }
    }

}