using FluentValidation;

namespace HIMS.API.Models.Inventory.Masters
{
    public class OTBookingRequestModel
    {
        public long OtbookingId { get; set; }
        public DateTime? OtbookingDate { get; set; }
        public DateTime? OtbookingTime { get; set; }
        public long? OpIpId { get; set; }
        public byte? OpIpType { get; set; }
        public long? SurgeonId { get; set; }
        public long? SurgeryId { get; set; }
        public string? SurgeryType { get; set; }
        public long? DepartmentId { get; set; }
        public long? CategoryId { get; set; }

    }
    public class OTBokkingRequestModelValidator : AbstractValidator<OTBookingRequestModel>
    {
        public OTBokkingRequestModelValidator()
        {
            RuleFor(x => x.OtbookingId).NotNull().NotEmpty().WithMessage("OtbookingId is required");
            RuleFor(x => x.OpIpId).NotNull().NotEmpty().WithMessage("OpIpId is required");  

        }
    }
}
