using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.Transaction
{
    public class smsConfigModel
    {
        public string? Url { get; set; }
        public string? Keys { get; set; }
        public string? Campaign { get; set; }
        public long? Routeid { get; set; }
        public string? SenderId { get; set; }
        public string? UserName { get; set; }
        public string? Spassword { get; set; }
        public string? StorageLocLink { get; set; }
        public string? ConType { get; set; }
    }
    public class smsConfigModelValidator : AbstractValidator<smsConfigModel>
    {
        public smsConfigModelValidator()
        {
            RuleFor(x => x.Keys).NotNull().NotEmpty().WithMessage("Keys Date is required");
            RuleFor(x => x.Campaign).NotNull().NotEmpty().WithMessage("Campaign Time is required");

        }
    }
}
