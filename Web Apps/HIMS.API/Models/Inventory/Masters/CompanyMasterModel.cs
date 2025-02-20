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
        public string? MobileNo { get; set; }
        public string? FaxNo { get; set; }
        public long? TraiffId { get; set; }
        public bool? IsActive { get; set; }
        public long? AddedBy { get; set; }
        public long? UpdatedBy { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDate { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
    public class CompanyMasterModelValidator : AbstractValidator<CompanyMasterModel>
    {
        public CompanyMasterModelValidator()
        {
            RuleFor(x => x.CompanyName).NotNull().NotEmpty().WithMessage("Company is required");
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage("Address is required");
            RuleFor(x => x.City).NotNull().NotEmpty().WithMessage("City is required");
            RuleFor(x => x.PinNo).NotNull().NotEmpty().WithMessage("PinNo is required");
            RuleFor(x => x.PhoneNo).NotNull().NotEmpty().WithMessage("PhoneNo is required");
            RuleFor(x => x.MobileNo).NotNull().NotEmpty().WithMessage("MobileNo is required");
            RuleFor(x => x.FaxNo).NotNull().NotEmpty().WithMessage("FaxNo is required");
            RuleFor(x => x.TraiffId).NotNull().NotEmpty().WithMessage("TraiffId is required");
        }
    }
}
