using HIMS.API.Models.Inventory;
using HIMS.Data.Models;

namespace HIMS.API.Models.IPPatient
{
    public class OTInOperationModel
    {
        public long OtinOperationId { get; set; }
        public long? OtreservationId { get; set; }
        public DateTime? OtinOperationDate { get; set; }
        public string? OtinOperationTime { get; set; }
        //public string? OtinOperationNo { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public long? CategoryType { get; set; }
        public long? Ottable { get; set; }
        public DateTime? SurgeryDate { get; set; }
        public int? Duration { get; set; }
        public string? FromTime { get; set; }
        public string? ToTime { get; set; }
        public string? BloodLoss { get; set; }
        public long? AnesthesiaType { get; set; }
        public DateTime? TheaterInDate { get; set; }
        public string? TheaterInTime { get; set; }
        public DateTime? TheaterOutData { get; set; }
        public string? TheaterOutTime { get; set; }
        public int? BloodArranged { get; set; }
        public int? Pacrequired { get; set; }
        public int? EquipmentsRequired { get; set; }
        public int? Infective { get; set; }
        public int? ClearanceMedical { get; set; }
        public int? ClearanceFinancial { get; set; }
        public int? IntraOpeChangeInSurgeryPlan { get; set; }
        public int? MopCount { get; set; }
        public string? StepsOfProc { get; set; }
        public string? ClosureNotes { get; set; }
        public string? OperativeFindingsNotes { get; set; }
        public string? PostOperativeNotes { get; set; }
        public string? ConditionOfPatientNotes { get; set; }
        public List<OtInOperationAttendingDetailModel> TOtInOperationAttendingDetails { get; set; }
        public List<OtInOperationDiagnosisModel> TOtInOperationDiagnoses { get; set; }
        public List<OtInOperationPostOperDiagnosisModel> TOtInOperationPostOperDiagnoses { get; set; }
        public List<OtInOperationSurgeryDetailModel> TOtInOperationSurgeryDetails { get; set; }


    }
    public class OtInOperationAttendingDetailModel
    {
        public long OtinOperationAttendingDetId { get; set; }
        public long? OtinOperationId { get; set; }
        public long? DoctorTypeId { get; set; }
        public long? DoctorId { get; set; }
        public int? SeqNo { get; set; }


    }
    public class OtInOperationDiagnosisModel
    {
        public long OtinOperationDiagnosisDetId { get; set; }
        public long? OtinOperationId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }


    }
    public partial class OtInOperationPostOperDiagnosisModel
    {
        public long OtinOperationPostOperDiagnosisDetId { get; set; }
        public long? OtinOperationId { get; set; }
        public string? DescriptionName { get; set; }
        public string? DescriptionType { get; set; }

    }
    public class OtInOperationSurgeryDetailModel
    {
        public long OtinOperationSurgeryDetId { get; set; }
        public long? OtinOperationId { get; set; }
        public long? SurgeryCategoryId { get; set; }
        public long? SurgeryId { get; set; }
        public string? SurgeryPart { get; set; }
        public string? SurgeryFromTime { get; set; }
        public string? SurgeryEndTime { get; set; }
        //public double? SurgeryDuration { get; set; }
        public float? SurgeryDuration { get; set; }

        public string? IsPrimary { get; set; }
        public long? SurgeonId { get; set; }
        public long? AnesthetistId { get; set; }
        public int? SeqNo { get; set; }

    }
    public class OTInOperationHeaderModel
    {
        public long OtinOperationId { get; set; }
        public long? OtreservationId { get; set; }
        public DateTime? OtinOperationDate { get; set; }
        public string? OtinOperationTime { get; set; }
        public string? OtinOperationNo { get; set; }
        public long? Opipid { get; set; }
        public byte? Opiptype { get; set; }
        public long? CategoryType { get; set; }
        public long? Ottable { get; set; }
        public DateTime? SurgeryDate { get; set; }
        public int? Duration { get; set; }
        public string? FromTime { get; set; }
        public string? ToTime { get; set; }
        public string? BloodLoss { get; set; }
        public long? AnesthesiaType { get; set; }
        public DateTime? TheaterInDate { get; set; }
        public string? TheaterInTime { get; set; }
        public DateTime? TheaterOutData { get; set; }
        public string? TheaterOutTime { get; set; }
        public int? BloodArranged { get; set; }
        public int? Pacrequired { get; set; }
        public int? EquipmentsRequired { get; set; }
        public int? Infective { get; set; }
        public int? ClearanceMedical { get; set; }
        public int? ClearanceFinancial { get; set; }
        public int? IntraOpeChangeInSurgeryPlan { get; set; }
        public int? MopCount { get; set; }
        public string? StepsOfProc { get; set; }
        public string? ClosureNotes { get; set; }
        public string? OperativeFindingsNotes { get; set; }
        public string? PostOperativeNotes { get; set; }
        public string? ConditionOfPatientNotes { get; set; }
    }
}
