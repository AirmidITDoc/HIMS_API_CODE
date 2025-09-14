using FluentValidation;
using HIMS.API.Models.Inventory;
using HIMS.API.Models.Inventory.Masters;
using static HIMS.API.Models.IPPatient.OtbookingModelValidator;

namespace HIMS.API.Models.IPPatient
{
    public class OTReservationModel
    {
        public long OtreservationId { get; set; }
        public DateTime? ReservationDate { get; set; }
        public string? ReservationTime { get; set; }
        public long? OpIpId { get; set; }
        public bool? OpIpType { get; set; }
        public DateTime? Opdate { get; set; }
        public string? OpstartTime { get; set; }
        public string? OpendTime { get; set; }
        public int? Duration { get; set; }
        public long? OttableId { get; set; }
        public long? SurgeonId { get; set; }
        public long? SurgeonId1 { get; set; }
        public long? AnestheticsDr { get; set; }
        public long? AnestheticsDr1 { get; set; }
        public long? SurgeryId { get; set; }
        public long? AnesthTypeId { get; set; }
        public string? Instruction { get; set; }
        public long? OttypeId { get; set; }
        public bool? UnBooking { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
        public long? DepartmentId { get; set; }
        public long? OtrequestId { get; set; }





    }
    public class OtbookingModelValidator : AbstractValidator<OTReservationModel>
    {
        public OtbookingModelValidator()
        {
            RuleFor(x => x.ReservationDate).NotNull().NotEmpty().WithMessage("ReservationDate is required");
            RuleFor(x => x.ReservationTime).NotNull().NotEmpty().WithMessage("ReservationTime  is required");
            RuleFor(x => x.Opdate).NotNull().NotEmpty().WithMessage(" Opdate required");
            RuleFor(x => x.OpendTime).NotNull().NotEmpty().WithMessage("OpendTime is required");
            RuleFor(x => x.OpstartTime).NotNull().NotEmpty().WithMessage("OpstartTime is required");
            RuleFor(x => x.IsCancelledDateTime).NotNull().NotEmpty().WithMessage("IsCancelledDateTime is required");


        }
        public class OTReservationCancel
        {
            public long OtreservationId {  get; set; }
            public string? Reason { get; set; }
            public long? IsCancelledBy { get; set; }

        }
        public class OTBookingPostPoneModel
        {
            public long OldOTReservationId { get; set; }
            public long? OpIpId { get; set; }
            public DateTime? Opdate { get; set; }
            public int? CreatedBy { get; set; }
            public string? Reason { get; set; }
            public long NewOTReservationId { get; set; }

        }
    }

}

