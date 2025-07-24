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
        public long? SurgeryType { get; set; }
        public long? DepartmentId { get; set; }
        public long? CategoryId { get; set; }
        public long? SiteDescId { get; set; }
        public long? SurgeryId { get; set; }
        public long? SurgeonId { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
        public DateTime? OtrequestDate { get; set; }
        public string? OtrequestTime { get; set; }
        public long? OtrequestId { get; set; }
        public class OTBokkingRequestModelValidator : AbstractValidator<OTBookingRequestModel>
        {
            public OTBokkingRequestModelValidator()
            {
                RuleFor(x => x.OtbookingDate).NotNull().NotEmpty().WithMessage("OtbookingDate is required");
                RuleFor(x => x.OtbookingTime).NotNull().NotEmpty().WithMessage("OtbookingTime is required");
                RuleFor(x => x.IsCancelledDateTime).NotNull().NotEmpty().WithMessage("IsCancelledDateTime is required");


            }
        }
        public class OTBookingRequestCancel
        {
            public long OtbookingId { get; set; }
           

        }
    }
}
