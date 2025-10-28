using FluentValidation;
namespace HIMS.API.Models.OutPatient
{
    public class AddChargesDeleteModel
    {
        public long? ChargesId { get; set; }
        public long? IsCancelledBy { get; set; }
    }
    public class AddChargesDeleteModelValidator : AbstractValidator<AddChargesDeleteModel>
    {
        public AddChargesDeleteModelValidator()
        {
            RuleFor(x => x.IsCancelledBy).NotNull().NotEmpty().WithMessage("UserId is required");

        }
    }
    public class AddChargeDModel
    {

        public AddChargesDeleteModel DeleteCharges { get; set; }


    }

}