using FluentValidation;

namespace HIMS.API.Models.Inventory
{
    public class SupplierModel
    {
        public long SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Address { get; set; }
        public long? CityId { get; set; }
        public long? StateId { get; set; }
        public long? CountryId { get; set; }
        public string? CreditPeriod { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public bool? IsActive { get; set; }
        public string? Email { get; set; }
        public long? ModeofPayment { get; set; }
        public long? TermofPayment { get; set; }
        public long? CurrencyId { get; set; }
        public long? Octroi { get; set; }
        public long? Freight { get; set; }
        public string? GSTNo { get; set; }
        public string? PanNo { get; set; }
        public long? Taluka { get; set; }
        public string? LicNo { get; set; }
        public string? PinCode { get; set; }
        public long? TaxNature { get; set; }
        public DateTime? ExpDate { get; set; }
        public string? DlNo { get; set; }
        public long? BankId { get; set; }
        public string? Bankname { get; set; }
        public string? Branch { get; set; }
        public long? BankNo { get; set; }
        public string? Ifsccode { get; set; }
        public long? VenderTypeId { get; set; }
        public long? OpeningBalance { get; set; }
        public string? SupplierTime { get; set; }

        public List<AssignSupplierToStoreModel> MAssignSupplierToStores { get; set; }
    }
    public class SupplierModelValidator : AbstractValidator<SupplierModel>
    {
        public SupplierModelValidator()
        {
            RuleFor(x => x.SupplierName).NotNull().NotEmpty().WithMessage("SupplierName is required");
            RuleFor(x => x.ContactPerson).NotNull().NotEmpty().WithMessage("ContactPerson  is required");
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage(" Address required");
            RuleFor(x => x.CityId).NotNull().NotEmpty().WithMessage("CityId is required");
            RuleFor(x => x.StateId).NotNull().NotEmpty().WithMessage("StateId is required");
            RuleFor(x => x.CountryId).NotNull().NotEmpty().WithMessage("CountryId  is required");
            RuleFor(x => x.GSTNo).NotNull().NotEmpty().WithMessage(" GSTNo required");
            RuleFor(x => x.PanNo).NotNull().NotEmpty().WithMessage(" PanNo required");

        }
    }

    public class AssignSupplierToStoreModel
    {
        public long AssignId { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierId { get; set; }
    }
    public class AssignSupplierToStoreModelValidator : AbstractValidator<AssignSupplierToStoreModel>
    {
        public AssignSupplierToStoreModelValidator()
        {
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("StoreId is required");

        }
    }
    public class SupplierCancel
    {
        public long SupplierId { get; set; }

    }
}
