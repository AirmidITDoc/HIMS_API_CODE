using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class CompanyMasterModel
    {
        public long CompanyId { get; set; }
        public long? CompTypeId { get; set; }
        public string? CompanyName { get; set; }
        public string? CompanyShortName { get; set; }
        public string? Address { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public string? PinNo { get; set; }
        public string? PhoneNo { get; set; }
        public string? FaxNo { get; set; }
        public long? TraiffId { get; set; }
        public string? ContactPerson { get; set; }
        public string? ContactNumber { get; set; }
        public string? EmailId { get; set; }
        public string? Website { get; set; }
        public long? PaymodeOfPayId { get; set; }
        public int? CreditDays { get; set; }
        public string? PanNo { get; set; }
        public string? Tanno { get; set; }
        public string? Gstin { get; set; }
        public double? AdminCharges { get; set; }
        public string? LoginWebsiteUser { get; set; }
        public string? LoginWebsitePassword { get; set; }
        public bool? IsSubCompany { get; set; }
        public decimal DayWiseCredit { get; set; }


    }
    public class CompanyMasterModelValidator : AbstractValidator<CompanyMasterModel>
    {
        public CompanyMasterModelValidator()
        {
            RuleFor(x => x.CompanyName).NotNull().NotEmpty().WithMessage("Company is required");
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage("Address is required");
            RuleFor(x => x.PinNo).NotNull().NotEmpty().WithMessage("PinNo is required");
            RuleFor(x => x.PhoneNo).NotNull().NotEmpty().WithMessage("PhoneNo is required");
            RuleFor(x => x.FaxNo).NotNull().NotEmpty().WithMessage("FaxNo is required");
            RuleFor(x => x.TraiffId).NotNull().NotEmpty().WithMessage("TraiffId is required");
        }
        public class updatecompanywiseservicerate

        {
            public long? ServiceId { get; set; }
            public long? TariffId { get; set; }
            public long? ClassId { get; set; }
            public decimal? ClassRate { get; set; }
            public decimal? DiscountAmount { get; set; }
            public double? DiscountPercentage { get; set; }


        }
    }
}
