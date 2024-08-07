using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class BankMasterModel
    {
        public long BankId { get; set; }
        public string? BankName { get; set; }
       
    }
    public class BankMasterModelValidator : AbstractValidator<BankMasterModel>
    {
        public BankMasterModelValidator()
        {
            RuleFor(x => x.BankName).NotNull().NotEmpty().WithMessage("Bank Name is required");
        }
    }
}

