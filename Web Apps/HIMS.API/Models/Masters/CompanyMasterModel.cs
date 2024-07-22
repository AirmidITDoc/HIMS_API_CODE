using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class CompanyMasterModel
    {
        public long CompanyId { get; set; }
        public long? CompTypeId { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PinNo { get; set; }
        public string? PhoneNo { get; set; }
        //public string? MobileNo { get; set; }
        //public string? FaxNo { get; set; }
        //public long? TraiffId { get; set; }
        //public bool? IsActive { get; set; }
        //public long? AddedBy { get; set; }
        //public long? UpdatedBy { get; set; }
        //public bool? IsCancelled { get; set; }
        //public long? IsCancelledBy { get; set; }
        //public DateTime? IsCancelledDate { get; set; }
        //public int? CreatedBy { get; set; }
        //public DateTime? CreatedDate { get; set; }
        //public int? ModifiedBy { get; set; }
        //public DateTime? ModifiedDate { get; set; }
    }
    public class CompanyMasterModelValidator : AbstractValidator<CompanyMasterModel>
    {
        public CompanyMasterModelValidator()
        {
            RuleFor(x => x.CompanyName).NotNull().NotEmpty().WithMessage("Company is required");
        }
    }
}
