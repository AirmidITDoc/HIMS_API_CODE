using FluentValidation;

namespace HIMS.API.Models.Inventory
{
    public class BillingServiceModel
    {
            public long ServiceId { get; set; }
            public long? GroupId { get; set; }
            public string? ServiceShortDesc { get; set; }
            public string? ServiceName { get; set; }
            public double? Price { get; set; }
            public bool? IsEditable { get; set; }
            public bool? CreditedtoDoctor { get; set; }
            public long? IsPathology { get; set; }
            public long? IsRadiology { get; set; }
            public long? PrintOrder { get; set; }
            public long? IsPackage { get; set; }
            public long? SubGroupId { get; set; }
            public long? DoctorId { get; set; }
            public bool? IsEmergency { get; set; }
            public decimal? EmgAmt { get; set; }
            public double? EmgPer { get; set; }
            public bool? IsDocEditable { get; set; }
            public List<ServiceDetailModel> serviceDetails { get; set; }
        }
        public class BillingServiceModelValidator : AbstractValidator<BillingServiceModel>
        {
            public BillingServiceModelValidator()
            {
                RuleFor(x => x.GroupId).NotNull().NotEmpty().WithMessage("GroupId is required");
                RuleFor(x => x.ServiceShortDesc).NotNull().NotEmpty().WithMessage("ServiceShortDesc is required");
                RuleFor(x => x.ServiceName).NotNull().NotEmpty().WithMessage("ServiceName is required");
                RuleFor(x => x.Price).NotNull().NotEmpty().WithMessage("Price is required");
                RuleFor(x => x.IsEditable).NotNull().NotEmpty().WithMessage("IsEditable is required");
                RuleFor(x => x.CreditedtoDoctor).NotNull().NotEmpty().WithMessage("CreditedtoDoctor is required");
                RuleFor(x => x.IsPathology).NotNull().NotEmpty().WithMessage("IsPathology is required");
                RuleFor(x => x.IsRadiology).NotNull().NotEmpty().WithMessage("IsRadiology is required");
                RuleFor(x => x.PrintOrder).NotNull().NotEmpty().WithMessage("PrintOrder is required");
                RuleFor(x => x.IsPackage).NotNull().NotEmpty().WithMessage("IsPackage is required");
                RuleFor(x => x.SubGroupId).NotNull().NotEmpty().WithMessage("SubGroupId is required");
                RuleFor(x => x.DoctorId).NotNull().NotEmpty().WithMessage("DoctorId is required");
                RuleFor(x => x.IsEmergency).NotNull().NotEmpty().WithMessage("IsEmergency is required");
            }
        }
        public class ServiceDetailModel
        {
            public long ServiceDetailId { get; set; }
            public long? ServiceId { get; set; }
            public long? TariffId { get; set; }
            public long? ClassId { get; set; }
            public decimal? ClassRate { get; set; }
        }
        public class ServiceDetailModelValidator : AbstractValidator<ServiceDetailModel>
        {
            public ServiceDetailModelValidator()
            {
                RuleFor(x => x.ServiceId).NotNull().NotEmpty().WithMessage("ServiceId is required");
                RuleFor(x => x.TariffId).NotNull().NotEmpty().WithMessage("TariffId is required");
                RuleFor(x => x.ClassId).NotNull().NotEmpty().WithMessage("ClassId is required");
                RuleFor(x => x.ClassRate).NotNull().NotEmpty().WithMessage("ClassRate is required");

            }
        }
        public class DifferTraiffModel
        {
        public long? OldTariffId { get; set; }
        public long? NewTariffId { get; set; }

        
    }
       
}

