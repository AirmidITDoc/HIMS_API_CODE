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
        public DateTime? OtrequestDate { get; set; }
        public string? OtrequestTime { get; set; }
        public long? OtrequestId { get; set; }
        public long? SurgeryCategoryId { get; set; }
        public long? DepartmentId { get; set; }
        public long? CategoryId { get; set; }
        public long? SiteDescId { get; set; }
        public long? SurgeryId { get; set; }
        public long? SurgeonId { get; set; }
        public int? SurgeryTypeId { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
        public class OTBookingRequestModelValidator : AbstractValidator<OTBookingRequestModel>
        {
            public OTBookingRequestModelValidator()
            {
                RuleFor(x => x.OtbookingDate).NotNull().NotEmpty().WithMessage("OtbookingDate is required");
                RuleFor(x => x.OtbookingTime).NotNull().NotEmpty().WithMessage("OtbookingTime is required");
                RuleFor(x => x.IsCancelledDateTime).NotNull().NotEmpty().WithMessage("IsCancelledDateTime is required");


            }
        }
        public class OTBookingRequestCancel
        {
            public long OtbookingId { get; set; }
            public string? Reason { get; set; }
            public long IsCancelledBy { get; set; }


        }
    }
    public class TOtRequestHeaderModel
    {
        public long OtrequestId { get; set; }
        public DateTime? OtrequestDate { get; set; }
        public string? OtrequestTime { get; set; }
        public string? OtrequestNo { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public string? BloodGroup { get; set; }
        public long? CategoryType { get; set; }
        public long? Ottable { get; set; }
        public DateTime? SurgeryDate { get; set; }
        public string? EstimateTime { get; set; }
        public string? Diagnosis { get; set; }
        public string? Comments { get; set; }
        public bool? RequestType { get; set; }
        public bool? Pacrequired { get; set; }
        public bool? EquipmentsRequired { get; set; }
        public bool? ClearanceMedical { get; set; }
        public bool? ClearanceFinancial { get; set; }
        public bool? Infective { get; set; }
        public bool? IsCancelled { get; set; }
        public long? IsCancelledBy { get; set; }
        public DateTime? IsCancelledDateTime { get; set; }
        public List<TOtRequestSurgeryDetailModel> TOtRequestSurgeryDetails { get; set; }
        public List<TOtRequestAttendingDetailModel> TOtRequestAttendingDetails { get; set; }


    }
    public class LabPatientRegistrationModelValidator : AbstractValidator<TOtRequestHeaderModel>
    {
        public LabPatientRegistrationModelValidator()
        {
            RuleFor(x => x.OtrequestDate).NotNull().NotEmpty().WithMessage("OtrequestDate is required");
            RuleFor(x => x.OtrequestTime).NotNull().NotEmpty().WithMessage("OtrequestTime is required");
            RuleFor(x => x.SurgeryDate).NotNull().NotEmpty().WithMessage("SurgeryDate is required");
            RuleFor(x => x.EstimateTime).NotNull().NotEmpty().WithMessage("EstimateTime is required");
        }
    }
    public partial class TOtRequestSurgeryDetailModel
    {
        public long OtrequestSurgeryDetId { get; set; }
        public long? OtrequestId { get; set; }
        public long? SurgeryCategoryId { get; set; }
        public long? SurgeryId { get; set; }
        public string? SurgeryPart { get; set; }
        public string? SurgeryFromTime { get; set; }
        public string? SurgeryEndTime { get; set; }
        public int? SurgeryDuration { get; set; }
        public string? IsPrimary { get; set; }
        public long? SurgeonId { get; set; }
        public long? AnesthetistId { get; set; }
        public int? SeqNo { get; set; }

    }
    public class TOtRequestSurgeryDetailModelModelValidator : AbstractValidator<TOtRequestSurgeryDetailModel>
    {
        public TOtRequestSurgeryDetailModelModelValidator()
        {
            RuleFor(x => x.SurgeryFromTime).NotNull().NotEmpty().WithMessage("SurgeryFromTime is required");
            RuleFor(x => x.SurgeryEndTime).NotNull().NotEmpty().WithMessage("SurgeryEndTime is required");

        }
    }
    public class TOtRequestAttendingDetailModel
    {
        public long OtrequestAttendingDetId { get; set; }
        public long? OtrequestId { get; set; }
        public long? DoctorTypeId { get; set; }
        public long? DoctorId { get; set; }
        public int? SeqNo { get; set; }

    }
}
