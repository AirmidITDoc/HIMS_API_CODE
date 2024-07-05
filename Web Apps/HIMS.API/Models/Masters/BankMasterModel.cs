using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public interface BankMasterModel
    {
        public long BankId { get; set; }
        public string? BankName { get; set; }
        //public long? CountryId { get; set; }
        public bool? IsDeleted { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public long? CreatedBy { get; set; }

        public long? CreatedDate { get; set; }

        public long? ModifiedBy { get; set; }
        public long? ModifiedDate { get; set; }

    }
    public class BankMasterModelValidator : AbstractValidator<BankMasterModel>
    {
        public BankMasterModelValidator()
        {
            RuleFor(x => x.BankName).NotNull().NotEmpty().WithMessage("Bank is required");
        }
    }
}

