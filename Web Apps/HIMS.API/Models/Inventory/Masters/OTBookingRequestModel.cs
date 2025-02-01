using FluentValidation;

namespace HIMS.API.Models.Inventory.Masters
{
    public class OTBookingRequestModel
    {
        public long OtbookingId { get; set; }
        public DateTime? OtbookingDate { get; set; }
        public string? OtbookingTime { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public long? SurgeonId { get; set; }
        public long? SurgeryId { get; set; }
        public string? SurgeryType { get; set; }
        public long? DepartmentId { get; set; }
        public long? CategoryId { get; set; }
        public DateTime? AddedDateTime { get; set; }
        public DateTime? UpdateDateTime { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
        public long? SiteDescId { get; set; }
    }
    public class OTBokkingRequestModelValidator : AbstractValidator<OTBookingRequestModel>
    {
        public OTBokkingRequestModelValidator()
        {
            RuleFor(x => x.OtbookingDate).NotNull().NotEmpty().WithMessage("OtbookingDate is required");
            RuleFor(x => x.OtbookingTime).NotNull().NotEmpty().WithMessage("OtbookingTime is required");  

        }
    }
}
