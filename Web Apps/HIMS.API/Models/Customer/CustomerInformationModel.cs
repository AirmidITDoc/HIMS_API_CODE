using FluentValidation;

namespace HIMS.API.Models.Customer
{
    public class CustomerInformationModel
    {
        public long CustomerId { get; set; }
        public string? CustomerNumber { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerAddress { get; set; }
        public string? CustomerContactNo { get; set; }
        public string? CustomerContactPerson { get; set; }
        public long? CustomerCity { get; set; }
        public decimal? OrderAmount { get; set; }
        public string? InstallationDate { get; set; }
        public string? Amcdate { get; set; }
        public int? Amcduration { get; set; }
        public string? NextAmcdate { get; set; }
    }
    public class CustomerInformationModelValidator : AbstractValidator<CustomerInformationModel>
    {
        public CustomerInformationModelValidator()
        {
            RuleFor(x => x.CustomerNumber).NotNull().NotEmpty().WithMessage("CustomerNumber  is required");
            RuleFor(x => x.CustomerName).NotNull().NotEmpty().WithMessage("CustomerName  is required");
            RuleFor(x => x.CustomerAddress).NotNull().NotEmpty().WithMessage("CustomerAddress  is required");
            RuleFor(x => x.CustomerContactNo).NotNull().NotEmpty().WithMessage("CustomerContactNo  is required");
            RuleFor(x => x.CustomerContactPerson).NotNull().NotEmpty().WithMessage("CustomerContactPerson  is required");
            RuleFor(x => x.CustomerCity).NotNull().NotEmpty().WithMessage("CustomerCity  is required");
            RuleFor(x => x.OrderAmount).NotNull().NotEmpty().WithMessage("OrderAmount  is required");

        }
    }
}
