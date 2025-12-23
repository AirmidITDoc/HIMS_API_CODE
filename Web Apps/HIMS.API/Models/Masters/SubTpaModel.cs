using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class SubTpaModel
    {
        public long SubCompanyId { get; set; }
        public long? CompTypeId { get; set; }
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? PinNo { get; set; }
        public string? PhoneNo { get; set; }
        public string? MobileNo { get; set; }
        public string? FaxNo { get; set; }
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

    public class SubTpaModelValidator : AbstractValidator<SubTpaModel>
    {
        public SubTpaModelValidator()
        {
            RuleFor(x => x.CompTypeId).NotNull().NotEmpty().WithMessage("CompTypeId is required");
            RuleFor(x => x.CompanyName).NotNull().NotEmpty().WithMessage("CompanyName is required");
            RuleFor(x => x.Address).NotNull().NotEmpty().WithMessage("Address is required");
            RuleFor(x => x.City).NotNull().NotEmpty().WithMessage("City is required");
            RuleFor(x => x.PinNo).NotNull().NotEmpty().WithMessage("PinNo is required");
            RuleFor(x => x.PhoneNo).NotNull().NotEmpty().WithMessage("PhoneNo is required");
            RuleFor(x => x.FaxNo).NotNull().NotEmpty().WithMessage("FaxNo is required");
        }
    }
}
