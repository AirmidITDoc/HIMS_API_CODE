using FluentValidation;

namespace HIMS.API.Models.Inventory.Masters
{
    public class MModeOfPaymentModel
    {
        public long Id { get; set; }
        public string? ModeOfPayment { get; set; }
        public bool? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public int? CreatedBy { get; set; }

    }
    public class MModeOfPaymentModelValidator : AbstractValidator<MModeOfPaymentModel>
    {
        public MModeOfPaymentModelValidator()
        {
            RuleFor(x => x.ModeOfPayment).NotNull().NotEmpty().WithMessage("ModeOfPayment is required");
            RuleFor(x => x.AddedBy).NotNull().NotEmpty().WithMessage("AddedBy is required");  

        }
    }
}
