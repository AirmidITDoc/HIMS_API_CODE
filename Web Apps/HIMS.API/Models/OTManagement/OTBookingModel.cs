using FluentValidation;

namespace HIMS.API.Models.OTManagement
{
    public class OTBookingModel
    {
        public long OtbookingId { get; set; }
        public DateTime? TranDate { get; set; }
        public string? TranTime { get; set; }
        public long? OpIpId { get; set; }
        public bool? OpIpType { get; set; }
        public DateTime? Opdate { get; set; }
        public string? Optime { get; set; }
        public int? Duration { get; set; }
        public long? OttableId { get; set; }
        public long? SurgeonId { get; set; }
        public long? SurgeonId1 { get; set; }
        public long? AnestheticsDr { get; set; }
        public long? AnestheticsDr1 { get; set; }
        public string? Surgeryname { get; set; }
        public long? ProcedureId { get; set; }
        public string? AnesthType { get; set; }
        public bool? UnBooking { get; set; }
        public string? Instruction { get; set; }
        public long? OttypeId { get; set; }

    }
    public class OTBookingModelValidator : AbstractValidator<OTBookingModel>
    {
        public OTBookingModelValidator()
        {
            RuleFor(x => x.Surgeryname).NotNull().NotEmpty().WithMessage("Surgeryname  is required");
            RuleFor(x => x.OpIpId).NotNull().NotEmpty().WithMessage("OpIpId is required");

        }
    }
}
