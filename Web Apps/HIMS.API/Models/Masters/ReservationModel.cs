using FluentValidation;

namespace HIMS.API.Models.Masters
{
    public class ReservationModel
    {
        public long OtreservationId { get; set; }
        public DateTime? OtreservationDate { get; set; }
        public string? OtreservationTime { get; set; }
        //public string? OtreservationNo { get; set; }
        public long? OtrequestId { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public string? BloodGroup { get; set; }
        public long? CategoryType { get; set; }
        public long? Ottable { get; set; }
        public DateTime? SurgeryDate { get; set; }
        public string? EstimateTime { get; set; }
        public string? Diagnosis { get; set; }
        public string? Comments { get; set; }
        public bool? ReservationType { get; set; }
        public bool? Pacrequired { get; set; }
        public bool? EquipmentsRequired { get; set; }
        public bool? ClearanceMedical { get; set; }
        public bool? ClearanceFinancial { get; set; }
        public bool? Infective { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
        public bool? IsAnaesthetistPaid { get; set; }
        public bool? IsMaterialReplacement { get; set; }
        public int? AnesthesiaType { get; set; }

        public List<OtReservationAttendingDetailModel> TOtReservationAttendingDetails { get; set; }
        public List<TOtReservationSurgeryDetailModel> TOtReservationSurgeryDetails { get; set; }
        public List<ReservationDiagnosisModel> TOtReservationDiagnoses { get; set; }

    }
    public class OTReservationModelValidator : AbstractValidator<ReservationModel>
    {
        public OTReservationModelValidator()
        {
            RuleFor(x => x.OtreservationDate).NotNull().NotEmpty().WithMessage("OtreservationDate is required");
            RuleFor(x => x.OtreservationTime).NotNull().NotEmpty().WithMessage("OtreservationTime is required");
            RuleFor(x => x.SurgeryDate).NotNull().NotEmpty().WithMessage("SurgeryDate is required");
            RuleFor(x => x.EstimateTime).NotNull().NotEmpty().WithMessage("EstimateTime is required");
            RuleFor(x => x.IsCancelledDateTime).NotNull().NotEmpty().WithMessage("IsCancelledDateTime is required");

        }
    }
    public class OtReservationAttendingDetailModel
    {
        public long OtreservationAttendingDetId { get; set; }
        public long? OtreservationId { get; set; }
        public long? DoctorTypeId { get; set; }
        public long? DoctorId { get; set; }
        public int? SeqNo { get; set; }

    }

    public partial class TOtReservationSurgeryDetailModel
    {
        public long OtreservationSurgeryDetId { get; set; }
        public long? OtreservationId { get; set; }
        public long? SurgeryCategoryId { get; set; }
        public long? SurgeryId { get; set; }
        public string? SurgeryPart { get; set; }
        public string? SurgeryFromTime { get; set; }
        public string? SurgeryEndTime { get; set; }
        public float? SurgeryDuration { get; set; }
        public string? IsPrimary { get; set; }
        public long? SurgeonId { get; set; }
        public long? AnesthetistId { get; set; }
        public int? SeqNo { get; set; }

    }
    public class ReservationGetModel
    {
        public long OtreservationId { get; set; }
        public DateTime? OtreservationDate { get; set; }
        public string? OtreservationTime { get; set; }
        public long? OtrequestId { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public string? BloodGroup { get; set; }
        public long? CategoryType { get; set; }
        public long? Ottable { get; set; }
        public DateTime? SurgeryDate { get; set; }
        public string? EstimateTime { get; set; }
        public string? Diagnosis { get; set; }
        public string? Comments { get; set; }
        public bool? ReservationType { get; set; }
        public bool? Pacrequired { get; set; }
        public bool? EquipmentsRequired { get; set; }
        public bool? ClearanceMedical { get; set; }
        public bool? ClearanceFinancial { get; set; }
        public bool? IsAnaesthetistPaid { get; set; }
        public bool? IsMaterialReplacement { get; set; }
        public bool? Infective { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
        public int? AnesthesiaType { get; set; }

    }
    public class ReservationDiagnosisModel
    {
        public long OtreservationDiagnosisDetId { get; set; }
        public long? OtreservationId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }

    }


}
