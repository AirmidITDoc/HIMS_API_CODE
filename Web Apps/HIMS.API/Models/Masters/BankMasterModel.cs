using FluentValidation;
using HIMS.API.Validator;

namespace HIMS.API.Models.Masters
{
    public class BankMasterModel
    {
        public long BankId { get; set; }
        public string? BankName { get; set; }

    }
    public class BankMasterModelValidator : CommonDynamicValidator<BankMasterModel>
    {
        public BankMasterModelValidator()
        {
            RuleFor(x => x.BankName).NotNull().NotEmpty().WithMessage("Bank Name is required");
        }
    }
}

