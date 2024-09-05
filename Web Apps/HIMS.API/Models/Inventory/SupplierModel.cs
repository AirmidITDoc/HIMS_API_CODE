using FluentValidation;

namespace HIMS.API.Models.Inventory
{
    public class SupplierModel
    {
        public long SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? ContactPerson { get; set; }
        public string? Address { get; set; }
        public int? CityId { get; set; }
        public int? StateId { get; set; }
        public int? CountryId { get; set; }
        public string? CreditPeriod { get; set; }
        public string? Mobile { get; set; }
        public string? Phone { get; set; }
        public string? Fax { get; set; }
        public string? Email { get; set; }
        public int? ModeofPayment { get; set; }
        public int? TermsofPayment { get; set; }
        public int? CurrencyId { get; set; }
        public int? Octroi { get; set; }
        public int? Freight { get; set; }
        public long? IsDeleted { get; set; }
        public int? Addedby { get; set; }
        public string? GSTNo { get; set; }
        public string? PanNo { get; set; }
        public string? SupplierTime { get; set; }
        public string? ModifiedDate { get; set; }
        public List<AssignSupplierToStoreModel> MAssignSupplierToStore { get; set; }
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
        public long? StoreId { get; set; }
        public long? SupplierId { get; set; }
    }
    public class AssignSupplierToStoreModelValidator : AbstractValidator<AssignSupplierToStoreModel>
    {
        public AssignSupplierToStoreModelValidator()
        {
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("StoreId is required");
            RuleFor(x => x.SupplierId).NotNull().NotEmpty().WithMessage("SupplierId  is required");
            
        }
    }

}
