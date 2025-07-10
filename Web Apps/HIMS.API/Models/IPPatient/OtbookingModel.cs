using FluentValidation;
using HIMS.API.Models.Inventory;

namespace HIMS.API.Models.IPPatient
{
    public class OtbookingModel
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
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }

    }
    public class OtbookingModelValidator : AbstractValidator<OtbookingModel>
    {
        public OtbookingModelValidator()
        {
            RuleFor(x => x.TranDate).NotNull().NotEmpty().WithMessage("TranDate is required");
            RuleFor(x => x.TranTime).NotNull().NotEmpty().WithMessage("TranTime  is required");
            RuleFor(x => x.Opdate).NotNull().NotEmpty().WithMessage(" Opdate required");
            RuleFor(x => x.Optime).NotNull().NotEmpty().WithMessage("Optime is required");
            RuleFor(x => x.IsCancelledDateTime).NotNull().NotEmpty().WithMessage("IsCancelledDateTime is required");



        }
    }
}
