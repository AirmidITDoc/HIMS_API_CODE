using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class SupplierMasterModel1
    {

        public long SupplierId { get; set; }
        public string? SupplierName { get; set; }
        public string? ContactPerson { get; set; }
        //public string? Address { get; set; }
        //public long? CityId { get; set; }
        //public long? StateId { get; set; }
        //public long? CountryId { get; set; }
        //public string? CreditPeriod { get; set; }
        //public string? Mobile { get; set; }
        //public string? Phone { get; set; }
        //public string? Fax { get; set; }
        //public string? Email { get; set; }
        //public long? ModeOfPayment { get; set; }
        //public long? TermOfPayment { get; set; }
        //public long? TaxNature { get; set; }
        //public long? CurrencyId { get; set; }
        //public long? Octroi { get; set; }
        //public long? Freight { get; set; }
        //public bool? IsActive { get; set; }
        //public long? AddedBy { get; set; }
        //public long? UpdatedBy { get; set; }
        //public string? Gstno { get; set; }
        //public string? PanNo { get; set; }
        //public string? PinCode { get; set; }
        //public long? Taluka { get; set; }
        //public string? LicNo { get; set; }
        //public string? ExpDate { get; set; }
        //public string? DlNo { get; set; }
        //public long? BankId { get; set; }
        //public string? Bankname { get; set; }
        //public string? Branch { get; set; }
        //public long? BankNo { get; set; }
        //public string? Ifsccode { get; set; }
        //public long? VenderTypeId { get; set; }
        //public decimal? OpeningBalance { get; set; }
        //public int? CreatedBy { get; set; }
        //public string? CreatedDate { get; set; }
        //public int? ModifiedBy { get; set; }
        //public string? ModifiedDate { get; set; }

    }


    public class SupplierMasterModel1Validator : AbstractValidator<SupplierMasterModel1>
    {
        public SupplierMasterModel1Validator()
        {
            RuleFor(x => x.SupplierName).NotNull().NotEmpty().WithMessage("Supplier Name is required");
        }
    }


    //public class AssignSupplierTostoreModel
    //{
    //    public long AssignId { get; set; }
    //    public long? StoreId { get; set; }
    //    public long? SupplierId { get; set; }
    //}


    //public class AssignSupplierTostoreModelValidator : AbstractValidator<AssignSupplierTostoreModel>
    //{
    //    public AssignSupplierTostoreModelValidator()
    //    {
    //        RuleFor(x => x.SupplierId).NotNull().NotEmpty().WithMessage("SupplierId  is required");
    //    }
    //}


}
