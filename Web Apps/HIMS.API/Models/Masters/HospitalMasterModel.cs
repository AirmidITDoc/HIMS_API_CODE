using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class HospitalMasterModel
    {
        public long HospitalId { get; set; }
        public string? HospitalHeaderLine { get; set; }
        public string? HospitalName { get; set; }
        public string? HospitalShortName { get; set; }

        public string? HospitalAddress { get; set; }
        public string? City { get; set; }
        public string? Pin { get; set; }
        public string? Phone { get; set; }
        public string? EmailId { get; set; }
        public string? WebSiteInfo { get; set; }
        public string? Header { get; set; }
        public long? OpdBillingCounterId { get; set; }
        public long? OpdReceiptCounterId { get; set; }
        public long? OpdRefundBillCounterId { get; set; }
        public long? OpdRefundBillReceiptCounterId { get; set; }
        public long? OpdAdvanceCounterId { get; set; }
        public long? OpdRefundAdvanceCounterId { get; set; }
        public long? IpdAdvanceCounterId { get; set; }
        public long? IpdAdvanceReceiptCounterId { get; set; }
        public long? IpdBillingCounterId { get; set; }
        public long? IpdReceiptCounterId { get; set; }
        public long? IpdRefundOfBillCounterId { get; set; }
        public long? IpdRefundOfBillReceiptCounterId { get; set; }
        public long? IpdRefundOfAdvanceCounterId { get; set; }
        public long? IpdRefundOfAdvanceReceiptCounterId { get; set; }
        public long? CityId { get; set; }
    }
    public class HospitalMasterModelModelValidator : AbstractValidator<HospitalMasterModel>
    {
        public HospitalMasterModelModelValidator()
        {
            RuleFor(x => x.HospitalHeaderLine).NotNull().NotEmpty().WithMessage("HospitalHeaderLine is required");
            RuleFor(x => x.HospitalName).NotNull().NotEmpty().WithMessage("HospitalName is required");
            RuleFor(x => x.HospitalAddress).NotNull().NotEmpty().WithMessage("HospitalAddress is required");
            RuleFor(x => x.City).NotNull().NotEmpty().WithMessage("City is required");
            RuleFor(x => x.Phone).NotNull().NotEmpty().WithMessage("Phone is required");
            RuleFor(x => x.EmailId).NotNull().NotEmpty().WithMessage("EmailId is required");
            RuleFor(x => x.Header).NotNull().NotEmpty().WithMessage("Header is required");


        }
    }
}
