using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.Nursing
{
    public class TransactionConsentMasterModel
    {
        public long ConsentId { get; set; }
        public DateTime? ConsentDate { get; set; }
        public string? ConsentTime { get; set; }
        public long? RefId { get; set; }
        public long? RefType { get; set; }
        public long? Opipid { get; set; }
        public long? Opiptype { get; set; }
        public long? ConsentTempId { get; set; }
        public string? ConsentName { get; set; }
        public long? ConsentDepartment { get; set; }
        public string? ConsentDescription { get; set; }
    }
    public class TransactionConsentMasterModelValidator : AbstractValidator<TransactionConsentMasterModel>
    {
        public TransactionConsentMasterModelValidator()
        {
            RuleFor(x => x.ConsentDate).NotNull().NotEmpty().WithMessage("ConsentDate is required");
            RuleFor(x => x.ConsentTime).NotNull().NotEmpty().WithMessage("ConsentTime is required");
            RuleFor(x => x.ConsentName).NotNull().NotEmpty().WithMessage("ConsentName is required");


        }
    }
}
