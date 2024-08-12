using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.Masters
{
    public class SupplierMasterModel1
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
        
        public bool? IsActive { get; set; }
        public int? CreatedBy { get; set; }
        public string? CreatedDate { get; set; }
        
        public string? ModifiedDate { get; set; }
        public string? SupplierTime { get; set; }

        public List<AssignSupplierTostoreModel> AssignSupplierTostoreModel { get; set; }



    }


    public class SupplierMasterModel1Validator : AbstractValidator<SupplierMasterModel1>
    {
        public SupplierMasterModel1Validator()
        {
            RuleFor(x => x.SupplierName).NotNull().NotEmpty().WithMessage("Supplier Name is required");
            RuleFor(x => x.ContactPerson).NotNull().NotEmpty().WithMessage("ContactPerson Name is required");
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage("Address  is required");


        }
    }


    public class AssignSupplierTostoreModel
    {
        public long AssignId { get; set; }
        public long? StoreId { get; set; }
        public long? SupplierId { get; set; }
    }


    public class AssignSupplierTostoreModelValidator : AbstractValidator<AssignSupplierTostoreModel>
    {
        public AssignSupplierTostoreModelValidator()
        {
            RuleFor(x => x.AssignId).NotNull().NotEmpty().WithMessage("AssignId  is required");
            RuleFor(x => x.StoreId).NotNull().NotEmpty().WithMessage("StoreId  is required");
            RuleFor(x => x.SupplierId).NotNull().NotEmpty().WithMessage("SupplierId  is required");

        }
    }


}
